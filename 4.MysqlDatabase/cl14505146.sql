-- MySQL dump 10.13  Distrib 5.7.31, for Linux (x86_64)
--
-- Host: localhost    Database: cl14505146
-- ------------------------------------------------------
-- Server version	5.7.31

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: `cl14505146`
--

/*!40000 DROP DATABASE IF EXISTS `cl14505146`*/;

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `cl14505146` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;

USE `cl14505146`;

--
-- Table structure for table `address`
--

DROP TABLE IF EXISTS `address`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `address` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `addtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `address` varchar(200) NOT NULL COMMENT '地址',
  `name` varchar(200) DEFAULT NULL COMMENT '收货人',
  `phone` varchar(200) DEFAULT NULL COMMENT '电话',
  `isdefault` varchar(200) DEFAULT NULL COMMENT '是否默认地址',
  `userid` bigint(20) NOT NULL COMMENT '用户id',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6385205723339 DEFAULT CHARSET=utf8 COMMENT='地址';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `address`
--

LOCK TABLES `address` WRITE;
/*!40000 ALTER TABLE `address` DISABLE KEYS */;
INSERT INTO `address` VALUES (1,'2024-03-15 12:24:14','地址1','张三','19819881111','是',11),(2,'2024-03-15 12:24:14','地址2','李四','19819882222','是',12),(3,'2024-03-15 12:24:14','地址3','王五','19819883333','是',13),(4,'2024-03-15 12:24:14','地址4','赵六','19819884444','是',14),(5,'2024-03-15 12:24:14','地址5','孙七','19819885555','是',15),(6,'2024-03-15 12:24:14','地址6','周八','19819886666','是',16),(6385172790993,'2024-03-15 12:29:34','xxx','李李','13555555555','是',6384613135575),(6385205723338,'2024-03-15 12:41:42','11','11','13444444444','是',6384613206524);
/*!40000 ALTER TABLE `address` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cart`
--

