using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.WpfEmulator.Command;
using WpfEmulator;

namespace Games.NBall.WpfEmulator.Core
{
    public class ApiTestCore
    {
        public static ApiTestWindow MainWindow { get; set; }

        public static NbManagerEntity CurManager { get; set; }
        public static NbManagerextraEntity CurManagerExtra { get; set; }
        public static long CurPoint { get; set; }
        public static long ServerTime { get; set; }

        private static Dictionary<int, int> _taskRequireFuncDic;
        public static int DailyCount;
        public static int MaxDailyCount;

        public static void UpdateManagerData(NBManagerInfoData managerData)
        {
            MainWindow.BindManagerInfo(managerData);
        }

        public static void UpdatePoint(int point)
        {
            if(point<0)
                return;
            CurPoint = point;
            MainWindow.UpdatePoint();
        }

        public static void UpdateCoin(int coin)
        {
            if(coin<0)
                return;
            CurManager.Coin = coin;
            MainWindow.UpdateCoin();
        }

        public static void UpdateCurrency(int currencyType, int count)
        {
            switch (currencyType)
            {
                case 1:
                    UpdatePoint(count);
                    break;
                case 21:
                    UpdateCoin(count);
                    break;
            }
        }

        public static void UpdateTask()
        {
          
        }


        public static DateTime GetServerTime()
        {
            return ShareUtil.GetTime(ServerTime);
        }

    }


}
