IF NOT EXISTS (SELECT name FROM master.sys.databases WHERE name = N'RoseITAssignmentDB')
BEGIN
CREATE DATABASE RoseITAssignmentDB
END
GO
USE RoseITAssignmentDB
GO
IF NOT EXISTS(SELECT * FROM sys.schemas WHERE name = N'Customer')
BEGIN
PRINT 'CREATE Customer SCHEMA'
EXEC ('CREATE SCHEMA Customer')
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Customer].[CustomerWiseBonusRate]') AND type in (N'U'))
BEGIN
CREATE TABLE Customer.CustomerWiseBonusRate
(
	Id int Identity(1000,1) primary key,
	CustomerId bigint NOT NULL unique,
    CustomerName VARCHAR (50) NOT NULL,
	BonusRate DECIMAL(4,2),
	BonusDate DATE
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Customer].[CustomerInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE Customer.CustomerInfo
(
	Id int primary key,
	CustomerId bigint NOT NULL unique,
    CustomerName VARCHAR (50) NOT NULL
)
END
GO
------------------------------------------
-- truncate table Customer.CustomerInfo
-----------------------------------------
select * from Customer.CustomerInfo
GO
select * from Customer.CustomerWiseBonusRate 

