using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Jin.ChineseName
{
    public class Class1
    {
        /// <summary>
        /// 获取姓名拼音
        /// </summary>
        /// <param name="name">姓名</param>
        /// <returns>转换后的拼音</returns>
        public string GetNamePinYin(string name)
        {
            //初始化字典集
            var dictionary = ReadPinYinConfiguration();
            var namePinYin = new StringBuilder();
            //姓名拆成单字
            var arr = name.ToCharArray();
            //2个或以上字节
            if (arr.Count() >= 2)
            {
                var pinyin = string.Empty;
                var num = 0;
                //复姓命中
                if (string.IsNullOrWhiteSpace(pinyin))
                {
                    var xing = arr[0].ToString() + arr[1].ToString();
                    //判断key是否存在
                    if (dictionary.ContainsKey(xing))
                    {
                        pinyin = dictionary[xing];
                        namePinYin.Append(pinyin);
                        num = 2;
                    }
                }
                //单姓命中
                if (string.IsNullOrWhiteSpace(pinyin))
                {
                    var xing = arr[0].ToString();
                    //判断key是否存在
                    if (dictionary.ContainsKey(xing))
                    {
                        pinyin = dictionary[xing];
                        namePinYin.Append(pinyin);
                        num = 1;
                    }
                }
                //名命中
                for (var i = num; i < name.Count(); i++)
                {
                    //常规拼音转换
                    namePinYin.Append(GetPinyin(arr[i].ToString()));
                }
            }
            //单个字节
            if (arr.Count() <= 1)
            {
                //常规拼音转换
                namePinYin.Append(GetPinyin(name));
            }
            //返回
            return namePinYin.ToString();
        }

        /// <summary>
        /// 读取拼音配置项
        /// </summary>
        /// <returns>字典集</returns>
        public Dictionary<string, string> ReadPinYinConfiguration()
        {
            //文件路径
            DirectoryInfo rootDir = Directory.GetParent(Environment.CurrentDirectory);
            var root = rootDir.Parent.Parent.FullName;
            var filePath = root + @"\Jin.ChineseName\File\baijiaxing.txt";
            //定义字典集
            var dictionary = new Dictionary<string, string>();
            //读取文件
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    var line = string.Empty;
                    while ((line = sr.ReadLine()) != null)
                    {
                        //处理音调
                        line = ReplaceString(line);
                        //处理字典集
                        var key = new StringBuilder();
                        var value = new StringBuilder();
                        foreach (var item in line)
                        {
                            if (item <= 128)
                            {
                                //拼音，加入value
                                if (!char.IsWhiteSpace(item))
                                {
                                    value.Append(item);
                                }
                            }
                            else
                            {
                                //汉字，加入key
                                key.Append(item);
                            }
                        }
                        dictionary.Add(key.ToString(), value.ToString());
                    }
                }
            }
            return dictionary;
        }

        /// <summary>
        /// 替换声调
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>处理后的字符串</returns>
        public string ReplaceString(string str)
        {
            //a
            str = str.Replace("ā", "a");
            str = str.Replace("á", "a");
            str = str.Replace("ǎ", "a");
            str = str.Replace("à", "a");
            //e
            str = str.Replace("ē", "e");
            str = str.Replace("é", "e");
            str = str.Replace("ě", "e");
            str = str.Replace("è", "e");
            //i
            str = str.Replace("ī", "i");
            str = str.Replace("í", "i");
            str = str.Replace("ǐ", "i");
            str = str.Replace("ì", "i");
            //o
            str = str.Replace("ō", "o");
            str = str.Replace("ó", "o");
            str = str.Replace("ǒ", "o");
            str = str.Replace("ò", "o");
            //u
            str = str.Replace("ū", "u");
            str = str.Replace("ú", "u");
            str = str.Replace("ǔ", "u");
            str = str.Replace("ù", "u");
            //v
            str = str.Replace("ǖ", "v");
            str = str.Replace("ǘ", "v");
            str = str.Replace("ǚ", "v");
            str = str.Replace("ǜ", "v");

            return str;
        }

        /// <summary>   
        /// 汉字转化为拼音  
        /// </summary>   
        /// <param name="str">汉字</param>   
        /// <returns>全拼</returns>   
        public string GetPinyin(string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, t.Length - 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r.ToLower();
        }
    }

}
