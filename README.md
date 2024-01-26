# How to create .NET8 CRUD WebAPI Azure PostgreSQL Microservice and deploy to Docker Desktop and Kubernetes (in your local laptop)

The source code is available in this github: 

## 1. Prerequisite

### 1.1. Create Azure PostgreSQL instance

We navigate to **Create a resource** and select **Databases**

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/34b76cbb-5e03-4959-9eee-3e5d822b09e5)

We select Azure Database for **PostgreSQL**

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/dd6b48c7-0195-4cbf-90d8-0bd5863a3932)

We input the required data to create a new **Flexible Server**

Server Name: postgresqlserver1974

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/76d5eef3-e234-4b40-9236-bdbb6e6e29b9)

We **configure the server** (compute and storage) abd press the **Save** button 

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/6e2434a0-70d0-4821-844b-9ceddf0011e4)

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/c9677b96-fee6-4020-8938-4cf18ddd8730)

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/299ae028-9da3-4d99-8466-33fd9b485c62)

We continue configuring the **Authentication** data

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/e9655f2e-04c5-4a68-9475-f0fc7477da28)

We navigate to the **Networking** tab and we add our local laptop IP address as a FireWall rule

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/08ef7fc4-adbc-4a78-925d-c6363cc0b2b1)

We press the **Review and create** button 

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/7923bad1-364e-4932-8507-b1acc3c1335d)

### 1.2. Install and Run pgAdmin 4 and create new database

We can now access to **Azure PostgreSQL** from **pgAdmin 4** setting the hostname, username and password

We download and install **pgAdmin 4**

https://www.pgadmin.org/download/pgadmin-4-windows/

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/3c0d33f4-bfa8-4565-a78a-d0a9faebeee5)

We run **pgAdmin 4** and we create a new connection

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/870af1a5-c770-4862-9902-9b78ba55eadb)

We input the new connection data and we press the **Save** button to connect to the Azure PostgreSQL instance

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/47ff6864-3aa8-4c92-86b8-2a620b09bb1a)

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/a1c4914b-32ff-42b4-a034-d415bab3f3a1)

We verify the new Server in the list

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/fc2ca09c-8390-43ae-81fe-3cf5515e865d)

We right click on the databases and create a new one

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/52fbb820-66dc-4647-9083-5c9c1f69433d)

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/580bc0f4-05ee-4158-8664-c8a1d8965cbe)

We verify the new database in the list


**IMPORTANT NOTE**: 

The **LC_COLLATE** parameter determines the sorting order of strings in the database, such as how names and titles are sorted in queries. 

This setting is crucial for ensuring that data is sorted correctly according to the local conventions of the database's intended audience.

Ensure that you're using the correct syntax when specifying the LC_COLLATE setting. 

The syntax for setting this parameter when creating a new database should be part of the CREATE DATABASE statement, for example:

```sql
CREATE DATABASE postgresqldatabase
WITH ENCODING 'UTF8'
LC_COLLATE='en_US.utf8'
LC_CTYPE='en_US.utf8'
TEMPLATE=template0;
```

Make sure you're using the correct quotation marks and that the locale you're specifying is available on your system.

Use **Template0**: When creating a new database with specific **LC_COLLATE** and **LC_CTYPE** settings, it's recommended to use **TEMPLATE=template0**, as shown in the example above, because template0 is guaranteed to have the default settings, ensuring that the new database will inherit the specified LC_COLLATE and LC_CTYPE settings without issues.

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



