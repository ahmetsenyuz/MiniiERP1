# Configuration Guidelines

This document provides guidelines for configuring the Mini-ERP system in different deployment environments.

## Environment Configuration

The application uses environment-specific configuration files to manage settings across different deployment scenarios.

### Configuration Files

The system reads configuration from the following files:

1. `appsettings.json` - Default configuration
2. `appsettings.{Environment}.json` - Environment-specific overrides

### Common Configuration Sections

#### ConnectionStrings

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MiniERP;Trusted_Connection=true;"
  }
}
```

#### JwtSettings

```json
{
  "JwtSettings": {
    "SecretKey": "your-super-secret-key-here",
    "Issuer": "MiniERP",
    "Audience": "MiniERPUsers"
  }
}
```

#### Logging

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Development Environment

### Setup Requirements

- .NET SDK 6.0 or later
- SQL Server LocalDB or SQL Server Express
- Visual Studio or VS Code

### Configuration

For development, use `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MiniERPDev;Trusted_Connection=true;"
  },
  "JwtSettings": {
    "SecretKey": "development-secret-key",
    "Issuer": "MiniERPDev",
    "Audience": "MiniERPDevUsers"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  }
}
```

## Staging Environment

### Setup Requirements

- .NET SDK 6.0 or later
- SQL Server instance
- Web server (IIS, Apache, or Nginx)

### Configuration

For staging, use `appsettings.Staging.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=staging-server;Database=MiniERPStaging;User Id=staging-user;Password=staging-password;"
  },
  "JwtSettings": {
    "SecretKey": "staging-secret-key",
    "Issuer": "MiniERPStaging",
    "Audience": "MiniERPStagingUsers"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "System": "Warning",
      "Microsoft": "Warning"
    }
  }
}
```

## Production Environment

### Setup Requirements

- .NET SDK 6.0 or later
- SQL Server instance (preferably with high availability)
- Web server with SSL certificate
- Load balancer (optional but recommended)

### Configuration

For production, use `appsettings.Production.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-server;Database=MiniERPProd;User Id=prod-user;Password=prod-password;"
  },
  "JwtSettings": {
    "SecretKey": "production-secret-key",
    "Issuer": "MiniERPProd",
    "Audience": "MiniERPProdUsers"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "System": "Error",
      "Microsoft": "Error"
    }
  }
}
```

## Environment Variables

For sensitive information, use environment variables:

### Database Connection String

Set the `ConnectionStrings__DefaultConnection` environment variable:

```bash
export ConnectionStrings__DefaultConnection="Server=server-name;Database=MiniERP;User Id=username;Password=password;"
```

### JWT Secret Key

Set the `JwtSettings__SecretKey` environment variable:

```bash
export JwtSettings__SecretKey="your-production-secret-key"
```

## Security Considerations

### Database Security

1. Use strong, unique passwords for database users
2. Implement least privilege principle for database access
3. Enable encryption at rest and in transit
4. Regularly backup the database

### Application Security

1. Store JWT secret keys securely (environment variables)
2. Use HTTPS in production environments
3. Implement proper input validation
4. Regular security audits and updates

### Network Security

1. Restrict database access to application servers only
2. Use firewalls to protect database ports
3. Implement network segmentation
4. Monitor network traffic for suspicious activity

## Deployment Scripts

### Docker Deployment

For containerized deployments, use the provided Dockerfile and docker-compose.yml:

```yaml
version: '3.4'

services:
  mini-erp:
    image: ${DOCKER_REGISTRY-}mini-erp
    build:
      context: .
      dockerfile: MiniERP1/Dockerfile
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__DefaultConnection=${CONNECTION_STRING}
      - JwtSettings__SecretKey=${JWT_SECRET_KEY}
    ports:
      - "80:80"
      - "443:443"
```

### CI/CD Pipeline

Configure your CI/CD pipeline to:

1. Build the application with appropriate configuration
2. Run automated tests
3. Deploy to staging environment
4. Perform manual approval before production deployment
5. Apply configuration changes via environment variables

## Monitoring and Maintenance

### Health Checks

The application includes health checks that can be accessed at `/health` endpoint.

### Logging

Configure logging levels appropriately for each environment:
- Development: Debug
- Staging: Information
- Production: Warning

### Backup Strategy

Implement regular backups of:
- Database snapshots
- Configuration files
- Application binaries

## Troubleshooting

### Common Configuration Issues

1. **Connection String Errors**: Verify database connectivity and credentials
2. **JWT Token Issues**: Check secret key consistency across environments
3. **Environment Variable Not Found**: Ensure variables are properly set in deployment environment
4. **Missing Configuration Files**: Verify file paths and permissions

### Performance Tuning

1. Optimize database queries
2. Configure appropriate connection pooling
3. Set up caching where appropriate
4. Monitor resource usage in production