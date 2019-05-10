FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS builder
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY Messages.Api/*.csproj ./Messages.Api/
WORKDIR /src/Messages.Api
RUN dotnet restore

# Copy everything else and publish artifacts
COPY Messages.Api/. .
RUN dotnet publish --configuration Release --output artifacts

# Run unit tests
FROM builder AS testrunner
WORKDIR /src/Messages.Tests

COPY Messages.Tests/. .
RUN dotnet build --configuration Release

ENTRYPOINT ["dotnet", "test", "--logger", "console;verbosity=detailed"]

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.4-alpine AS runtime
WORKDIR /app

# Workaround for the fact that the alpine image does not have any cultures: https://github.com/dotnet/dotnet-docker/issues/533
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT false
RUN apk add --no-cache icu-libs
ENV LC_ALL en_CA.UTF-8
ENV LANG en_CA.UTF-8

COPY --from=builder /src/Messages.Api/artifacts .
ENTRYPOINT ["dotnet", "Messages.Api.dll"]
