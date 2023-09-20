IF NOT EXISTS(SELECT * FROM sys.databases WHERE name ='UnitessDb')
	CREATE DATABASE "UnitessDb"

USE [UnitessDb]

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Persons' and xtype='U')
	CREATE TABLE Persons (
    PersonID uniqueidentifier DEFAULT NEWID() PRIMARY KEY,
    Name varchar(80)    
);

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Cars' and xtype='U')
	CREATE TABLE Cars (
    CarId uniqueidentifier DEFAULT NEWID() PRIMARY KEY,
    PersonId uniqueidentifier null,
    Model varchar(80),
	FOREIGN KEY (PersonId) REFERENCES Persons(PersonId)
);

CREATE TABLE #tempPersons
(
  id uniqueidentifier,
  name varchar(80)
);

CREATE TABLE #tempCars
(
  id uniqueidentifier,
  personId uniqueidentifier,
  model varchar(80)
)

INSERT INTO #tempPersons(id, name)
VALUES	('b68a4c73-45b1-4d67-a2d9-8cefb8f13514','Walter White'),
		('c4ceb1f0-9b71-4273-9aaa-aea7c8269342','Jesse Pinkman'),
		('bfa3b85c-d6c7-4d6e-ab1b-f9112c965f84','Gus Fring'),
		('5682c8f6-1c32-4759-ab6b-021f130addc9','Saul Goodman'),
		('599744db-8eb0-4211-b0f5-ce069eca6b2b','Mike Ehrmantraut'),
		('4141a0a1-871c-4caa-a3b6-312959a7cba6','Lalo Salamanca'),
		('3ba80bee-f2e6-4df0-9485-8465bc00f90b','Tuco Salamanca'),
		('41143a90-6a57-437e-8e56-ef92617f9b08','Kim Wexler');

INSERT INTO #tempCars(id, personId, model)
VALUES	('01ba2307-2743-461d-91d8-ba08aa37cc08','b68a4c73-45b1-4d67-a2d9-8cefb8f13514','Pontiac Aztek'),
		('c45a8e0a-3c07-4f26-be6d-28fbe9834047','b68a4c73-45b1-4d67-a2d9-8cefb8f13514','Chrysler 300 SRT8'),
		('1989e013-1f7c-4f2d-9164-9092adced893','b68a4c73-45b1-4d67-a2d9-8cefb8f13514','Fleetwood Bounder'),
		('e5652473-d33c-47d6-b549-9dc113014aa7','c4ceb1f0-9b71-4273-9aaa-aea7c8269342','Toyota Tercel'),
		('3abfb454-eb4b-4bcd-82e2-3e074b856f15','c4ceb1f0-9b71-4273-9aaa-aea7c8269342','Chevrolet El Camino'),
		('c9b50c60-9441-4baf-9d69-998702a4bc51','c4ceb1f0-9b71-4273-9aaa-aea7c8269342','Chevrolet Monte Carlo'),
		('d5f2b6c4-38e6-4de5-a636-1d130ed2fa83','c4ceb1f0-9b71-4273-9aaa-aea7c8269342','Pontiac Fiero'),
		('d0abe97a-946c-4ac0-b0b0-c90cd30bef10','c4ceb1f0-9b71-4273-9aaa-aea7c8269342','Kawasaki'),
		('b99dedab-a992-46af-a594-f4d6f9a92bc5','bfa3b85c-d6c7-4d6e-ab1b-f9112c965f84','Volvo V70'),
		('e72b16a5-2a39-49ba-a1bb-26588058f3a8','5682c8f6-1c32-4759-ab6b-021f130addc9','SUZUKI ESTEEM BALENO'),
		('4b4b3d16-bbe6-4451-8c24-eedfa7b8c506','5682c8f6-1c32-4759-ab6b-021f130addc9','CADILLAC DEVILLE'),
		('5748a73e-3da7-4401-899a-237cd77ec463','5682c8f6-1c32-4759-ab6b-021f130addc9','MERCEDES-BENZ C240'),
		('a9ce7d0f-4d2d-41c8-9566-5cd53011929e','599744db-8eb0-4211-b0f5-ce069eca6b2b','Chrysler Fifth Avenue'),
		('dda4bdb7-b0b6-460e-915f-3753f0214546','599744db-8eb0-4211-b0f5-ce069eca6b2b','Buick LeSabre'),
		('e7c522f0-f81e-492d-aa58-525b49bbe193','599744db-8eb0-4211-b0f5-ce069eca6b2b','Dodge Charger'),
		('d82821ba-d344-4038-a7cd-b2ed13d17597','599744db-8eb0-4211-b0f5-ce069eca6b2b','Buick Century'),
		('a2ffcb6e-d189-4641-bb23-466dbded32b6','599744db-8eb0-4211-b0f5-ce069eca6b2b','Cadillac DTS'),		
		('c5a2e0a2-2f44-4d06-84b0-948557b7f4ca','4141a0a1-871c-4caa-a3b6-312959a7cba6','CHEVROLET MONTE CARLO'),
		('411e9bfc-e38d-41d9-823b-d5ddbed4a070','3ba80bee-f2e6-4df0-9485-8465bc00f90b','Cadillac Escalade'),
		('628aa828-32b5-4aec-b188-2845b3636a33','41143a90-6a57-437e-8e56-ef92617f9b08','Mitsubishi Eclipse'),
		('25105b37-1b72-4ac6-99e0-64c80ccc360c','41143a90-6a57-437e-8e56-ef92617f9b08','Audi A8 L D3'),
		('c0801c42-cbd9-4832-b8af-7417021527f2','41143a90-6a57-437e-8e56-ef92617f9b08','Nissan Versa'),
		('7c3b581a-232d-4b9c-b1cc-5e606768f0b5','41143a90-6a57-437e-8e56-ef92617f9b08','Toyota Prius II'),
		('cb2241c9-8a63-4ef6-9e86-771187a44dcc','41143a90-6a57-437e-8e56-ef92617f9b08','Subaru Legacy Outback'),
		('5ef05896-d35d-4b49-a306-65b880021f7f','41143a90-6a57-437e-8e56-ef92617f9b08','Pontiac Bonneville');

MERGE INTO Persons AS Target
USING (SELECT id, Name FROM #tempPersons) AS Source ON Target.PersonId = Source.id
WHEN NOT MATCHED THEN
    INSERT (PersonId, Name) VALUES (Source.id, Source.Name);

MERGE INTO Cars AS Target
USING (SELECT id, personId, model FROM #tempCars) AS Source ON Target.CarId = Source.id
WHEN NOT MATCHED THEN
    INSERT (CarId, PersonId, Model) VALUES (Source.id, Source.personId, Source.model);

DROP TABLE #tempCars
DROP TABLE #tempPersons