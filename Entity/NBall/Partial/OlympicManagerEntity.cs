using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Games.NBall.Common;

namespace Games.NBall.Entity
{

    public partial class OlympicManagerEntity
    {
        public OlympicManagerEntity(Guid managerId)
        {
            this.ManagerId = managerId;
            this.Badminton = 0;
            this.Basketball = 0;
            this.Football = 0;
            this.Gymnastics = 0;
            this.Judo = 0;
            this.Rowing = 0;
            this.RowTime = DateTime.Now;
            this.Shooting = 0;
            this.Swimming = 0;
            this.TableTennis = 0;
            this.TrackAndField = 0;
            this.UpdateTime = DateTime.Now;
            this.Volleyball = 0;
            this.WeightLifting = 0;
        }
        private Dictionary<int, int> _theGoldMedalDic;

        public Dictionary<int, int> TheGoldMedalDic
        {
            get
            {
                _theGoldMedalDic = new Dictionary<int, int>();
                _theGoldMedalDic.Add(1, this.Football);
                _theGoldMedalDic.Add(2, this.Basketball);
                _theGoldMedalDic.Add(3, this.Volleyball);
                _theGoldMedalDic.Add(4, this.Swimming);
                _theGoldMedalDic.Add(5, this.Gymnastics);
                _theGoldMedalDic.Add(6, this.Shooting);
                _theGoldMedalDic.Add(7, this.TrackAndField);
                _theGoldMedalDic.Add(8, this.WeightLifting);
                _theGoldMedalDic.Add(9, this.TableTennis);
                _theGoldMedalDic.Add(10, this.Badminton);
                _theGoldMedalDic.Add(11, this.Rowing);
                _theGoldMedalDic.Add(12, this.Judo);
                return _theGoldMedalDic;
            }
        }

        /// <summary>
        /// 增加金牌
        /// </summary>
        /// <param name="theGoldmedalId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool AddTheGoldMedal(int theGoldmedalId, int count)
        {
            switch (theGoldmedalId)
            {
                case 1:
                    this.Football += count;
                    break;
                case 2:
                    this.Basketball += count;
                    break;
                case 3:
                    this.Volleyball += count;
                    break;
                case 4:
                    this.Swimming += count;
                    break;
                case 5:
                    this.Gymnastics += count;
                    break;
                case 6:
                    this.Shooting += count;
                    break;
                case 7:
                    this.TrackAndField += count;
                    break;
                case 8:
                    this.WeightLifting += count;
                    break;
                case 9:
                    this.TableTennis += count;
                    break;
                case 10:
                    this.Badminton += count;
                    break;
                case 11:
                    this.Rowing += count;
                    break;
                case 12:
                    this.Judo += count;
                    break;
                default:
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 随机增加一个金牌
        /// </summary>
        public int RandomAddTheGoldMedal()
        {
            var theGoldMedalId = RandomHelper.GetInt32(1, 12);
            AddTheGoldMedal(theGoldMedalId, 1);
            return theGoldMedalId;
        }

        /// <summary>
        /// 扣除金牌
        /// </summary>
        /// <param name="theGoldmedalId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool DeductTheGoldMedal(int theGoldmedalId, int count)
        {
            switch (theGoldmedalId)
            {
                case 1:
                    if (this.Football < count)
                        return false;
                    this.Football -= count;
                    break;
                case 2:
                    if (this.Basketball < count)
                        return false;
                    this.Basketball -= count;
                    break;
                case 3:
                    if (this.Volleyball < count)
                        return false;
                    this.Volleyball -= count;
                    break;
                case 4:
                    if (this.Swimming < count)
                        return false;
                    this.Swimming -= count;
                    break;
                case 5:
                    if (this.Gymnastics < count)
                        return false;
                    this.Gymnastics -= count;
                    break;
                case 6:
                    if (this.Shooting < count)
                        return false;
                    this.Shooting -= count;
                    break;
                case 7:
                    if (this.TrackAndField < count)
                        return false;
                    this.TrackAndField -= count;
                    break;
                case 8:
                    if (this.WeightLifting < count)
                        return false;
                    this.WeightLifting -= count;
                    break;
                case 9:
                    if (this.TableTennis < count)
                        return false;
                    this.TableTennis -= count;
                    break;
                case 10:
                    if (this.Badminton < count)
                        return false;
                    this.Badminton -= count;
                    break;
                case 11:
                    if (this.Rowing < count)
                        return false;
                    this.Rowing -= count;
                    break;
                case 12:
                    if (this.Judo < count)
                        return false;
                    this.Judo -= count;
                    break;
                default:
                    return false;
            }
            return true;
        }
    }


    public partial class OlympicManagerResponse
    {

    }
}
