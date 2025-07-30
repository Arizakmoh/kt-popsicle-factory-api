Of course. Here is a polished and professional README.md based on the information you provided.

Popsicle Factory API 🧊
A clean, professional RESTful Web API built with .NET 8, following Clean Architecture principles. This project manages a simple inventory of popsicles with full CRUD functionality.

✨ Features
Full CRUD Functionality: Create, Read, Update, and Delete popsicles.

Clean Architecture: A well-organized solution promoting separation of concerns, testability, and maintainability.

Validation: Uses FluentValidation for robust request validation.

Unit Tested: Includes a suite of tests using xUnit and Moq.

API Documentation: Provides interactive documentation via Swagger/OpenAPI.

📦 Tech Stack
.NET 8 Web API

xUnit & Moq

FluentValidation

Mapster

Swagger

In-Memory Repository (mock data layer)

🧱 Project Structure
The solution is organized using Clean Architecture principles to ensure a clear separation of concerns.

PopsicleFactory/
├── PopsicleFactory.Domain/              # Domain Entities and Interfaces
├── PopsicleFactory.Application/         # DTOs, Services, Validation
├── PopsicleFactory.Infrastructure/      # Repositories, Data Access
├── PopsicleFactory.WebAPI/              # Controllers, API, Swagger
└── PopsicleFactory.Application.Tests/   # Unit tests with xUnit and Moq
🚀 Getting Started
Follow these steps to get the project running on your local machine.

1. Prerequisites

.NET 8 SDK

2. Clone the Repository

Bash

git clone https://github.com/Arizakmoh/kt-popsicle-factory-api.git
cd kt-popsicle-factory-api
3. Restore Dependencies

Bash

dotnet restore
4. Run the API

Bash

dotnet run --project PopsicleFactory.WebAPI
The API server will start. Take note of the local URL from the console output (e.g., http://localhost:5235).

5. Access Swagger UI
Once the server is running, open your browser and navigate to the Swagger UI to interact with the API:
http://localhost:<your-port>/swagger (e.g., http://localhost:5235/swagger)

🔌 API Endpoints
Method	Endpoint	Description
GET	/api/popsicles	Get all popsicles.
GET	/api/popsicles/{id}	Get a single popsicle by its ID.
POST	/api/popsicles	Create a new popsicle.
PUT	/api/popsicles/{id}	Update an existing popsicle.
DELETE	/api/popsicles/{id}	Delete a popsicle.

Export to Sheets
Example: Create a Popsicle
POST /api/popsicles

Request Body:

JSON

{
  "name": "Orange Cream",
  "flavor": "Orange & Vanilla",
  "price": 3.50
}
Success Response (201 Created):

JSON

{
  "id": "e8a7e5a7-9c8e-4f3a-b8e7-0a4e7e9a2b1c",
  "name": "Orange Cream",
  "flavor": "Orange & Vanilla",
  "price": 3.50
}
🧪 Unit Testing
The project includes a full suite of unit tests. To run them, execute the following command from the root directory:

Bash

dotnet test
👨‍💻 Author
Abdirizak Abdullahi - .NET Developer