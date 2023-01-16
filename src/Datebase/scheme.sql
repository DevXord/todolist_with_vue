CREATE SCHEMA `todolistvue` ;

CREATE TABLE `tasks` (
  `ID_Task` int(11) NOT NULL AUTO_INCREMENT,
  `Task` varchar(305) DEFAULT NULL,
  `DataEnd` datetime DEFAULT NULL,
  `DataCreate` datetime DEFAULT NULL,
  `IsDone` tinyint(4) DEFAULT 0,
  PRIMARY KEY (`ID_Task`),
  UNIQUE KEY `ID_Task_UNIQUE` (`ID_Task`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

