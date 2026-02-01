# MVP Scope Document for MiniiERP

## Overview
This document outlines the minimum viable product scope for the MiniiERP system, defining core functionalities, technical decisions, and functional boundaries that will guide the development process.

## Core Functionalities
- Product Management
- Procurement
- Sales
- Dashboard

## Technical Stack Decision Checklist
- .NET 6.0 for backend development
- SQL Server for database management
- RESTful API architecture
- Entity Framework Core for data access
- Swagger for API documentation

## User Role Definitions and Permissions Matrix

| Role | Permissions |
|------|-------------|
| Admin | Full access to all modules |
| Operation User | Access to Product Management, Procurement, Sales, and Dashboard modules |

## Database Schema Preliminary Design
- Products table with fields: Id, Name, Description, Price, StockLevel
- Procurement table with fields: Id, ProductId, Quantity, Supplier, Date
- Sales table with fields: Id, ProductId, Quantity, Customer, Date
- Users table with fields: Id, Username, Password, Role

## Out of Scope
- Advanced reporting features
- Inventory tracking beyond critical stock levels
- Integration with external systems
- Complex user permission management
- Multi-language support

## Acceptance Criteria
- Scope document clearly defines what is included/excluded from MVP
- Technical decisions are documented and approved
- User roles and their permissions are clearly defined
- Database schema preliminary design is complete and approved