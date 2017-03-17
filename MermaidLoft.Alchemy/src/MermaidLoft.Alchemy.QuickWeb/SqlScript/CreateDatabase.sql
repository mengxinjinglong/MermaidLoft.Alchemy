create database Alchemy CHARACTER SET 'utf8' COLLATE 'utf8_general_ci';
use Alchemy;

create table User
(
  Id varchar(36) NOT NULL ,
  UserName varchar(512) NOT NULL,
  Account varchar(512) DEFAULT NULL,
  SecureCode varchar(512) DEFAULT NULL,
  Email varchar(255),
  PhoneNumber varchar(512),
  ShopUserNameId varchar(512),
  ShopTitle varchar(512),
  UserType int default 1,
  Status int default 0,
  Version int default 0,
  AddTime datetime default null,
  LastLoginTime datetime default null,
  PRIMARY KEY (Id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

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
  ProductName varchar(512) DEFAULT NULL,
  ShopTitle varchar(512),
  Url  varchar(512) DEFAULT NULL,
  Price double DEFAULT 0,
  DiscountPrice double DEFAULT 0,
  PictureUrl varchar(512) DEFAULT NULL,
  UserId varchar(36) NOT NULL,
  AddTime datetime DEFAULT null,
  Version int default 0,
  PRIMARY KEY (Id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

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
  BaseUrl varchar(512) DEFAULT NULL,
  Title varchar(512) DEFAULT NULL,
  ShopName varchar(512) DEFAULT NULL,
  Url  varchar(512) DEFAULT NULL,
  ProductUrl  varchar(512) DEFAULT NULL,
  PictureUrl varchar(512) DEFAULT NULL,
  ProductDescription  varchar(512) DEFAULT NULL,
  RestCount int DEFAULT 0,
  Description text,
  StartDate datetime DEFAULT null,
  EndDate datetime DEFAULT null,
  AddTime datetime DEFAULT null,
  Version int default 0,
  IsExpired bit default 0,
  PRIMARY KEY (Id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

2016.11.22:
alter table Coupon change PictureUrl PictureUrl varchar(512);
alter table Coupon change ProductUrl ProductUrl varchar(512);
alter table Coupon change Url Url varchar(512);
alter table Coupon change BaseUrl BaseUrl varchar(512);

2016.12.03:
alter table Coupon add column ProductName varchar(512);
alter table Coupon add column ProductType varchar(512);
alter table Coupon add column Price double;

2017.03.14:

create table BaseResource
(
  Id varchar(36) NOT NULL ,
  ParentId varchar(36) NOT NULL ,
  Name varchar(512) DEFAULT NULL,
  Type varchar(512),
  Description varchar(512),
  AddTime datetime DEFAULT null,
  EditTime datetime DEFAULT null,
  Status int default 0,
  PRIMARY KEY (Id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

create table Article
(
  Id varchar(36) NOT NULL ,
  ArticleId int NOT NULL auto_increment ,
  AuthorId varchar(36) NOT NULL ,
  CategoryId varchar(36) DEFAULT NULL,
  Title varchar(512) DEFAULT NULL,
  Tags varchar(512) DEFAULT NULL,
  Content text,
  AddTime datetime DEFAULT null,
  EditTime datetime DEFAULT null,
  Hits bigint default 0,
  Status int default 0,
  PRIMARY KEY (ArticleId)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;