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
-- Table structure for table `tbl_mining_history`
--

DROP TABLE IF EXISTS `tbl_mining_history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tbl_mining_history` (
  `Idx` bigint(20) NOT NULL AUTO_INCREMENT,
  `MiningHistoryId` bigint(20) NOT NULL,
  `UserId` bigint(20) NOT NULL,
  `CurrencyType` varchar(20) NOT NULL,
  `MiningAmount` decimal(20,10) NOT NULL,
  `MiningTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`MiningHistoryId`),
  UNIQUE KEY `MiningId_UNIQUE` (`MiningHistoryId`),
  UNIQUE KEY `Idx_UNIQUE` (`Idx`),
  KEY `IdxUserId` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_mining_history`
--

LOCK TABLES `tbl_mining_history` WRITE;
/*!40000 ALTER TABLE `tbl_mining_history` DISABLE KEYS */;
INSERT INTO `tbl_mining_history` VALUES (17,791543123804802665,783380349351692948,'HENA_MINING',0.0006834955,'2018-10-24 09:06:56'),(16,791543123804807797,783380349351692948,'HENA_MINING',0.0000857700,'2018-10-24 09:06:11'),(15,791543123804812562,783380349351692948,'HENA_MINING',0.0005095911,'2018-10-24 09:05:56'),(11,791543123805563607,783380349351692948,'HENA_MINING',0.0003739734,'2018-10-24 08:51:01'),(6,791543123805565736,783380349351692948,'HENA_MINING',0.0003956162,'2018-10-24 08:50:36'),(14,791613492548962250,783380349351692948,'HENA_MINING',0.0005461274,'2018-10-24 09:05:41'),(13,791613492549735704,783380349351692948,'HENA_MINING',0.0009294761,'2018-10-24 08:51:11'),(10,791613492549740465,783380349351692948,'HENA_MINING',0.0009031034,'2018-10-24 08:50:56'),(3,791683861293893062,783380349351692948,'HENA_MINING',0.0002199323,'2018-10-24 08:50:21'),(4,791683861293893860,783380349351692948,'HENA_MINING',0.0000858848,'2018-10-24 08:50:26'),(5,791683861293894666,783380349351692948,'HENA_MINING',0.0003348254,'2018-10-24 08:50:31'),(2,791683861293899424,783380349351692948,'HENA_MINING',0.0006747645,'2018-10-24 08:50:16'),(12,791683861293912570,783380349351692948,'HENA_MINING',0.0002709970,'2018-10-24 08:51:06'),(9,791683861293916307,783380349351692948,'HENA_MINING',0.0002072419,'2018-10-24 08:50:51'),(7,791683861293921871,783380349351692948,'HENA_MINING',0.0000093945,'2018-10-24 08:50:41'),(8,791683861293923693,783380349351692948,'HENA_MINING',0.0001798579,'2018-10-24 08:50:46'),(19,791754230037285829,783380349351692948,'HENA_MINING',0.0009005836,'2018-10-24 09:07:56'),(18,791754230037287006,783380349351692948,'HENA_MINING',0.0004534719,'2018-10-24 09:07:31'),(20,791754230037305514,783380349351692948,'HENA_MINING',0.0006565553,'2018-10-24 09:08:44'),(1,791754230038076290,783380349351692948,'HENA_MINING',0.0008424820,'2018-10-24 08:50:11');
/*!40000 ALTER TABLE `tbl_mining_history` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-10-24 18:26:42
