# 中文名称转拼音

# 安装
Install-Package Jin.ChineseName
# 中文姓名转拼音
var result = ChineseNamePinyinConvert.GetChineseNamePinYin("单雄信");
# 返回结果：
shanxiongxin
# 说明：
同时支持net45和netcore，支持百家姓多音字


---


# 优化中文名称转拼音
1. 中文名称转拼音，通过百家姓可以解决姓的问题，但是由于多音字的问题，名的匹配度不高，在没有词频的情况下，维护一份多音字字典也不失为一种解决方案；
2. 如何通过不发布应用程序而快速实现更新多音字字典，需要做到与应用程序解耦，利用数据持久化来做；
3. 减少数据库查询次数，利用缓存来优化，但是如果新增，修改，删除多音字，需要记得清楚缓存；

# 创建多音字表

```
SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for duoyinzidictionary
-- ----------------------------
DROP TABLE IF EXISTS `duoyinzidictionary`;
CREATE TABLE `duoyinzidictionary` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `WordKey` varchar(100) NOT NULL DEFAULT '' COMMENT '键(多音字)',
  `WordValue` varchar(100) NOT NULL DEFAULT '' COMMENT '值(多音字优先命中的读音)',
  `CreateAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `idx_wordkey` (`WordKey`)
) ENGINE=InnoDB AUTO_INCREMENT=103 DEFAULT CHARSET=utf8mb4 COMMENT='多音字优先命中字典';

-- ----------------------------
-- Records of duoyinzidictionary
-- ----------------------------
INSERT INTO `duoyinzidictionary` VALUES ('77', '石', 'shi', '2019-02-13 16:27:06');
INSERT INTO `duoyinzidictionary` VALUES ('79', '叶', 'ye', '2019-02-13 16:27:06');
INSERT INTO `duoyinzidictionary` VALUES ('81', '齐', 'qi', '2019-02-13 16:27:06');
INSERT INTO `duoyinzidictionary` VALUES ('83', '若', 'ruo', '2019-02-13 16:27:06');
INSERT INTO `duoyinzidictionary` VALUES ('85', '杉', 'shan', '2019-02-13 16:27:06');
INSERT INTO `duoyinzidictionary` VALUES ('87', '思', 'si', '2019-02-13 16:27:06');
INSERT INTO `duoyinzidictionary` VALUES ('89', '家', 'jia', '2019-02-13 16:27:06');
INSERT INTO `duoyinzidictionary` VALUES ('91', '洋', 'yang', '2019-02-13 16:27:06');
INSERT INTO `duoyinzidictionary` VALUES ('93', '育', 'yu', '2019-02-13 16:27:06');
INSERT INTO `duoyinzidictionary` VALUES ('95', '奇', 'qi', '2019-02-13 16:27:06');
INSERT INTO `duoyinzidictionary` VALUES ('97', '靓', 'liang', '2019-02-13 16:27:06');
INSERT INTO `duoyinzidictionary` VALUES ('99', '屹', 'yi', '2019-02-13 16:27:06');
INSERT INTO `duoyinzidictionary` VALUES ('101', '伯', 'bo', '2019-02-13 16:27:06');
```

# 中文姓名转拼音
var result= ChineseNamePinyinConvert.GetChineseNamePinYin("石家屹", "数据库连接字符串"【可空】,"redis连接字符串"【可空】, "缓存前缀"【可空】,"过期时间"【可空】)
# 返回结果：
shijiayi