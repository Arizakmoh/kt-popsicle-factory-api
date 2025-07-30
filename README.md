# 🧊 PopsicleFactory API

A clean, professional RESTful Web API built with **.NET 8**, following **Clean Architecture** principles. This project manages a simple inventory of popsicles with full CRUD functionality.

---

## 📦 Tech Stack

- **.NET 8 Web API**
- **Clean Architecture**
- **FluentValidation**
- **xUnit** for unit testing
- **Swagger/OpenAPI** for API documentation
- **In-Memory Repository** (mock data layer)

---

## 🧱 Clean Architecture Structure

PopsicleFactory/
├── PopsicleFactory.Domain/ # Domain Entities and Interfaces
├── PopsicleFactory.Application/ # DTOs, Services, Validation
├── PopsicleFactory.Infrastructure/ # Repositories, Data Access
├── PopsicleFactory.WebAPI/ # Controllers, API, Swagger
└── PopsicleFactory.Application.Tests/ # Unit tests with xUnit and Moq

---

## 🚀 Getting Started

### 📥 Clone the Repository


git clone https://github.com/Arizakmoh/kt-popsicle-factory-api.git
cd popsicle-factory


---

## 🛠️ Run the API Locally

Open the solution in Visual Studio 2022+

Set PopsicleFactory.WebAPI as the startup project

Run the project (Ctrl + F5)


## ✅ Swagger UI will launch automatically at:
 http://localhost:5000/swagger (double check the port please)



 ## 🔌 API Endpoints
	Method	Endpoint	Description
	GET	/api/popsicles	Get all popsicles
	GET	/api/popsicles/{id}	Get a popsicle by ID
	POST	/api/popsicles	Create a new popsicle
	PUT	/api/popsicles/{id}	Update an existing popsicle
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
http://localhost:5000/swagger

It includes:

Full endpoint documentation
Sample requests/responses
Error responses



👨‍💻 Author
Abdirizak  Abdullahi
.NET Developer  

