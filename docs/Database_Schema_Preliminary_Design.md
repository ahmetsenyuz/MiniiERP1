# Database Schema Preliminary Design for MiniiERP

## Overview
This document presents the preliminary database schema design for the MiniiERP system. The schema is designed to support the core functionalities of product management, procurement, sales, and dashboard features while maintaining scalability and performance.

## Database Selection
- SQL Server 2019 or later
- Entity Framework Core for data access layer

## Tables

### 1. Products Table
| Column Name | Data Type | Constraints | Description |
|-------------|-----------|-------------|-------------|
| Id | int | Primary Key, Identity | Unique identifier for each product |
| Name | nvarchar(255) | Not Null | Name of the product |
| Description | nvarchar(max) | | Detailed description of the product |
| Price | decimal(18,2) | Not Null | Selling price of the product |
| StockLevel | int | Not Null | Current stock quantity |
| CreatedDate | datetime2 | Not Null | Date when product was created |
| UpdatedDate | datetime2 | | Date when product was last updated |

### 2. Procurement Table
| Column Name | Data Type | Constraints | Description |
|-------------|-----------|-------------|-------------|
| Id | int | Primary Key, Identity | Unique identifier for each procurement record |
| ProductId | int | Foreign Key to Products.Id | Reference to the product being procured |
| Quantity | int | Not Null | Quantity of products being procured |
| Supplier | nvarchar(255) | Not Null | Name of the supplier |
| Date | datetime2 | Not Null | Date of procurement |
| TotalCost | decimal(18,2) | Not Null | Total cost of the procurement |
| Status | nvarchar(50) | | Status of the procurement (Pending, Completed, Cancelled) |

### 3. Sales Table
| Column Name | Data Type | Constraints | Description |
|-------------|-----------|-------------|-------------|
| Id | int | Primary Key, Identity | Unique identifier for each sale record |
| ProductId | int | Foreign Key to Products.Id | Reference to the product being sold |
| Quantity | int | Not Null | Quantity of products being sold |
| Customer | nvarchar(255) | Not Null | Name of the customer |
| Date | datetime2 | Not Null | Date of sale |
| TotalRevenue | decimal(18,2) | Not Null | Total revenue from the sale |
| Status | nvarchar(50) | | Status of the sale (Pending, Completed, Cancelled) |

### 4. Users Table
| Column Name | Data Type | Constraints | Description |
|-------------|-----------|-------------|-------------|
| Id | int | Primary Key, Identity | Unique identifier for each user |
| Username | nvarchar(100) | Not Null, Unique | Username for login |
| PasswordHash | nvarchar(max) | Not Null | Hashed password for security |
| Role | nvarchar(50) | Not Null | User role (Admin, Operation User) |
| Email | nvarchar(255) | | Email address of the user |
| CreatedDate | datetime2 | Not Null | Date when user account was created |
| IsActive | bit | Not Null | Flag indicating if user account is active |

## Relationships
- Products ↔ Procurement: One-to-Many (One product can have many procurement records)
- Products ↔ Sales: One-to-Many (One product can have many sales records)
- Products ↔ Users: One-to-Many (One product can be managed by many users)

## Indexes
- Primary keys are automatically indexed
- Additional indexes on frequently queried columns:
  - Products.Name
  - Procurement.Date
  - Sales.Date
  - Users.Username

## Constraints
- All foreign key relationships are enforced
- Not null constraints on required fields
- Check constraints for valid data ranges (e.g., positive prices, quantities)

## Implementation Notes
- All tables will have audit fields (CreatedDate, UpdatedDate)
- Soft delete pattern will be implemented for data integrity
- Stored procedures will be used for complex queries
- Transactions will be used for data consistency