USE [carrental]
GO
/****** Object:  StoredProcedure [dbo].[car_cardetail]    Script Date: 05/30/2022 12:53:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Batch submitted through debugger: SQLQuery1.sql|0|0|C:\Users\omkar\AppData\Local\Temp\~vsEFF5.sql

ALTER proc [dbo].[car_cardetail]
@carid int=0,
@carmodel varchar (max)='',
@carno varchar (max)='',
@chassisno varchar(max)='',
@caravg varchar(max)='',
@rent varchar(max)='',
@fuel varchar(max)='',
@seat varchar(max)='',
@flag varchar(50)=''
as 
if (@flag='insert')
begin
insert into car(carmodel, carno,chassisno,caravg,rent,fuel,seat)values( @carmodel, @carno,@chassisno,@caravg,@rent,@fuel,@seat)
end
if(@flag='update')
begin
update car set  carmodel=@carmodel, carno=@carno,chassisno=@chassisno,caravg=@caravg,rent=@rent,fuel=@fuel,seat=@seat where carid=@carid
end
if(@flag='delete')
begin
delete from car where carid=@carid
end
if(@flag='search')
begin
select * from car where carmodel like @carmodel+'%'
end
if(@flag='getall')
begin
select * from car
end


