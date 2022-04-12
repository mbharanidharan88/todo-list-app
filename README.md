I've followd the DDD design here with SSO.

![Alt text](TodoList_Architecture.png?raw=true "Todo List Architecture")

Why certain approaches were used, why others were not selected

 Any design patterns used
* Have followed DDD
* Have used Result and Paged custom Wrappers used
* Repository pattern followed
* OAuth2 JWT implementation for Token Management
* ASP.NET Identity for User Management
* Have used Localisation
* Service Wrapper for API Calls

 Anything extra you would have done given more time
* Already containerised the project with docker-compose but few issues with connection between containers.
* Refactor the code a bit 
* Proper try/catch implementation
* Unit Test Cases
* If it is any database, would have added migrations


 Anything else you feel we should know 

* Please open the project in Visual Studio rather than Visual Studio Code. Organized the project with solution folders.

![Alt text](Project_Structure.png?raw=true "Project Structure")

* Other Repositories
https://github.com/mbharanidharan88?tab=repositories


Technical Stack:

* ASP.NET CORE MVC
* .NET CORE API 
* Identity Server 4
* ASP.NET Core Identity
* Entity Framework Core
* Docker and Docker Compose