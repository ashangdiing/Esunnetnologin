
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SYS_Menu]') AND type in (N'U'))
DROP TABLE SYS_Menu
go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[User_RecordPermission]') AND type in (N'U'))
DROP TABLE User_RecordPermission
go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SYS_LoginLog]') AND type in (N'U'))
DROP TABLE SYS_LoginLog
go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SYS_Ext]') AND type in (N'U'))
DROP TABLE SYS_Ext
go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[User_HandleTask]') AND type in (N'U'))
DROP TABLE User_HandleTask
go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[User_TaskFlow]') AND type in (N'U'))
DROP TABLE User_TaskFlow
go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[User_Task]') AND type in (N'U'))
DROP TABLE User_Task
go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[User_ClientInfo]') AND type in (N'U'))
DROP TABLE User_ClientInfo
go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SYS_Dictionary]') AND type in (N'U'))
DROP TABLE SYS_Dictionary
go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SYS_DictionaryType]') AND type in (N'U'))
DROP TABLE SYS_DictionaryType
go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SYS_Area]') AND type in (N'U'))
DROP TABLE SYS_Area
go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[User_Knowledge]') AND type in (N'U'))
DROP TABLE User_Knowledge
go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[User_KnowledgeType]') AND type in (N'U'))
DROP TABLE User_KnowledgeType
go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SYS_User]') AND type in (N'U'))
DROP TABLE SYS_User
go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SYS_Permissions]') AND type in (N'U'))
DROP TABLE SYS_Permissions
go


create table SYS_Menu
(
	[id] int primary key,
	[pid] int foreign key references SYS_Menu(id) null,
	[text] varchar(20) not null,
	[title] varchar(100) not null default(''),
	[path] varchar(255),
	[function] varchar(100),
	[remark] varchar(200),
	[adminlook] bit not null default(1),
	[userlook] bit not null default(0),
	[look] bit not null default(1)
)
GO
create table SYS_Permissions
(
	id int identity (1,1) primary key,
	[text] varchar(20) not null,
	remark varchar(20) null,
	permission varchar(2000) null,
	allowDelete bit not null default(1),
	isShow bit not null default(1)
)
go
create table SYS_User
(
	id int identity(1,1) primary key,
	userId varchar(16) not null,
	userName varchar(20) null,
	userPassword char(32) not null,
	agentId varchar(10) null,
	lastLoginTime datetime,
	lastLoginIp varchar(15),
	permission varchar(2000) null,
	isDelete bit not null default(0),
	permId int foreign key references SYS_Permissions(id) not null,
	allowDelete bit not null default(1)
)
go
create table User_RecordPermission(
	--id int identity(1,1) primary key,--不采用增长列，修改权限采用删除后添加
	userId int unique foreign key references SYS_User(id) not null,
	type bit not null default(0),--类型是电话还是工号，0代表电话1代表工号
	num varchar(10) not null ,--可以采用like查询，以后做铺垫。
	Per int--1,2,4 1代表查询，2代表听，4代表下载。3代表能查询能听，7代表能查询能听能下载。
)
go
create table SYS_LoginLog
(
	id bigint identity(1,1) primary key,
	userId int foreign key references SYS_User(id),
	loginTime datetime not null default(getdate()),
	ip varchar(15) not null
)
go
create table SYS_Ext
(
	ext varchar(10) primary key,
	ip varchar(15),
	userIp varchar(15),
	mac char(17),
	userName varchar(20)
)
go
create table SYS_DictionaryType
(
	id int identity(1,1) primary key,
	[text] varchar(20)not null unique,
	typeCode varchar(20) not null,
	remark varchar(200),
	allowDelete bit not null default(1)
)
go
create table SYS_Dictionary
(
	id int identity(1,1) primary key,
	pid int foreign key references SYS_DictionaryType(id),
	[text] varchar(20) not null,
	[order] int not null default(0),
	remark varchar(200)
)
go
create table SYS_Area
(
	id int primary key,
	pid int foreign key references SYS_Area(id),
	[text] varchar(50) not null,
	zipCode char(6),
	[level] int
)
go
create table User_ClientInfo
(
	id bigint identity(1,1) primary key,
	[caller] varchar(20) not null unique,
	cardId varchar(20),
	name varchar(32),
	userId int foreign key references SYS_User(id),
	sex bit,
	clientType int foreign key references SYS_Dictionary(id),
	clientLevel int foreign key references SYS_Dictionary(id),
	birthday datetime,
	companyName varchar(50),
	[address] varchar(200),
	email varchar(100),
	province int,
	area int foreign key references SYS_Area(id),
	remark varchar(2000)
)
go
create table User_Task
(
	id int identity(1,1) primary key,
	clientId bigint foreign key references User_ClientInfo(id),
	userId int foreign key references SYS_User(id),
	whoTask int foreign key references SYS_User(id),
	title varchar(200) not null,
	taskType int foreign key references SYS_Dictionary(id),
	taskLevel int foreign key references SYS_Dictionary(id),
	remark varchar(2000) not null,
	taskState int foreign key references SYS_Dictionary(id)
)
go
create table User_TaskFlow
(
	id int identity(1,1) primary key,
	taskId int foreign key references User_Task(id) not null,
	flowUser int foreign key references SYS_User(id) not null,
	toUser int foreign key references SYS_User(id) not null,
	sendACopy varchar(2000)
)
go
create table User_HandleTask
(
	id int identity(1,1) primary key,
	taskId int foreign key references User_Task(id) not null,
	handleTime datetime not null default(getdate()),
	handleType int foreign key references SYS_Dictionary(id),
	remark varchar(2000) not null
)
go
create table User_KnowledgeType
(
	id  int identity(1,1) primary key,
	pid int foreign key references User_KnowledgeType(id),
	text varchar(20) not null,
	title varchar(200),
)
go
create table User_Knowledge
(
	id int identity(1,1) primary key,
	[type] int foreign key references User_KnowledgeType(id),
	content text not null,
	createTime datetime not null default(getdate()),
	expireTimt datetime,
	isDelete bit not null default(0),
)