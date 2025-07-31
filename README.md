# 🧊 PopsicleFactory API

A clean, professional RESTful Web API built with **.NET 8**, following **Clean Architecture** principles. This project manages a simple inventory of popsicles with full CRUD functionality

---

## ✨ Features

- **Full CRUD Operations** - Create, Read, Update, and Delete popsicles
- **Partial Updates** - PATCH support for efficient resource updates
- **Clean Architecture** - Separation of concerns with domain, application, and infrastructure layers
- **Robust Validation** - FluentValidation integration for request validation
- **API Documentation** - Interactive Swagger documentation
- **Performance Optimized** - Async operations and efficient data mapping

## 📦 Tech Stack

- **.NET 8 Web API**
- **Clean Architecture**
- **FluentValidation**
- **xUnit** for unit testing
- **Swagger** for API documentation
- **In-Memory Repository** (mock data layer)
- **Framework**: .NET 8 Web API
- **Testing**: xUnit, Moq, FluentAssertions
- **Validation**: FluentValidation
- **Mapping**: Mapster (for high-performance object mapping)
- **Documentation**: Swagger
- **Patching**: Microsoft.AspNetCore.JsonPatch


## 🚀 Quick Start

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (Recommended) or VS Code

### Installation
```bash
git clone https://github.com/Arizakmoh/kt-popsicle-factory-api.git
cd popsicle-factory-api
dotnet restore
Running the API
bash
dotnet run --project PopsicleFactory.WebAPI
The API will be available at : "https://localhost:7272;http://localhost:5235", (or your configured port)

🧪 Testing
To run the complete test suite:

bash
dotnet test

📚 API Documentation
Interactive Documentation
Access Swagger UI at:
http://localhost:5235/swagger
https://localhost:7272/swagger



## 🛠️ Run the API Locally

Open the solution in Visual Studio 2022+

Set PopsicleFactory.WebAPI as the startup project

Run the project (Ctrl + F5)


## ✅ Swagger UI will launch automatically at:
 http://localhost:5235/swagger or  https://localhost:7272/swagger (double check the port please)



 ## 🔌 API Endpoints
	Method	Endpoint	Description
	GET	/api/popsicles	Get all popsicles
	GET	/api/popsicles/{id}	Get a popsicle by ID
	POST	/api/popsicles	Create a new popsicle
	PUT	/api/popsicles/{id}	Update an existing popsicle
 	PATCH	/api/popsicles/{id}	Partially updates an existing popsicle.
	DELETE	/api/popsicles/{id}	Delete a popsicle (optional)


 ## 📄 Data Model
 	 {
	  "id": "uuid (optional for POST)",
	  {
		"name": "string",
		"flavor": "string",
		"price": 0
	  }



 ##  🧪 Unit Testing
	Unit tests for PopsicleService live in PopsicleFactory.Application.Tests

	Run tests via:

	dotnet test


##  🌐 Swagger Docs
Automatically available at:
http://localhost:7272/swagger


It includes:

Full endpoint documentation
Sample requests/responses
Error responses


# Restore dependencies
dotnet restore

Endpoint Reference
Method	Endpoint	Description
GET	/api/popsicles	Get all popsicles
GET	/api/popsicles/{id}	Get a specific popsicle
POST	/api/popsicles	Create a new popsicle
PUT	/api/popsicles/{id}	Fully update a popsicle
PATCH	/api/popsicles/{id}	Partially update a popsicle
DELETE	/api/popsicles/{id}	Delete a popsicle
Example Requests
Create a Popsicle (POST)

json
{
  "name": "Strawberry Dream",
  "flavor": "Strawberry & Cream",
  "price": 3.75,
}
Partial Update (PATCH)

json
[
  {
    "op": "replace",
    "path": "/price",
    "value": 4.25
  },
  {
    "op": "add",
    "path": "/description",
    "value": "Limited edition summer flavor"
  }
]

for more example please use postman json collections for all request and response 

🏗️ Project Structure
text
PopsicleFactory/
├── Domain/             # Domain layer
│   ├── Entities/       # Business models
│   └── Interfaces/     # Repository contracts
│
├── Infrastructure/     # Infrastructure layer
│   └── Data/           # In Memory Db
│
├── Application/        # Application layer
│   ├── DTOs/           # Data transfer objects
│   └── Validators/     # FluentValidation rules
│
├── WebAPI/             # Presentation layer
│   ├── Controllers/    # API endpoints
│   └── Properties/     # Startup configuration
│
└── Tests/              # Test projects
    └── UnitTests/      # Core unit tests
 
👨‍💻 Author
Abdirizak Abdullahi
