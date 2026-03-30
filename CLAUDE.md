# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a legacy ASP.NET Web Forms application (BW_WebApp) targeting .NET Framework 4.8. It appears to be an inventory/warehouse management system for mobile device repairs and parts management, with features for receiving, tracking, labeling, and reporting on electronic components and repairs.

## Build and Development Commands

### Building the Application
```bash
# Build the solution using MSBuild
msbuild BW_WebApp.sln /p:Configuration=Debug
# or for Release
msbuild BW_WebApp.sln /p:Configuration=Release
```

### Running the Application
- The application is designed to run on IIS or IIS Express
- Database connection string is configured in Web.config pointing to SQL Server instance
- Application uses ASP.NET membership for authentication

## Architecture Overview

### Technology Stack
- **Framework**: ASP.NET Web Forms (.NET Framework 4.8)
- **Database**: SQL Server with LINQ to SQL for data access
- **UI Components**: Syncfusion controls, AjaxControlToolkit
- **Authentication**: ASP.NET Membership with SQL Server provider
- **Excel Processing**: EPPlus library

### Key Directories Structure
- **Account/**: User authentication and role management pages
- **Classes/**: Business logic, API integration, upload processors, utility classes
- **DataManagers/**: Data access layer with managers for different business domains
- **Dashboard/**: Various dashboard pages for different user roles and functions
- **Content/**: Static content and styling
- **App_Data/**: Database files and application data

### Data Access Pattern
- Uses LINQ to SQL (DataLinq.cs) as the primary ORM
- Manager classes handle specific business domains:
  - `DataManager.cs`: Base data operations
  - `DashboardManager.cs`: Dashboard data
  - `SalesOrderManager.cs`: Sales order processing
  - `ExcelManager.cs`: Excel import/export operations
  - `CycleCountManager.cs`: Inventory cycle counting

### Key Business Components
- **Upload Processors**: Handle various IMEI and data uploads
- **API Integration**: Cellbie API integration for external system communication
- **Reporting**: Excel-based reporting system
- **Barcode/Label Generation**: Support for various label types
- **Inventory Management**: Stock tracking, receiving, transfers

### Configuration
- **Connection Strings**: Configured in Web.config for production SQL Server database
- **App Settings**: Various application-specific settings in Web.config
- **Authentication**: Forms authentication with SQL membership provider
- **NuGet Packages**: Managed via packages.config

## Development Notes

### Database Connection
The application connects to a SQL Server database with connection string configured in Web.config. The database appears to be production-ready with proper authentication.

### Authentication & Authorization
- Uses ASP.NET Membership with custom role-based access
- Role management through `Menu_RoleManager.cs`
- User-based authorization with forced password reset functionality

### Key Business Processes
- **Receiving Process**: Multi-step receiving workflow with authorization, transfer, and quick ship options
- **Order Entry**: Multiple order entry screens for different scenarios
- **Reporting**: Various report forms for repairs, submissions, and authorizations
- **Label Printing**: Barcode and product label generation
- **Dashboard Management**: Role-specific dashboards for different user types

### Third-Party Integrations
- Syncfusion UI components for enhanced user interface
- EPPlus for Excel file processing
- AjaxControlToolkit for enhanced web controls
- External API integration through Cellbie_API.cs