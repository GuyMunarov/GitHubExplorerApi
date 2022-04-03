


# HOW TO RUN:

SERVER:

To run the project open the .sln file. restore the nuget packages build and run is throw visual studio. the db should create a migration itself, if it doesnt run the command cd into the project folder, "dotnet ef migrations add initial -o Data/Migrations", and then "dotnet ef database update" in the developer powershell.
