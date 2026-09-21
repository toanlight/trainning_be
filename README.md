# 🏗️ CleanArch Training BE

Hệ thống CRUD nhỏ theo chuẩn **Clean Architecture** — dùng để học tập và thực hành.

## 📐 Kiến trúc

```
CleanArch.TrainingBE/
├── src/
│   ├── CleanArch.Domain/            ← Layer 1: Entities, Exceptions, Interfaces
│   ├── CleanArch.Application/       ← Layer 2: DTOs, Services, Validators, AutoMapper
│   ├── CleanArch.Infrastructure/    ← Layer 3: EF Core, SQL Server, Repositories
│   └── CleanArch.API/               ← Layer 4: Controllers, Swagger, Middleware
└── CleanArch.sln
```

## 🛠️ Tech Stack

| Công nghệ | Mục đích |
|-----------|---------|
| .NET 8 | Runtime |
| SQL Server | Database |
| Entity Framework Core 8 | ORM |
| AutoMapper 13 | Object Mapping |
| FluentValidation 11 | Input Validation |
| Swashbuckle (Swagger) | API Documentation |

## 🚀 Chạy ứng dụng

### 1. Cấu hình Connection String

Mở `src/CleanArch.API/appsettings.json` và cập nhật:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=CleanArchTrainingDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 2. Tạo Database (Migration)

```bash
cd src/CleanArch.API
dotnet ef migrations add InitialCreate --project ../CleanArch.Infrastructure
dotnet ef database update
```

### 3. Chạy API

```bash
dotnet run --project src/CleanArch.API
```

### 4. Mở Swagger UI

Truy cập: `https://localhost:{port}/` (Swagger UI ở root URL)

## 📋 API Endpoints

| Method | Endpoint | Mô tả |
|--------|----------|-------|
| `GET` | `/api/products` | Lấy danh sách (phân trang + tìm kiếm) |
| `GET` | `/api/products/{id}` | Lấy theo Id |
| `POST` | `/api/products` | Tạo mới |
| `PUT` | `/api/products/{id}` | Cập nhật |
| `DELETE` | `/api/products/{id}` | Xóa mềm (Soft Delete) |

## 📦 Query Parameters (GET /api/products)

| Tham số | Mặc định | Mô tả |
|---------|---------|-------|
| `pageNumber` | 1 | Số trang |
| `pageSize` | 10 | Số bản ghi/trang (tối đa 100) |
| `searchTerm` | null | Tìm theo tên, danh mục, mô tả |
| `sortBy` | createdAt | name, price, quantity, category |
| `sortDescending` | false | Sắp xếp giảm dần |

## 🎯 Clean Architecture Dependency Rule

```
API → Application → Domain ← Infrastructure
```

- **Domain**: Không phụ thuộc vào bất kỳ layer nào khác
- **Application**: Chỉ phụ thuộc vào Domain
- **Infrastructure**: Implement các interfaces của Domain
- **API**: Phụ thuộc vào Application và Infrastructure (chỉ để DI)
