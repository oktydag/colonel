
# Colonel

- Colonel is the simplest e-commerce backend application that written with Asp.net Core 2.2 and includes only Add to Basket Service with Domains such as Stock, Price, Product and User.

- DDD, TDD is applied to this application, that dockerized due to including 5 different API project and 1 Mongo Client. 
- Nunit, FluentAssertion and Moq libraries used for Test Projects.
- Swagger is used and initial endpoints set up swagger/index.html for each project.
- The Infrastructure of Event Driven Architecture is applied for the possibility of using RabbitMQ, Kafka, Elasticsearch etc..
- The Infrastructure of Log Service is applied and writes to Console, may be, you can write them Elasticsearch or File System.

 # Getting Started

# Download or clone this repo with

```
git clone https://github.com/oktydag/colonel.git
```


# Steps For Run the application
## 1.  Docker Compose Build


```
$ cd src
$ docker-compose up --build
```

After that, you can see started containers;

```
$ docker container ls
```
## 2.  Projects and Mongo Server Connection

There are 5 project and these are configurated by **docker-compose.yml** file and their ports to connect with Swagger;

- **Colonel.Price**  : http://localhost:5001/swagger/index.html
- **Colonel.Stock**  : http://localhost:5002/swagger/index.html
- **Colonel.Product**  : http://localhost:5003/swagger/index.html
- **Colonel.Shopping**  : http://localhost:5004/swagger/index.html
- **Colonel.User**  : http://localhost:5005/swagger/index.html

Also there are 1 Mongo Server that also by **docker-compose.yml** file and generated port to ;
```
$ 0.0.0.0:27017->27017/tcp
```

# Data Initializer

When the projects are start up via Docker, Price, Product, Stock and User projects connect to Mongo Client and set up dummy data that seen in Startup.cs. and each Database can be control from Mongo Client.

```csharp
Task.Factory.StartNew(() => priceRepository.InitializeData());
Task.Factory.StartNew(() => _productRepository.InitializeData());
Task.Factory.StartNew(() => _stockRepository.InitializeData());
Task.Factory.StartNew(() => _userRepository.InitializeData());
```


# Ekstra
You can set up Dockerfiles and docker-compose.yml to your own port configuration. 

- Each Project setted up own port **Entrypoint** such as **Dockerfile** in Colonel.Shopping;
```dockerfile
ENTRYPOINT  ["dotnet","Colonel.Shopping.dll",  "--port",  "5004"]
```

and listen to own ports into **docker-compose.yml** file and setted the dependency docker images such as;
```
    shopping:
        build:
            context: "./Colonel.Shopping"
        depends_on:
            - "mongo"
            - "product"
            - "price"
            - "user"
            - "stock"
        ports:
            - "5004:5004"
        links: 
            - "mongo"
            - "product"
            - "price"
            - "user"
            - "stock"
```
