# How to create .NET8 CRUD WebAPI Azure PostgreSQL Microservice and deploy to Docker Desktop and Kubernetes (in your local laptop)

The source code is available in this github: 

https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL

This example was develop for protocol **HTTP** if you would like to use also protocol **HTTPS** please refer to the repo:

https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-MySQL

## 1. Prerequisite

### 1.1. Create Azure PostgreSQL instance

We navigate to **Create a resource** and select **Databases**

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/34b76cbb-5e03-4959-9eee-3e5d822b09e5)

We select Azure Database for **PostgreSQL**

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/dd6b48c7-0195-4cbf-90d8-0bd5863a3932)

We input the required data to create a new **Flexible Server**

**Server Name**: postgresqlserver1974

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

### 1.2. Install and Run PostgreSQL and pgAdmin

We first **download and install PostgreSQL** from this URL: https://www.postgresql.org/download/windows/

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/3a50ac64-83ab-4aa3-912f-a06faea2ec69)

https://www.enterprisedb.com/downloads/postgres-postgresql-downloads

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/2e3af074-77b9-48da-8401-2156176cd409)

We open a command prompt window as Administrator and **connect to the Azure PostgreSQL** instance with this command

```
psql -h postgresqlserver1974.postgres.database.azure.com -d postgres -U adminpostgresql
```

We **create a database** with this command

```
CREATE DATABASE postgresqldb
    WITH ENCODING 'UTF8'
    LC_COLLATE='en_US.utf8'
    LC_CTYPE='en_US.utf8'
    TEMPLATE=template0;
```
![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/6a68ffee-6182-468b-bdce-190f23830501)

**IMPORTANT NOTE**: 

The **LC_COLLATE** parameter determines the sorting order of strings in the database, such as how names and titles are sorted in queries. This setting is crucial for ensuring that data is sorted correctly according to the local conventions of the database's intended audience. Ensure that you're using the correct syntax when specifying the LC_COLLATE setting. 

Use **Template0**: When creating a new database with specific **LC_COLLATE** and **LC_CTYPE** settings, it's recommended to use **TEMPLATE=template0**, as shown in the example above, because template0 is guaranteed to have the default settings, ensuring that the new database will inherit the specified LC_COLLATE and LC_CTYPE settings without issues.

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

If we click on the **databsename**, after we click on the **Schemas** and the in **Tables**

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/5e308d0e-7159-47a8-9771-24796900accc)

For running a SQL query we click on the **Query Tool** button

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/0451248e-fea7-4c05-873c-7c512a1087a4)

### 2. How to Create .NET 8 WebAPI CRUD Microservice with Dapper

We run **Visual Studio 2022 Community Edition** and we create a new project

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/86e4fd69-cb10-42de-b683-fbe8e8f4fd71)

We select the **ASP Net Core Web API** project template

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/67ce7f97-daca-4db7-bd38-e935e1b11d6d)

We set the project name and location

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/95597ce6-8e9d-438b-bcf0-483b0468b5c0)

We select the project main features

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/649966dd-6d49-4908-9c8b-39f9160e0620)

We create the following project folders structure, with the **Data** and **Models** new folders

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/0e321226-9d7d-4c5e-9174-344bd208bc89)

We load the **dependencies**: Dapper, Microsoft.VisualStudio.Azure.Containers.Tools.Targets, **Npgsql** and Swashbuckle.AspNetCore

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/78d5a637-82a0-454b-b95b-6f1b760875b3)

We define in the **appsettings.json** file the database connection string

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Host=postgresqlserver1974.postgres.database.azure.com;Database=postgresqldb;Username=adminpostgresql;Port=5432;Password=Luiscoco123456;SSL Mode=Require;Trust Server Certificate=true"
  }
}
```

These are the parameters in the connection string:

**Host**=postgresqlserver1974.postgres.database.azure.com;

**Database**=postgresqldb;

**Username**=adminpostgresql;

**Port**=5432;

**Password**=Luiscoco123456;

**SSL Mode**=Require;

**Trust Server Certificate**=true

We configure the **middleware** with the **program.cs** file

**program.cs**

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AzureMySQLWebAPI.Data;
using AzureMySQLWebAPI.Models;
using Npgsql;
using System;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register your repository
builder.Services.AddScoped<ItemRepository>(serviceProvider =>
    new ItemRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Initialize the database and create the table if it doesn't exist
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
await InitializeDatabaseAsync(connectionString);
await InitializeProcedureAsync(connectionString); // Create the procedure if it doesn't exist

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Database initialization logic
async Task InitializeDatabaseAsync(string connectionString)
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        await connection.OpenAsync();
        string createTableCommand = @"
            CREATE TABLE IF NOT EXISTS public.Items
            (
                id SERIAL PRIMARY KEY,
                name VARCHAR(255) NOT NULL
                -- Add other column definitions as needed
            );
        ";

        using (var command = new NpgsqlCommand(createTableCommand, connection))
        {
            await command.ExecuteNonQueryAsync();
        }
    }
}

// Procedure initialization logic
async Task InitializeProcedureAsync(string connectionString)
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        await connection.OpenAsync();
        string createProcedureCommand = @"
            CREATE OR REPLACE PROCEDURE addnewitem(itemName TEXT)
            LANGUAGE plpgsql
            AS $$
            BEGIN
                INSERT INTO Items (name) VALUES (itemName);
            END;
            $$;
        ";

        using (var command = new NpgsqlCommand(createProcedureCommand, connection))
        {
            try
            {
                await command.ExecuteNonQueryAsync();
                Console.WriteLine("Procedure addnewitem created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating procedure: " + ex.Message);
            }
        }
    }
}
```

