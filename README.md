This project will work without any change/installation. 

Please check database connection string in case of database connection issue/error.

### I have used Repository Design Pattern -
The repository and unit of work patterns are intended to create an abstraction layer between the data access layer and the business logic layer of an application. Implementing these patterns can help insulate your application from changes in the data store and can facilitate automated unit testing or test-driven development (TDD).
 
 ![alt text](https://www.asp.net/media/2578149/Windows-Live-Writer_8c4963ba1fa3_CE3B_Repository_pattern_diagram_1df790d3-bdf2-4c11-9098-946ddd9cd884.png)
 
Below are the details of projects and files.

### RefactoringChallenge Project

#### Repositories
    • IRepositoryAsync – Generic interface to connect database for CRUD operations.

    • RepositoryBase – Implemented IRepositoryAsync.

    • IOrderRepositoryAsync – Interface is having all the order related signatures.

    • OrderRepositoryAsync – Implemented all the order related methods.

    • IOrderDetailsRepositoryAsync – Interface is having signature related to order details.

    • OrderDetailsRepositoryAsync – Implemented all the order details related methods.

#### Controllers
    • OrdersController – Is having all the API endpoints.
      o Get
      o GetById
      o Create
      o AddProductsToOrder
      o Delete
#### Data
    • NorthwindDbContext – DB Context.
#### Entities
    • All the Database entities.
#### Mapper
    • Auto Mapper Profile and all the order API request and response.

### APIUnitTest Project
    • OrderUnitTest – 8 Unit test cases for API. 
    • OrderMockData – Mock data for testing. 
