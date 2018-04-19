using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;

namespace Games.NBall.Bll.Cache
{
    public class GiftPackPrizeCache
    {

        private Dictionary<int, List<DicGiftpackprizeEntity>> _packDic =
            new Dictionary<int, List<DicGiftpackprizeEntity>>();

        public GiftPackPrizeCache(int p)
        {
            InitCache();
        }

        public static GiftPackPrizeCache Instance
        {
            get { return SingletonFactory<GiftPackPrizeCache>.SInstance; }
        }

        private void InitCache()
        {
            var list = DicGiftpackprizeMgr.GetAll();
            foreach (var entity in list)
            {
                var key = entity.PackId;
                if (!_packDic.ContainsKey(key))
                    _packDic.Add(key, new List<DicGiftpackprizeEntity>());
                _packDic[key].Add(entity);
            }
        }

        public List<DicGiftpackprizeEntity> GetGiftPackPrize(int packId)
        {
            if (_packDic.ContainsKey(packId))
                return _packDic[packId];
            return null;
        }

        public List<GiftPackEntity> GetGiftPackForAS()
        {
            var giftPackList = DicGiftpackMgr.GetAll();
            List<GiftPackEntity> packList = new List<GiftPackEntity>();
            foreach (var gift in giftPackList)
            {
                GiftPackEntity giftPack = new GiftPackEntity();
                giftPack.PackId = gift.Idx;
                giftPack.GiftPrize = new List<PackPrizeEntity>();
                var giftPackPrize = GetGiftPackPrize(gift.Idx);
                foreach (var prize in giftPackPrize)
                {
                    PackPrizeEntity packPrize = new PackPrizeEntity();
                    packPrize.PrizeType = prize.PrizeType;
                    packPrize.SubType = prize.SubType;
                    packPrize.Count = prize.Count;
                    giftPack.GiftPrize.Add(packPrize);
                }
                packList.Add(giftPack);
            }
            return packList;
        }
    }

    public class GiftPackEntity
    {
        public int PackId { get; set; }
        public List<PackPrizeEntity> GiftPrize { get; set; }
    }

    public class PackPrizeEntity
    {
        public int PrizeType { get; set; }
        public int SubType { get; set; }
        public int Count { get; set; }
    }
}
