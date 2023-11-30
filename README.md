# ChickenFarmApi

The Chicken Farm API is a web application that allows users to track and maintain egg laying records for chickens on a farm. It provides a RESTful API for managing chickens and their egg laying history. The Chicken Farm API also includes seeded data for 5 chickens and 2 egg laying records per chicken.   

## Notes and Instructions:

This program was created using Visual Studio 2022 and .NET 7.0

#### After cloning the repository, but before running the program, you will need to update the database. To do this: 

*Using the .NET CLI, navigate to the ChickenFarmApi.DataAccess folder using this command: cd ..\ChickenFarmApi.DataAccess

*Once you have navigated to the correct location, type: dotnet ef database update in the .NET CLI and press enter. This will apply migrations and ensure the seeded data will show up once you run the program.

#### Next, you will want to make sure that the only Startup Project is the ChickenFarmApi project. To do this:

*Right click on the Solution and click "Properties". Under Common Properties-> Startup Project, make sure you have selected ChickenFarmApi under the "Single startup project" option.

#### Once the above steps have been completed, make sure to Build the Solution. To do so: 

*Right click on the Solution and click "Build Solution" 

#### Then Run the Solution and interact with the API through Swagger.

### The following features included in this project are:

1. Make your application an API
2. Make your application a CRUD API
3. Make your application asynchronous
4. Have 2 or more tables (entities) in your application that are related and have a function return data from both entities. In entity framework, this is equivalent to a join
5.Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program.  

##### Further Notes:
This is an application that I intend to continue expanding upon, so please check back for updates. Thank you for taking the time to check out my work!
