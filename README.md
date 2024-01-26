# How to create .NET8 CRUD WebAPI Azure PostgreSQL Microservice and deploy to Docker Desktop and Kubernetes (in your local laptop)

The source code is available in this github: 

## 1. Prerequisite

### 1.1. Create Azure MySQL instance

We navigate to **Create a resource** and select **Databases**

We select Azure Database for **PostgreSQL**

We input the required data to create a new **Flexible Server**

Server Name: postgresqlserver1974

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/76d5eef3-e234-4b40-9236-bdbb6e6e29b9)

We **configure the server** (compute and storage) abd press the **Save** button 

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/c9677b96-fee6-4020-8938-4cf18ddd8730)

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/299ae028-9da3-4d99-8466-33fd9b485c62)

We continue configuring the

Admin username: adminmysql

Password: LuiscocoXXXXXXXXXXX

We navigate to the Networking tab and we add our laptop IP address as a FireWall rule

We can now access to **Azure PostgreSQL** from **pgAdmin 4** setting the hostname, username and password

### 1.2. Install and Run pgAdmin 4 and create new database

We run **pgAdmin 4** and we create a new connection

We input the new connection data

Now we can test the connection pressing on the Test connection button

We create a database running this command

```sql
CREATE DATABASE mysqldatabase
```

We create a new Table an insert some rows

```sql
CREATE TABLE Items (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL
    -- Add other fields here as per your model
);

INSERT INTO Items (Name) VALUES ('Item 1');
INSERT INTO Items (Name) VALUES ('Item 2');
INSERT INTO Items (Name) VALUES ('Item 3');
```

We can verify the inserted items

### 2. How to Create .NET 8 WebAPI CRUD Microservice with Dapper

We run **Visual Studio 2022 Community Edition** and we create a new project

We select the **ASP Net Core Web API** project template

We set the project name and location

We select the project main features

We create the following project folders structure, with the Data and Models new folders

We load the **dependencies**: Dapper, Microsoft.VisualStudio.Azure.Containers.Tools.Targets, **Npgsql** and Swashbuckle.AspNetCore

We define in the **appsettings.json** file the database connection string




