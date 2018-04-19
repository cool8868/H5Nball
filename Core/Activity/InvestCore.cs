using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response.Activity;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Activity
{
    public class InvestCore
    {
        private readonly int _investDeposit;
        private readonly int _investDepositMonthly;

        public InvestCore(int p)
        {
            _investDeposit = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.InvestDepositMonth);
            _investDepositMonthly = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.DepositMonthly);
        }

        public static InvestCore Instance
        {
            get { return SingletonFactory<InvestCore>.SInstance; }
        }

        #region Facade
               
        public InvestInfoResponse InvestInfo(Guid managerId)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return ResponseHelper.InvalidParameter<InvestInfoResponse>();
            var investInfo = InvestManagerMgr.GetById(managerId);
            if (investInfo == null)
                investInfo = InnerBuildEntity(managerId);
            if (investInfo.TheMonthly && DateTime.Today > investInfo.ExpirationTime)
            {
                investInfo.TheMonthly = false;
            }
            string stepStatus = investInfo.StepStatus;
            string newStepStatus = Rebuild(stepStatus, manager.Level);
            investInfo.StepStatus = newStepStatus;
            if (!InvestManagerMgr.Update(investInfo))
                return ResponseHelper.Create<InvestInfoResponse>(MessageCode.NbUpdateFail);
            List<int> restitution = CacheFactory.ActivityCache.BuildRestitution(stepStatus);
            PayUserEntity payUser = PayUserMgr.GetById(manager.Account);
            return BuildInvestInfoResponse(investInfo, restitution, newStepStatus, payUser, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="step">1-4</param>
        /// <returns></returns>
        public InvestInfoResponse InvestDeposit(Guid managerId, int step)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return ResponseHelper.InvalidParameter<InvestInfoResponse>();
            var investInfo = InvestManagerMgr.GetById(managerId);
            if(investInfo == null)
                return ResponseHelper.InvalidParameter<InvestInfoResponse>();
            string stepStatus = investInfo.StepStatus;
            var curStep = GetCurStepStr(stepStatus, step);
            if (curStep[0].ToInt32() > 0)
            {
                return ResponseHelper.Create<InvestInfoResponse>(MessageCode.InvestDeposited);
            }
            var configList = CacheFactory.ActivityCache.GetInvestEntityList(step);
            int needPoint = configList[0].Point;
            PayUserEntity payUser = PayCore.Instance.GetPayUser(managerId);
            if (payUser.TotalPoint < needPoint)
                return ResponseHelper.Create<InvestInfoResponse>(MessageCode.NbPointShortage);
            for (int i = 0; i < configList.Count; i++)
            {
                if (manager.Level >= configList[i].Lv)
                {
                    curStep[i] = "1";
                }
            }
            var newStepStatus = RebuildStepStatus(stepStatus, curStep, step);
            investInfo.StepStatus = newStepStatus;
            investInfo.Deposit += needPoint;

            var code = Save_TranInvestDeposit(payUser, investInfo, needPoint);
            if(code != MessageCode.Success)
                return ResponseHelper.Create<InvestInfoResponse>(code);
            List<int> restitution = CacheFactory.ActivityCache.BuildRestitution(newStepStatus);
            //ChatHelper.SendUpdateManagerInfoPop(manager, true, payUser.TotalPoint);
            return BuildInvestInfoResponse(investInfo, restitution, newStepStatus, payUser, needPoint);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="index">0-6</param>
        /// <returns></returns>
        public InvestInfoResponse ReceiveBindPoint(Guid managerId, int index)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return ResponseHelper.InvalidParameter<InvestInfoResponse>();
            var investInfo = InvestManagerMgr.GetById(managerId);
            if (investInfo == null)
                return ResponseHelper.InvalidParameter<InvestInfoResponse>();
            string stepStatus = investInfo.StepStatus;
            var status = BuildReceiveStatus(stepStatus);
            if(status[index] != "1")
                return ResponseHelper.InvalidParameter<InvestInfoResponse>();
            List<int> restitution = CacheFactory.ActivityCache.BuildRestitution(stepStatus);
            int receiveBindPoint = restitution[index];
            restitution[index] = 0;
            string[] ss = stepStatus.Split('|');
            string[] newStatus = new string[4];
            for(int i=0;i < ss.Length; i++)
            {
                string[] st = ss[i].Split(',');
                for (int j = 0; j < st.Length; j++)
                {
                    if (st[index] == "1")
                    {
                        st[index] = "2";
                    }
                }
                newStatus[i] = ReBuildCurStepStatus(st);
            }
            var newStepStatus = RebuildStepStatus(newStatus);
            PayUserEntity payUser = PayCore.Instance.GetPayUser(managerId);
            payUser.BindPoint += receiveBindPoint;
            investInfo.StepStatus = newStepStatus;
            var code = Save_TranReceiveBindPoint(payUser, investInfo);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<InvestInfoResponse>(code);
            //ChatHelper.SendBindPoint(manager.Idx, payUser.BindPoint);
            return BuildInvestInfoResponse(investInfo, restitution, newStepStatus, payUser, 0);
        }

        public InvestInfoResponse InvestDepositMonth(Guid managerId)
        {
            var investInfo = InvestManagerMgr.GetById(managerId);
            if (investInfo == null)
            {
                return ResponseHelper.Create<InvestInfoResponse>(MessageCode.InvalidArgs);
            }
            investInfo.DepositCount++;
            if(investInfo.DepositCount > 0)
                investInfo.Once = true;
            if (investInfo.TheMonthly)
            {
                investInfo.ExpirationTime = investInfo.ExpirationTime.AddDays(30);
            }
            else
            {
                investInfo.TheMonthly = true;
                investInfo.MonthlyTime = DateTime.Today;
                investInfo.ExpirationTime = DateTime.Today.AddDays(30);
                investInfo.ReceivedCount = 0;
            }
            List<int> restitution = CacheFactory.ActivityCache.BuildRestitution(investInfo.StepStatus);
            PayUserEntity payUser = PayCore.Instance.GetPayUser(managerId);
            if (payUser.TotalPoint < _investDepositMonthly)
                return ResponseHelper.Create<InvestInfoResponse>(MessageCode.NbPointShortage);
            var code = Save_TranInvestDeposit(payUser, investInfo, _investDepositMonthly);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<InvestInfoResponse>(code);
            return BuildInvestInfoResponse(investInfo, restitution, investInfo.StepStatus, payUser, _investDepositMonthly);
        }

        public InvestInfoResponse ReceiveBindPointPerDay(Guid managerId, bool once)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
            {
                return ResponseHelper.InvalidParameter<InvestInfoResponse>();
            }
            PayUserEntity payUser = PayCore.Instance.GetPayUser(managerId);
            var investInfo = InvestManagerMgr.GetById(managerId);
            if (investInfo == null)
            {
                return ResponseHelper.Create<InvestInfoResponse>(MessageCode.InvalidArgs);
            }
            if (!investInfo.TheMonthly && DateTime.Today > investInfo.ExpirationTime)
            {
                return ResponseHelper.Create<InvestInfoResponse>(MessageCode.InvalidArgs);
            }
            int totalCount = (investInfo.ExpirationTime - investInfo.MonthlyTime).Days;
            int oddCount = (investInfo.ExpirationTime - DateTime.Today).Days;
            if (once && investInfo.Once && investInfo.DepositCount > 0)
            {
                var bindPoint = _investDeposit*investInfo.DepositCount;
                payUser.BindPoint += bindPoint;
                investInfo.Once = false;
                investInfo.DepositCount = 0;
            }
            else
            {
                if (investInfo.ReceivedCount >  (totalCount - oddCount))
                {
                    return ResponseHelper.Create<InvestInfoResponse>(MessageCode.InvestGetTomorrow);
                }
                payUser.BindPoint += 100;
                investInfo.ReceivedCount++;
            }

            var code = Save_TranReceiveBindPoint(payUser, investInfo);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<InvestInfoResponse>(code);
            List<int> restitution = CacheFactory.ActivityCache.BuildRestitution(investInfo.StepStatus);
            //ChatHelper.SendBindPoint(manager.Idx, payUser.BindPoint);
            return BuildInvestInfoResponse(investInfo, restitution, investInfo.StepStatus, payUser, 0);
        }


        #endregion

        #region encapsulation

        InvestInfoResponse BuildInvestInfoResponse(InvestManagerEntity investInfo, List<int>restitution, string newStepStatus, PayUserEntity payUser, int depositPoint)
        {
            var response = ResponseHelper.CreateSuccess<InvestInfoResponse>();
            response.Data = new InvestInfoEntity();
            
            response.Data.Deposit = investInfo.Deposit;
            response.Data.Restitution = restitution;
            response.Data.ReceiveStatus = BuildReceiveStatus(newStepStatus);
            response.Data.Once = investInfo.Once;

            if (DateTime.Today > investInfo.ExpirationTime)
            {
                response.Data.TotalCount = 0;
                response.Data.ReceiveCount = 0;
            }
            else
            {
                response.Data.TotalCount = (investInfo.ExpirationTime - investInfo.MonthlyTime).Days;
                response.Data.ReceiveCount = investInfo.ReceivedCount;
            }
                
            if (investInfo.TheMonthly)
                response.Data.DayCount = (DateTime.Today - investInfo.MonthlyTime).Days + 1;
            else
                response.Data.DayCount = 0;
            if (payUser == null)
            {
                response.Data.BindPoint = 0;
            }
            else
            {
                response.Data.BindPoint = payUser.BindPoint;
                //ChatHelper.SendUpdateManagerInfoPop(investInfo.ManagerId, payUser.TotalPoint - depositPoint, true);
            }
            
            return response;
        }

        InvestManagerEntity InnerBuildEntity(Guid managerId)
        {
            InvestManagerEntity entity = new InvestManagerEntity();
            entity.ManagerId = managerId;
            entity.Deposit = 0;
            entity.StepStatus = "0,0,0,0,0,0,0,0,0,0,0,0|0,0,0,0,0,0,0,0,0,0,0,0|0,0,0,0,0,0,0,0,0,0,0,0|0,0,0,0,0,0,0,0,0,0,0,0";
            entity.TheMonthly = false;
            entity.MonthlyTime = DateTime.Today;
            entity.ExpirationTime = DateTime.Today;
            entity.Once = false;
            entity.ReceivedCount = 0;
            entity.RowTime = DateTime.Now;
            entity.DepositCount = 0;
            InvestManagerMgr.Insert(entity);
            return entity;
        }

        string[] BuildReceiveStatus(string stepStatus)
        {
            string[] ars = new string[12];
            string[] ss = stepStatus.Split('|');
            foreach (var step in ss)
            {
                string[] st = step.Split(',');
                for (int i = 0; i < st.Length; i++)
                {
                    if (st[i] == "1")
                    {
                        ars[i] = "1";
                    }
                    else
                    {
                        if (ars[i] != "1" && st[i] == "2")
                        {
                            ars[i] = "2";
                        }
                    }
                }
            }
            for (int i = 0; i < ars.Length; i++)
            {
                if (ars[i] == null)
                    ars[i] = "0";
            }

            return ars;
        }

        string Rebuild(string stepStatus, int managerLv)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= 4; i++)
            {
                sb.AppendFormat(Step(stepStatus, managerLv, i));
                sb.AppendFormat("|");
            }
            return sb.ToString().TrimEnd('|');
        }
        string Step(string stepStatus, int managerLevel, int step)
        { 
            var configList = CacheFactory.ActivityCache.GetInvestEntityList(step);
            var s = GetCurStepStr(stepStatus, step);
            if (s[0].ToInt32() <= 0)
                return ReBuildCurStepStatus(s);
            for (int j = 0; j < configList.Count; j++)
            {   
                if (managerLevel >= configList[j].Lv)
                { 
                    if (s[j] != "1" && s[j] != "2")
                    {
                        s[j] = "1";
                    }   
                }
            }
            return ReBuildCurStepStatus(s);
        }


        string[] GetCurStepStr(string stepStatus, int step)
        {
            string[] ss = stepStatus.Split('|');
            return ss[step - 1].Split(',');
        }

        string RebuildStepStatus(string stepStatus, string[] curStep, int step)
        {
            string[] ss = stepStatus.Split('|');
            string cur = "";
            foreach (var s in curStep)
            {
                cur += s + ",";
            }
            ss[step - 1] = cur.TrimEnd(',');
            string newStep = "";
            foreach (var s in ss)
            {
                newStep += s + "|";
            }
            var nStep = newStep.TrimEnd('|');
            return nStep;
        }

        string RebuildStepStatus(string[] stepStatus)
        {
            string newStepStatus = string.Empty;
            foreach (var s in stepStatus)
            {
                newStepStatus += s + "|";
            }
            return newStepStatus.TrimEnd('|');
        }

        string ReBuildCurStepStatus(string[] ss)
        {
            string stepStatus = string.Empty;
            foreach (var s in ss)
            {
                stepStatus += s + ",";
            }
            return stepStatus.TrimEnd(',');
        }


        MessageCode Save_TranInvestDeposit(PayUserEntity payUser, InvestManagerEntity investInfo, int point)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var code = Tran_InvestDeposit(payUser, investInfo, point, transactionManager.TransactionObject);
                    if (code == ShareUtil.SuccessCode)
                    {
                        transactionManager.Commit();
                    }
                    else
                    {
                        transactionManager.Rollback();
                    }
                    return code;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("InvestDeposit", ex);
                return MessageCode.Exception;
            }
        }

        MessageCode Tran_InvestDeposit(PayUserEntity payUser, InvestManagerEntity investInfo, int point, DbTransaction transaction)
        {
            if (!InvestManagerMgr.Update(investInfo, transaction))
                return MessageCode.NbUpdateFail;
            int returnCode = -2;
            PayUserMgr.ConsumePoint(payUser.Account, investInfo.ManagerId, (int)EnumConsumeSourceType.InvestDeposit,
                Guid.NewGuid().ToString(), point, DateTime.Now, payUser.RowVersion, ref returnCode, transaction);
            if (returnCode != (int)MessageCode.PaySuccess)
                return MessageCode.NbUpdateFail;
            return MessageCode.Success;

        }

        MessageCode Save_TranReceiveBindPoint(PayUserEntity payUser, InvestManagerEntity investInfo)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var code = Tran_ReceiveBindPoint(payUser, investInfo, transactionManager.TransactionObject);
                    if (code == ShareUtil.SuccessCode)
                    {
                        transactionManager.Commit();
                    }
                    else
                    {
                        transactionManager.Rollback();
                    }
                    return code;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ReceiveBindPoint", ex);
                return MessageCode.Exception;
            }
        }

        MessageCode Tran_ReceiveBindPoint(PayUserEntity payUser, InvestManagerEntity investInfo,
            DbTransaction transaction)
        {
            if (!InvestManagerMgr.Update(investInfo, transaction))
                return MessageCode.NbUpdateFail;

            if (!PayUserMgr.Update(payUser, transaction))
                return MessageCode.NbUpdateFail;
            return MessageCode.Success;
        }

        #endregion
    }
}
