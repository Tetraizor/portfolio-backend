# 🔧 Portfolio Backend Service

This is a .NET Web API application for managing blog posts, featured items and other stuff. The project uses Entity Framework Core for database interactions and Docker for deployment.

## 📝 Prerequisites

Before running or deploying the project, ensure you have the following installed:

- **.NET SDK (7.0 or later)**
- **MySQL (8.0 or later)**
- **Docker**

## ⚙️ Environment Variables Required

### Backend Configuration:

- **BACKEND_HTTP_PORT:** Port for the ASP.NET application's HTTP connection.
- **BACKEND_HTTPS_PORT:** Port for the ASP.NET application's HTTPS connection.
- **BACKEND_USE_HTTPS:** Set to "true" to enable HTTPS, "false" to disable.
- **BACKEND_LOG_LEVEL:** Set the logging level (e.g., "Information", "Debug", "Error").

### Database Configuration

- **MYSQL_PASSWORD:** Database password.
- **MYSQL_USER:** Database username.
- **MYSQL_SERVER:** Database host/hostname.
- **MYSQL_PORT:** Port for the MySQL server (default: 3306).
- **MYSQL_DATABASE:** The name of the database (e.g., portfolio).

## 🚀 Getting Started

### 1. Clone the repository

    git clone https://github.com/Tetraizor/portfolio-backend.git
    cd portfolio-backend

### 2. Build and Run the Application

Ensure the required environment variables are set, then run the application locally or in a container.

**To run locally:**

    dotnet run

**To run with Docker:**

    docker-compose up

## 🛠️ Configuration Details

### 1. Docker Compose Configuration

This project includes a **docker-compose.yml** file for easy containerization. The containers are configured to run both the backend and MySQL database. If you wish to use Docker, ensure you have Docker and Docker Compose installed. Once everything is set up, run the following:

    docker-compose up

This will start both the backend service and the database in separate containers.

### 2. Database Setup

The project uses MySQL as the database. If you're running it locally, make sure the following environment variables are configured correctly:

- **MYSQL_PASSWORD**
- **MYSQL_USER**
- **MYSQL_SERVER** (localhost if running locally)
- **MYSQL_PORT** (default is 3306)
- **MYSQL_DATABASE** (e.g., portfolio)

You can either set up the database manually or use a **migration** command:

    dotnet ef database update

This will apply any pending migrations to your MySQL database.

## ⌛ Testing, Troubleshooting, Documentation etc...

I will try to include them as well, as soon as I can.
