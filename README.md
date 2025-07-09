# Login and Dashboard Web Application

This project is a simple full-stack web application featuring a .NET Core backend API and an Angular frontend. It includes a login page and a dashboard page that displays a chart with dummy data.

## Project Structure

-   `LoginDashboardApi/`: Contains the .NET Core Web API backend.
-   `LoginDashboardApp/`: Contains the Angular frontend application.

## Prerequisites

Before you begin, ensure you have the following installed:

-   [.NET SDK](https://dotnet.microsoft.com/download) (Version 6.0 or later, as used in this project)
-   [Node.js and npm](https://nodejs.org/) (Node.js LTS version recommended, which includes npm)
-   [Angular CLI](https://angular.io/cli) (globally installed: `npm install -g @angular/cli`)

## Backend Setup (LoginDashboardApi)

The backend is a .NET Core Web API that handles authentication and serves data for the dashboard.

1.  **Navigate to the backend directory:**
    ```bash
    cd LoginDashboardApi
    ```

2.  **Restore dependencies:**
    ```bash
    dotnet restore
    ```

3.  **Run the application:**
    ```bash
    dotnet run
    ```
    By default, the API will run on:
    -   HTTP: `http://localhost:5172`
    -   HTTPS: `https://localhost:7076` (if HTTPS redirection is enabled and dev certs are trusted)

    The Angular frontend is configured to proxy requests to `http://localhost:5172`.

## Frontend Setup (LoginDashboardApp)

The frontend is an Angular application that provides the user interface.

1.  **Navigate to the frontend directory:**
    ```bash
    cd LoginDashboardApp
    ```

2.  **Install dependencies:**
    If you encounter issues, try removing `node_modules` and `package-lock.json` first, then clear npm cache:
    ```bash
    # Optional cleaning steps if issues occur
    # rm -rf node_modules package-lock.json
    # npm cache clean --force
    npm install
    ```

3.  **Run the Angular development server:**
    ```bash
    ng serve
    ```
    The application will typically be available at `http://localhost:4200/`.
    The development server is configured with a proxy (`proxy.conf.json`) to forward API requests starting with `/api` to the backend server at `http://localhost:5172`.

## Using the Application

1.  **Ensure both the backend API and the frontend application are running.**
2.  Open your web browser and navigate to `http://localhost:4200/`.
3.  You should see the login page.
4.  **Login Credentials:**
    -   Username: `testuser`
    -   Password: `password123`
5.  Upon successful login, you will be redirected to the dashboard page, which will display a sample chart.

## Key Features Implemented

-   User login with username/password.
-   JWT token-based authentication.
-   Dashboard page displaying a chart with dummy data from the backend.
-   Backend API protected using JWT.
-   Rate-limiting on the login endpoint (5 requests per minute per IP).
```
