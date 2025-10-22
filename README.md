#AgriEnergy Connect#

AgriEnergy Connect is a web-based application developed using the ASP.NET core MVC Framework. The system serves as a digital bridge between agricultural employees and farmers enabling rolebased management of agricultural data. The application demonstrates fundamental software engineering principles, including modular architecture, secure authentication, database integration and user interface design.

#System Purpose and Objectives#

The main objective of AgriEnergy Connect is to facilitate structured data management between two key user roles:

Employees: Responsible for registering, editing, and managing farmer profiles within the system.

Farmers: Granted the ability to add, view, and manage agricultural products associated with their profiles.

The system integrates Entity Framework Core for database management and ASP.NET Identity for authentication and authorization ensuring data integrity and secure access control


#Key Features#

Role based access control using Identity Roles (Farmer and Employee).

CRUD functionality for both Farmers and Products.

Relational mapping between Farmers and their respective Products.

Responsive front-end interface developed using Bootstrap.

Automatic database seeding of default users and roles.

SQL Server LocalDB integration for persistent data storage.

#System Setup and Configuration#

Repository Access and Installation
Clone the repository from GitHub using Visual Studio:

https://github.com/VumaMadonsela/AgriEnergyConnect-EAPD7111


#Project Configuration#
Open the .sln file in Visual Studio and allow the environment to restore all NuGet dependencies automatically.

Database Configuration
Open the Package Manager Console and execute the following commands to initialize the database:

add-migration InitialCreate
update-database


#Application Execution#
Press Ctrl + F5 or click Run Without Debugging to start the application.
The system will open in the browser at https://localhost:xxxx.

#Pre Seeded User Accounts#
The following accounts are automatically seeded:

Employee Account: employee@agri.com / Employee123!

Farmer Account: farmer@agri.com / Farmer123!

#Known Issue and Development Limitation#

Although the system successfully compiles and runs, a functional limitation has been identified within the Create (Save) operations for both the Farmers and Products modules.
The “Save” button fails to execute the POST submission despite the correct implementation of the MVC structure, models, and controller methods.
This issue is presumed to stem from either a client-side script or environmental restriction affecting form submission rather than a logical or architectural flaw in the system itself.

Technological Stack
Component	Technology
Framework	ASP.NET Core MVC 8.0
Database	SQL Server LocalDB
ORM	Entity Framework Core
Authentication	ASP.NET Identity
Front-End	Bootstrap
IDE	Visual Studio 2022


