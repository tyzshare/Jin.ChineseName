using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jin.ChineseName.Test
{
    [TestClass]
    public class ChineseNamePinyinConvertTest
    {

        /// <summary>
        /// 测试参数为空
        /// </summary>
        [TestMethod]
        public void ArgumentNullTest()
        {
            try
            {
                var result = ChineseNamePinyinConvert.GetChineseNamePinYin("");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }

            try
            {
                var result = ChineseNamePinyinConvert.GetChineseNamePinYin("  ");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }

            try
            {
                var result = ChineseNamePinyinConvert.GetChineseNamePinYin(null);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }


        /// <summary>
        /// 测试参数为空
        /// </summary>
        [TestMethod]
        public void PinyinConvertResultTest()
        {
            var dbHost = "server=192.168.50.208;port=3306;User Id=myth_dev;Pwd=myth_dev;Persist Security Info=True;database=consultant";
            var redisConnect = "192.168.50.210:6379,defaultDatabase=1,abortConnect=false,ssl=false";
            var cachePrefix = "myth.sis";

            //不使用缓存 测试
            //Assert.AreEqual("shi", ChineseNamePinyinConvert.GetChineseNamePinYin("石"));
            //Assert.AreEqual("shigu", ChineseNamePinyinConvert.GetChineseNamePinYin("石家"));
            //Assert.AreEqual("shiguge", ChineseNamePinyinConvert.GetChineseNamePinYin("石家屹"));

            //使用缓存 测试
            //Assert.AreEqual("shi", ChineseNamePinyinConvert.GetChineseNamePinYin("石", dbHost, redisConnect, cachePrefix));
            //Assert.AreEqual("shijia", ChineseNamePinyinConvert.GetChineseNamePinYin("石家", dbHost, redisConnect, cachePrefix, DateTime.Now.AddSeconds(60)));

            //数据库，redis 
            Assert.AreEqual("shijiayi", ChineseNamePinyinConvert.GetChineseNamePinYin("石家屹", dbHost, redisConnect, cachePrefix,null));

            ////单姓氏测试-只有单姓氏
            //Assert.AreEqual("shan", ChineseNamePinyinConvert.GetChineseNamePinYin("单"));
            ////单姓氏测试
            //Assert.AreEqual("shan", ChineseNamePinyinConvert.GetChineseNamePinYin("单雄信").Substring(0, 4));

            ////复姓测试-只有复姓
            //Assert.AreEqual("huangfu", ChineseNamePinyinConvert.GetChineseNamePinYin("皇甫"));
            ////复姓测试
            //Assert.AreEqual("huangfu", ChineseNamePinyinConvert.GetChineseNamePinYin("皇甫鹏").Substring(0, 7));

            //非常规字符测试
            //Assert.AreEqual(ChineseNamePinyinConvert.GetChineseNamePinYin("A"), "a");
            //Assert.AreEqual(ChineseNamePinyinConvert.GetChineseNamePinYin("^"), "^");
        }
    }
}
