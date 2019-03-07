FROM microsoft/dotnet:2.1-sdk-alpine AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish --configuration Release --output artifacts

# Build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine

# Workaround for the fact that the alpine image does not have any cultures: https://github.com/dotnet/dotnet-docker/issues/533
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT false
RUN apk add --no-cache icu-libs
ENV LC_ALL en_CA.UTF-8
ENV LANG en_CA.UTF-8

WORKDIR /app
COPY --from=build-env /app/artifacts .
ENTRYPOINT ["dotnet", "Messages.Api.dll"]