DROP TABLE IF EXISTS `cart`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cart` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `addtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `tablename` varchar(200) DEFAULT NULL COMMENT '商品表名',
  `goodid` bigint(20) NOT NULL COMMENT '商品id',
  `goodname` varchar(200) DEFAULT NULL COMMENT '商品名称',
  `picture` longtext NOT NULL COMMENT '图片',
  `buynumber` int(11) DEFAULT NULL COMMENT '购买数量',
  `price` double DEFAULT NULL COMMENT '单价',
  `discountprice` double DEFAULT NULL COMMENT '折扣价',
  `userid` bigint(20) NOT NULL COMMENT '用户id',
  `shangjiazhanghao` varchar(200) DEFAULT NULL COMMENT '商户名称',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6385172668939 DEFAULT CHARSET=utf8 COMMENT='购物车';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cart`
--

LOCK TABLES `cart` WRITE;
/*!40000 ALTER TABLE `cart` DISABLE KEYS */;
INSERT INTO `cart` VALUES (6384926058956,'2024-03-15 12:30:39','shangpinxinxi',62,'商品名称2','file/shangpinxinxiShangpintupian2.jpg',1,99.9,NULL,6384613135575,'商家账号2'),(6385172668938,'2024-03-15 12:41:21','shangpinxinxi',61,'商品名称1','file/shangpinxinxiShangpintupian1.jpg',1,99.9,NULL,6384613206524,'商家账号1');
/*!40000 ALTER TABLE `cart` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `config`
--

DROP TABLE IF EXISTS `config`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `config` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `name` varchar(100) NOT NULL COMMENT '配置参数名称',
  `value` varchar(100) DEFAULT NULL COMMENT '配置参数值',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COMMENT='配置文件';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `config`
--

LOCK TABLES `config` WRITE;
/*!40000 ALTER TABLE `config` DISABLE KEYS */;
INSERT INTO `config` VALUES (1,'swiper1','file/swiperPicture1.jpg'),(2,'swiper2','file/swiperPicture2.jpg'),(3,'swiper3','file/swiperPicture3.jpg');
/*!40000 ALTER TABLE `config` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `discussshangpinxinxi`
--

DROP TABLE IF EXISTS `discussshangpinxinxi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `discussshangpinxinxi` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `addtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `refid` bigint(20) NOT NULL COMMENT '关联表id',
  `userid` bigint(20) NOT NULL COMMENT '用户id',
  `avatarurl` longtext COMMENT '头像',
  `nickname` varchar(200) DEFAULT NULL COMMENT '用户名',
  `content` longtext NOT NULL COMMENT '评论内容',
  `reply` longtext COMMENT '回复内容',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='商品信息评论表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `discussshangpinxinxi`
--

LOCK TABLES `discussshangpinxinxi` WRITE;
/*!40000 ALTER TABLE `discussshangpinxinxi` DISABLE KEYS */;
/*!40000 ALTER TABLE `discussshangpinxinxi` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `menu`
--

DROP TABLE IF EXISTS `menu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `menu` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `addtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `menujson` longtext COMMENT '菜单',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='菜单';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `menu`
--

LOCK TABLES `menu` WRITE;
/*!40000 ALTER TABLE `menu` DISABLE KEYS */;
INSERT INTO `menu` VALUES (1,'2024-03-15 12:24:14','[{\"backMenu\":[{\"child\":[{\"appFrontIcon\":\"cuIcon-goodsnew\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\"],\"menu\":\"轮播图\",\"menuJump\":\"列表\",\"tableName\":\"config\"}],\"fontClass\":\"icon-common34\",\"menu\":\"系统管理\",\"unicode\":\"&#xee85;\"},{\"child\":[{\"appFrontIcon\":\"cuIcon-paint\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\"],\"menu\":\"管理员\",\"menuJump\":\"列表\",\"tableName\":\"users\"},{\"appFrontIcon\":\"cuIcon-rank\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\"],\"menu\":\"用户\",\"menuJump\":\"列表\",\"tableName\":\"yonghu\"},{\"appFrontIcon\":\"cuIcon-explore\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\"],\"menu\":\"商家\",\"menuJump\":\"列表\",\"tableName\":\"shangjia\"}],\"fontClass\":\"icon-common40\",\"menu\":\"系统用户管理\",\"unicode\":\"&#xeebb;\"},{\"child\":[{\"appFrontIcon\":\"cuIcon-skin\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\"],\"menu\":\"商品分类\",\"menuJump\":\"列表\",\"tableName\":\"shangpinfenlei\"},{\"appFrontIcon\":\"cuIcon-vipcard\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\",\"查看评论\"],\"menu\":\"商品信息\",\"menuJump\":\"列表\",\"tableName\":\"shangpinxinxi\"}],\"fontClass\":\"icon-common32\",\"menu\":\"商品信息管理\",\"unicode\":\"&#xee66;\"},{\"child\":[{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\",\"年销额\",\"月销额\",\"日销统计\",\"品销额\",\"导出\",\"首页总数\",\"首页统计\"],\"menu\":\"商品订单\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\",\"年销额\",\"月销额\",\"日销统计\",\"品销额\",\"导出\",\"首页总数\",\"首页统计\"],\"menu\":\"未支付订单\",\"menuJump\":\"未支付\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\",\"年销额\",\"月销额\",\"日销统计\",\"品销额\",\"导出\",\"首页总数\",\"首页统计\",\"核销\"],\"menu\":\"已支付订单\",\"menuJump\":\"已支付\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\",\"年销额\",\"月销额\",\"日销统计\",\"品销额\",\"导出\",\"首页总数\",\"首页统计\"],\"menu\":\"已完成订单\",\"menuJump\":\"已完成\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\",\"年销额\",\"月销额\",\"日销统计\",\"品销额\",\"导出\",\"首页总数\",\"首页统计\"],\"menu\":\"已取消订单\",\"menuJump\":\"已取消\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\",\"年销额\",\"月销额\",\"日销统计\",\"品销额\",\"导出\",\"首页总数\",\"首页统计\"],\"menu\":\"已退款订单\",\"menuJump\":\"已退款\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\",\"年销额\",\"月销额\",\"日销统计\",\"品销额\",\"导出\",\"首页总数\",\"首页统计\"],\"menu\":\"已发货订单\",\"menuJump\":\"已发货\",\"tableName\":\"orders\"}],\"fontClass\":\"icon-common44\",\"menu\":\"订单管理\",\"unicode\":\"&#xef28;\"}],\"frontMenu\":[{\"child\":[{\"appFrontIcon\":\"cuIcon-explore\",\"buttons\":[\"查看\"],\"fontClass\":\"icon-common12\",\"menu\":\"商品信息\",\"menuJump\":\"列表\",\"tableName\":\"shangpinxinxi\",\"unicode\":\"&#xedf4;\"}],\"fontClass\":\"icon-common12\",\"menu\":\"商品信息管理\",\"unicode\":\"&#xedf4;\"},{\"child\":[{\"appFrontIcon\":\"cuIcon-keyboard\",\"buttons\":[\"查看\"],\"fontClass\":\"icon-common2\",\"menu\":\"购物车\",\"menuJump\":\"列表\",\"tableName\":\"cart\",\"unicode\":\"&#xeda4;\"}],\"fontClass\":\"icon-common2\",\"menu\":\"购物车管理\",\"unicode\":\"&#xeda4;\"}],\"hasBackLogin\":\"是\",\"hasBackRegister\":\"否\",\"hasFrontLogin\":\"否\",\"hasFrontRegister\":\"否\",\"roleName\":\"管理员\",\"tableName\":\"users\"},{\"backMenu\":[{\"child\":[{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"查看\"],\"menu\":\"商品订单\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"查看\"],\"menu\":\"未支付订单\",\"menuJump\":\"未支付\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"查看\"],\"menu\":\"已支付订单\",\"menuJump\":\"已支付\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"查看\"],\"menu\":\"已完成订单\",\"menuJump\":\"已完成\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"查看\"],\"menu\":\"已取消订单\",\"menuJump\":\"已取消\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"查看\"],\"menu\":\"已退款订单\",\"menuJump\":\"已退款\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"查看\",\"确认收货\",\"物流\"],\"menu\":\"已发货订单\",\"menuJump\":\"已发货\",\"tableName\":\"orders\"}],\"fontClass\":\"icon-common44\",\"menu\":\"订单管理\",\"unicode\":\"&#xef28;\"},{\"child\":[{\"appFrontIcon\":\"cuIcon-flashlightopen\",\"buttons\":[\"查看\"],\"menu\":\"我的收藏\",\"menuJump\":\"1\",\"tableName\":\"storeup\"}],\"fontClass\":\"icon-common32\",\"menu\":\"我的收藏管理\",\"unicode\":\"&#xee66;\"},{\"child\":[{\"appFrontIcon\":\"cuIcon-present\",\"buttons\":[\"查看\"],\"menu\":\"购物车\",\"menuJump\":\"列表\",\"tableName\":\"cart\"}],\"fontClass\":\"icon-common33\",\"menu\":\"购物车管理\",\"unicode\":\"&#xee6a;\"},{\"child\":[{\"appFrontIcon\":\"cuIcon-camera\",\"buttons\":[\"查看\"],\"menu\":\"地址\",\"menuJump\":\"列表\",\"tableName\":\"address\"}],\"fontClass\":\"icon-common8\",\"menu\":\"地址管理\",\"unicode\":\"&#xedb6;\"}],\"frontMenu\":[{\"child\":[{\"appFrontIcon\":\"cuIcon-explore\",\"buttons\":[\"查看\"],\"fontClass\":\"icon-common12\",\"menu\":\"商品信息\",\"menuJump\":\"列表\",\"tableName\":\"shangpinxinxi\",\"unicode\":\"&#xedf4;\"}],\"fontClass\":\"icon-common12\",\"menu\":\"商品信息管理\",\"unicode\":\"&#xedf4;\"},{\"child\":[{\"appFrontIcon\":\"cuIcon-keyboard\",\"buttons\":[\"查看\"],\"fontClass\":\"icon-common2\",\"menu\":\"购物车\",\"menuJump\":\"列表\",\"tableName\":\"cart\",\"unicode\":\"&#xeda4;\"}],\"fontClass\":\"icon-common2\",\"menu\":\"购物车管理\",\"unicode\":\"&#xeda4;\"}],\"hasBackLogin\":\"否\",\"hasBackRegister\":\"否\",\"hasFrontLogin\":\"是\",\"hasFrontRegister\":\"是\",\"roleName\":\"用户\",\"tableName\":\"yonghu\"},{\"backMenu\":[{\"child\":[{\"appFrontIcon\":\"cuIcon-vipcard\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\",\"查看评论\"],\"menu\":\"商品信息\",\"menuJump\":\"列表\",\"tableName\":\"shangpinxinxi\"}],\"fontClass\":\"icon-common32\",\"menu\":\"商品信息管理\",\"unicode\":\"&#xee66;\"},{\"child\":[{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\",\"年销额\",\"月销额\",\"日销统计\",\"品销额\",\"导出\",\"首页总数\",\"首页统计\"],\"menu\":\"商品订单\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\",\"年销额\",\"月销额\",\"日销统计\",\"品销额\",\"导出\",\"首页总数\",\"首页统计\"],\"menu\":\"未支付订单\",\"menuJump\":\"未支付\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\",\"年销额\",\"月销额\",\"日销统计\",\"品销额\",\"导出\",\"首页总数\",\"首页统计\",\"发货\",\"物流\",\"核销\"],\"menu\":\"已支付订单\",\"menuJump\":\"已支付\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\",\"年销额\",\"月销额\",\"日销统计\",\"品销额\",\"导出\",\"首页总数\",\"首页统计\"],\"menu\":\"已完成订单\",\"menuJump\":\"已完成\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\",\"年销额\",\"月销额\",\"日销统计\",\"品销额\",\"导出\",\"首页总数\",\"首页统计\"],\"menu\":\"已取消订单\",\"menuJump\":\"已取消\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\",\"年销额\",\"月销额\",\"日销统计\",\"品销额\",\"导出\",\"首页总数\",\"首页统计\"],\"menu\":\"已退款订单\",\"menuJump\":\"已退款\",\"tableName\":\"orders\"},{\"appFrontIcon\":\"cuIcon-addressbook\",\"buttons\":[\"新增\",\"查看\",\"修改\",\"删除\",\"年销额\",\"月销额\",\"日销统计\",\"品销额\",\"导出\",\"首页总数\",\"首页统计\"],\"menu\":\"已发货订单\",\"menuJump\":\"已发货\",\"tableName\":\"orders\"}],\"fontClass\":\"icon-common44\",\"menu\":\"订单管理\",\"unicode\":\"&#xef28;\"}],\"frontMenu\":[{\"child\":[{\"appFrontIcon\":\"cuIcon-explore\",\"buttons\":[\"查看\"],\"fontClass\":\"icon-common12\",\"menu\":\"商品信息\",\"menuJump\":\"列表\",\"tableName\":\"shangpinxinxi\",\"unicode\":\"&#xedf4;\"}],\"fontClass\":\"icon-common12\",\"menu\":\"商品信息管理\",\"unicode\":\"&#xedf4;\"},{\"child\":[{\"appFrontIcon\":\"cuIcon-keyboard\",\"buttons\":[\"查看\"],\"fontClass\":\"icon-common2\",\"menu\":\"购物车\",\"menuJump\":\"列表\",\"tableName\":\"cart\",\"unicode\":\"&#xeda4;\"}],\"fontClass\":\"icon-common2\",\"menu\":\"购物车管理\",\"unicode\":\"&#xeda4;\"}],\"hasBackLogin\":\"是\",\"hasBackRegister\":\"是\",\"hasFrontLogin\":\"否\",\"hasFrontRegister\":\"否\",\"roleName\":\"商家\",\"tableName\":\"shangjia\"}]');
/*!40000 ALTER TABLE `menu` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orders` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `addtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `orderid` varchar(200) NOT NULL COMMENT '订单编号',
  `tablename` varchar(200) DEFAULT NULL COMMENT '商品表名',
  `goodid` bigint(20) NOT NULL COMMENT '商品id',
  `goodname` varchar(200) DEFAULT NULL COMMENT '商品名称',
  `picture` longtext NOT NULL COMMENT '图片',
  `buynumber` int(11) DEFAULT NULL COMMENT '购买数量',
  `price` double DEFAULT NULL COMMENT '单价',
  `discountprice` double DEFAULT NULL COMMENT '折扣价',
  `total` double DEFAULT NULL COMMENT '总价',
  `discounttotal` double DEFAULT NULL COMMENT '折扣总价格',
  `type` varchar(200) DEFAULT NULL COMMENT '支付类型',
  `status` varchar(200) DEFAULT NULL COMMENT '订单状态',
  `address` varchar(200) DEFAULT NULL COMMENT '地址',
  `tel` varchar(200) DEFAULT NULL COMMENT '电话',
  `consignee` varchar(200) DEFAULT NULL COMMENT '收货人',
  `remark` varchar(200) DEFAULT NULL COMMENT '备注',
  `logistics` longtext COMMENT '物流',
  `userid` bigint(20) NOT NULL COMMENT '用户id',
  `shangjiazhanghao` varchar(200) DEFAULT NULL COMMENT '商户名称',
  PRIMARY KEY (`id`),
  UNIQUE KEY `orderid` (`orderid`)
) ENGINE=InnoDB AUTO_INCREMENT=6384876109819 DEFAULT CHARSET=utf8 COMMENT='商品订单';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (6384722598830,'2024-03-15 12:41:48','2024315204232439','shangpinxinxi',61,'商品名称1','file/shangpinxinxiShangpintupian1.jpg',1,99.9,99.9,99.9,99.9,'1','已发货','11','13444444444','11','','<p>11111111111</p>',6384613206524,'商家账号1'),(6384876109818,'2024-02-22 12:29:40','2024315203024508','shangpinxinxi',62,'商品名称2','file/shangpinxinxiShangpintupian2.jpg',1,99.9,99.9,99.9,99.9,'1','已支付','xxx','13555555555','李李','','',6384613135575,'商家账号2');
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shangjia`
--

