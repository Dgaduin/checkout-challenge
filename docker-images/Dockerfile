
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

COPY /src/ /app/
RUN dotnet restore /app/CheckoutChallenge.API/CheckoutChallenge.API.csproj

RUN mkdir /dist
RUN dotnet publish /app/CheckoutChallenge.API/CheckoutChallenge.API.csproj -c release -o ./dist --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build ./dist ./
ENTRYPOINT ["dotnet", "CheckoutChallenge.API.dll"]