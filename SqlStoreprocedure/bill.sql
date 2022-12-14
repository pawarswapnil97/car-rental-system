USE [carrental]
GO
/****** Object:  StoredProcedure [dbo].[car_bill]    Script Date: 05/30/2022 12:52:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER proc [dbo].[car_bill]

@Billno int='',
@Billdate varchar(max)='',
@Cname varchar(max)='',
@carmodel varchar(max)='',
@Rent int='',
@Fuel varchar(max)='',
@Startkm int='',
@Endkm int='',
@Totalkm int='',
@Amount int='',
@Discount int='',
@Total int='',
@flag varchar(50)=''
as 
if (@flag='insert')
begin
insert into Bill(Billdate,Cname,carmodel,Rent,Fuel,Startkm,Endkm,Totalkm ,Amount,Discount,Total)values(@Billdate,@Cname,@carmodel,@Rent,@Fuel,@Startkm,@Endkm,@Totalkm ,@Amount,@Discount,@Total)
end
if(@flag='update')
begin
update Bill set Billdate=@Billdate, Cname=@Cname,carmodel=@carmodel,Rent=@Rent,Fuel=@Fuel,Startkm=@Startkm,Endkm=@Endkm,Totalkm=@Totalkm ,Amount=@Amount,Discount=@Discount,Total=@Total where Billno=@Billno
end
if(@flag='delete')
begin
delete from Bill where Billno=@Billno
end
if(@flag='search')
begin
select * from Bill where Cname  like @Cname+'%'
end
if(@flag='getall')
begin
select * from Bill
end


