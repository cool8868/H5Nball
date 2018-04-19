using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Item;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Response.Item;
using NUnit.Framework;

namespace Games.NBall.NUnitTest.Manager
{
    [TestFixture]
    public class PandoraTest
    {
        private Guid managerId = new Guid("634C1420-B9F3-442B-8600-A58A01178E7A");
        private Guid baida1 = new Guid("e22dcda2-53e3-4b02-851d-a54100ecfc9d");
        private Guid baida2 = new Guid("ecc68480-fb97-42f6-94da-a58b00a953e9");
        private Guid baohu1 = new Guid("d2300c89-2c3b-47e2-8a99-a54100ed06fb");
        private Guid baohu2 = new Guid("7bdea931-2c69-4eff-82e7-a54100ed0426");
        private Guid hecheng = new Guid("c436313e-ac45-4afd-9d4b-a54100ed0a1a");
        #region 潘多拉

        [Test]
        public StrengthParamResponse StrengthenParam()
        {

            //var package = ItemCore.Instance.GetPackage(managerId,EnumTransactionType.PlayreTrain);
            //package.AddItems(310101, 99);
            //package.AddItems(120025, 1);
            //package.Save();


            var response = PandoraCore.Instance.StrengthenParam(managerId, new Guid("d516448c-c4cb-4c4e-ac2d-a58b00a95781"), baida2);
            return response;
        }

        [Test]
        public StrengthResponse Strengthen()
        {
            var response = PandoraCore.Instance.Strengthen(managerId, new Guid("673b3e5a-cc5d-4ff6-890a-a54100ee061c"), baida2, true, baohu2);
            return response;
        }

        [Test]
        public DecomposeResponse Decompose()
        {
            var response = PandoraCore.Instance.Decompose(managerId,"673b3e5a-cc5d-4ff6-890a-a54100ee061c");
            return response;
        }

        [Test]
        public SynthesisResponse Synthesis()
        {
            var response = PandoraCore.Instance.Synthesis(managerId, new Guid("4b8a9cbf-7e34-4c74-9252-a54100ee22e3"), new Guid("65d20057-b314-4ed7-8b4a-a54100ee24e7"), new Guid("b599a232-ff05-4f4b-95c2-a54100ee26bf"), new Guid("be800af8-6239-4e37-a35a-a54100ee29b4"), new Guid("e1555548-ac47-4be1-b5b1-a54100ee2e2c"), true, hecheng);
            return response;
        }

        [Test]
        public SynthesisParamResponse SynthesisParam()
        {
            var response = PandoraCore.Instance.SynthesisParam(managerId, 1);
            return response;
        }

        [Test]
        public SynthesisResponse TheContractSynthetic()
        {
            var response = PandoraCore.Instance.TheContractSynthetic(managerId, new Guid("c91617da-7534-4123-93c4-a54101091eb1"), new Guid("c91617da-7534-4123-93c4-a54101091eb1"), new Guid("c91617da-7534-4123-93c4-a54101091eb1"), new Guid("c91617da-7534-4123-93c4-a54101091eb1"), new Guid("c91617da-7534-4123-93c4-a54101091eb1"));
            return response;
        }

        //[Test]
        //public EquipmentSynthesisParamResponse EquipmentSynthesisParam()
        //{
        //    var response = PandoraCore.Instance.EquipmentSynthesisParam(managerId, 2);
        //    return response;
        //}

        [Test]
        public SynthesisResponse EquipmentSynthesis()
        {
            var response = PandoraCore.Instance.EquipmentSynthesis(managerId, new Guid("910ee1fa-d97a-4ab9-8d9b-a541010c2fc8"), new Guid("5c7adcb6-8348-4bc3-a417-a541010c2c5d"), new Guid("0499da84-7a99-4fd3-82f5-a541010c2a73"), new Guid("d4069360-776c-4c46-9179-a541010c269d"), new Guid("5bf60cef-0be9-4a42-97f6-a541010c24f0"), false, hecheng);
            return response;
        }

