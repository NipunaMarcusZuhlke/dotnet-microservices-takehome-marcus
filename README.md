# dotnet-microservices-takehome-marcus


An order processing system with event driven architecture.


## How to Run the Service Locally


### Prerequisites
  - .NET 8+


### Steps


  - Navigate to `OrderProcessingSystem\OrderProcessor`
  - Run `dotnet build` command
  - Run `dotnet run` command
    - Application should be up and running on `http://localhost:5207`
    - Access Swagger UI on browser using `http://localhost:5207/swagger/index.html`
  - To start the event flow, start by creating an order using `api/orders`. Can use the below example Curl in CMD or Example request body in swagger UI.
 
    *Curl:*


    ```sh
    curl -X 'POST' \
    'http://localhost:5207/api/orders' \
    -H 'accept: text/plain' \
    -H 'Content-Type: application/json' \
    -d '{
    "amount": 49.09,
    "customerEmail": "sample@gmail.com"
    }'
    ```
    *Swagger UI:*


    POST `api/orders`


    Example Request Body:
    ```json
    {
      "amount": 49.09,
      "customerEmail": "sample@gmail.com"
    }
    ```
  - Can observe the logs to see the Event Flow.


### Run Tests


Navigate to `.\OrderProcessingSystem` and then run `dotnet test` command


### All available endpoints


  - POST api/orders          : Create new order
  - GET api/orders           : Get all created orders
  - GET api/orders/{orderId} : Get order by order ID
  - GET api/payments         : Get all processed payments
  - GET api/notifications    : Get all notifications


## Architecture Overview


  - Three Independent microservices with clear separation on endpoint, application, domain and infrastructure using Domain Driven Design to cleanly separate each layer.
  - Services communicating only using Events via in memory event bus to reduce any dependency.
  - All three services have three different storages (EF In Memory DBs) to separate the data storages as well and follow `Repository Pattern`.


## Event Flow Description


Event flow is as described below in the diagram.


![Event Flow](Docs/event_flow.jpg)


As shown in the diagram


  - Orders service will receive order creation from a client and then validate the request and proceed to publish the `Order Created Event` with Order ID and other data.
  - Payments service will receive `Order Created Event` through the event bus and then proceed to process and then save the processed payment with Payment ID and order data. If all goes well proceed to publish the `Payment Succeeded Event` with Payment ID and other order related data.
  - Notification service will receive `Payment Succeeded Event` through the event bus and then proceed to process and then save the notification with Notification ID and Payment Data and log.


## Design Decisions and Assumptions
  - Used Domain Driven Design to structure each microservice to separate the following layers.
   
    ![Event Flow](Docs/application_ddd_structure_dependency.jpg)
 
    - endpoints
      - contains controllers which use an application layer (services) to process or fetch data.
      - This will only use the application layer.
    - application
      - contains DTOs, services and message processors and publisher contracts.
      - this layer will contain implementation for the process orchestration such as saving information to the database and publish the saved information as an event.
      - this will contain the message processor implementations and service implementations which will use domain and shared domain.
    - domain
      - contains entities and repository contracts
      - This only contains the core business logics such as Entity models and contracts for repositories.
    - infrastructure
      - contains external system integrations (DB Context) and repository implementations.
      - Infrastructure will use other layers such as application and domain to integrate external systems such as databases by implementing the repository contract and publishing and subscribing logic for event bus.


  - Used EF Inmemory DB for easiness and cleanliness of implementation and to keep the future extendability in mind.
  - Extracted `OrderCreatedEvent` and `PaymentSucceededEvent` domain models to a shared domain as these are shared between services. In the future these can be put on to a different project to be used among different microservices without creating a dependency.
  - Extracted out Even bus to a different structure as it is also shared among each service.
  - Created a single middleware which is used to handle global exceptions for the `OrderProcessor` project and in the future as well this middleware implementation can be shared among microservices.
  - Added tests to cover only business logic such as services, mapping and event processing in payment and notification services and all the controllers. Other implementations such as repository and event bus are not covered in unit tests as they are using external systems.


## Any known limitations and future improvements


  - Since using the in memory event bus didn't create three different microservices but only the structures and separation to mimic three different microservices. In the future create three microservices which can deploy independently.
  - No Tracing added for application flow as events can only track through Order ID, Payment ID or Notification ID. Maybe in the future add tracing and add observability.
  - In Memory Event Bus has no retrying mechanism or better way to subscribe hence Implement to use RabbitMQ or Kafka instead of in memory event bus to handle events.
  - In the solution now we have used EF In Memory database but in future, a database can be used instead of in memory EF database.
