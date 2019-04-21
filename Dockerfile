FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS builder

# Restore packages and publish artifacts
WORKDIR /src
COPY Messages.Api/*.csproj ./Messages.Api/

WORKDIR /src/Messages.Api
RUN dotnet restore

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

# Workaround for the fact that the alpine image does not have any cultures: https://github.com/dotnet/dotnet-docker/issues/533
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT false
RUN apk add --no-cache icu-libs
ENV LC_ALL en_CA.UTF-8
ENV LANG en_CA.UTF-8

COPY --from=builder /src/Messages.Api/artifacts .
ENTRYPOINT ["dotnet", "Messages.Api.dll"]