We create the data **Models**

**Item.cs**

```csharp
namespace AzureMySQLWebAPI.Models
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        // Add other properties as needed
    }
}
```

We also create the Data repository

**ItemRepository.cs**

```csharp
using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AzureMySQLWebAPI.Models;

namespace AzureMySQLWebAPI.Data
{
    public class ItemRepository
    {
        private readonly string _connectionString;

        public ItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                return await db.QueryAsync<Item>("SELECT * FROM items");
            }
        }

        // Add method to retrieve a single item by id
        public async Task<Item> GetItemByIdAsync(int id)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<Item>("SELECT * FROM items WHERE id = @id", new { id = id });
            }
        }

        // Add method to insert a new item
        public async Task<int> AddItemAsync(Item item)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sql = "INSERT INTO items (name) VALUES (@name) RETURNING id;";
                return await db.ExecuteScalarAsync<int>(sql, item);
            }
        }

        // Add method to update an existing item
        public async Task UpdateItemAsync(Item item)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sql = "UPDATE items SET name = @name WHERE id = @id";
                await db.ExecuteAsync(sql, item);
            }
        }

        // Add method to delete an item
        public async Task DeleteItemAsync(int id)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                await db.ExecuteAsync("DELETE FROM items WHERE id = @id", new { id = id });
            }
        }

        public async Task AddItemUsingStoredProcedureAsync(Item item)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                await db.ExecuteAsync("CALL addnewitem(@itemName)", new { itemName = item.name });
            }
        }
    }
}
```

We create the **Controller** for defining the database **CRUD** actions

**ItemsController.cs**

```csharp
using Microsoft.AspNetCore.Mvc;
using AzureMySQLWebAPI.Data;
using AzureMySQLWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureMySQLWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemRepository _repository;

        public ItemsController(ItemRepository repository)
        {
            _repository = repository;
        }

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            var items = await _repository.GetAllItemsAsync();
            return Ok(items);
        }

        // GET: api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _repository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // POST: api/items
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            int id = await _repository.AddItemAsync(item);
            item.id = id;
            return CreatedAtAction(nameof(GetItem), new { id = item.id }, item);
        }

        // PUT: api/items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.id)
            {
                return BadRequest();
            }

            await _repository.UpdateItemAsync(item);

            return NoContent();
        }

        // DELETE: api/items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _repository.DeleteItemAsync(id);
            return NoContent();
        }

        // POST: api/items/sp
        [HttpPost("sp")]
        public async Task<IActionResult> PostItemUsingStoredProcedure([FromBody] Item item)
        {
            await _repository.AddItemUsingStoredProcedureAsync(item);
            return CreatedAtAction("GetItem", new { id = item.id }, item);
        }
    }
}
```

### 3. We run and verify the application

We run the application and we verify the enpoints with swagger

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/bc16c33a-50aa-418e-99c8-fda50a3c0c5f)

We send a GET request to list all items in the database

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/6fa92849-4919-4bc8-8da8-0698e141266f)

We insert one item with a POST request

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/4397f191-4197-4503-9e91-5b4e7bb6ba8d)

We run again the GET request to retrieve all the items in the database

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/9d07dd18-5515-4079-8d49-14a5ad174be7)

### 4. How to deploy the WebAPI Microservice to Docker Desktop

We right click on the project and we **add Docker support...**

Automatically Visual Studio will create the Dockerfile

**Dockerfile**

```
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AzurePostgreSQLWebAPI.csproj", "."]
RUN dotnet restore "./././AzurePostgreSQLWebAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./AzurePostgreSQLWebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AzurePostgreSQLWebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AzurePostgreSQLWebAPI.dll"]
```

