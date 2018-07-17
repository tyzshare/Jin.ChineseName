using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jin.ChineseName.Test
{
    [TestClass]
    public class UnitTest1
    {
        Class1 _class1;
        public UnitTest1()
        {
            _class1 = new Class1();
        }

        [TestMethod]
        public void TestMethod1()
        {
            //测试
            var list = new List<string>
            {
                _class1.GetNamePinYin("解雨花"),
                _class1.GetNamePinYin("皇甫"),
                _class1.GetNamePinYin("单雄信"),
                _class1.GetNamePinYin("尉迟恭"),
                _class1.GetNamePinYin("盖聂"),
                _class1.GetNamePinYin("吕归尘"),
                _class1.GetNamePinYin("A"),
                _class1.GetNamePinYin(""),
                _class1.GetNamePinYin("灱灲灳灴灷灸灹灺灻灼炀炁炂炃炄炅炆炇炈炋炌炍炏"),
                _class1.GetNamePinYin("《》（），。？：“”!@#$%^&*()_+-=[]\';/.,<>?"),
                _class1.GetNamePinYin("很感謝讀者們在「unwire 讀者交流會」中跟我們真情對話，當晚我們收集了很多寶貴意見，有讀者反映雖每天到訪 unwire 網站，但向朋友介紹時又難以說出 unwire 究竟是甚麼網站，是 3c？娛樂？八掛新聞？。其實由創立至今，unwire 對科技態度跟香港老牌 3C 網站有所不同，我們想把港人在科技生活的趣事，透過輕鬆易明的手法報導出來，令科技及電子產品都不是甚麼達人或機迷的專利，無論你是 OL 或新手，只要對科技有興趣的便可以跟我們 unwire 連起來，以下是我們 unwire 有別於香港傳統 3C 科技網的地方。")
            };
            Console.WriteLine();
        }
    }
}
