# MongoRepository
This repository showcases a .NET Core application utilizing MongoDB with a repository pattern to manage client and employee data. It features API controllers for client and employee interactions, as well as repository implementations that manage CRUD operations and transaction handling using MongoDB's C# driver.

Project Structure:

Controllers: Provide API endpoints for client and employee management.
* ClientController: Handles operations related to client data.
* EmployeeController: Handles operations related to employee data.

Repositories: Contains MongoDB repository interface implementations.
* MongoRepository<TDocument>: A generic repository for common CRUD operations and transactions.
* ClientRepository: A repository tailored for Client entities, derived from MongoRepository<Client>.
* EmployeeRepository: A repository tailored for Employee entities, derived from MongoRepository<Employee>.

Interfaces:
* IMongoRepository<TDocument>: Specifies generic methods for a MongoDB repository.
* IClientRepository and IEmployeeRepository: Define specialized methods for client and employee data operations, extending IMongoRepository<>.
* Dependency Injection Configuration: Sets up MongoDB repositories and clients within the .NET Core DI container.

Usage:
* Prerequisites:
  * .NET Core SDK.
  * MongoDB installed and operational locally or remotely.
* Setup:
  * Clone the repository.
  * Adjust MongoDB connection settings in Startup.cs or through environment variables.
* Running the Application:
  * Compile and execute the application using 'dotnet run'.
  * Navigate to API endpoints:
  * Clients: /api/client
    * GET / - Lists all clients.
    * GET /{id} - Fetches a client by their ID.
    * POST / - Registers a new client.
  * Employees: /api/employee
    * GET / - Lists all employees.
    * GET /{id} - Fetches an employee by their ID.
    * POST / - Registers a new employee.