We right click on the project and select Open in Terminal and the we run the following command to **create the Docker image**

```
docker build -t myapp:latest .
```

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/1a4b34a0-df3f-4e30-8c4e-9672f8fc1d17)

We verify the Docker image was created with the command

```
docker images
```

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/b491773f-50ee-4aaf-86e0-ad547c652f92)


We **run the Docker container** with the following command

```
docker run -d -p 8080:8080 myapp:latest
```

Also in **Docker Desktop** we can see the Docker image and the running container

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/8d7afb26-00f5-444a-b705-f4fb30e35543)

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/4fd28f75-6a17-44b5-9cc9-6bb7a6e76b52)

We verify the application is properly running 

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/4c1dc389-338a-4b3c-a138-c437afb133a2)

## 5. How to deploy the WebAPI Microservice to Kubernetes (in Docker Desktop)

For more details about this section see the repo: https://github.com/luiscoco/Kubernetes_Deploy_dotNET_8_Web_API

We enable **Kubernetes** in **Docker Desktop**

We run **Docker Desktop** and press on **Settings** button

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-MySQL/assets/32194879/8400e0df-1b4b-48c6-8b8d-a2abc4cd1868)

We select **Enable Kubernetes** in the left hand side menu an press **Apply & Restart** button

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-MySQL/assets/32194879/40aac32a-67a4-4b1e-9bf6-c1bff1843896)

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-MySQL/assets/32194879/c7823d67-e0cc-4162-b6db-2218420ac3a6)

Here are the general steps to deploy your .NET 8 Web API to Kubernetes:

- **Build** and **Push** the Docker image to the **Docker Hub registry/repo**

- Create Kubernetes **Deployment YAML file**. This file defines how your application is deployed in Kubernetes.

- Create Kubernetes **Service YAML file**. This file defines how your application is exposed, either within Kubernetes cluster or to the outside world.

- Apply the **YAML** files to your Kubernetes Cluster: use the command "kubectl apply" to create the resource defined in your YAML file in your Kubernetes cluster.

We start building and pushing the application Docker image to the Docker Hub registry/repo

```
docker build -t luiscoco/myapp:latest .
```

To verify we created the docker image run the command:

```
docker images
```

Then we use the docker push command to upload the image to the Docker Hub repository:

```
docker push luiscoco/myapp:latest
```

**Note**: run the "**docker login**" command if you have no access to Docker Hub repo

We create the **deployment.yml** and the **service.yml** files in our project

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/5f0d3e9c-a85b-4f94-ac7c-2bc722c2cc8f)

**deployment.yml**

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: myapp-deployment
spec:
  replicas: 2
  selector:
    matchLabels:
      app: myapp
  template:
    metadata:
      labels:
        app: myapp
    spec:
      containers:
      - name: myapp
        image: luiscoco/myapp:latest
        ports:
        - containerPort: 8080
        env:
        - name: ConnectionStrings__DefaultConnection
          value: Host=postgresqlserver1974.postgres.database.azure.com;Database=postgresqldb;Username=adminpostgresql;Port=5432;Password=Luiscoco123456;SSL Mode=Require;Trust Server Certificate=true
      # Removed volumeMounts section related to the certificate
```

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/638d86a9-3bda-4884-acd6-77e3fbfbbc75)

We also create the **service.yml** file

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/0dae9ea3-92f3-4d53-a6a4-661ed5a55f40)

**service.yml**

```yaml
apiVersion: v1
kind: Service
metadata:
  name: myapp-service
spec:
  type: LoadBalancer
  selector:
    app: myapp
  ports:
    - name: http
      protocol: TCP
      port: 80
      targetPort: 8080
```

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/baf2ae45-db90-4d3a-99cc-951a035917b3)

We set the current Kubernetes context to Docker Desktop Kubernetes with this command:

```
kubectl config use-context docker-desktop
```

Now we can apply both kubernetes manifest files with these commands

```
kubectl apply -f deployment.yml
```

and

```
kubectl apply -f service.yml
```

We can use the command "**kubectl get services**" to check the **external IP** and port your application is accessible on, if using a LoadBalancer.

Verify the Deployment with the command:

```
kubectl get deployments
```

Verify the service status with the command:

```
kubectl get services
```

We can also verify the deployment with this command

```
kubectl get all
```

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/2fff241d-9d2d-4a12-8a8b-bc110700defb)

We verify the application access endpoint

http://localhost/api/Items

![image](https://github.com/luiscoco/MicroServices_dotNET8_CRUD_WebAPI-Azure-PostgreSQL/assets/32194879/cf8bbbf9-1db8-4482-ba07-8c2e9a3491d3)

