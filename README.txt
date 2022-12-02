Run the following to update/install databases: 

In Package Manager with Schma.E3ProjectManager.Infrastructure selected as the Default Project, and have Schma.E3ProjectManager.Presentation.Web set as solution startup project

update-database -context ApplicationDbContext
update-database -context EventStoreDbContext
update-database -context IdentityDbContext