﻿delete SYS_Menu
insert into SYS_Menu([id],[text],[title],[userlook])values(1,'系统管理','系统管理',1)
insert into SYS_Menu([id],[pid],[text],[title],[userlook],[path],[function])values(2,1,'密码修改','用户密码修改',1,'js\page\record\SelectRecord.js','Global.System.UpdatePassword')
insert into SYS_Menu([id],[pid],[text],[title],[userlook],[path],[function])values(3,1,'用户管理','用户管理',1,'Scripts/System/User.js','Global.System.User')
--insert into SYS_Menu([id],[pid],[text],[title],[userlook],[path],[function])values(4,1,'数据字典','数据字典维护',0,'Scripts/System/Dictionary.js','Global.System.Dictionary')
insert into SYS_Menu([id],[pid],[text],[title],[userlook],[path],[function])values(5,1,'分机管理','分机管理',0,'Scripts/System/Ext.js','Global.System.Ext')
--insert into SYS_Menu([id],[text],[title],[userlook])values(6,'客户管理','客户管理',1)
--insert into SYS_Menu([id],[pid],[text],[title],[userlook],[path],[function])values(7,6,'客户信息','客户信息',1,'Scripts/Client/Info.js','Global.Client.Info')
--insert into SYS_Menu([id],[text],[title],[userlook])values(8,'工单管理','工单管理',1)
--insert into SYS_Menu([id],[text],[title],[userlook])values(9,'知识库','知识库',1)
insert into SYS_Menu([id],[text],[title],[userlook])values(10,'录音','录音',1)
insert into SYS_Menu([id],[pid],[text],[title],[userlook],[path],[function])values(11,10,'录音查询','录音查询',1,'js/page/record/SelectRecord.js','SelectRecord')
insert into SYS_Menu([id],[pid],[text],[title],[userlook],[path],[function])values(12,10,'录音权限','录音权限修改',1,'Scripts/Record/Permission.js','Global.Record.Permission')
insert into SYS_Menu([id],[pid],[text],[title],[userlook],[path],[function])values(13,10,'留言查询','留言查询',1,'Scripts/Record/Leave.js','Global.Record.Leave')
insert into SYS_Menu([id],[pid],[text],[title],[userlook],[path],[function])values(14,10,'系统信息','系统信息',1,'SystemInfo.html','html')

insert into SYS_Menu([id],[text],[title],[userlook])values(15,'报表','报表',1)
insert into SYS_Menu([id],[pid],[text],[title],[userlook],[path],[function],[remark])values(16,15,'电话统计','电话统计',1,'js/page/report/Report.js','Report','UP_Report_Total')
insert into SYS_Menu([id],[pid],[text],[title],[userlook],[path],[function],[remark])values(17,15,'通话时长统计','通话时长统计',1,'js/page/report/Report.js','Report','UP_Report_DateTotal')
insert into SYS_Menu([id],[pid],[text],[title],[userlook],[path],[function],[remark])values(18,15,'呼叫统计','呼叫统计',1,'js/page/report/QueryReport.js','QueryReport','UP_CallPort')
insert into SYS_Menu([id],[pid],[text],[title],[userlook],[path],[function],[remark])values(19,15,'呼叫记录','呼叫记录',1,'js/page/report/QueryReport.js','QueryReport','UP_CallTotal')

insert into SYS_Menu([id],[pid],[text],[title],[userlook])values(88,1,'退出','退出',1)
insert into SYS_Permissions(text,remark,allowDelete,isShow)values('系统管理员','系统管理员',0,0)
insert into SYS_Permissions(text,remark,allowDelete)values('管理员','管理员',0)
insert into SYS_Permissions(text,remark,allowDelete,permission)values('班长坐席','班长坐席',1,'1,2,10,11,88')
insert into SYS_Permissions(text,remark,allowDelete,permission)values('普通坐席','普通坐席',1,'1,2,10,11,88')

insert into SYS_User(userId,userPassword,permId,allowDelete)values('esunnet',SUBSTRING(sys.fn_sqlvarbasetostr(HASHBYTES('MD5','esun5005')),3,32),1,0)
insert into SYS_User(userId,userPassword,permId,allowDelete)values('admin',SUBSTRING(sys.fn_sqlvarbasetostr(HASHBYTES('MD5','admin')),3,32),2,0)
