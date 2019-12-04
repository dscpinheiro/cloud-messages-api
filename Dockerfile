FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS builder
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY Messages.Api/*.csproj ./Messages.Api/
WORKDIR /src/Messages.Api
RUN dotnet restore

# Copy everything else and publish artifacts
COPY Messages.Api/. .
RUN dotnet publish --configuration Release --output artifacts

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app

# Workaround for the fact that the alpine image does not have any cultures: https://github.com/dotnet/dotnet-docker/issues/533
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT false
RUN apk add --no-cache icu-libs
ENV LC_ALL en_CA.UTF-8
ENV LANG en_CA.UTF-8

# Create user for API
RUN addgroup -S apigroup && adduser -S apiuser -G apigroup
USER apiuser

# Copy artifacts and change permissions
COPY --from=builder --chown=apiuser:apigroup /src/Messages.Api/artifacts .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Messages.Api.dll"]
