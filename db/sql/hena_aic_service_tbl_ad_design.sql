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
-- Table structure for table `tbl_ad_design`
--

DROP TABLE IF EXISTS `tbl_ad_design`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tbl_ad_design` (
  `Idx` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserId` bigint(20) NOT NULL,
  `CampaignId` bigint(20) NOT NULL,
  `AdDesignId` bigint(20) NOT NULL,
  `Name` varchar(80) NOT NULL,
  `AdResourceId` bigint(20) NOT NULL,
  `AdDesignType` varchar(45) NOT NULL DEFAULT 'None' COMMENT 'None, MobileLeaderboard, MobileBannerLandscape, LargeMobileBanner, Banner, Leaderboard, InlineRectangle, SmartphoneInterstitialPortrait, SmartphoneInterstitialLandscape, TabletInterstitialPortrait, TabletInterstitialLandscape',
  `DestinationUrl` text NOT NULL,
  `IsPause` tinyint(4) NOT NULL DEFAULT '0',
  `CreateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastUpdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`UserId`,`CampaignId`,`AdDesignId`),
  UNIQUE KEY `Idx_UNIQUE` (`Idx`),
  UNIQUE KEY `CampaignDesignId_UNIQUE` (`AdDesignId`),
  KEY `Idx_AdDesignType` (`AdDesignType`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_ad_design`
--

LOCK TABLES `tbl_ad_design` WRITE;
/*!40000 ALTER TABLE `tbl_ad_design` DISABLE KEYS */;
INSERT INTO `tbl_ad_design` VALUES (27,791472617497798814,792457779829684238,792387411101549553,'배너 1',792598517284369868,'Banner','comic.naver.com/webtoon/weekday.nhn',0,'2018-10-26 04:40:30','2018-10-26 04:41:52');
/*!40000 ALTER TABLE `tbl_ad_design` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-10-26 13:48:36
