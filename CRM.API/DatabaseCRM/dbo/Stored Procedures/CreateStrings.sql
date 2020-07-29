CREATE procedure [dbo].[CreateStrings]
@rowValue bigint
as 
begin
	create table #RandomGender
		(id int,
		gender nchar(1))

	insert into #RandomGender
		(id, gender)
		select 1,'W' union
		select 2,'M' 

	create table #RandomFirstNames
		(id int,
		 [name] nvarchar(20),
		 gender nchar(1))

	insert into #RandomFirstNames
		(id, [name], gender)
		select 1,'Anatoliy','M' union
		select 2,'Anton','M' union
		select 3,'Arkadiy','M' union
		select 4,'Artur','M' union
		select 5,'Boris','M' union
		select 6,'Vadim','M' union
		select 7,'Valentin','M' union
		select 8,'Valeriy','M' union
		select 9,'Viktor','M' union
		select 10,'Vitaliy','M' union
		select 11,'Vladimir','M' union
		select 12,'Vladislav','M' union
		select 13,'Vyacheslav','M' union
		select 14,'Gennadiy','M' union
		select 15,'Georgiy','M' union
		select 16,'Denis','M' union
		select 17,'Dmitriy','M' union
		select 18,'Egor','M' union
		select 19,'Ivan','M' union
		select 20,'Igor','M' union
		select 21,'Ilya','M' union
		select 22,'Kirill','M' union
		select 23,'Konstantin','M' union
		select 24,'Leonid','M' union
		select 25,'Maksim','M' union
		select 26,'Alena','W' union
		select 27,'Alina','W' union
		select 28,'Alla','W' union
		select 29,'Angelina','W' union
		select 30,'Anzhela','W' union
		select 31,'Anna','W' union
		select 32,'Valentina','W' union
		select 33,'Vera','W' union
		select 34,'Galina','W' union
		select 35,'Diana','W' union
		select 36,'Elena','W' union
		select 37,'Elizaveta','W' union
		select 38,'Zoya','W' union
		select 39,'Inna','W' union
		select 40,'Irina','W' union
		select 41,'Kira','W' union
		select 42,'Kristina','W' union
		select 43,'Larisa','W' union
		select 44,'Margarita','W' union
		select 45,'Nina','W' union
		select 46,'Oksana','W' union
		select 47,'Olesya','W' union
		select 48,'Olga','W' union
		select 49,'Polina','W' union
		select 50,'Regina','W'

	create table #RandomLastNames
		(id int,
		 lastName nvarchar(20),
		 gender nchar(1))

	insert into #RandomLastNames
		(id, lastName, gender)
		select 1,'Sobol','М' union
		select 2,'Aleksandrova','W' union
		select 3,'Babushkin','М' union
		select 4,'Vasilyev','М' union
		select 5,'Georgiyev','М' union
		select 6,'Dudinsky','М' union
		select 7,'Yeremeyeva','W' union
		select 8,'Vorobyov','М' union
		select 9,'Strizh','М' union
		select 10,'Strizh','W' union
		select 11,'Yazova','W' union
		select 12,'Ilyina','W' union
		select 13,'Ilyin','М' union
		select 14,'Zaytsev','М' union
		select 15,'Akopyan','М' union
		select 16,'Akopyan','W' union
		select 17,'Kovalsky','М' union
		select 18,'Timmerman','М' union
		select 19,'Timmerman','W' union
		select 20,'Anichkina','W' union
		select 21,'Stroyev','М' union
		select 22,'Lapshin','М' union
		select 23,'Lapshinа','W' union
		select 24,'Nuriyev','М' union
		select 25,'Nuriyeva','W' union
		select 26,'Stasova','W' union
		select 27,'Terentyeva','W' union
		select 28,'Bushuyeva','W' union
		select 29,'Frolova','W' union
		select 30,'Tikhonov','M' union
		select 31,'Kuznetsov','M' union
		select 32,'Chistovich','M' union
		select 33,'Shishkina','W' union
		select 34,'Tatishchev','M' union
		select 35,'Malysheva','W' union
		select 36,'Malyshev','M' union
		select 37,'Kasyanova','W' union
		select 38,'Kasyanov','M' union
		select 39,'Etush','W' union
		select 40,'Etush','M' union
		select 41,'Tyurina','W' union
		select 42,'Tyurin','M' union
		select 43,'Smelyanskaya','W' union
		select 44,'Smelyanskay','M' union
		select 45,'Aleksandrov','M' union
		select 46,'Babushkina','W' union
		select 47,'Vasilyeva','W' union
		select 48,'Georgiyeva','W' union
		select 49,'Yeremeyev','M' union
		select 50,'Vorobyova','W'

	create table #RandomPatronymic
		(id int,
		[patronymic] nvarchar(30),
		gender nchar(1))

	insert into #RandomPatronymic
		(id, [patronymic], gender)
		select 1,'Andreevna','W' union
		select 2,'Andreevich','M' union
		select 3,'Ivanovna','W' union
		select 4,'Ivanovich','M' union
		select 5,'Matveevna','W' union
		select 6,'Matveevich','M' union
		select 7,'Victorovna','W' union
		select 8,'Victorovich','M' union
		select 9,'Aleksandrovna','W' union
		select 10,'Aleksandrovich','M' union
		select 11,'Alekseevna','W' union
		select 12,'Alekseevich','M' union
		select 13,'Sergeevna','W' union
		select 14,'Sergeevich','M' union
		select 15,'Dmitrievna','W' union
		select 16,'Dmitrievich','M' union
		select 17,'Petrovna','W' union
		select 18,'Petrovich','M' union
		select 19,'Stanislavovna','W' union
		select 20,'Stanislavovich','M' union
		select 21,'Vladislavovna','W' union
		select 22,'Vladislavovich','M' union
		select 23,'Vyacheslavovna','W' union
		select 24,'Vyacheslavovich','M' union
		select 25,'Olegovna','W' union
		select 26,'Maksimovna','W' union
		select 27,'Maksimovich','M' union
		select 28,'Olegovich','M' union
		select 29,'Grigorievna','W' union
		select 30,'Grigorievich','M' union
		select 31,'Igorevna','W' union
		select 32,'Igorevich','M' union
		select 33,'Artemovna','W' union
		select 34,'Artemovich','M' union
		select 35,'Egorovna','W' union
		select 36,'Egorovich','M' union
		select 37, 'Vladimirovna', 'W' union
		select 38,'Vladimirovich','M' union
		select 39,'Konstantinovna','W' union
		select 40,'Konstantinovich','M' union
		select 41,'Valentinovna','W' union
		select 42,'Valentinovich','M' union
		select 43,'Kirillovna','W' union
		select 44,'Kirillovich','M' union
		select 45,'Denisovna','W' union
		select 46,'Denisovich','M' union
		select 47,'Pavlovna', 'W' union
		select 48,'Pavlovich','M' union
		select 49,'Benidiktovna','W' union
		select 50,'Benidiktovich','M'

	create table #RandomAddress
		(id int,
		 street nvarchar(90), 
		 house int)

	insert into #RandomAddress
		(id, street, house)
		select 1,'Kosmonavtov Street','1' union
		select 2,'9 Maya Street','2' union
		select 3,'Nevsky Prospect','3' union
		select 4,'Liteyny Prospect','4' union
		select 5,'Ligovskiy Prospekt','5' union
		select 6,'Admiralteysky Prospect','6' union
		select 7,'Sadovaya Street','7' union
		select 8,'Griboedov Channel','8' union
		select 9,'Fontanka river embankment','9' union
		select 10,'Dvortsovaya embankment','10' union
		select 11,'Marsovo Polye','11' union
		select 12,'1 liniya, Vasilievsky island','12' union
		select 13,'2 liniya, Vasilievsky island','13' union
		select 14,'3 liniya, Vasilievsky island','14' union
		select 15,'4 liniya, Vasilievsky island','15' union
		select 16,'5 liniya, Vasilievsky island','16' union
		select 17,'6 liniya, Vasilievsky island','17' union
		select 18,'7 liniya, Vasilievsky island','18' union
		select 19,'8 liniya, Vasilievsky island','19' union
		select 20,'9 liniya, Vasilievsky island','20' union
		select 21,'10 liniya, Vasilievsky island','21' union
		select 22,'11 liniya, Vasilievsky island','22' union
		select 23,'13 liniya, Vasilievsky island','23' union
		select 24,'14 liniya, Vasilievsky island','24' union
		select 25,'Malaya Sadovaya Ulitsa','25' union
		select 26,'Bolshaya Morskaya Ulitsa','26' union
		select 27,'Ulitsa Zodchego Rossi','27' union
		select 28,'Malaya Konyushennaya Ulitsa','28' union
		select 29,'Gorokhovaya Ulitsa','29' union
		select 30,'Millionnaya Ulitsa','30' union
		select 31,'1st Alekseevskaya Street','31' union
		select 32,'1st Birch Alley','32' union
		select 33,'2nd Duck Street','33' union
		select 34,'Akhmatova Street','34' union
		select 35,'Alexander Popov Street','35' union
		select 36,'Alexander Ulyanov Street','36' union
		select 37,'Aptekarskaya nab','37' union
		select 38,'Arsenal Street','38' union
		select 39,'Artillery Street','39' union
		select 40,'Arts Square','40' union
		select 41,'Aviation Street','41' union
		select 42,'Avtovskaya Street','42' union
		select 43,'Balkan Square','43' union
		select 44,'Baltic Street','44' union
		select 45,'Baroque Street','45' union
		select 46,'Bering Street','46' union
		select 47,'Big Marsh Street','47' union
		select 48,'Black River embankment','48' union
		select 49,'Brotherly street','49' union
		select 50,'Burenina Street','50'

	declare @length int =0
	declare @currentDate datetime = getdate()

	while @length < @rowValue
		begin
			declare @gender nchar(1)
			declare @roleId int = 3
			declare @firstName	nvarchar(30)
			declare @lastName	nvarchar(30)
			declare @patronymic	nvarchar(30)
			declare @login		nvarchar(30)
			declare @password	nvarchar(64) = '9b18947ad9854ce3f6c5264081fd49e394888986f3ce26bbfbe0db90dbb6da10'
			declare @phone		nvarchar(20)
			declare @email		nvarchar(40)
			declare @cityId		int
			declare @address	nvarchar(90)
			declare @street	nvarchar(90)
			declare @hause	nvarchar(10)
			declare @flat	nvarchar(10)
			declare @birthDate	date
			declare @registrationDate datetime2 
			declare @changeDate datetime2
			declare @lengthForLogin int = 10
			declare @randomForLogin nvarchar(10) = ''
			declare @lengthForPhone int = 10
			declare @randomForPhone nvarchar(10) = ''

			set @gender =(select top 1 gender
							from #RandomGender
							order by newid())

			set @firstName =(select top 1 [name]
							from #RandomFirstNames
							where gender = @gender
							order by newid())

			set @lastName =(select top 1 lastName
							from #RandomLastNames
							where gender = @gender
							order by newid())

			set @patronymic=(select top 1 [patronymic]
							from #RandomPatronymic
							where gender = @gender
							order by newid())

			set @street=(select top 1 street
							from #RandomAddress
							order by newid())

			set @hause=(select top 1 house
							from #RandomAddress
							order by newid())

			set @flat=(select top 1 house
							from #RandomAddress
							order by newid())
			set @address = @street+@hause+@flat

			set @cityId =(select top 1 Id
							from City
							order by newid())
			
			while @lengthForLogin <> 0
				begin
					select @randomForLogin=@randomForLogin + CHAR(CAST(RAND()*10+48 as int))
					set @lengthForLogin = @lengthForLogin-1
				end
			set @login = @firstName+@lastName+@randomForLogin

			while @lengthForPhone <> 0
				begin
					select @randomForPhone=@randomForPhone + CHAR(CAST(RAND()*10+48 as int))
					set @lengthForPhone = @lengthForPhone-1
				end
			set @phone = '+7'+@randomForPhone

			set @email = @login+'@gmail.com'

			set @birthDate = dateAdd(day, rand()*(10000-1)+1, '1970-01-01')

			set @registrationDate = dateAdd(day, rand()*(1500-1)+1, '2010-01-01')

			set @changeDate = @currentDate

			insert into dbo.[Lead](	
					RoleId, 
					FirstName, 
					LastName, 
					Patronymic, 
					[Login], 
					[Password], 
					Phone, 
					Email, 
					CityId, 
					[Address], 
					BirthDate, 
					RegistrationDate, 
					ChangeDate, 
					IsDeleted) 
					values (@roleId, 
							@firstName, 
							@lastName, 
							@patronymic, 
							@login, 
							@password, 
							@phone, 
							@email, 
							@cityId, 
							@address, 
							@birthDate, 
							@registrationDate, 
							@changeDate, 
							default);
			set @length=@length+1
		end
	drop table #RandomGender
	drop table #RandomFirstNames
	drop table #RandomLastNames
	drop table #RandomAddress
	drop table #RandomPatronymic
end


