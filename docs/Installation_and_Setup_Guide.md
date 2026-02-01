# Installation and Setup Guide

This document provides step-by-step instructions for installing and setting up the Mini-ERP system.

## Prerequisites

Before installing the Mini-ERP system, ensure you have the following:

- .NET SDK 6.0 or later
- Visual Studio or VS Code
- SQL Server or SQL Server Express
- Node.js (for frontend development, if applicable)

## Installation Steps

### 1. Clone the Repository

```bash
git clone https://github.com/ahmetsenyuz/MiniiERP1.git
cd MiniiERP1
```

### 2. Restore NuGet Packages

```bash
dotnet restore
```

### 3. Build the Solution

```bash
dotnet build
```

### 4. Run Database Migrations

```bash
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run
```

## Configuration

The application uses `appsettings.json` for configuration. You can modify the connection strings and other settings in this file.

## Environment Variables

For production deployments, consider using environment variables for sensitive information such as database passwords and API keys.

## Testing

To run unit tests:

```bash
dotnet test
```

## Troubleshooting

If you encounter any issues during installation:

1. Ensure all prerequisites are installed correctly
2. Check that the database server is running
3. Verify that the connection string in `appsettings.json` is correct
4. Make sure you have sufficient permissions to access the database

## Support

For additional support, please contact the development team or refer to the project's issue tracker.