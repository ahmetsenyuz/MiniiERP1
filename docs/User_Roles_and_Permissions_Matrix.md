# User Roles and Permissions Matrix for MiniiERP

## Overview
This document defines the user roles and their corresponding permissions within the MiniiERP system. The system supports two primary user roles with distinct access levels to ensure proper security and operational efficiency.

## User Roles

### 1. Admin
#### Description
The administrator role has full access to all modules and features within the MiniiERP system. This role is responsible for system administration, user management, and overall platform oversight.

#### Permissions
- Access to all modules (Product Management, Procurement, Sales, Dashboard)
- Create, Read, Update, Delete (CRUD) operations on all entities
- User management (create, modify, delete users)
- System configuration settings
- View all reports and analytics
- Manage system-wide settings
- Access to audit logs

### 2. Operation User
#### Description
The operation user role is designed for daily operational tasks within the MiniiERP system. This role has limited access to core business functions necessary for day-to-day operations.

#### Permissions
- Product Management: View and update product information
- Procurement: Create and manage procurement orders
- Sales: Create and manage sales orders
- Dashboard: View operational dashboards and reports
- Limited access to system configuration
- Access to basic analytics and reporting

## Permission Mapping

| Module | Admin | Operation User |
|--------|-------|----------------|
| Product Management | Full CRUD | Read/Update |
| Procurement | Full CRUD | Create/Read |
| Sales | Full CRUD | Create/Read |
| Dashboard | Full Access | View Only |
| User Management | Full Access | No Access |
| System Configuration | Full Access | Limited Access |
| Reports | Full Access | View Only |

## Implementation Notes
- Role-based access control (RBAC) will be implemented using JWT tokens
- Each user session will include role information for authorization checks
- Permissions will be validated at both API level and UI level where applicable
- Regular review of role permissions will be conducted to maintain security standards