# Payment Gateway

This project is to proved a payment gateway between different merchants and banks, with 2 main requirement:

1. Processing payments (forwarding them to the appropriate bank)
2. Retrieving payment details

## Assumptions made

- The merchant service is external to the payment gateway (foreign system)
- There is a single bank endpoint to call to process a payment, probably a WebAPI part of our internal systems

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
