Here is a setup guide to run the DocConnect Presentation API:
Make sure you have .NET SDK 7.0 installed dotnet sdk install --version 7.0.0 For windows PowerShell use winget install Microsoft.DotNet.SDK.7

Open a terminal in the "DocConnect" directory

Navigate into the project folder: cd DocConnect.Presentation.Api

Build the project and restore the required dependencies: dotnet build

Run the project: dotnet run

You should be able to access the API endpoints through the following URL: /swagger/index.html to access the swagger. And for the test endpoint use /api/Hello to ensure the server is working.

Run the tests
In order to run the unit tests use dotnet test in the Developer PowerShell
