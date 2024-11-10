
# Advanced Online Hotel Booking System - Backend

This backend project powers the core functionalities of the Online Hotel Booking System, providing APIs that support multiple features for both users and administrators. The platform includes endpoints for login, search, hotel details, secure booking, and admin management. With a focus on Clean Architecture and RESTful principles, these APIs ensure secure and efficient data management.

## Project Overview
This project is structured following the **Clean Architecture** model, organized into layers to ensure high cohesion and low coupling:

![image](https://github.com/user-attachments/assets/05df0514-d8ab-431a-a6db-5bc48b0b42a7)

- **Domain Layer**: Contains business logic and entities.
- **Application Layer**: Responsible for service interfaces, DTOs, and use cases.
- **Infrastructure Layer**: Database and external dependencies.
- **Presentation Layer**: API endpoints defined using FastEndpoints.

### FastEndpoints
![image](https://github.com/user-attachments/assets/0a960fd9-c8b9-4ffe-a997-dc2fb0171071)

The **FastEndpoints** framework is utilized in the Presentation Layer to streamline API endpoint management and improve response times.

**Each API route is organized as a separate class, making it easier to manage and extend functionalities across the application.**

Key advantages of using FastEndpoints in this project include:

- **Simplified Endpoint Definitions**: Each endpoint is organized in a modular way, allowing clear separation of concerns.
- **Enhanced Performance**: FastEndpoints is optimized for speed, resulting in faster response times compared to traditional controllers.
- **Built-in Validation**: FastEndpoints provides request and response validation out of the box.
- **Dependency Injection**: Supports dependency injection directly within endpoints, aligning with Clean Architecture principles.

## Technologies Used

- **ASP.NET Core**
- **FastEndpoints**
- **JWT Authentication**
- **AutoMapper**
- **Entity Framework Core**
- **Swagger**
- **XUnit**
- **Moq**
- **FluentAssertions**

## Database ER Diagram

This ER diagram shows the database schema. Which includes tables representing essential entities such as Users, Hotels, Bookings, and more.

![ERdiagram](https://github.com/user-attachments/assets/7d12d740-44e9-4dae-8e4a-a6bacf155063)

Before you run the project, make sure to:
   - Create a SQL Server database named **`TBPDatabase`**.
   - Set up Entity Framework Core in the project.
   - Write migrations to create tables for entities such as Users, Hotels, Reservations, etc. The migration files should reside in the **`Infrastructure/Migrations`** folder.

## Features

### 1. Login
- **Login Page**: Fields for entering a username and password.
- **JWT Authentication**: Secure login with JSON Web Token for session management.
- **Role-based Access Control (RBAC)**: Access control based on roles like admin and regular user.

### 2. Search Functionality
- **Search APIs**: APIs for handling hotel and room searches with various parameters.
- **Features**:
  - Central search bar, interactive calendar, adjustable controls for adults and children, and room selection.
  - Sidebar filters for price range, star rating, and amenities.
  - List of hotels/rooms matching search criteria with thumbnails and relevant details.

### 3. Featured Deals Functionality
- **Featured Deals' APIs**: APIs for fetching and displaying featured hotels with relevant details.
- **Features**: Display of 3-5 hotels, each with a thumbnail, hotel name, location, and both original and discounted prices.

### 4. Hotels Functionality
- **Hotels APIs**: APIs for fetching and displaying all the hotel's information with images.
- **Features**: Detailed Hotel Information, Visual Gallery, Room Availability and Selection.
- **Booking APIs**: APIs that enable users to book selected rooms based on thier availability.

### 5. Admin Management
- **City, Hotel, and Room Management**: CRUD operations for cities, hotels, and rooms via a user-friendly interface.
- **Search and Filtering**: Filters for easy management of hotels, and rooms.
- **Detailed Grids**: Editable grids for cities, hotels, and rooms.
- Note that the (Create/Update/Delete) operations are only allowed for Admins.

## Additional Features

1. **Clean Code Principles**
    - Code is readable, maintainable, and adheres to standard coding conventions.
    - Implemented consistent naming conventions, code structuring, and comments for better readability.
  
2. **Robust Error Handling and Logging**
    - Implemented comprehensive error handling to catch and manage exceptions gracefully.
    - Used logging to track errors, for easier debugging and monitoring.
  
3. **Secure JWT Authentication**
    - Implemented JSON Web Token (JWT) authentication to securely handle user sessions.
    - Tokens are stored and transmitted securely.

4. **User Permissions Management**
    - Implemented role-based access control (RBAC) for fine-grained permissions.
    - Designd a robust permission system to control user access to different parts of the application.
    - Regular users are only authorized to fetch data and create bookings
    - Admins have extra permissions to modify the data (create/update/delete).

5. **Unit Testing**
    - Developed a suite of unit tests that ensures the functionality and reliability of the App.
    - Tests covers all the application's use cases with a wide range of scenarios.

6. **Documentation**
    - API documentation is provided through Swagger for easy reference and testing.

## Setup and Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/OsamaRimawi/Travel-and-Accommodation-Platform-.Net-Clean-Architecture.git
   cd TravelBookingPlatform/Src/TBP.Presentation
   ```

2. Update the configuration settings in `appsettings.json` for your SQL database and JWT token settings.

3. Start the application:
   ```bash
   dotnet run
   ```

## API Documentation

To access the API documentation, start the application and navigate to:
```
http://localhost:5239/swagger
```

The Swagger documentation includes details for each endpoint, including request parameters, responses, and example payloads.

## Testing

This project includes comprehensive unit tests to ensure robustness and functionality. Run the tests using:
```bash
cd TravelBookingPlatform/Tests/TBP.Core.UnitTests
dotnet test
```

## Contributing
Osama Rimawi (Feel free to open issues or submit pull requests).

## License

This project is licensed under the MIT License.
