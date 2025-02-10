# Använd officiell .NET 9.0 runtime som bas
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Kopiera utdata från build-steget
COPY out ./

# Exponera port 8080 för Render
EXPOSE 8080

# Starta applikationen
ENTRYPOINT ["dotnet", "Mvc.dll"]
