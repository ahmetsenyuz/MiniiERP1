# Detailed Database Schema Design for MiniiERP

## Overview
This document presents the detailed database schema design for the MiniiERP system. The schema is designed to support the core functional areas of product management, procurement, sales, and dashboard features while maintaining scalability and performance.

## Database Selection
- SQL Server 2019 or later
- Entity Framework Core for data access layer

## Tables

### 1. Products Table
| Column Name | Data Type | Constraints | Description |
|-------------|-----------|-------------|-------------|
| Id | int | Primary Key, Identity | Unique identifier for each product |
| Name | nvarchar(255) | Not Null | Name of the product |
| Description | nvarchar(max) |  | Detailed description of the product |
| Price | decimal(18,2) | Not Null | Selling price of the product |
| StockLevel | int | Not Null | Current stock quantity |
| CriticalStockLevel | int | Not Null | Critical stock level for alerts |
| CreatedDate | datetime2 | Not Null | Date when product was created |
| UpdatedDate | datetime2 |  | Date when product was last updated |

### 2. Suppliers Table
| Column Name | Data Type | Constraints | Description |
|-------------|-----------|-------------|-------------|
| Id | int | Primary Key, Identity | Unique identifier for each supplier |
| CompanyName | nvarchar(255) | Not Null | Name of the supplier company |
| ContactInfo | nvarchar(max) |  | Contact information of the supplier |
| CreatedDate | datetime2 | Not Null | Date when supplier was created |
| UpdatedDate | datetime2 |  | Date when supplier was last updated |

### 3. Purchase Orders Table
| Column Name | Data Type | Constraints | Description |
|-------------|-----------|-------------|-------------|
| Id | int | Primary Key, Identity | Unique identifier for each purchase order record |
| SupplierId | int | Foreign Key to Suppliers.Id | Reference to the supplier being purchased from |
| OrderDate | datetime2 | Not Null | Date of purchase order |
| TotalCost | decimal(18,2) | Not Null | Total cost of the purchase order |
| Status | nvarchar(50) |  | Status of the purchase order (Pending, Completed, Cancelled) |
| CreatedDate | datetime2 | Not Null | Date when purchase order was created |
| UpdatedDate | datetime2 |  | Date when purchase order was last updated |

### 4. Sales Orders Table
| Column Name | Data Type | Constraints | Description |
|-------------|-----------|-------------|-------------|
| Id | int | Primary Key, Identity | Unique identifier for each sale record |
| ProductId | int | Foreign Key to Products.Id | Reference to the product being sold |
| Quantity | int | Not Null | Quantity of products being sold |
| Customer | nvarchar(255) | Not Null | Name of the customer |
| OrderDate | datetime2 | Not Null | Date of sale |
| TotalRevenue | decimal(18,2) | Not Null | Total revenue from the sale |
| Status | nvarchar(50) |  | Status of the sale (Pending, Completed, Cancelled) |
| CreatedDate | datetime2 | Not Null | Date when sale was created |
| UpdatedDate | datetime2 |  | Date when sale was last updated |

### 5. Inventory Table
| Column Name | Data Type | Constraints | Description |
|-------------|-----------|-------------|-------------|
| Id | int | Primary Key, Identity | Unique identifier for each inventory record |
| ProductId | int | Foreign Key to Products.Id | Reference to the product being tracked |
| QuantityOnHand | int | Not Null | Current quantity on hand |
| LastUpdated | datetime2 | Not Null | Date when inventory was last updated |
| CreatedDate | datetime2 | Not Null | Date when inventory record was created |

## Relationships
- Products ↔ Purchase Orders: One-to-Many (One product can have many purchase order records)
- Products ↔ Sales Orders: One-to-Many (One product can have many sales records)
- Products ↔ Inventory: One-to-One (Each product has one inventory record)

## Indexes
- Primary keys are automatically indexed
- Additional indexes on frequently queried columns:
  - Products.Name
  - PurchaseOrders.OrderDate
  - Sales.OrderDate
  - Suppliers.CompanyName
  - Inventory.LastUpdated

## Constraints
- All foreign key relationships are enforced
- Not null constraints on required fields
- Check constraints for valid data ranges (e.g., positive prices, quantities)
- Unique constraint on Products.SKU (if applicable)

## Implementation Notes
- All tables will have audit fields (CreatedDate, UpdatedDate)
- Soft delete pattern will be implemented for data integrity
- Stored procedures will be used for complex queries
- Transactions will be used for data consistency