DROP TABLE IF EXISTS `shangjia`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `shangjia` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `addtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `shangjiazhanghao` varchar(200) NOT NULL COMMENT '商家账号',
  `mima` varchar(200) NOT NULL COMMENT '密码',
  `shangjiamingcheng` varchar(200) NOT NULL COMMENT '商家名称',
  `touxiang` longtext COMMENT '头像',
  `lianxiren` varchar(200) DEFAULT NULL COMMENT '联系人',
  `lianxifangshi` varchar(200) DEFAULT NULL COMMENT '联系方式',
  PRIMARY KEY (`id`),
  UNIQUE KEY `shangjiazhanghao` (`shangjiazhanghao`)
) ENGINE=InnoDB AUTO_INCREMENT=6384613150972 DEFAULT CHARSET=utf8 COMMENT='商家';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shangjia`
--

LOCK TABLES `shangjia` WRITE;
/*!40000 ALTER TABLE `shangjia` DISABLE KEYS */;
INSERT INTO `shangjia` VALUES (41,'2024-03-15 12:24:14','商家账号1','e10adc3949ba59abbe56e057f20f883e','商家名称1','file/shangjiaTouxiang1.jpg','联系人1','19819881111'),(42,'2024-03-15 12:24:14','商家账号2','e10adc3949ba59abbe56e057f20f883e','商家名称2','file/shangjiaTouxiang2.jpg','联系人2','19819881112'),(43,'2024-03-15 12:24:14','商家账号3','e10adc3949ba59abbe56e057f20f883e','商家名称3','file/shangjiaTouxiang3.jpg','联系人3','19819881113'),(44,'2024-03-15 12:24:14','商家账号4','e10adc3949ba59abbe56e057f20f883e','商家名称4','file/shangjiaTouxiang4.jpg','联系人4','19819881114'),(45,'2024-03-15 12:24:14','商家账号5','e10adc3949ba59abbe56e057f20f883e','商家名称5','file/shangjiaTouxiang5.jpg','联系人5','19819881115'),(46,'2024-03-15 12:24:14','商家账号6','e10adc3949ba59abbe56e057f20f883e','商家名称6','file/shangjiaTouxiang6.jpg','联系人6','19819881116'),(6384613150971,'2024-03-15 12:31:50','123','202CB962AC59075B964B07152D234B70','张张','file/1710505899192.jpeg','张张','13555555555');
/*!40000 ALTER TABLE `shangjia` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shangpinfenlei`
--

DROP TABLE IF EXISTS `shangpinfenlei`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `shangpinfenlei` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `addtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `shangpinfenlei` varchar(200) DEFAULT NULL COMMENT '商品分类',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=utf8 COMMENT='商品分类';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shangpinfenlei`
--

LOCK TABLES `shangpinfenlei` WRITE;
/*!40000 ALTER TABLE `shangpinfenlei` DISABLE KEYS */;
INSERT INTO `shangpinfenlei` VALUES (51,'2024-03-15 12:24:14','商品分类1'),(52,'2024-03-15 12:24:14','商品分类2'),(53,'2024-03-15 12:24:14','商品分类3'),(54,'2024-03-15 12:24:14','商品分类4'),(55,'2024-03-15 12:24:14','商品分类5'),(56,'2024-03-15 12:24:14','水果');
/*!40000 ALTER TABLE `shangpinfenlei` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shangpinxinxi`
--

DROP TABLE IF EXISTS `shangpinxinxi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `shangpinxinxi` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `addtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `shangpinmingcheng` varchar(200) NOT NULL COMMENT '商品名称',
  `shangpinfenlei` varchar(200) DEFAULT NULL COMMENT '商品分类',
  `shangpintupian` longtext COMMENT '商品图片',
  `chandi` varchar(200) DEFAULT NULL COMMENT '产地',
  `guige` varchar(200) DEFAULT NULL COMMENT '规格',
  `shangpinxiangqing` longtext COMMENT '商品详情',
  `storeupnum` int(11) DEFAULT NULL COMMENT '收藏数量',
  `clicknum` int(11) DEFAULT NULL COMMENT '点击次数',
  `onelimittimes` int(11) DEFAULT NULL COMMENT '单限',
  `alllimittimes` int(11) DEFAULT NULL COMMENT '库存',
  `price` double DEFAULT NULL COMMENT '价格',
  `shangjiazhanghao` varchar(200) DEFAULT NULL COMMENT '商家账号',
  `shangjiamingcheng` varchar(200) DEFAULT NULL COMMENT '商家名称',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6384939652458 DEFAULT CHARSET=utf8 COMMENT='商品信息';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shangpinxinxi`
--

LOCK TABLES `shangpinxinxi` WRITE;
/*!40000 ALTER TABLE `shangpinxinxi` DISABLE KEYS */;
INSERT INTO `shangpinxinxi` VALUES (61,'2024-03-15 12:24:14','商品名称1','商品分类1','file/shangpinxinxiShangpintupian1.jpg,file/shangpinxinxiShangpintupian2.jpg,file/shangpinxinxiShangpintupian3.jpg','产地1','规格1','商品详情1',2,9,1,98,99.9,'商家账号1','商家名称1'),(62,'2024-03-15 12:24:14','商品名称2','商品分类2','file/shangpinxinxiShangpintupian2.jpg,file/shangpinxinxiShangpintupian3.jpg,file/shangpinxinxiShangpintupian4.jpg','产地2','规格2','商品详情2',3,7,2,98,99.9,'商家账号2','商家名称2'),(63,'2024-03-15 12:24:14','商品名称3','商品分类3','file/shangpinxinxiShangpintupian3.jpg,file/shangpinxinxiShangpintupian4.jpg,file/shangpinxinxiShangpintupian5.jpg','产地3','规格3','商品详情3',3,3,3,99,99.9,'商家账号3','商家名称3'),(64,'2024-03-15 12:24:14','商品名称4','商品分类4','file/shangpinxinxiShangpintupian4.jpg,file/shangpinxinxiShangpintupian5.jpg,file/shangpinxinxiShangpintupian6.jpg','产地4','规格4','商品详情4',4,4,4,99,99.9,'商家账号4','商家名称4'),(65,'2024-03-15 12:24:14','商品名称5','商品分类5','file/shangpinxinxiShangpintupian5.jpg,file/shangpinxinxiShangpintupian6.jpg,file/shangpinxinxiShangpintupian7.jpg','产地5','规格5','商品详情5',5,5,5,99,99.9,'商家账号5','商家名称5'),(66,'2024-03-15 12:24:14','商品名称6','商品分类6','file/shangpinxinxiShangpintupian6.jpg,file/shangpinxinxiShangpintupian7.jpg,file/shangpinxinxiShangpintupian8.jpg','产地6','规格6','商品详情6',6,6,6,99,99.9,'商家账号6','商家名称6'),(6384939652457,'2024-03-15 12:32:40','名称1','商品分类1','file/1710505941100.jpg','111','1','<p>111111111111</p>',0,1,1,11,700,'123','张张');
/*!40000 ALTER TABLE `shangpinxinxi` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `storeup`
--

DROP TABLE IF EXISTS `storeup`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `storeup` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `addtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `refid` bigint(20) DEFAULT NULL COMMENT 'refid',
  `tablename` varchar(200) DEFAULT NULL COMMENT '表名',
  `name` varchar(200) NOT NULL COMMENT '名称',
  `picture` longtext NOT NULL COMMENT '图片',
  `type` varchar(200) DEFAULT NULL COMMENT '类型(1:收藏,21:赞,22:踩,31:竞拍参与,41:关注)',
  `inteltype` varchar(200) DEFAULT NULL COMMENT '推荐类型',
  `remark` varchar(200) DEFAULT NULL COMMENT '备注',
  `userid` bigint(20) NOT NULL COMMENT '用户id',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6385522282429 DEFAULT CHARSET=utf8 COMMENT='我的收藏';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `storeup`
--

LOCK TABLES `storeup` WRITE;
/*!40000 ALTER TABLE `storeup` DISABLE KEYS */;
INSERT INTO `storeup` VALUES (6384891257687,'2024-03-15 12:41:19',61,'shangpinxinxi','商品名称1','file/shangpinxinxiShangpintupian1.jpg','1',NULL,NULL,6384613206524),(6385522282428,'2024-03-15 12:30:19',62,'shangpinxinxi','商品名称2','file/shangpinxinxiShangpintupian2.jpg','1',NULL,NULL,6384613135575);
/*!40000 ALTER TABLE `storeup` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `addtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `username` varchar(200) NOT NULL COMMENT '用户名',
  `password` varchar(200) NOT NULL COMMENT '密码',
  `role` varchar(200) DEFAULT NULL COMMENT '角色',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='管理员';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'2024-03-15 12:24:14','admin','admin','管理员');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `yonghu`
--

DROP TABLE IF EXISTS `yonghu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `yonghu` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `addtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `yonghuzhanghao` varchar(200) NOT NULL COMMENT '用户账号',
  `yonghumima` varchar(200) NOT NULL COMMENT '用户密码',
  `yonghuxingming` varchar(200) NOT NULL COMMENT '用户姓名',
  `touxiang` longtext COMMENT '头像',
  `xingbie` varchar(200) DEFAULT NULL COMMENT '性别',
  `shoujihaoma` varchar(200) DEFAULT NULL COMMENT '手机号码',
  `youxiang` varchar(200) DEFAULT NULL COMMENT '邮箱',
  `money` double DEFAULT '0' COMMENT '余额',
  PRIMARY KEY (`id`),
  UNIQUE KEY `yonghuzhanghao` (`yonghuzhanghao`)
) ENGINE=InnoDB AUTO_INCREMENT=6384613206525 DEFAULT CHARSET=utf8 COMMENT='用户';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `yonghu`
--

LOCK TABLES `yonghu` WRITE;
/*!40000 ALTER TABLE `yonghu` DISABLE KEYS */;
INSERT INTO `yonghu` VALUES (31,'2024-03-15 12:24:14','用户账号1','e10adc3949ba59abbe56e057f20f883e','用户姓名1','file/yonghuTouxiang1.jpg','男','19819881111','102020001@qq.com',200),(32,'2024-03-15 12:24:14','用户账号2','e10adc3949ba59abbe56e057f20f883e','用户姓名2','file/yonghuTouxiang2.jpg','男','19819881112','102020002@qq.com',200),(33,'2024-03-15 12:24:14','用户账号3','e10adc3949ba59abbe56e057f20f883e','用户姓名3','file/yonghuTouxiang3.jpg','男','19819881113','102020003@qq.com',200),(34,'2024-03-15 12:24:14','用户账号4','e10adc3949ba59abbe56e057f20f883e','用户姓名4','file/yonghuTouxiang4.jpg','男','19819881114','102020004@qq.com',200),(35,'2024-03-15 12:24:14','用户账号5','e10adc3949ba59abbe56e057f20f883e','用户姓名5','file/yonghuTouxiang5.jpg','男','19819881115','102020005@qq.com',200),(36,'2024-03-15 12:24:14','用户账号6','e10adc3949ba59abbe56e057f20f883e','用户姓名6','file/yonghuTouxiang6.jpg','男','19819881116','102020006@qq.com',200),(6384613135575,'2024-03-15 12:29:16','123','202CB962AC59075B964B07152D234B70','李李','file/1710505740084.jpeg','女','13555555555','215955555@qq.com',300.1),(6384613206524,'2024-03-15 12:41:05','111','698D51A19D8A121CE581499D7B701668','呵呵','file/1710506452987.jpeg','女','13555555555','214555555@qq.com',600.1);
/*!40000 ALTER TABLE `yonghu` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-03-15 23:01:25
