# Securities & Trades Aggregation Console App

## Task Details

This project is a C# console application designed to:

- **Read `C:\Engineer Code Test\Securities.xml`**  
  (contains a list of known securities)
- **Read all XML files matching `Trades*.xml`**  
  (within the root folder `C:\Engineer Code Test\Test` and all subfolders)
- **Use `TradeSample.xml` as schema reference**
- **Output:**  
    - **Aggregated trade data**: grouped by `BloombergId`, `TransactionCode`, `TradeDate`, with sum of quantity and average price
    - **List of files and counts**: all trades that do *not* have a valid security (i.e., where BloombergId does not match)

## Setup & Usage

1. **Prerequisites**
    - .NET 6+ SDK
    - Place your `Securities.xml` and all `Trades*.xml` files in the specified folders

2. **App Configuration**
    - Update the file/folder paths in `App.config` (`SecuritiesXmlPath` and `TradesRootPath`)
    - If these are blank or missing, you will be prompted at runtime

3. **Build & Run**
    ```sh
    dotnet build
    dotnet run --project CodeTest.Presentation
    ```
    Or use Visual Studio to build and run the solution.

4. **Output**
    - Aggregated trade results will be shown in the console, in a neatly aligned tabular format
    - Files with invalid securities and their invalid trade counts will be listed

## Features

- **Dependency Injection:** Uses [Unity Container](https://github.com/unitycontainer/unity) for IoC and DI
- **Object-Oriented Design:** All logic is separated by layer and concern (Presentation, Application, Domain, Infrastructure)
- **Interface Usage:** All services and repositories are abstracted by interfaces for testability and extensibility
- **File/Folder Optimization:** Reads all relevant XML files efficiently using directory recursion and LINQ to XML
- **User Experience:** If a configured path is invalid or missing, the app prompts until a valid path is provided, without crashing

## Solution Structure
/CodeTestSolution
/CodeTest.Domain # Entities, interfaces (POCOs, IRepository)
/CodeTest.Application # Aggregation services (business logic)
/CodeTest.Infrastructure # XML repository implementations
/CodeTest.Presentation # Console app (entry point, DI setup)
App.config
README.md


## Patterns & Principles Used

- **Layered Architecture**
- **Repository Pattern**
- **Dependency Injection (Unity)**
- **SOLID Principles** (SRP, ISP, SoC)
- **Separation of Concerns**

## Customization

- To add support for other file types or data sources, implement the repository interfaces in `/Infrastructure`
- To change output formatting, edit the Application layer’s aggregation display methods

## Author

This project was developed as a code test for an engineering interview.  
**Contact:** *Mandar Thorat / mandarbthorat@gmail.com*