        [Test]
        public EquipmentSellResponse EquipmentSell()
        {
            var response = PandoraCore.Instance.EquipmentSell(managerId, new Guid("3a287822-fb61-4147-982d-a541010bfa69"));
            return response;
        }

        #endregion

        [Test]
        public void BinaryTest()
        {
            var package = ItemCore.Instance.GetPackageResponse(new Guid("31F0198F-B610-454C-8565-A60E01313CF6"));
            var buffer = GetPackageBuffer(package);
            Console.WriteLine(buffer.Length);
            var newPackage = ReadPackage(buffer);
            var buffer2 = GetPackageBuffer(newPackage);
            Console.WriteLine(buffer2.Length);
        }

        #region WritePackage
        public byte[] GetPackageBuffer(ItemPackageResponse response)
        {
            using (var ms = new MemoryStream())
            {
                var writer = new BinaryWriter(ms);
                var packageData = response.Data;
                ByteWriter.WriteTo(writer, packageData.PackageSize);//32位 int
                ByteWriter.WriteTo(writer, packageData.Items.Count);//32位 int
                foreach (var entity in packageData.Items)
                {
                    ByteWriter.WriteTo(writer, entity.ItemCount);//32位 int
                    ByteWriter.WriteTo(writer, entity.ItemCode);//32位 int
                    ByteWriter.WriteTo(writer, entity.ItemId);//string
                    ByteWriter.WriteTo(writer, (byte)entity.ItemType);//8位 byte
                    switch (entity.ItemType)
                    {
                        case (int)EnumItemType.PlayerCard:
                            WritePlayerCardProperty(writer, entity.ItemProperty as PlayerCardProperty);
                            break;
                        case (int)EnumItemType.Equipment:
                            WriteEquipmentProperty(writer, entity.ItemProperty as EquipmentProperty);
                            break;
                    }
                }

                writer.Flush();
                var bytes = ms.ToArray();
                return bytes;
            }
        }

        void WritePlayerCardProperty(BinaryWriter writer, PlayerCardProperty property)
        {
            ByteWriter.WriteTo(writer, property != null);
            if (property == null)
                return;
            ByteWriter.WriteTo(writer, property.Exp);
            ByteWriter.WriteTo(writer, property.IsMain);
            ByteWriter.WriteTo(writer, property.IsTrain);
            ByteWriter.WriteTo(writer, property.Level);
            ByteWriter.WriteTo(writer, property.Strength);
            ByteWriter.WriteTo(writer, property.TeammemberId);
            ByteWriter.WriteTo(writer, property.TheActualKpi);
            WriteEquipmentUsed(writer, property.Equipment);
            
        }

        void WriteEquipmentUsed(BinaryWriter writer, EquipmentUsedEntity property)
        {
            ByteWriter.WriteTo(writer, property != null);
            if (property == null)
                return;
            ByteWriter.WriteTo(writer, property.ItemId);
            ByteWriter.WriteTo(writer, property.ItemCode);
            ByteWriter.WriteTo(writer, property.IsBinding);
            WriteEquipmentProperty(writer, property.Property);
            
        }

        void WriteEquipmentProperty(BinaryWriter writer, EquipmentProperty property)
        {
            ByteWriter.WriteTo(writer, property != null);
            if (property == null)
                return;
            int num1 = (property.PropertyPluses == null) ? 0 : property.PropertyPluses.Count;
            ByteWriter.WriteTo(writer, num1);
            if (num1 > 0)
            {
                for (int i = 0; i < property.PropertyPluses.Count; i++)
                {
                    WriteEquipmentPropertyPlus(writer, property.PropertyPluses[i]);
                }
            }
        }

        void WriteEquipmentPropertyPlus(BinaryWriter writer, PropertyPlusEntity property)
        {
            ByteWriter.WriteTo(writer, property != null);
            if (property == null)
                return;
            ByteWriter.WriteTo(writer, property.PlusType);
            ByteWriter.WriteTo(writer, property.PlusValue);
            ByteWriter.WriteTo(writer, property.PropertyId);
        }
        #endregion

