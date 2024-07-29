# finShark Web API

finShark is a web API focused on the stock market. It provides endpoints for managing stocks, comments on stocks, and user portfolios. The API uses JWT for authentication and SQL for the database. Swagger is used for API documentation.

## Table of Contents

- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [Models](#models)
- [Endpoints](#endpoints)
- [Authentication](#authentication)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)

## Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/yourusername/finShark.git
   cd finShark

2. **Install dependencies:**

``` bash
dotnet restore 
```

3. **Set up the database:**
Ensure you have a SQL Server instance running and update the connection string in   `appsetings.json`.

4. **Apply migrations:**

``` bash
dotnet ef database update
```
## Configuration

Configure your database connection string and other settings in the appsettings.json file.

## Usage

To run the application:

1. Start the application using:

   ```bash
   dotnet run

Start the application using dotnet run and navigate to https://localhost:5001/swagger to view the Swagger API documentation.

## Authentication

This API uses JWT (JSON Web Tokens) for authentication. Include the token in the `Authorization` header with the `Bearer` scheme for protected endpoints.

### Example

```http
Authorization: Bearer your_jwt_token
