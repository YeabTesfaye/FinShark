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

1. Apply migrations:

```bash
dotnet ef database update

2. Start the application using:

   ```bash
   dotnet run

Start the application using dotnet run and navigate to https://localhost:5001/swagger to view the Swagger API documentation.
## Models

### Comment

- **CommentId**: `int` - The unique identifier for the comment.
- **Title**: `string` - The title of the comment.
- **Content**: `string` - The content of the comment.
- **CreatedOn**: `DateTime` - The date and time when the comment was created.
- **StockId**: `int` - The ID of the stock the comment is associated with.
- **Stock**: `Stock` - The stock entity associated with the comment.
- **AppUserId**: `string` - The ID of the user who made the comment.
- **AppUser**: `AppUser` - The user entity associated with the comment.

### Portfolio

- **AppUserId**: `string` - The ID of the user who owns the portfolio.
- **StockId**: `int` - The ID of the stock in the portfolio.
- **AppUser**: `AppUser` - The user entity associated with the portfolio.
- **Stock**: `Stock` - The stock entity associated with the portfolio.

### Stock

- **StockId**: `int` - The unique identifier for the stock.
- **Symbol**: `string` - The stock symbol.
- **CompanyName**: `string` - The name of the company.
- **Purchase**: `decimal` - The purchase price of the stock.
- **LastDiv**: `decimal` - The last dividend of the stock.
- **Industry**: `string` - The industry the stock belongs to.
- **MarketCap**: `long` - The market capitalization of the stock.
- **Comments**: `List<Comment>` - The list of comments associated with the stock.
- **Portfolios**: `List<Portfolio>` - The list of portfolios that include the stock.

## Endpoints

### Authentication

- **Register**: `POST /api/auth/register`  
  Registers a new user.

- **Login**: `POST /api/auth/login`  
  Logs in an existing user and returns a JWT token.

### Stocks

- **Get all stocks**: `GET /api/stocks`  
  Retrieves a list of all stocks.

- **Get stock by ID**: `GET /api/stocks/{id}`  
  Retrieves a stock by its unique identifier.

- **Create stock**: `POST /api/stocks`  
  Creates a new stock.

- **Update stock**: `PUT /api/stocks/{id}`  
  Updates an existing stock by its unique identifier.

- **Delete stock**: `DELETE /api/stocks/{id}`  
  Deletes a stock by its unique identifier.

### Comments

- **Get comments for stock**: `GET /api/stocks/{stockId}/comments`  
  Retrieves a list of comments associated with a specific stock.

- **Add comment to stock**: `POST /api/stocks/{stockId}/comments`  
  Adds a new comment to a specific stock.

- **Update comment**: `PUT /api/comments/{id}`  
  Updates an existing comment by its unique identifier.

- **Delete comment**: `DELETE /api/comments/{id}`  
  Deletes a comment by its unique identifier.

### Portfolios

- **Get portfolio for user**: `GET /api/portfolios/{userId}`  
  Retrieves the portfolio of a specific user.

- **Add stock to portfolio**: `POST /api/portfolios`  
  Adds a stock to the user's portfolio.

- **Remove stock from portfolio**: `DELETE /api/portfolios/{userId}/{stockId}`  
  Removes a stock from the user's portfolio.



## Authentication

This API uses JWT (JSON Web Tokens) for authentication. Include the token in the `Authorization` header with the `Bearer` scheme for protected endpoints.

### Example

```http
Authorization: Bearer your_jwt_token
