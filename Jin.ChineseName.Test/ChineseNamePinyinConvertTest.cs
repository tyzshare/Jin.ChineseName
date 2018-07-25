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
            //单姓氏测试-只有单姓氏
            Assert.AreEqual("shan", ChineseNamePinyinConvert.GetChineseNamePinYin("单"));
            //单姓氏测试
            Assert.AreEqual("shan", ChineseNamePinyinConvert.GetChineseNamePinYin("单雄信").Substring(0, 4));

            //复姓测试-只有复姓
            Assert.AreEqual("huangfu", ChineseNamePinyinConvert.GetChineseNamePinYin("皇甫"));
            //复姓测试
            Assert.AreEqual("huangfu", ChineseNamePinyinConvert.GetChineseNamePinYin("皇甫鹏").Substring(0, 7));

            //非常规字符测试
            Assert.AreEqual(ChineseNamePinyinConvert.GetChineseNamePinYin("A"), "a");
            Assert.AreEqual(ChineseNamePinyinConvert.GetChineseNamePinYin("^"), "^");
        }
    }
}
