# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY WebApp/*.csproj .
RUN dotnet restore

# copy everything else and build app
COPY WebApp/. .
WORKDIR /source
RUN dotnet publish -c Release -o out

# Build runtime image
# FROM mcr.microsoft.com/dotnet/core/runtime:3.1
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /source
COPY --from=build /source/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "WebApp.dll"]

# docker build --pull --rm -f "Dockerfile" -t webapp:v1 "."
# docker push badokun/webapp:v1