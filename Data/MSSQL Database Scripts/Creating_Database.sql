USE master;
GO

IF DB_ID('haircutSalonDB') IS NOT NULL
DROP DATABASE haircutSalonDB;
GO

CREATE DATABASE haircutSalonDB;
GO

USE haircutSalonDB;