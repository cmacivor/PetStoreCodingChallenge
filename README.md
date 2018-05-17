# PetStoreCodingChallenge
This is the coding challenge for SingleStone.

This project used the Entity Framework with the "Code First" model. To set up the database, running "Update-Database" in the Nuget Package
Manager Console should create the database for you. If you don't have SQL Server locally, it should use localdb by default. For Visual
Studio 2017, you can connect to localdb in Server Explorer with the Server Name "(localdb)\MSSQLLocalDB". I think it would be 
"(localdb)\v11.0" or something similar for earlier versions. The db name is "DAL.PetStoreDbContext".

Side note: I'm more familiar with Dapper as an ORM. It's what I used at my two previous jobs. So for this I had to relearn how to use
Code First.

To approach this, I used the API that retrieves the entire inventory and caches it, with a timeout of 15 minutes. 
This is to minimize network IO. I structured the project to demonstrate I have some knowledge of the importance of
separating concerns, DI, and SOLID principles. 

This project still needs some error handling in places. I would add more to polish it, but I don't have any more time this morning. 





