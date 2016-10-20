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

2016-09-13:
alter table User add column Email varchar(255);
alter table User add column PhoneNumber varchar(255);
alter table User add column ShopUserNameId varchar(255);
alter table User add column UserType int default 1;
alter table User add column Status int default 0;
alter table User add column AddTime datetime default now();
alter table User add column LastLoginTime datetime default now();

create table Product
(
  Id varchar(36) NOT NULL ,
  ProductName varchar(255) DEFAULT NULL,
  Url  varchar(255) DEFAULT NULL,
  Price double DEFAULT 0,
  DiscountPrice double DEFAULT 0,
  PictureUrl varchar(255) DEFAULT NULL,
  UserId varchar(36) NOT NULL,
  AddTime datetime DEFAULT now(),
  Version int default 0,
  PRIMARY KEY (Id)
);

2016-09-18:
alter table User add column ShopTitle varchar(255);
alter table Product add column ShopTitle varchar(255);

2016-09-20:
alter table User change PasswordContent SecureCode varchar(255);

2016-10-09:
create table Coupon
(
  Id varchar(36) NOT NULL ,
  UserId varchar(36) NOT NULL ,
  BaseUrl varchar(255) DEFAULT NULL,
  Title varchar(255) DEFAULT NULL,
  ShopName varchar(255) DEFAULT NULL,
  Url  varchar(255) DEFAULT NULL,
  ProductUrl  varchar(255) DEFAULT NULL,
  PictureUrl varchar(255) DEFAULT NULL,
  ProductDescription  varchar(255) DEFAULT NULL,
  RestCount int DEFAULT 0,
  Description text,
  StartDate datetime DEFAULT now(),
  EndDate datetime DEFAULT now(),
  AddTime datetime DEFAULT now(),
  Version int default 0,
  IsExpired bit default 0,
  PRIMARY KEY (Id)
);

2016.10.20:
alter table Coupon add column PictureUrl varchar(255);