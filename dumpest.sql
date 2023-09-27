-- MariaDB dump 10.19  Distrib 10.4.27-MariaDB, for Win64 (AMD64)
--
-- Host: localhost    Database: client
-- ------------------------------------------------------
-- Server version	10.4.27-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `pay_history`
--

DROP TABLE IF EXISTS `pay_history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pay_history` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` bigint(20) NOT NULL,
  `price` float DEFAULT NULL,
  `currency` varchar(5) NOT NULL,
  `payment_method` varchar(9) DEFAULT NULL,
  `payment_type` varchar(8) DEFAULT NULL,
  `status_pay` tinyint(4) DEFAULT NULL,
  `paid_at` datetime DEFAULT NULL,
  `create_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `pay_history_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=109 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pay_history`
--

LOCK TABLES `pay_history` WRITE;
/*!40000 ALTER TABLE `pay_history` DISABLE KEYS */;
INSERT INTO `pay_history` VALUES (62,1,56.0932,'USD','balance','output',1,'2023-03-27 20:22:06','2023-01-11 20:22:06'),(63,1,97.9298,'USD','balance','output',1,'2022-11-06 20:22:06','2023-05-09 20:22:06'),(64,1,19.8645,'USD','balance','output',1,'2022-10-12 20:22:06','2023-08-29 20:22:06'),(65,1,45.0594,'USD','balance','output',1,'2023-08-06 20:22:06','2023-07-03 20:22:06'),(66,1,68.4088,'USD','balance','output',1,'2022-11-24 20:22:06','2022-09-16 20:22:06'),(67,1,56.1019,'USD','balance','output',1,'2022-11-24 20:22:06','2023-05-04 20:22:06'),(68,1,41.1103,'USD','balance','output',1,'2022-10-04 20:22:06','2023-03-10 20:22:06'),(69,1,74.0728,'USD','balance','output',1,'2023-07-23 20:22:06','2023-03-09 20:22:06'),(70,1,15.8444,'USD','balance','output',1,'2023-06-24 20:22:06','2023-01-13 20:22:06'),(71,1,67.5889,'USD','balance','output',1,'2023-05-07 20:22:06','2022-12-07 20:22:06'),(72,1,79.3116,'USD','balance','output',1,'2023-01-25 20:22:06','2022-11-19 20:22:06'),(73,1,20.0319,'USD','balance','output',1,'2023-03-07 20:22:06','2023-09-01 20:22:06'),(74,1,61.1366,'USD','balance','output',1,'2022-10-08 20:22:06','2022-11-04 20:22:06'),(75,1,50.0643,'USD','balance','output',1,'2022-10-18 20:22:06','2023-08-26 20:22:06'),(76,1,54.4559,'USD','balance','output',1,'2023-02-25 20:22:06','2023-07-25 20:22:06'),(77,1,4.73938,'USD','balance','output',1,'2022-11-24 20:22:06','2022-10-21 20:22:06'),(78,1,8.83989,'USD','balance','output',1,'2022-12-22 20:22:06','2023-04-23 20:22:06'),(79,1,79.2283,'USD','balance','output',1,'2022-12-09 20:22:06','2023-04-01 20:22:06'),(80,1,99.5863,'USD','balance','output',1,'2023-02-08 20:22:06','2023-09-10 20:22:06'),(81,1,27.7431,'USD','balance','output',1,'2023-05-15 20:22:06','2022-11-04 20:22:06'),(82,1,30.6141,'USD','balance','output',1,'2022-10-09 20:22:06','2022-12-11 20:22:06'),(83,1,0.0646573,'USD','balance','output',1,'2022-12-27 20:22:06','2023-02-13 20:22:06'),(84,1,78.5923,'USD','balance','output',1,'2023-07-17 20:22:06','2023-03-28 20:22:06'),(85,1,85.6401,'USD','balance','output',1,'2022-11-02 20:22:06','2022-12-05 20:22:06'),(86,1,29.2119,'USD','balance','output',1,'2023-08-03 20:22:06','2022-12-26 20:22:06'),(87,1,10.5,'usdt','tariff','input',1,'2022-01-01 10:00:00','2023-09-20 21:43:16'),(88,1,20.3,'rub','tariff','output',1,'2022-01-02 12:30:00','2023-09-20 21:43:16'),(89,1,5.2,'del','tariff','input',1,'2022-01-03 15:45:00','2023-09-20 21:43:16'),(90,1,15.8,'ton','tariff','output',1,'2022-01-04 18:20:00','2023-09-20 21:43:16'),(91,2,10.5,'usdt','tariff','input',1,'2022-01-01 10:00:00','2023-09-20 22:58:34'),(92,3,20.3,'rub','tariff','output',1,'2022-01-02 12:30:00','2023-09-20 22:58:34'),(93,2,19.93,'del','tariff',NULL,NULL,'2022-09-29 22:02:15','2022-12-21 22:02:15'),(94,14,84.24,'rub','tariff',NULL,NULL,'2023-06-26 22:02:15','2022-10-12 22:02:15'),(95,16,12.65,'del','tariff',NULL,NULL,'2022-10-21 22:02:15','2023-02-07 22:02:15'),(96,6,83.94,'usdt','tariff',NULL,NULL,'2023-05-09 22:02:15','2023-05-29 22:02:15'),(97,8,41.87,'del','tariff',NULL,NULL,'2023-08-14 22:02:15','2023-03-19 22:02:15'),(98,4,64.68,'del','tariff',NULL,NULL,'2023-01-11 22:02:15','2022-10-30 22:02:15'),(99,7,23.56,'usdt','tariff',NULL,NULL,'2023-04-16 22:02:15','2023-08-22 22:02:15'),(100,2,35.19,'ton','tariff',NULL,NULL,'2022-10-19 22:02:15','2023-04-15 22:02:15'),(101,7,75.92,'del','tariff',NULL,NULL,'2023-03-30 22:02:15','2022-12-22 22:02:15'),(102,5,29.31,'del','tariff',NULL,NULL,'2022-11-28 22:02:15','2023-04-09 22:02:15'),(103,14,78.3,'ton','tariff',NULL,NULL,'2022-12-08 22:02:15','2023-01-23 22:02:15'),(104,16,70.7,'del','tariff',NULL,NULL,'2023-03-28 22:02:15','2023-06-17 22:02:15'),(105,14,52.12,'usdt','tariff',NULL,NULL,'2023-03-12 22:02:15','2023-02-19 22:02:15'),(106,6,2.39,'usdt','tariff',NULL,NULL,'2023-07-28 22:02:15','2023-02-04 22:02:15'),(107,11,53.08,'del','tariff',NULL,NULL,'2023-04-17 22:02:15','2023-05-19 22:02:15'),(108,7,9.81,'usdt','tariff',NULL,NULL,'2022-12-16 22:02:15','2023-07-13 22:02:15');
/*!40000 ALTER TABLE `pay_history` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `promocodes`
--

DROP TABLE IF EXISTS `promocodes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `promocodes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `value_code` varchar(5) NOT NULL,
  `user_id` bigint(20) DEFAULT NULL,
  `tariff_id` int(11) NOT NULL,
  `price_del` float DEFAULT NULL,
  `price_ton` float DEFAULT NULL,
  `price_usdt` float DEFAULT NULL,
  `price_rub` float DEFAULT NULL,
  `status` tinyint(4) DEFAULT NULL,
  `create_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`),
  KEY `promocodes_ibfk_2` (`tariff_id`),
  CONSTRAINT `promocodes_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`),
  CONSTRAINT `promocodes_ibfk_2` FOREIGN KEY (`tariff_id`) REFERENCES `tariffs` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=62 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `promocodes`
--

LOCK TABLES `promocodes` WRITE;
/*!40000 ALTER TABLE `promocodes` DISABLE KEYS */;
INSERT INTO `promocodes` VALUES (1,'Code8',1,10,0.509997,15.4652,75.796,32.5842,1,'2023-05-11 10:35:29'),(5,'Code7',1,14,17.8571,36.4233,28.5451,33.4558,1,'2022-11-24 10:35:29'),(6,'Code8',16,8,57.3965,42.7579,41.6004,79.728,1,'2022-12-22 10:35:29'),(7,'Code3',5,9,77.6102,30.1223,17.781,98.5379,1,'2023-04-27 10:35:29'),(10,'Code4',12,8,5.27781,91.6338,42.3358,36.7774,1,'2023-02-22 10:35:29'),(11,'Code7',16,13,82.152,82.0309,63.6984,72.3993,1,'2023-01-02 10:35:29'),(12,'Code3',12,10,64.8783,52.5494,68.1122,82.9128,1,'2023-08-11 10:35:29'),(13,'Code3',14,16,52.8548,66.2019,72.4453,63.6208,1,'2023-09-15 10:35:29'),(14,'Code1',11,12,82.2515,89.249,99.4904,29.7048,1,'2023-03-19 10:35:29'),(15,'Code6',9,16,5.82468,45.5579,10.3154,14.9032,1,'2023-04-11 10:35:29'),(16,'Code7',6,9,72.6936,96.8685,66.2616,40.7025,1,'2023-08-31 10:35:29'),(17,'Code2',15,11,33.5148,79.2061,95.4861,39.8123,1,'2023-08-02 10:35:29'),(18,'Code4',13,12,8.19486,31.9036,34.9334,78.9564,1,'2022-10-24 10:35:29'),(22,'Code3',9,15,98.5729,12.5904,67.2333,98.3954,1,'2022-10-23 10:35:29'),(23,'Code5',2,14,80.5655,56.1985,39.2959,27.8841,1,'2023-07-01 10:35:29'),(25,'Code3',9,11,77.9981,86.5489,98.7504,34.1053,1,'2022-12-20 10:35:29'),(40,'Code5',NULL,16,21.2863,10.1653,86.9677,4.34268,1,'2023-02-08 10:37:17'),(41,'Code9',NULL,12,90.3516,33.7057,97.4736,86.2512,1,'2023-04-29 10:37:17'),(42,'Code3',NULL,10,96.7526,1.9521,19.5028,91.6579,1,'2022-09-18 10:37:17'),(47,'Code8',NULL,13,13.3277,40.4294,62.1641,89.5323,1,'2023-02-06 10:37:17'),(54,'Code9',NULL,16,90.807,66.3867,59.5127,98.4032,1,'2023-07-30 10:37:17'),(56,'Code7',NULL,15,30.8549,90.7034,60.9524,32.652,1,'2022-11-28 10:37:17'),(57,'ZQNYA',NULL,8,NULL,NULL,NULL,NULL,1,'2023-09-20 13:24:09'),(58,'VDRWY',NULL,8,NULL,NULL,NULL,NULL,1,'2023-09-20 13:24:11'),(59,'EABID',NULL,8,NULL,NULL,NULL,NULL,1,'2023-09-20 13:24:12'),(60,'BWAAI',NULL,8,NULL,NULL,NULL,NULL,1,'2023-09-20 15:03:47'),(61,'JCVZT',NULL,9,NULL,NULL,NULL,NULL,1,'2023-09-20 15:03:54');
/*!40000 ALTER TABLE `promocodes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `referrals_tree`
--

DROP TABLE IF EXISTS `referrals_tree`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `referrals_tree` (
  `Parent_id` bigint(20) NOT NULL,
  `Child_id` bigint(20) NOT NULL,
  `created_at` datetime DEFAULT NULL,
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`Id`),
  KEY `parent_id` (`Parent_id`),
  KEY `child_id` (`Child_id`),
  CONSTRAINT `referrals_tree_ibfk_1` FOREIGN KEY (`parent_id`) REFERENCES `users` (`id`),
  CONSTRAINT `referrals_tree_ibfk_2` FOREIGN KEY (`child_id`) REFERENCES `users` (`id`),
  CONSTRAINT `referrals_tree_ibfk_3` FOREIGN KEY (`parent_id`) REFERENCES `users` (`id`),
  CONSTRAINT `referrals_tree_ibfk_4` FOREIGN KEY (`child_id`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `referrals_tree`
--

LOCK TABLES `referrals_tree` WRITE;
/*!40000 ALTER TABLE `referrals_tree` DISABLE KEYS */;
INSERT INTO `referrals_tree` VALUES (16,15,NULL,1),(15,1,NULL,2);
/*!40000 ALTER TABLE `referrals_tree` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `servers`
--

DROP TABLE IF EXISTS `servers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `servers` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `apiurl` varchar(255) DEFAULT NULL,
  `certsha256` varchar(255) DEFAULT NULL,
  `created_at` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `servers`
--

LOCK TABLES `servers` WRITE;
/*!40000 ALTER TABLE `servers` DISABLE KEYS */;
/*!40000 ALTER TABLE `servers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `settings`
--

DROP TABLE IF EXISTS `settings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `settings` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `commission_output_del` int(11) DEFAULT NULL,
  `commission_output_ton` int(11) DEFAULT NULL,
  `commission_output_usdt` int(11) DEFAULT NULL,
  `commission_output_rub` int(11) DEFAULT NULL,
  `commission_input_del` int(11) DEFAULT NULL,
  `commission_input_ton` int(11) DEFAULT NULL,
  `commission_input_usdt` int(11) DEFAULT NULL,
  `referral_reward_lvl_1` int(11) DEFAULT NULL,
  `referral_reward_lvl_2` int(11) DEFAULT NULL,
  `referral_reward_lvl_3` int(11) DEFAULT NULL,
  `min_output` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `settings`
--

LOCK TABLES `settings` WRITE;
/*!40000 ALTER TABLE `settings` DISABLE KEYS */;
INSERT INTO `settings` VALUES (1,1,7,8,79,4,8,9,2,4,6,57);
/*!40000 ALTER TABLE `settings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tariffs`
--

DROP TABLE IF EXISTS `tariffs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tariffs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tariff_name` varchar(45) NOT NULL,
  `duration` int(11) DEFAULT NULL,
  `price` int(11) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tariffs`
--

LOCK TABLES `tariffs` WRITE;
/*!40000 ALTER TABLE `tariffs` DISABLE KEYS */;
INSERT INTO `tariffs` VALUES (8,'Tariff 32',82,123,'2023-07-17 02:03:27'),(9,'Tariff 49',92,12,'2022-11-01 02:03:27'),(10,'Tariff 2',45,188,'2023-02-13 02:03:27'),(11,'Tariff 41',29,101,'2023-01-03 02:03:27'),(12,'Tariff 23',99,282,'2023-04-09 02:03:27'),(13,'Tariff 37',51,423,'2023-02-10 02:03:27'),(14,'Tariff 74',89,205,'2023-05-04 02:03:27'),(15,'Tariff 26',14,918,'2023-07-12 02:03:27'),(16,'Tariff 18',33,91,'2023-03-26 02:03:27'),(17,'Tariff 13',21,648,'2023-02-05 02:03:27'),(18,'Tariff 14',82,694,'2023-09-13 02:03:27'),(19,'Tariff 98',86,329,'2023-08-17 02:03:27'),(20,'Tariff 46',99,593,'2022-09-19 02:03:27'),(21,'Tariff 21',4,562,'2023-01-05 02:03:27'),(22,'Tariff 82',100,499,'2023-03-12 02:03:27'),(23,'Tariff 11',96,452,'2023-04-24 02:03:27'),(24,'Tariff 66',7,349,'2023-02-25 02:03:27'),(25,'Tariff 76',9,149,'2023-03-19 02:03:27'),(32,'Tariff 6',77,682,'2023-08-12 02:08:12'),(33,'Tariff 47',1,661,'2023-06-09 02:08:12'),(34,'Tariff 40',15,530,'2023-06-29 02:08:12'),(35,'Tariff 51',89,910,'2022-10-28 02:08:12'),(36,'Tariff 71',89,279,'2022-12-18 02:08:12'),(37,'Tariff 92',33,875,'2023-04-24 02:08:12'),(38,'Tariff 38',69,307,'2023-03-31 02:08:12'),(39,'Tariff 42',68,144,'2023-01-11 02:08:12'),(40,'Tariff 100',91,548,'2023-09-08 02:08:12'),(41,'Tariff 49',35,264,'2023-06-05 02:08:12'),(42,'Tariff 65',37,881,'2023-05-25 02:08:12'),(43,'Tariff 94',73,810,'2022-10-30 02:08:12'),(44,'Tariff 99',28,411,'2023-06-24 02:08:12'),(45,'Tariff 94',100,146,'2022-12-18 02:08:12'),(46,'Tariff 32',31,608,'2023-08-08 02:08:12'),(47,'Tariff 74',35,538,'2023-01-28 02:08:12'),(48,'Tariff 58',98,146,'2022-11-29 02:08:12'),(49,'Tariff 57',45,503,'2023-07-12 02:08:12'),(50,'Tariff 42',53,395,'2023-04-30 02:08:12'),(51,'Tariff 75',55,519,'2022-10-06 02:08:12'),(52,'Tariff 20',13,24,'2022-12-17 02:08:12'),(53,'Tariff 70',22,975,'2023-06-22 02:08:12'),(54,'Tariff 28',67,506,'2023-03-10 02:08:12'),(55,'Tariff 11',98,532,'2022-12-20 02:08:12'),(56,'Tariff 12',37,458,'2023-07-07 02:08:12'),(57,'GELOO',78,5,'2023-09-19 19:36:37'),(58,'GELOOmm',56,65,'2023-09-19 19:48:26'),(59,'Hello',65,65,'2023-09-19 19:49:44'),(60,'GELOOmE',563,566,'2023-09-19 19:53:40');
/*!40000 ALTER TABLE `tariffs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `use_promocodes`
--

DROP TABLE IF EXISTS `use_promocodes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `use_promocodes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `promocode_id` int(11) DEFAULT NULL,
  `user_id` bigint(20) DEFAULT NULL,
  `create_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `promocode_id` (`promocode_id`),
  CONSTRAINT `use_promocodes_ibfk_1` FOREIGN KEY (`promocode_id`) REFERENCES `promocodes` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `use_promocodes`
--

LOCK TABLES `use_promocodes` WRITE;
/*!40000 ALTER TABLE `use_promocodes` DISABLE KEYS */;
/*!40000 ALTER TABLE `use_promocodes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `chat_id` bigint(20) DEFAULT NULL,
  `username` varchar(200) NOT NULL,
  `first_name` varchar(200) NOT NULL,
  `last_name` varchar(200) DEFAULT NULL,
  `is_replay` tinyint(4) DEFAULT NULL,
  `is_free` tinyint(4) DEFAULT NULL,
  `status_tariff` tinyint(4) DEFAULT NULL,
  `status` tinyint(4) DEFAULT NULL,
  `created_at` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=65 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,669222,'username1','first_name1','last_name1',0,1,0,1,'2023-09-12 20:16:03'),(2,979143,'username2','first_name2','last_name2',1,1,2,1,'2023-09-12 20:16:03'),(3,615839,'username3','first_name3','last_name3',0,1,1,1,'2023-09-12 20:16:03'),(4,431615,'username4','first_name4','last_name4',1,1,2,1,'2023-09-12 20:16:03'),(5,984221,'username5','first_name5','last_name5',0,1,2,1,'2023-09-12 20:16:03'),(6,844191,'username6','first_name6','last_name6',1,1,1,1,'2023-09-12 20:16:03'),(7,16810,'username7','first_name7','last_name7',1,1,2,1,'2023-09-12 20:16:03'),(8,656623,'username8','first_name8','last_name8',0,1,0,1,'2023-09-12 20:16:03'),(9,434914,'username9','first_name9','last_name9',1,0,2,1,'2023-09-12 20:16:03'),(10,938705,'username10','first_name10','last_name10',0,0,2,1,'2023-09-12 20:16:03'),(11,907064,'username11','first_name11','last_name11',0,1,0,1,'2023-09-12 20:16:03'),(12,814356,'username12','first_name12','last_name12',1,1,3,1,'2023-09-12 20:16:03'),(13,758680,'username13','first_name13','last_name13',0,1,3,2,'2023-09-12 20:16:03'),(14,165607,'username14','first_name14','last_name14',0,1,2,3,'2023-09-12 20:16:03'),(15,911942,'username15','first_name15','last_name15',0,1,1,1,'2023-09-12 20:16:03'),(16,871866,'username16','first_name16','last_name16',1,1,3,1,'2023-09-12 20:16:03'),(32,521945,'user1','First1','Last1',1,1,0,0,'2023-04-01 19:23:57'),(33,904342,'user2','First2','Last2',0,1,3,1,'2023-03-23 19:23:57'),(34,647801,'user3','First3','Last3',0,1,1,1,'2022-12-11 19:23:57'),(35,716590,'user4','First4','Last4',1,1,0,2,'2023-01-16 19:23:57'),(36,835465,'user5','First5','Last5',1,0,1,1,'2023-06-07 19:23:57'),(37,229939,'user6','First6','Last6',0,1,3,1,'2023-07-27 19:23:57'),(38,787469,'user7','First7','Last7',1,0,1,1,'2022-11-28 19:23:57'),(39,547317,'user8','First8','Last8',0,0,1,1,'2023-08-02 19:23:57'),(40,785631,'user9','First9','Last9',1,1,0,2,'2023-09-09 19:23:57'),(41,388166,'user10','First10','Last10',1,1,2,3,'2022-10-23 19:23:57'),(42,959881,'user11','First11','Last11',1,1,2,0,'2023-08-08 19:23:57'),(43,243024,'user12','First12','Last12',1,0,1,3,'2022-09-21 19:23:57'),(44,946487,'user13','First13','Last13',1,0,1,0,'2023-02-08 19:23:57'),(45,323613,'user14','First14','Last14',1,0,3,1,'2023-08-25 19:23:57'),(46,824028,'user15','First15','Last15',1,1,0,3,'2022-09-28 19:23:57'),(47,629135,'user16','First16','Last16',1,1,2,1,'2023-08-24 19:23:57'),(48,286330,'user17','First17','Last17',0,0,2,1,'2023-07-09 19:23:57'),(49,199031,'user18','First18','Last18',1,0,3,1,'2022-12-28 19:23:57'),(50,779928,'user19','First19','Last19',1,1,2,1,'2023-02-02 19:23:57'),(51,489408,'user20','First20','Last20',0,1,1,1,'2023-04-30 19:23:57'),(52,941698,'user21','First21','Last21',1,1,0,1,'2022-09-30 19:23:57'),(53,471300,'user22','First22','Last22',1,0,2,1,'2023-03-02 19:23:57'),(54,633314,'user23','First23','Last23',1,1,0,1,'2022-09-24 19:23:57'),(55,534718,'user24','First24','Last24',0,1,2,1,'2023-06-20 19:23:57'),(56,515845,'user25','First25','Last25',1,0,2,1,'2023-07-17 19:23:57'),(57,NULL,'hello','world',NULL,NULL,NULL,NULL,NULL,'2023-09-12 20:16:03'),(58,NULL,'hello','world',NULL,NULL,NULL,NULL,NULL,'2023-09-12 20:13:03'),(59,NULL,'hello','world',NULL,NULL,NULL,NULL,NULL,'2023-09-14 20:13:03'),(60,NULL,'hello','world',NULL,NULL,NULL,NULL,NULL,'2023-09-15 20:13:03'),(61,NULL,'hello','world',NULL,NULL,NULL,NULL,NULL,'2023-09-16 20:13:03'),(62,NULL,'hello','world',NULL,NULL,NULL,NULL,NULL,'2023-09-16 20:13:03'),(63,NULL,'hello','world',NULL,NULL,NULL,NULL,NULL,'2023-09-13 20:13:03'),(64,NULL,'hello','world',NULL,NULL,NULL,NULL,NULL,'2023-09-13 20:13:03');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users_keys`
--

DROP TABLE IF EXISTS `users_keys`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users_keys` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` bigint(20) DEFAULT NULL,
  `server_Id` int(11) DEFAULT NULL,
  `key` varchar(255) DEFAULT NULL,
  `status` tinyint(4) DEFAULT NULL,
  `date_end` datetime NOT NULL,
  `create_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`),
  KEY `server_Id` (`server_Id`),
  CONSTRAINT `users_keys_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`),
  CONSTRAINT `users_keys_ibfk_2` FOREIGN KEY (`server_Id`) REFERENCES `servers` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users_keys`
--

LOCK TABLES `users_keys` WRITE;
/*!40000 ALTER TABLE `users_keys` DISABLE KEYS */;
INSERT INTO `users_keys` VALUES (1,1,NULL,NULL,NULL,'2023-09-27 00:00:00',NULL);
/*!40000 ALTER TABLE `users_keys` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users_tariff`
--

DROP TABLE IF EXISTS `users_tariff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users_tariff` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` bigint(20) NOT NULL,
  `tariff_id` int(11) DEFAULT NULL,
  `duration` int(11) NOT NULL,
  `status` tinyint(4) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`),
  KEY `tariff_id` (`tariff_id`),
  CONSTRAINT `users_tariff_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`),
  CONSTRAINT `users_tariff_ibfk_2` FOREIGN KEY (`tariff_id`) REFERENCES `tariffs` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users_tariff`
--

LOCK TABLES `users_tariff` WRITE;
/*!40000 ALTER TABLE `users_tariff` DISABLE KEYS */;
INSERT INTO `users_tariff` VALUES (1,16,NULL,5,0,'2023-09-12 23:21:56'),(2,4,NULL,5,0,'2023-09-17 19:55:47'),(3,5,NULL,6,0,'2023-09-17 19:57:09'),(4,6,NULL,6,0,'2023-09-17 19:57:09'),(5,13,NULL,4,0,'2023-09-17 19:58:11'),(6,11,NULL,665,0,'2023-09-17 20:00:57'),(7,33,NULL,665,0,'2023-09-17 20:00:57'),(8,15,NULL,56,0,'2023-09-20 11:07:51'),(9,34,NULL,56,0,'2023-09-20 11:07:51');
/*!40000 ALTER TABLE `users_tariff` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `wallets`
--

DROP TABLE IF EXISTS `wallets`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `wallets` (
  `user_id` bigint(20) NOT NULL,
  `type` varchar(15) DEFAULT NULL,
  `balance` float DEFAULT NULL,
  `addresse` varchar(255) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  KEY `user_id` (`user_id`),
  CONSTRAINT `wallets_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `wallets`
--

LOCK TABLES `wallets` WRITE;
/*!40000 ALTER TABLE `wallets` DISABLE KEYS */;
INSERT INTO `wallets` VALUES (1,'ton',100,'address1','2023-09-12 20:17:38'),(2,'usdt_bep20',200,'address2','2023-09-12 20:17:38'),(3,'del',300,'address3','2023-09-12 20:17:38'),(4,'usdt_trx20',400,'address4','2023-09-12 20:17:38'),(5,'ton',500,'address5','2023-09-12 20:17:38'),(6,'usdt_bep20',600,'address6','2023-09-12 20:17:38'),(7,'del',700,'address7','2023-09-12 20:17:38'),(8,'usdt_trx20',800,'address8','2023-09-12 20:17:38'),(9,'ton',900,'address9','2023-09-12 20:17:38'),(10,'usdt_bep20',1000,'address10','2023-09-12 20:17:38');
/*!40000 ALTER TABLE `wallets` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-09-27 21:26:31
