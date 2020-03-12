# Payment Gateway

This project is to proved a payment gateway between different merchants and banks, with 2 main requirement:

1. Processing payments (forwarding them to the appropriate bank)
2. Retrieving payment details

## Building

Run `scripts/build.sh(ps1)`

## Testing

### Unit tests

Run `scripts/test.sh(ps1)`

### Run and test simulator

Run `scripts/serve.sh(ps1)` and visit the `/swagger/` endpoint on the url from the console

Use `thisIsKey1` for the authentication key

Use `B75D838E-E7B0-4B7B-BDA3-1903008085E6` for the MerchantId field in MakePayment endpoint

The simulator will generate different statuses based on the Mod10/2 of the amount
|Mod10/2|Status|
|-------|------|
|0 |Success|
|1 |DetailsNotRecognised|
|2 |Denied|
|3 |InsufficientFunds|
|5 |ServiceError|

## Publishing

Run `scripts/build-docker.sh(ps1)` and run the image - the app exposes ports `5000` and `5001` for http and https respectively

## Assumptions made

- The merchant service is external to the payment gateway (foreign system)
- The bank service is abstracted behind an interface - can be network wide, like an API or just in internal to this service and has a mechanism to resolve between multiple providers
- 3D security won't be required

## Technical considerations

- The service should be a stateless HTTP service acting as a proxy between merchants and multiple banks.

  - We will be using .Net Core 3.1 since it's the current long term support version and ASP Core

- The API it provides should be versioned and discoverable

  - For endpoints that would be URL versioning and providing an API data sheet, Swagger most probably, since it's a common standard

    - As a bonus we can provide a Postman collection as an API playground

  - For models the version should be embedded in the response warper to allow for request tracing and verification

- The API should be secure, multiple options available

  - Simplest, with liabilities - plain API key backed by an API manager, like Azure API Management

    - No implicit cryptographic qualities
      - But the key itself can map to security details or contain them encrypted in itself
    - Relies on just TLS to secure payloads

  - JWTs with an external Auth Service

    - This moves common functionality in it's own abstraction, but requires additional development or spend on a off the shelf service

    - As long as the token lifespan is limited the attack window is significancy small

    - Requires an additional call to authenticate first before calling the gateway

  - Public-private key pair

    - More elaborate than the others, but allows for more flexibility in how it's used

    - It allows for both payload encryption and signing, depending on the context

      - Encrypting the whole payload is not desirable, since TLS already does that for us, but this can introduced for specific fields, as a anti-MTM option

    - Certificate management is complicated and requires a good shared strategy between multiple teams

    - The initial certificate transfer is the most crucial part

  - For the purpose of this demo we will use a simple API key, with an abstracted security provider to verify it, but for an actual implementation there would be a lot more considerations including legislative, overarching team strategy and current technical solutions.

- Requests should be traceable through the systems

- The service should provide historical logs and live monitoring

  - The logging system will be Serilog, since it has a good collection of targets to push to, and has some features not available in the baseline ASP logging providers, like captured contexts. It also supports the base abstraction of ASP's ILogger

  - Monitoring would be provided from the ASP health check middleware

- The service's repository should include all the information on how to build, test and deploy the service

  - This includes build scripts and environment setup for both running and building (plain Docker images for this scenario, since it's relatively multi-platform and can plug in a lot of other systems)

    - I opted for plain shell scripts for the build, since different CI/CD providers have their own configurations and the build is relatively straightforward.

- The system will follow DDD principles to keep it open for extension and for a better fit into a microservice environment
  - It won't use an internal CQRS pattern(like MediatR) to handle domain events, due to the small size of the domain and the complexity of such a setup, but will abstract domain behavior behind a domain service, to allow for future split if necessary
  - External CQRS will remain an option, behind the repository interfaces

## Things to improve

- More specific exceptions for easier handling
- Use different validation system than just exceptions
- Rename the tests and use something like Specflow to define the behaviour of the domain
- Introduces additional entities like Merchant
- Some of the tests might fail under a devil's advocate process
- Corelate the api key with the merchantId for a proper authz flow
- An actual data store which can be included in the Docker image instead of the in-memory provider
- Maybe move some of the services around
- Provide proper XML docs across the board
- Don't store all the payment data in the DB - like CVV
- Look into in-memory encryption for the card number and CVV while processing - it's less than a second but memory sniffing attacks for GC languages are relatively well known
- Enhance the HealthCheck endpoint to report on 3rd part availabilities
- Add more unit tests
- Data storage remains open ended since the scope of the service can vary huge amounts, probably a relational DB which supports encrypted columns and at rest encryption will be something that can fit most scenarios. Support for sharding, multi-tenancy and geo splitting are additional plusses.
- Produce a SDK to integrate against the API
- Performance loads would be mostly network related in this scenario, but having some data to back it up would be good in prod
