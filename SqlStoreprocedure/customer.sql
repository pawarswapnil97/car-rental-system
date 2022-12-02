USE [carrental]
GO
/****** Object:  StoredProcedure [dbo].[car_customer]    Script Date: 05/30/2022 12:53:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Batch submitted through debugger: SQLQuery1.sql|0|0|C:\Users\omkar\AppData\Local\Temp\~vs23C2.sql
ALTER proc [dbo].[car_customer]
@cid int=0,
@cname varchar (max)='',
@cmob varchar (max)='',
@cadhar varchar(max)='',
@clino varchar(max)='',
@cadd varchar(max)='',
@flag varchar(50)=''
as 
if (@flag='insert')
begin
insert into customer(cname, cmob, cadhar,clino,cadd)values(@cname, @cmob, @cadhar,@clino,@cadd)
end
if(@flag='update')
begin
update customer set cname=@cname, cmob=@cmob,cadhar=@cadhar,clino=@clino,cadd=@cadd where cid=@cid
end
if(@flag='delete')
begin
delete from customer where cid=@cid
end
if(@flag='search')
begin
select * from customer where cname like @cname+'%'
end
if(@flag='getall')
begin
select * from customer 
end


