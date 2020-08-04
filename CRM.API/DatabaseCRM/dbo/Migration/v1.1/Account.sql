create table Account(
		Id bigint Identity, 
		LeadId bigint not null,
		Balance bigint null,
		СurrencyId int null,
		IsDeleted bit default (0),  
		PRIMARY KEY (Id),
		FOREIGN KEY (LeadId)  REFERENCES [Lead] (Id))
