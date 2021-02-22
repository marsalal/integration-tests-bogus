# integration-tests-bogus
Quick project to demostrate how to do integration tests with EF In memory database and bogus library

# Overview
Webpi core project based on .Net 5 and Postgres database setup with EFCore as ORM. 

# Run webapi

1. Run InitialCreate migration on local Postgres database. Connection string is in appsettings.json and username is default for postgres databases
2. Build and Run the solution

## Dockerfile
You can run the dockerfile to startup a container with postgres image. 

# Run Tests

Main integration tests are in PersonsControllerTest, go to test explorer and run them all. `BaseWebApplicationFactory` contains the recreation of the Startup class to define the database in memory and fake data using bogus

