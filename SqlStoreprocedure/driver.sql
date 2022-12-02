USE [carrental]
GO
/****** Object:  StoredProcedure [dbo].[car_driver]    Script Date: 05/30/2022 12:54:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Batch submitted through debugger: SQLQuery2.sql|0|0|C:\Users\omkar\AppData\Local\Temp\~vs73F0.sql
ALTER proc [dbo].[car_driver]
@did int=0,
@dname varchar (max)='',
@dmobno varchar (max)='',
@dadhar varchar(max)='',
@dlno varchar(max)='',
@dadd varchar(max)='',
@flag varchar(50)=''
as 
if (@flag='insert')
begin
insert into driver(dname, dmobno, dadhar,dlno,dadd)values(@dname, @dmobno, @dadhar,@dlno,@dadd)
end
if(@flag='update')
begin
update driver set dname=@dname, dmobno=@dmobno, dadhar=@dadhar,dlno=@dlno,dadd=@dadd where did=@did
end
if(@flag='delete')
begin
delete from driver where did=@did
end
if(@flag='search')
begin
select * from driver where dname like @dname+'%'
end
if(@flag='getall')
begin
select * from driver 
end


