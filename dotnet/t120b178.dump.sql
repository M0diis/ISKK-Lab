-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: db
-- Generation Time: Jul 31, 2022 at 05:51 PM
-- Server version: 10.7.3-MariaDB-1:10.7.3+maria~focal
-- PHP Version: 8.0.20

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

--
-- Database: `t120b178`
--

-- --------------------------------------------------------

--
-- Table structure for table `DemoEntities`
--

CREATE TABLE `DemoEntities` (
  `id` int(11) NOT NULL,
  `date` datetime NOT NULL,
  `name` mediumtext NOT NULL,
  `condition` int(11) NOT NULL,
  `deletable` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `DemoEntities`
--

INSERT INTO `DemoEntities` (`id`, `date`, `name`, `condition`, `deletable`) VALUES
(1, '2022-07-04 00:00:00', 'dogo', 9, 1),
(2, '2022-07-03 19:15:12', 'dog', 5, 1),
(3, '2022-07-03 19:15:12', 'dog', 5, 1),
(4, '2022-07-03 19:15:12', 'dog', 5, 1),
(5, '2022-07-03 19:15:12', 'dog', 5, 1),
(6, '2022-07-03 19:15:12', 'dog', 5, 1),
(7, '2022-07-03 19:15:12', 'dog', 5, 1),
(8, '2022-07-03 19:15:12', 'dog', 5, 1),
(9, '2022-07-03 19:15:12', 'dog', 5, 1),
(10, '2022-07-03 19:15:12', 'dog', 5, 1),
(11, '2022-07-03 19:15:12', 'dog', 5, 1),
(12, '2022-07-03 19:15:12', 'dog', 5, 1),
(13, '2022-07-03 19:15:12', 'dog', 5, 1),
(14, '2022-07-03 19:15:12', 'dog', 5, 1),
(15, '2022-07-03 19:15:12', 'dog', 5, 1),
(16, '2022-07-03 19:15:12', 'dog', 5, 1),
(17, '2022-07-03 19:15:12', 'dog', 5, 1),
(18, '2022-07-03 19:15:12', 'dog', 5, 1),
(19, '2022-07-03 19:15:12', 'dog', 5, 1),
(20, '2022-07-03 19:15:12', 'dog', 5, 1),
(21, '2022-07-03 19:15:12', 'dog', 5, 1),
(22, '2022-07-06 19:15:24', 'aaa', 0, 1),
(23, '2022-07-06 19:15:24', 'aaa', 8, 1),
(24, '2022-07-06 19:23:01', 'bbb', 0, 1),
(25, '2022-07-06 19:23:01', 'bbb', 0, 0),
(26, '2022-07-06 19:25:17', 'ccc', 0, 1),
(27, '2022-07-06 19:25:17', 'ccc', 7, 1),
(30, '2022-07-07 00:00:00', 'q2', 0, 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `DemoEntities`
--
ALTER TABLE `DemoEntities`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `DemoEntities`
--
ALTER TABLE `DemoEntities`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=33;
COMMIT;
