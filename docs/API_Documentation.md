# API Documentation

This document provides detailed information about the Mini-ERP system's RESTful API endpoints.

## Authentication

All API requests require authentication. Use the `/api/auth/login` endpoint to obtain a JWT token.

### Login Request

```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "password123"
}
```

### Login Response

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresIn": 86400
}
```

## Endpoints

### Products

#### Get All Products

```http
GET /api/products
Authorization: Bearer {token}
```

#### Get Product by ID

```http
GET /api/products/{id}
Authorization: Bearer {token}
```

#### Search Products

```http
GET /api/products/search?name={productName}
Authorization: Bearer {token}
```

#### Create Product

```http
POST /api/products
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Sample Product",
  "sku": "SKU001",
  "sellingPrice": 29.99,
  "criticalStockLevel": 10
}
```

#### Update Product

```http
PUT /api/products/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Updated Product",
  "sku": "SKU001",
  "sellingPrice": 39.99,
  "criticalStockLevel": 15
}
```

#### Delete Product

```http
DELETE /api/products/{id}
Authorization: Bearer {token}
```

### Suppliers

#### Get All Suppliers

```http
GET /api/suppliers
Authorization: Bearer {token}
```

#### Get Supplier by ID

```http
GET /api/suppliers/{id}
Authorization: Bearer {token}
```

#### Create Supplier

```http
POST /api/suppliers
Authorization: Bearer {token}
Content-Type: application/json

{
  "companyName": "ABC Supplies",
  "contactPerson": "John Doe",
  "phone": "123-456-7890",
  "email": "john@abc.com"
}
```

#### Update Supplier

```http
PUT /api/suppliers/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "companyName": "ABC Supplies Updated",
  "contactPerson": "Jane Smith",
  "phone": "098-765-4321",
  "email": "jane@abc.com"
}
```

#### Delete Supplier

```http
DELETE /api/suppliers/{id}
Authorization: Bearer {token}
```

### Purchase Orders

#### Get Purchase Order by ID

```http
GET /api/purchaseorders/{id}
Authorization: Bearer {token}
```

#### Get Purchase Orders by Supplier

```http
GET /api/purchaseorders/supplier/{supplierId}
Authorization: Bearer {token}
```

#### Create Purchase Order

```http
POST /api/purchaseorders
Authorization: Bearer {token}
Content-Type: application/json

{
  "supplierId": 1,
  "orderDate": "2023-01-01T00:00:00Z",
  "items": [
    {
      "productId": 1,
      "quantity": 10,
      "unitPrice": 15.99
    }
  ],
  "totalAmount": 159.90
}
```

#### Confirm Purchase Order

```http
POST /api/purchaseorders/{id}/confirm
Authorization: Bearer {token}
```

#### Cancel Purchase Order

```http
POST /api/purchaseorders/{id}/cancel
Authorization: Bearer {token}
```

## Error Handling

The API returns standard HTTP status codes along with JSON error responses:

```json
{
  "error": "Invalid credentials"
}
```

## Rate Limiting

The API implements rate limiting to prevent abuse. Exceeding the limit will result in a 429 Too Many Requests response.