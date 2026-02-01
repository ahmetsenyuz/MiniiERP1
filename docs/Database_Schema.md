# Database Schema Documentation

This document describes the database schema for the Mini-ERP system, including tables, relationships, and field definitions.

## Overview

The Mini-ERP system uses a relational database to store all application data. The schema is designed to support product management, supplier information, and purchase order processing.

## Tables

### Users Table

Stores user account information and authentication data.

| Field Name       | Data Type     | Description                          |
|------------------|---------------|--------------------------------------|
| Id               | Integer       | Primary key                          |
| Username         | String (50)   | Unique user identifier               |
| Email            | String (100)  | User's email address                 |
| PasswordHash     | String        | Hashed password                      |
| Role             | Enum          | User role (Administrator, OperationalUser) |

### Products Table

Stores product information including pricing and inventory details.

| Field Name           | Data Type     | Description                              |
|----------------------|---------------|------------------------------------------|
| Id                   | Integer       | Primary key                              |
| Name                 | String (100)  | Product name                             |
| SKU                  | String (50)   | Stock Keeping Unit                       |
| SellingPrice         | Decimal       | Selling price per unit                   |
| CriticalStockLevel   | Integer       | Minimum stock level that triggers alerts |

### Suppliers Table

Stores supplier information including contact details.

| Field Name       | Data Type     | Description                          |
|------------------|---------------|--------------------------------------|
| Id               | Integer       | Primary key                          |
| CompanyName      | String (100)  | Supplier company name                |
| ContactPerson    | String (100)  | Primary contact person               |
| Phone            | String (20)   | Contact phone number                 |
| Email            | String (100)  | Contact email address                |

### PurchaseOrders Table

Stores purchase order information including supplier references and order status.

| Field Name       | Data Type     | Description                          |
|------------------|---------------|--------------------------------------|
| Id               | Integer       | Primary key                          |
| SupplierId       | Integer       | Foreign key to Suppliers table       |
| OrderDate        | DateTime      | Date the order was placed            |
| Status           | Enum          | Order status (Pending, Confirmed, Cancelled) |
| TotalAmount      | Decimal       | Total order amount                   |

### PurchaseOrderItems Table

Stores individual items within purchase orders.

| Field Name       | Data Type     | Description                          |
|------------------|---------------|--------------------------------------|
| Id               | Integer       | Primary key                          |
| PurchaseOrderId  | Integer       | Foreign key to PurchaseOrders table  |
| ProductId        | Integer       | Foreign key to Products table        |
| Quantity         | Integer       | Number of units ordered              |
| UnitPrice        | Decimal       | Price per unit at time of order      |

## Relationships

### One-to-Many Relationships

1. **Users to PurchaseOrders**: One user can create multiple purchase orders (via audit trail)
2. **Suppliers to PurchaseOrders**: One supplier can have multiple purchase orders
3. **Products to PurchaseOrderItems**: One product can appear in multiple purchase order items
4. **PurchaseOrders to PurchaseOrderItems**: One purchase order can contain multiple items

### Indexes

- Primary keys are automatically indexed
- Foreign key columns are indexed for performance
- Username column in Users table is unique and indexed
- SupplierId in PurchaseOrders table is indexed
- ProductId in PurchaseOrderItems table is indexed

## Constraints

### Primary Key Constraints

- Users.Id
- Products.Id
- Suppliers.Id
- PurchaseOrders.Id
- PurchaseOrderItems.Id

### Foreign Key Constraints

- PurchaseOrders.SupplierId → Suppliers.Id
- PurchaseOrderItems.PurchaseOrderId → PurchaseOrders.Id
- PurchaseOrderItems.ProductId → Products.Id

### Check Constraints

- Products.SellingPrice >= 0
- Products.CriticalStockLevel >= 0
- PurchaseOrders.TotalAmount >= 0
- PurchaseOrderItems.Quantity > 0
- PurchaseOrderItems.UnitPrice >= 0

## Data Types

### Numeric Types

- Integer: 32-bit signed integer
- Decimal: Fixed-point decimal with precision and scale

### Text Types

- String (length): Variable-length character string with specified maximum length

### Date/Time Types

- DateTime: Date and time values with timezone information

### Enum Types

- User roles: Administrator, OperationalUser
- Purchase order status: Pending, Confirmed, Cancelled