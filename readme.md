## Run Requirements
You Need Docker and Docker-compose installed to run this app.

## How To Run
in solution directory run ```docker-compose up```

## Notes
- You can run E2E Tests with running ```dotnet test``` in solution directory(it uses testContainers)


- Also there is an Acceptance Test project that can test the UI, for this to work first do ```docker-compose up``` and then use ```dotnet test``` in ```test/CustomerTest.AcceptanceTests``` project directory


- Swagger UI is available on ```http://localhost:5090/swagger```


- SWA UI is available on ```http://localhost:5091```
