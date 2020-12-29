EF
1. dotnet ef migrations add initialEmployee -o Data/Migrations
2. dotnet ef database update
3. dotnet ef database drop --force
4. dotnet ef migrations remove --force