FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.19 AS builder
WORKDIR /build
COPY . .
RUN dotnet publish --configuration Release --output publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.19 AS final
USER app
WORKDIR /app
COPY --from=builder /build/publish/ .
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_HTTP_PORTS=80
ENTRYPOINT ["dotnet", "RentingAPI.dll"]