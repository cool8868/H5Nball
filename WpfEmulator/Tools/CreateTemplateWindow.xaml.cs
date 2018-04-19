using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Match;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using NPOI.SS.Formula.Functions;

namespace Games.NBall.WpfEmulator.Tools
{
    /// <summary>
    /// CreateTemplateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CreateTemplateWindow : Window
    {
        public CreateTemplateWindow()
        {
            InitializeComponent();
        }

        private Thread _thread;
        private int _finishCount = 0;
        private int _totalCount = 0;
        public delegate void CreateDelegate(bool isSuccess);
        public delegate void FinishDelegate();

        private void BtnGenerate_OnClick(object sender, RoutedEventArgs e)
        {
            string countStr = txtCount.Text;
            if (string.IsNullOrEmpty(countStr))
            {
                MessageBox.Show("请输入参数！");
                return;
            }
            int count = ConvertHelper.ConvertToInt(countStr, 1);
            if (count > 10000)
            {
                MessageBox.Show("一次最多生成一万个.");
                return;
            }
            _totalCount = count;
            _finishCount = 0;
            ProgressBar1.Maximum = _totalCount;
            ProgressBar1.Value = 0;
            lblProcess.Content = string.Format("{0}/{1}", 0, _totalCount);
            StartLoop();
        }

        public void StartLoop()
        {
            _thread = new Thread(() => Start(_totalCount, CreateCallback, FinishCallback));
            _thread.IsBackground = true;
            _thread.Start();
        }

        public void Start(int totalCount,CreateDelegate createDelegate,FinishDelegate finishDelegate)
        {
            List<DicPlayerEntity> list = DicPlayerMgr.GetAllForCache();
            for (int i = 0; i < totalCount; i++)
            {
                try
                {
                    CreateTemplate(list);
                    Dispatcher.Invoke((Action)delegate { createDelegate(true); }); 
                }
                catch (Exception ex)
                {
                    LogHelper.Insert(ex);
                    Dispatcher.Invoke((Action) delegate { createDelegate(false); });
                }
            }
            Dispatcher.Invoke((Action) delegate { finishDelegate(); });
        }

        void CreateTemplate(List<DicPlayerEntity> list)
        {
            var positionList = BuildTemplatePosition();
            var conditionList = BuildTemplateCondition();
            string s = "";
            List<int> ids=new List<int>(11);
            //for (int i = 0; i < 11; i++)
            //{
            //    var items = list.FindAll(d => d.Position == (int)positionList[i].Position
            //        &&d.Kpi>=conditionList[i].MinPower && d.Kpi<=conditionList[i].MaxPower);
            //    var item = items[RandomHelper.GetInt32WithoutMax(0, items.Count)];
            //    while (ids.Contains(item.Idx))
            //    {
            //        item = items[RandomHelper.GetInt32WithoutMax(0, items.Count)];
            //    }
            //    ids.Add(item.Idx);
            //    s += item.Idx + ",";
            //}
            //不能有元老卡
            DTOBuffMemberView view = new DTOBuffMemberView();
            view.BuffMembers = new Dictionary<Guid, NbManagerbuffmemberEntity>();
            for (int i = 0; i < 7; i++)
            {
                var condition = conditionList[i];
                if (condition.MaxPower == 0)
                    condition.MaxPower = 200;
                var position = positionList[i];
                var items = list.FindAll(d => d.Position == (int)position.Position && d.CardLevel == condition.CardLevel
                    && d.Capacity >= condition.MinPower && d.Capacity <= condition.MaxPower);
                var item = items[RandomHelper.GetInt32WithoutMax(0, items.Count)];
                while (ids.Contains(item.Idx))
                {
                    item = items[RandomHelper.GetInt32WithoutMax(0, items.Count)];
                }
                view.BuffMembers.Add(Guid.NewGuid(), BuildBuffMember(item));
                ids.Add(item.Idx);
                s += item.Idx + ",";
            }
            MatchDataHelper.CalKpi(view);
            s = s.TrimEnd(',');
            TemplateRegisterMgr.Add(s,view.Kpi);
        }

        NbManagerbuffmemberEntity BuildBuffMember(DicPlayerEntity dic)
        {
            NbManagerbuffmemberEntity member = new NbManagerbuffmemberEntity();
            member.PPos = dic.Position;
            member.IsMain = true;
            var props = dic.GetRawProps();
            member.SpeedCount += props[0];
            member.ShootCount += props[1];
            member.FreeKickCount += props[2];
            member.BalanceCount += props[3];
            member.PhysiqueCount += props[4];
            member.BounceCount += props[5];
            member.AggressionCount += props[6];
            member.DisturbCount += props[7];
            member.InterceptionCount += props[8];
            member.DribbleCount += props[9];
            member.PassCount += props[10];
            member.MentalityCount += props[11];
            member.ResponseCount += props[12];
            member.PositioningCount += props[13];
            member.HandControlCount += props[14];
            member.AccelerationCount += props[15];
            return member;
        }

        List<TemplatePosition> BuildTemplatePosition()
        {
            //3个后卫，2个中场，1个前锋，1个门将 
            var list = new List<TemplatePosition>(11);
            int i = 1;

            list.Add(new TemplatePosition() { Idx = i++, Position = EnumPosition.Goalkeeper });
            list.Add(new TemplatePosition(){Idx = i++,Position = EnumPosition.Fullback});
            list.Add(new TemplatePosition(){Idx = i++,Position = EnumPosition.Fullback});
            list.Add(new TemplatePosition(){Idx = i++,Position = EnumPosition.Fullback});
            list.Add(new TemplatePosition(){Idx = i++,Position = EnumPosition.Midfielder});
            list.Add(new TemplatePosition(){Idx = i++,Position = EnumPosition.Midfielder});
            list.Add(new TemplatePosition(){Idx = i++,Position = EnumPosition.Forward});


            list.Sort((left, right) =>
                {
                    return RandomHelper.GetInt32(0, 1);
                });
            return list;
        }

        List<TemplateCondition> BuildTemplateCondition()
        {
            //4个绿色球员
            //2个蓝色球员
            //1个紫色球员(70-75)
            var x = RandomHelper.GetInt32(0, 5);
            switch (x)
            {
                case 1:
                    return BuildCondition1();
                    break;
                case 2:
                    return BuildCondition2();
                    break;
                case 3:
                    return BuildCondition3();
                    break;
                case 4:
                    return BuildCondition4();
                    break;
                case 5:
                    return BuildCondition5();
                    break;
                default:
                    return BuildCondition2();
                    break;
            }

        }

        List<TemplateCondition> BuildCondition1()
        {
            var list = new List<TemplateCondition>(7);
            int i = 1;
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Goalkeeper });

            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 5, Position = EnumPosition.Fullback });
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Fullback });
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 4, MinPower = 70, MaxPower = 75, Position = EnumPosition.Fullback });

            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 5, Position = EnumPosition.Midfielder });
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Midfielder });

            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Forward });
            return list;
        }

        List<TemplateCondition> BuildCondition2()
        {
            var list = new List<TemplateCondition>(7);
            int i = 1;
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Goalkeeper });

            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 5, Position = EnumPosition.Fullback });
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Fullback });
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Fullback });

            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 4, MinPower = 70, MaxPower = 75, Position = EnumPosition.Midfielder });
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Midfielder });

            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 5, Position = EnumPosition.Forward });
            return list;
        }

        List<TemplateCondition> BuildCondition3()
        {
            var list = new List<TemplateCondition>(7);
            int i = 1;
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Goalkeeper });

            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 5, Position = EnumPosition.Fullback });
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Fullback });
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Fullback });

            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 5, Position = EnumPosition.Midfielder });
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Midfielder });

            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 4, MinPower = 70, MaxPower = 75, Position = EnumPosition.Forward });
            return list;
        }

        List<TemplateCondition> BuildCondition4()
        {
            var list = new List<TemplateCondition>(7);
            int i = 1;
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Goalkeeper });

            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Fullback });
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Fullback });
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 4, MinPower = 70, MaxPower = 75, Position = EnumPosition.Fullback });

            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 5, Position = EnumPosition.Midfielder });
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Midfielder });

            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 5, Position = EnumPosition.Forward });
            return list;
        }

        List<TemplateCondition> BuildCondition5()
        {
            var list = new List<TemplateCondition>(7);
            int i = 1;
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 5, Position = EnumPosition.Goalkeeper });

            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Fullback });
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Fullback });
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 4, MinPower = 70, MaxPower = 75, Position = EnumPosition.Fullback });

            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 5, Position = EnumPosition.Midfielder });
            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Midfielder });

            list.Add(new TemplateCondition() { Idx = i++, CardLevel = 6, Position = EnumPosition.Forward });
            return list;
        }
        public void CreateCallback(bool isSuccess)
        {
            _finishCount++;
            ProgressBar1.Value = _finishCount;
            lblProcess.Content = string.Format("{0}/{1}", _finishCount, _totalCount);
            if (!isSuccess)
            {
                lblMessage.Content = string.Format("{0} 生成失败", _finishCount);
            }
        }

        public void FinishCallback()
        {
            lblMessage.Content = "生成成功!";
        }
    }

    public class TemplatePosition
    {
        public int Idx { get; set; }

        public EnumPosition Position { get; set; }
    }

    public class TemplateCondition
    {
        public int Idx { get; set; }

        public int CardLevel { get; set; }

        public int MinPower { get; set; }

        public int MaxPower { get; set; }

        public EnumPosition Position { get; set; }
    }
}
