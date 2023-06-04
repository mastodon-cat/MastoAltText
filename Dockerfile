# dockerfile for this .net 7 application.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy everything from MastoAltText directory to /app in the container.
COPY src/ ./

# Restore the project.
RUN dotnet restore --configfile nuget.config MastoAltText/

# Publish the project.
RUN dotnet publish -c Release -o out MastoAltText/

# Build the runtime image.
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copy the published app to the container.
COPY --from=build-env /app/out .


# Run the app when the container launches.
ENTRYPOINT ["dotnet", "MastoAltText.dll"]
