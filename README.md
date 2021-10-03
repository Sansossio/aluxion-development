# Aluxion Development

## File Handler

To see test details click [here](./TEST-DETAILS.md)

## Run

### Run services
This project needs to have a database in postgres and a smtp server, in this case I have used a smtp mock to simplify. Just use:
```sh
docker-compose up -d
```

### Run project
Install dependencies and then use VsCode launch.

### Swagger
Local swagger url: [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html)

### Smtp mock
Local smtp mock url: [http://localhost:8092/](http://localhost:8092/)
- user = aluxion
- password = m41ler

## Unit tests

```sh
cd AluxionDevelopment
dotnet test
```
