# 中文名称转拼音

## 安装
`Install-Package Jin.ChineseName`
## 调用方法
```
中文姓名转拼音
var result = ChineseNamePinyinConvert.GetChineseNamePinYin("单雄信");
### 返回结果：
shanxiongxin
```
## 说明：
同时支持net45和netcore，支持百家姓多音字

---

# 优化中文名称转拼音
1. 中文名称转拼音，通过百家姓可以解决姓的问题，但是由于多音字的问题，名的匹配度不高，在没有词频的情况下，维护一份多音字字典也不失为一种解决方案；
2. 如何通过不发布应用程序而快速实现更新多音字字典，需要做到与应用程序解耦，利用数据持久化来做；
3. 减少数据库查询次数，利用缓存来优化，但是如果新增，修改，删除多音字，需要记得清楚缓存；

## 创建多音字表

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
INSERT INTO `duoyinzidictionary` VALUES ('199', '露', 'lu', '2019-03-29 12:50:52');
INSERT INTO `duoyinzidictionary` VALUES ('201', '敦', 'dun', '2019-03-29 13:05:45');
INSERT INTO `duoyinzidictionary` VALUES ('203', '强', 'qiang', '2019-03-29 13:07:54');
INSERT INTO `duoyinzidictionary` VALUES ('205', '红', 'hong', '2019-03-29 13:08:54');
INSERT INTO `duoyinzidictionary` VALUES ('207', '乾', 'qian', '2019-03-29 13:11:43');
INSERT INTO `duoyinzidictionary` VALUES ('209', '无', 'wu', '2019-03-29 13:15:10');
INSERT INTO `duoyinzidictionary` VALUES ('211', '觉', 'jue', '2019-03-29 13:16:45');
INSERT INTO `duoyinzidictionary` VALUES ('213', '万', 'wan', '2019-03-29 13:20:00');
INSERT INTO `duoyinzidictionary` VALUES ('215', '炅', 'jiong', '2019-03-29 13:24:22');
INSERT INTO `duoyinzidictionary` VALUES ('217', '括', 'kuo', '2019-03-29 13:37:52');
INSERT INTO `duoyinzidictionary` VALUES ('219', '招', 'zhao', '2019-03-29 13:47:23');
INSERT INTO `duoyinzidictionary` VALUES ('220', '厂', 'chang', '2019-10-21 20:45:55');
INSERT INTO `duoyinzidictionary` VALUES ('221', '骞', 'qian', '2019-10-21 20:55:46');
INSERT INTO `duoyinzidictionary` VALUES ('222', '卜', 'bo', '2019-10-21 20:57:54');
INSERT INTO `duoyinzidictionary` VALUES ('223', '区', 'qu', '2019-10-21 20:58:08');
INSERT INTO `duoyinzidictionary` VALUES ('224', '爪', 'zhua', '2019-10-21 20:58:33');
INSERT INTO `duoyinzidictionary` VALUES ('225', '尺', 'chi', '2019-10-21 20:58:53');
INSERT INTO `duoyinzidictionary` VALUES ('226', '轧', 'ya', '2019-10-21 20:59:15');
INSERT INTO `duoyinzidictionary` VALUES ('227', '且', 'qie', '2019-10-21 20:59:26');
INSERT INTO `duoyinzidictionary` VALUES ('228', '乐', 'le', '2019-10-21 20:59:39');
INSERT INTO `duoyinzidictionary` VALUES ('229', '句', 'ju', '2019-10-21 21:00:19');
INSERT INTO `duoyinzidictionary` VALUES ('230', '鸟', 'niao', '2019-10-21 21:00:34');
INSERT INTO `duoyinzidictionary` VALUES ('231', '召', 'zhao', '2019-10-21 21:00:45');
INSERT INTO `duoyinzidictionary` VALUES ('232', '页', 'ye', '2019-10-21 21:01:04');
INSERT INTO `duoyinzidictionary` VALUES ('233', '夹', 'jia', '2019-10-21 21:01:25');
INSERT INTO `duoyinzidictionary` VALUES ('234', '合', 'he', '2019-10-21 21:02:05');
INSERT INTO `duoyinzidictionary` VALUES ('235', '戏', 'xi', '2019-10-21 21:02:18');
INSERT INTO `duoyinzidictionary` VALUES ('236', '约', 'yue', '2019-10-21 21:02:30');
INSERT INTO `duoyinzidictionary` VALUES ('237', '弄', 'nong', '2019-10-21 21:02:47');
INSERT INTO `duoyinzidictionary` VALUES ('238', '折', 'zhe', '2019-10-21 21:02:58');
INSERT INTO `duoyinzidictionary` VALUES ('239', '拆', 'chai', '2019-10-21 21:03:10');
INSERT INTO `duoyinzidictionary` VALUES ('240', '其', 'qi', '2019-10-21 21:03:25');
INSERT INTO `duoyinzidictionary` VALUES ('241', '茄', 'qie', '2019-10-21 21:03:39');
INSERT INTO `duoyinzidictionary` VALUES ('242', '迫', 'po', '2019-10-21 21:03:55');
INSERT INTO `duoyinzidictionary` VALUES ('243', '底', 'di', '2019-10-21 21:04:06');
INSERT INTO `duoyinzidictionary` VALUES ('244', '浅', 'qian', '2019-10-21 21:04:17');
INSERT INTO `duoyinzidictionary` VALUES ('245', '拾', 'shi', '2019-10-21 21:04:28');
INSERT INTO `duoyinzidictionary` VALUES ('246', '巷', 'xiang', '2019-10-21 21:04:39');
INSERT INTO `duoyinzidictionary` VALUES ('247', '哑', 'ya', '2019-10-21 21:04:49');
INSERT INTO `duoyinzidictionary` VALUES ('248', '虾', 'xia', '2019-10-21 21:04:58');
INSERT INTO `duoyinzidictionary` VALUES ('249', '咳', 'ke', '2019-10-21 21:05:25');
INSERT INTO `duoyinzidictionary` VALUES ('250', '选', 'xuan', '2019-10-21 21:05:53');
INSERT INTO `duoyinzidictionary` VALUES ('251', '适', 'shi', '2019-10-21 21:06:02');
INSERT INTO `duoyinzidictionary` VALUES ('252', '种', 'zhong', '2019-10-21 21:06:15');
INSERT INTO `duoyinzidictionary` VALUES ('253', '炮', 'pao', '2019-10-21 21:06:40');
INSERT INTO `duoyinzidictionary` VALUES ('254', '说', 'shuo', '2019-10-21 21:06:50');
INSERT INTO `duoyinzidictionary` VALUES ('255', '络', 'luo', '2019-10-21 21:07:02');
INSERT INTO `duoyinzidictionary` VALUES ('256', '校', 'xiao', '2019-10-21 21:07:13');
INSERT INTO `duoyinzidictionary` VALUES ('257', '秘', 'mi', '2019-10-21 21:07:27');
INSERT INTO `duoyinzidictionary` VALUES ('258', '倘', 'tang', '2019-10-21 21:07:40');
INSERT INTO `duoyinzidictionary` VALUES ('259', '胳', 'ge', '2019-10-21 21:07:54');
INSERT INTO `duoyinzidictionary` VALUES ('260', '衰', 'shuai', '2019-10-21 21:08:15');
INSERT INTO `duoyinzidictionary` VALUES ('261', '剥', 'bo', '2019-10-21 21:08:28');
INSERT INTO `duoyinzidictionary` VALUES ('262', '能', 'neng', '2019-10-21 21:08:42');
INSERT INTO `duoyinzidictionary` VALUES ('263', '著', 'zhu', '2019-10-21 21:08:52');
INSERT INTO `duoyinzidictionary` VALUES ('264', '圈', 'quan', '2019-10-21 21:09:03');
INSERT INTO `duoyinzidictionary` VALUES ('265', '颈', 'jing', '2019-10-21 21:09:14');
INSERT INTO `duoyinzidictionary` VALUES ('266', '骑', 'qi', '2019-10-21 21:09:25');
INSERT INTO `duoyinzidictionary` VALUES ('267', '塔', 'ta', '2019-10-21 21:09:41');
INSERT INTO `duoyinzidictionary` VALUES ('268', '提', 'ti', '2019-10-21 21:09:55');
INSERT INTO `duoyinzidictionary` VALUES ('269', '期', 'qi', '2019-10-21 21:10:05');
INSERT INTO `duoyinzidictionary` VALUES ('270', '殖', 'zhi', '2019-10-21 21:10:14');
INSERT INTO `duoyinzidictionary` VALUES ('271', '遗', 'yi', '2019-10-21 21:10:24');
INSERT INTO `duoyinzidictionary` VALUES ('272', '粥', 'zhou', '2019-10-21 21:10:36');
INSERT INTO `duoyinzidictionary` VALUES ('273', '献', 'xian', '2019-10-21 21:12:23');
INSERT INTO `duoyinzidictionary` VALUES ('274', '解', 'jie', '2019-10-21 21:12:35');
INSERT INTO `duoyinzidictionary` VALUES ('275', '辟', 'pi', '2019-10-21 21:12:46');
INSERT INTO `duoyinzidictionary` VALUES ('276', '裳', 'shang', '2019-10-21 21:12:56');
INSERT INTO `duoyinzidictionary` VALUES ('277', '魄', 'po', '2019-10-21 21:13:05');
INSERT INTO `duoyinzidictionary` VALUES ('278', '摩', 'mo', '2019-10-21 21:13:17');
INSERT INTO `duoyinzidictionary` VALUES ('279', '臂', 'bi', '2019-10-21 21:13:27');
INSERT INTO `duoyinzidictionary` VALUES ('280', '蹲', 'dun', '2019-10-21 21:13:36');
INSERT INTO `duoyinzidictionary` VALUES ('281', '许', 'xu', '2019-11-26 19:24:01');
```
## 调用方法
```
var result= ChineseNamePinyinConvert.GetChineseNamePinYin("石家屹", "数据库连接字符串"【可空】,"redis连接字符串"【可空】, "缓存前缀"【可空】,"过期时间"【可空】)
### 返回结果：
shijiayi
```


