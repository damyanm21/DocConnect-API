# doc-connect-be

## Here is a setup guide to run the DocConnect Presentation API:

1. Make sure you have .NET SDK 7.0 installed
`dotnet sdk install --version 7.0.0` 
For windows PowerShell use 
`winget install Microsoft.DotNet.SDK.7`

2. Open a terminal in the "DocConnect" directory

3. Navigate into the project folder:
`cd DocConnect.Presentation.Api`

4. Build the project and restore the required dependencies:
`dotnet build`

5. Run the project:
`dotnet run`

6. You should be able to access the API endpoints through the following URL:
/swagger/index.html to access the swagger. 
And for the test endpoint use /api/Hello to ensure the server is working.

## Run the tests

In order to run the unit tests use `dotnet test` in the Developer PowerShell
