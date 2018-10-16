-- MySQL dump 10.13  Distrib 8.0.12, for Win64 (x86_64)
--
-- Host: localhost    Database: hena_aic_report
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
-- Table structure for table `tbl_ad_history`
--

DROP TABLE IF EXISTS `tbl_ad_history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tbl_ad_history` (
  `Idx` bigint(20) NOT NULL AUTO_INCREMENT,
  `AdHistoryId` bigint(20) NOT NULL,
  `PublisherId` bigint(20) NOT NULL,
  `AppId` bigint(20) NOT NULL,
  `AdUnitId` bigint(20) NOT NULL,
  `AdvertiserId` bigint(20) NOT NULL COMMENT '광고주 ID',
  `CampaignId` bigint(20) NOT NULL,
  `AdDesignId` bigint(20) NOT NULL,
  `IPAddress` varchar(45) NOT NULL,
  `UserAgent` text NOT NULL,
  `CampaignType` varchar(10) NOT NULL DEFAULT '' COMMENT 'ex) CPM, CPC',
  `AdDesignType` varchar(45) NOT NULL DEFAULT 'None' COMMENT 'None, MobileLeaderboard, MobileBannerLandscape, LargeMobileBanner, Banner, Leaderboard, InlineRectangle, SmartphoneInterstitialPortrait, SmartphoneInterstitialLandscape, TabletInterstitialPortrait, TabletInterstitialLandscape',
  `Cost` decimal(20,10) NOT NULL DEFAULT '0.0000000000',
  `IsDisplayed` tinyint(4) NOT NULL DEFAULT '0',
  `DisplayTime` datetime DEFAULT NULL,
  `IsClicked` tinyint(4) NOT NULL DEFAULT '0',
  `ClickTime` datetime DEFAULT NULL,
  `CreateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastUpdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Idx`,`AdHistoryId`),
  UNIQUE KEY `Idx_UNIQUE` (`Idx`),
  UNIQUE KEY `AdHistoryId_UNIQUE` (`AdHistoryId`),
  KEY `Idx_PublisherId` (`PublisherId`) /*!80000 INVISIBLE */,
  KEY `Idx_AdvertiserId` (`AdvertiserId`) /*!80000 INVISIBLE */,
  KEY `Idx_AppId` (`AppId`) /*!80000 INVISIBLE */,
  KEY `Idx_AdUnitId` (`AdUnitId`),
  KEY `Idx_CampaignId` (`CampaignId`),
  KEY `Idx_AdDesignId` (`AdDesignId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-10-16 18:20:37
