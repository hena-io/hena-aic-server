-- MySQL dump 10.13  Distrib 8.0.12, for Win64 (x86_64)
--
-- Host: localhost    Database: hena_aic_service
-- ------------------------------------------------------
-- Server version	8.0.12

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tbl_ad_resource`
--

DROP TABLE IF EXISTS `tbl_ad_resource`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tbl_ad_resource` (
  `Idx` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserId` bigint(20) NOT NULL,
  `AdResourceId` bigint(20) NOT NULL,
  `AdDesignType` varchar(45) NOT NULL DEFAULT 'None' COMMENT 'None, MobileLeaderboard, MobileBannerLandscape, LargeMobileBanner, Banner, Leaderboard, InlineRectangle, SmartphoneInterstitialPortrait, SmartphoneInterstitialLandscape, TabletInterstitialPortrait, TabletInterstitialLandscape',
  `ContentType` varchar(45) NOT NULL DEFAULT '' COMMENT 'image/png ...',
  `Width` smallint(6) NOT NULL DEFAULT '0',
  `Height` smallint(6) NOT NULL DEFAULT '0',
  `CreateTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `LastUpdate` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`UserId`,`AdResourceId`),
  UNIQUE KEY `ResourceId_UNIQUE` (`AdResourceId`),
  UNIQUE KEY `Idx_UNIQUE` (`Idx`),
  KEY `Idx_UserId` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=110 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_ad_resource`
--

LOCK TABLES `tbl_ad_resource` WRITE;
/*!40000 ALTER TABLE `tbl_ad_resource` DISABLE KEYS */;
INSERT INTO `tbl_ad_resource` VALUES (9,783380349351692948,787039455210233168,'Image','image/png',480,32,'2018-10-11 09:44:30','2018-10-11 09:44:30'),(3,783380349351692948,787039455210257669,'Image','image/png',480,32,'2018-10-11 09:40:43','2018-10-11 09:40:43'),(17,783380349351692948,787039455213837645,'Image','image/png',480,32,'2018-10-11 10:23:49','2018-10-11 10:23:49'),(16,783380349351692948,787039455213973653,'Image','image/png',480,32,'2018-10-11 10:14:20','2018-10-11 10:14:20'),(22,783380349351692948,787039523945412917,'Image','image/jpeg',960,1280,'2018-10-12 00:54:47','2018-10-12 00:54:47'),(13,783380349351692948,787109823954303773,'Image','image/png',480,32,'2018-10-11 09:49:36','2018-10-11 09:49:36'),(10,783380349351692948,787109823954363472,'Image','image/png',480,32,'2018-10-11 09:45:12','2018-10-11 09:45:12'),(11,783380349351692948,787109823954364467,'Image','image/png',480,32,'2018-10-11 09:45:16','2018-10-11 09:45:16'),(5,783380349351692948,787109823954420550,'Image','image/png',480,32,'2018-10-11 09:41:19','2018-10-11 09:41:19'),(4,783380349351692948,787109823954427209,'Image','image/gif',480,32,'2018-10-11 09:41:13','2018-10-11 09:41:13'),(2,783380349351692948,787109823954977517,'Image','image/gif',480,32,'2018-10-11 09:04:15','2018-10-11 09:04:15'),(14,783380349351692948,787109823957711720,'Image','image/png',480,32,'2018-10-11 10:06:57','2018-10-11 10:06:57'),(8,783380349351692948,787180192698587401,'Image','image/png',480,32,'2018-10-11 09:44:26','2018-10-11 09:44:26'),(7,783380349351692948,787180192698623470,'Image','image/gif',480,32,'2018-10-11 09:41:53','2018-10-11 09:41:53'),(6,783380349351692948,787180192698628373,'Image','image/png',480,32,'2018-10-11 09:41:42','2018-10-11 09:41:42'),(19,783380349351692948,787180192702143337,'Image','image/png',480,32,'2018-10-11 10:24:52','2018-10-11 10:24:52'),(18,783380349351692948,787180192702147411,'Image','image/gif',480,32,'2018-10-11 10:24:36','2018-10-11 10:24:36'),(20,783380349351692948,787180192702154000,'Image','image/png',480,32,'2018-10-11 10:26:08','2018-10-11 10:26:08'),(12,783380349351692948,787250561442717534,'Image','image/png',480,32,'2018-10-11 09:45:37','2018-10-11 09:45:37'),(1,783380349351692948,787250561443783338,'Image','null',0,0,'2018-10-11 08:36:22','2018-10-11 08:36:22'),(15,783380349351692948,787250561446487241,'Image','image/png',480,32,'2018-10-11 10:13:35','2018-10-11 10:13:35'),(23,783380349351692948,787250630176912551,'Image','image/jpeg',480,32,'2018-10-12 02:01:21','2018-10-12 02:01:21'),(21,783380349351692948,787250630177924539,'Image','image/jpeg',960,1280,'2018-10-12 00:53:53','2018-10-12 00:53:53'),(39,783380349351692948,787320998908136769,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:30:59','2018-10-12 06:30:59'),(72,783380349351692948,787320998908528292,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:38','2018-10-12 06:39:38'),(73,783380349351692948,787320998908528846,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:40','2018-10-12 06:39:40'),(76,783380349351692948,787320998908530678,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:45','2018-10-12 06:39:45'),(62,783380349351692948,787320998908531651,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:16','2018-10-12 06:39:16'),(64,783380349351692948,787320998908531835,'InlineRectangle','image/png',300,250,'2018-10-12 06:39:20','2018-10-12 06:39:20'),(67,783380349351692948,787320998908534148,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:27','2018-10-12 06:39:27'),(52,783380349351692948,787320998908535042,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:38:59','2018-10-12 06:38:59'),(53,783380349351692948,787320998908535740,'InlineRectangle','image/png',300,250,'2018-10-12 06:39:00','2018-10-12 06:39:00'),(45,783380349351692948,787320998908540211,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:38:46','2018-10-12 06:38:46'),(46,783380349351692948,787320998908540740,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:38:48','2018-10-12 06:38:48'),(85,783380349351692948,787320998908549081,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:40:26','2018-10-12 06:40:26'),(78,783380349351692948,787320998908553944,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:40:15','2018-10-12 06:40:15'),(25,783380349351692948,787320998908992778,'InlineRectangle','image/png',300,250,'2018-10-12 06:08:53','2018-10-12 06:08:53'),(99,783380349351692948,787320998911298709,'Leaderboard','image/png',728,90,'2018-10-12 07:42:00','2018-10-12 07:42:00'),(97,783380349351692948,787320998911303217,'LargeMobileBanner','image/png',640,200,'2018-10-12 07:41:46','2018-10-12 07:41:46'),(33,783380349351692948,787391367652352638,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:28:06','2018-10-12 06:28:06'),(35,783380349351692948,787391367652354716,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:28:14','2018-10-12 06:28:14'),(37,783380349351692948,787391367652364689,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:29:25','2018-10-12 06:29:25'),(32,783380349351692948,787391367652463952,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:22:56','2018-10-12 06:22:56'),(31,783380349351692948,787391367652498034,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:20:52','2018-10-12 06:20:52'),(29,783380349351692948,787391367652500089,'InlineRectangle','image/png',300,250,'2018-10-12 06:20:27','2018-10-12 06:20:27'),(30,783380349351692948,787391367652503320,'InlineRectangle','image/png',300,250,'2018-10-12 06:20:39','2018-10-12 06:20:39'),(70,783380349351692948,787391367652704922,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:34','2018-10-12 06:39:34'),(71,783380349351692948,787391367652705363,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:36','2018-10-12 06:39:36'),(63,783380349351692948,787391367652708923,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:18','2018-10-12 06:39:18'),(66,783380349351692948,787391367652710546,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:24','2018-10-12 06:39:24'),(68,783380349351692948,787391367652712349,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:29','2018-10-12 06:39:29'),(55,783380349351692948,787391367652713560,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:04','2018-10-12 06:39:04'),(54,783380349351692948,787391367652713983,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:02','2018-10-12 06:39:02'),(57,783380349351692948,787391367652714771,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:07','2018-10-12 06:39:07'),(59,783380349351692948,787391367652715906,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:11','2018-10-12 06:39:11'),(43,783380349351692948,787391367652716608,'InlineRectangle','image/png',300,250,'2018-10-12 06:38:43','2018-10-12 06:38:43'),(44,783380349351692948,787391367652717218,'InlineRectangle','image/png',300,250,'2018-10-12 06:38:45','2018-10-12 06:38:45'),(50,783380349351692948,787391367652719734,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:38:55','2018-10-12 06:38:55'),(51,783380349351692948,787391367652720336,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:38:57','2018-10-12 06:38:57'),(84,783380349351692948,787391367652726134,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:40:25','2018-10-12 06:40:25'),(42,783380349351692948,787391367652757003,'InlineRectangle','image/png',300,250,'2018-10-12 06:38:41','2018-10-12 06:38:41'),(24,783380349351692948,787391367653258637,'InlineRectangle','image/png',300,250,'2018-10-12 06:04:54','2018-10-12 06:04:54'),(91,783380349351692948,787391367655460856,'InlineRectangle','image/png',300,250,'2018-10-12 07:40:55','2018-10-12 07:40:55'),(88,783380349351692948,787391367655462892,'Banner','image/png',936,120,'2018-10-12 07:40:30','2018-10-12 07:40:30'),(95,783380349351692948,787391367655477501,'LargeMobileBanner','image/png',320,100,'2018-10-12 07:41:31','2018-10-12 07:41:31'),(96,783380349351692948,787391367655479418,'LargeMobileBanner','image/png',640,200,'2018-10-12 07:41:40','2018-10-12 07:41:40'),(94,783380349351692948,787391367655484384,'LargeMobileBanner','image/png',320,100,'2018-10-12 07:41:24','2018-10-12 07:41:24'),(100,783380349351692948,787391367655962743,'Leaderboard','image/png',1456,180,'2018-10-12 07:42:12','2018-10-12 07:42:12'),(74,783380349351692948,787461736396884714,'InlineRectangle','image/png',300,250,'2018-10-12 06:39:42','2018-10-12 06:39:42'),(65,783380349351692948,787461736396887604,'InlineRectangle','image/png',300,250,'2018-10-12 06:39:22','2018-10-12 06:39:22'),(61,783380349351692948,787461736396893722,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:14','2018-10-12 06:39:14'),(60,783380349351692948,787461736396894115,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:12','2018-10-12 06:39:12'),(47,783380349351692948,787461736396896733,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:38:50','2018-10-12 06:38:50'),(49,783380349351692948,787461736396897780,'InlineRectangle','image/png',300,250,'2018-10-12 06:38:54','2018-10-12 06:38:54'),(82,783380349351692948,787461736396902496,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:40:21','2018-10-12 06:40:21'),(77,783380349351692948,787461736396908748,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:40:13','2018-10-12 06:40:13'),(79,783380349351692948,787461736396909941,'InlineRectangle','image/png',300,250,'2018-10-12 06:40:16','2018-10-12 06:40:16'),(89,783380349351692948,787461736399641780,'Banner','image/png',936,120,'2018-10-12 07:40:38','2018-10-12 07:40:38'),(86,783380349351692948,787461736399642985,'Banner','image/png',468,60,'2018-10-12 07:40:08','2018-10-12 07:40:08'),(98,783380349351692948,787461736399651906,'Leaderboard','image/png',728,90,'2018-10-12 07:41:52','2018-10-12 07:41:52'),(101,783380349351692948,787461736400130227,'Leaderboard','image/png',1456,180,'2018-10-12 07:42:37','2018-10-12 07:42:37'),(38,783380349351692948,787532105140673299,'InlineRectangle','image/png',300,250,'2018-10-12 06:30:41','2018-10-12 06:30:41'),(36,783380349351692948,787532105140706298,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:28:29','2018-10-12 06:28:29'),(34,783380349351692948,787532105140709049,'InlineRectangle','image/png',300,250,'2018-10-12 06:28:10','2018-10-12 06:28:10'),(27,783380349351692948,787532105140874454,'InlineRectangle','image/png',300,250,'2018-10-12 06:17:21','2018-10-12 06:17:21'),(28,783380349351692948,787532105140884430,'InlineRectangle','image/png',300,250,'2018-10-12 06:18:31','2018-10-12 06:18:31'),(69,783380349351692948,787532105141059671,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:32','2018-10-12 06:39:32'),(75,783380349351692948,787532105141063012,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:43','2018-10-12 06:39:43'),(56,783380349351692948,787532105141069495,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:05','2018-10-12 06:39:05'),(58,783380349351692948,787532105141070678,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:39:09','2018-10-12 06:39:09'),(48,783380349351692948,787532105141073925,'InlineRectangle','image/png',300,250,'2018-10-12 06:38:52','2018-10-12 06:38:52'),(81,783380349351692948,787532105141080546,'InlineRectangle','image/png',300,250,'2018-10-12 06:40:20','2018-10-12 06:40:20'),(83,783380349351692948,787532105141080777,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:40:23','2018-10-12 06:40:23'),(80,783380349351692948,787532105141088136,'InlineRectangle','image/png',300,250,'2018-10-12 06:40:18','2018-10-12 06:40:18'),(40,783380349351692948,787532105141112147,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:38:38','2018-10-12 06:38:38'),(41,783380349351692948,787532105141112760,'MobileBannerLandscape','image/png',480,32,'2018-10-12 06:38:40','2018-10-12 06:38:40'),(26,783380349351692948,787532105141464193,'InlineRectangle','image/png',300,250,'2018-10-12 06:12:59','2018-10-12 06:12:59'),(90,783380349351692948,787532105143814063,'InlineRectangle','image/png',300,250,'2018-10-12 07:40:47','2018-10-12 07:40:47'),(87,783380349351692948,787532105143823120,'Banner','image/png',468,60,'2018-10-12 07:40:19','2018-10-12 07:40:19'),(92,783380349351692948,787532105143842785,'InlineRectangle','image/png',600,500,'2018-10-12 07:41:03','2018-10-12 07:41:03'),(93,783380349351692948,787532105143844419,'InlineRectangle','image/png',600,500,'2018-10-12 07:41:13','2018-10-12 07:41:13'),(102,783380349351692948,789009848778307044,'InlineRectangle','image/gif',300,250,'2018-10-17 06:34:39','2018-10-17 06:34:39'),(106,783380349351692948,789854273698843114,'SmartphoneInterstitialLandscape','image/png',480,320,'2018-10-19 06:33:41','2018-10-19 06:33:41'),(104,783380349351692948,789924642443029088,'SmartphoneInterstitialPortrait','image/png',320,480,'2018-10-19 06:33:12','2018-10-19 06:33:12'),(103,783380349351692948,789924642443032812,'SmartphoneInterstitialPortrait','image/png',320,480,'2018-10-19 06:32:53','2018-10-19 06:32:53'),(105,783380349351692948,789995011187203590,'SmartphoneInterstitialLandscape','image/png',480,320,'2018-10-19 06:33:33','2018-10-19 06:33:33'),(108,783380349351692948,790769067384266259,'Banner','image/gif',468,60,'2018-10-22 01:26:31','2018-10-22 01:26:31'),(107,783380349351692948,790909804872655951,'Banner','image/png',468,60,'2018-10-22 01:23:53','2018-10-22 01:23:53'),(109,783380349351692948,790980036168038021,'MobileLeaderboard','image/png',640,100,'2018-10-22 07:03:21','2018-10-22 07:03:21');
/*!40000 ALTER TABLE `tbl_ad_resource` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-10-25 17:24:32
