create database Alchemy;
use Alchemy;

create table User
(
  Id varchar(36) NOT NULL ,
  UserName varchar(255) DEFAULT NULL,
  Account varchar(255) DEFAULT NULL,
  PasswordContent varchar(255) DEFAULT NULL,
  Version int default 0,
  PRIMARY KEY (Id)
);