        #region ReadPackage
        public ItemPackageResponse ReadPackage(byte[] buffer)
        {
            var response = new ItemPackageResponse();
            response.Data=new ItemPackageEntity();
            int offset = 0;
            response.Data.PackageSize = ByteReader.ReadInt32(buffer, ref offset);
            response.Data.Items = new List<ItemInfoEntity>();
            int num1 = ByteReader.ReadInt32(buffer, ref offset);
            if (num1 > 0)
            {
                for (int i = 0; i < num1; i++)
                {
                    ItemInfoEntity item = new ItemInfoEntity();
                    item.ItemCount = ByteReader.ReadInt32(buffer, ref offset);
                    item.ItemCode = ByteReader.ReadInt32(buffer, ref offset);
                    item.ItemId =new Guid(ByteReader.ReadString(buffer, ref offset));
                    item.ItemType = ByteReader.ReadByte(buffer, ref offset);
                    switch (item.ItemType)
                    {
                        case (int)EnumItemType.PlayerCard:
                            item.ItemProperty =ReadPlayerCardProperty(buffer, ref offset);
                            break;
                        case (int)EnumItemType.Equipment:
                            item.ItemProperty = ReadEquipmentProperty(buffer, ref offset);
                            break;
                    }
                    response.Data.Items.Add(item);
                }
            }
            return response;
        }

        PlayerCardProperty ReadPlayerCardProperty(byte[] buffer, ref int offset)
        {
            PlayerCardProperty property = null;
            var flag = ByteReader.ReadBoolean(buffer, ref offset);
            if (flag)
            {
                property = new PlayerCardProperty();
                property.Exp = ByteReader.ReadInt32(buffer, ref offset);
                property.IsMain=ByteReader.ReadBoolean(buffer, ref offset);
                property.IsTrain=ByteReader.ReadBoolean(buffer, ref offset);
                property.Level=ByteReader.ReadInt32(buffer, ref offset);
                property.Strength=ByteReader.ReadInt32(buffer, ref offset);
                property.TeammemberId=new Guid(ByteReader.ReadString(buffer, ref offset));
                property.TheActualKpi=ByteReader.ReadInt32(buffer, ref offset);
                property.Equipment= ReadEquipmentUsed(buffer,ref offset);
                
            }
            return property;
        }

        EquipmentUsedEntity ReadEquipmentUsed(byte[] buffer, ref int offset)
        {
            EquipmentUsedEntity property = null;
            var flag = ByteReader.ReadBoolean(buffer, ref offset);
            if (flag)
            {
                property = new EquipmentUsedEntity();
                property.ItemId = new Guid(ByteReader.ReadString(buffer, ref offset));
                property.ItemCode = ByteReader.ReadInt32(buffer, ref offset);
                property.IsBinding = ByteReader.ReadBoolean(buffer, ref offset);
                property.Property = ReadEquipmentProperty(buffer, ref offset);
            }
            return property;
        }

        EquipmentProperty ReadEquipmentProperty(byte[] buffer, ref int offset)
        {
            EquipmentProperty property = null;
            var flag = ByteReader.ReadBoolean(buffer, ref offset);
            if (flag)
            {
                property = new EquipmentProperty();
                int num1 = ByteReader.ReadInt32(buffer, ref offset);
                if (num1 > 0)
                {
                    for (int i = 0; i < num1; i++)
                    {
                        var plus = ReadEquipmentPropertyPlus(buffer,ref offset);
                        property.PropertyPluses.Add(plus);
                    }
                }
            }
            return property;
        }

        PropertyPlusEntity ReadEquipmentPropertyPlus(byte[] buffer, ref int offset)
        {
            PropertyPlusEntity property = null;
            var flag = ByteReader.ReadBoolean(buffer, ref offset);
            if (flag)
            {
                property = new PropertyPlusEntity();
                property.PlusType = ByteReader.ReadInt32(buffer, ref offset);
                property.PlusValue = ByteReader.ReadDouble(buffer, ref offset);
                property.PropertyId = ByteReader.ReadInt32(buffer, ref offset);
            }
            return property;
        }
        #endregion
    }
}
