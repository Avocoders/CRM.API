using System;
namespace CRM.NUnitTest
{
	public static class Queries
	{
		public const string fillTestBase =

			 "Insert Into Lead" +

					   "(FirstName," +
						"LastName," +
						"Patronymic," +
						"Login," +
						"Password," +
						"Phone," +
						"Email," +
						"CityId," +
						"Address," +
						"BirthDate," +
						"RoleId," +
						"RegistrationDate," +
						"changeDate," +
						"IsDeleted) Values" +
 "('Alena','Nuratova','Nikolaevna','AlenaNurashka7639','9b18947ad9854ce3f6c5264081fd49e394888986f3ce26bbfbe0db90dbb6da10','+79261111111','alenanuratova@gmail.com',3,'Kaliningradskaya, 25, 5','1970-01-01',3,'2020-01-01','2020-01-01', 0)," +
"('Pavel','Muratov','Nikolaevich','PashkaNurashka7639','9b18947ad9854ce3f6c5264081fd49e394888986f3ce26bbfbe0db90dbb6da10','+79322222222','pashkamuratov@gmail.com',3,'Kaliningradskaya, 25, 10','1995-08-01',3,'2020-08-10','2020-08-10', 0)," +
"('Elena','Galich','Ivanovna','Elenaera1978','9b18947ad9854ce3f6c5264081fd49e394888986f3ce26bbfbe0db90dbb6da10','+79263333333','elenagalich@gmail.com',11,'Stroitelei, 13, 78','1980-04-11',3,'2020-01-01','2020-01-01', 0)," +
"('Ivan','Piratov','Nikolaevich','IvashkaNurashkaaaaa7639','9b18947ad9854ce3f6c5264081fd49e394888986f3ce26bbfbe0db90dbb6da10','+79324444444','pirat@gmail.com',14,'Kaliningradskaya, 25, 10','1997-12-01',3,'2020-08-10','2020-08-10', 0)," +
"('Sergei','Piratov','Nikolaevich','IvashkaNurashka7639','9b18947ad9854ce3f6c5264081fd49e394888986f3ce26bbfbe0db90dbb6da10','+79265555555','piratov@gmail.com',14,'Kaliningradskaya, 25, 10','1997-12-01',3,'2020-08-10','2020-08-10', 0)," +
"('Daria','Piratova','Ivanovna','Piratova1980','9b18947ad9854ce3f6c5264081fd49e394888986f3ce26bbfbe0db90dbb6da10','+79267777777','piratovadaria@gmail.com',14,'Kaliningradskaya, 25, 10','1997-12-01',3,'2020-08-10','2020-08-10', 0)," +
"('Vladimir','Galich','Ivanovich','GalichVladimir1965','9b18947ad9854ce3f6c5264081fd49e394888986f3ce26bbfbe0db90dbb6da10','+79268888888','galivova@gmail.com',11,'Stroitelei, 13, 78','1965-04-11',3,'2020-01-01','2020-01-01', 0)," +
"('Oksana','Galich','Dmitrievna','GalichOksana1965','9b18947ad9854ce3f6c5264081fd49e394888986f3ce26bbfbe0db90dbb6da10','+79269999999','galichvova@gmail.com',11,'Stroitelei, 13, 78','1965-04-11',3,'2020-01-01','2020-01-01', 0)," +
"('Vlada','Gala','Ivanovovna','GalaVlada1969','9b18947ad9854ce3f6c5264081fd49e394888986f3ce26bbfbe0db90dbb6da10','+79268888887','gala@gmail.com',11,'Stroitelei, 13, 9','1969-04-11',3,'2020-01-02','2020-01-02', 0)," +
"('Oksi','Miron','Dmitrievich','Oksi1965','9b18947ad9854ce3f6c5264081fd49e394888986f3ce26bbfbe0db90dbb6da10','+79269999955','vova@gmail.com',11,'Stroitelei, 13, 70','1965-04-12',3,'2020-01-01','2020-01-01', 0)     " +


"  insert into [dbo].Account " +
"(LeadId, СurrencyId) " +
"Values " +
"(38, 1)," +
"(39, 2)," +
"(40, 3)," +
"(41, 4)," +
"(42, 1)," +
"(43, 2)," +
"(44, 3)," +
"(45, 4)," +
"(46, 1)," +
"(47, 2)," +
"(38, 3)," +
"(39, 4)";

		public const string clearTestBase =
			"truncate table [dbo].Account go"+
			"truncate table [dbo].Lead go";








	}
}
