# ── Etapa 1: Build ──────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
 
# Copia los archivos de proyecto primero
# (mejor aprovechamiento del cache de Docker)
COPY ["Shell.Blazor/Shell.Blazor.csproj", "Shell.Blazor/"]
COPY ["nuget.config", "."]
 
# Restaura dependencias (incluyendo MFE.Shared de GitHub Packages)
RUN dotnet restore "Shell.Blazor/Shell.Blazor.csproj"
 
# Copia el resto del código
COPY . .
 
# Publica en modo Release
RUN dotnet publish "Shell.Blazor/Shell.Blazor.csproj" \
    -c Release \
    -o /app/publish
 
# ── Etapa 2: Runtime ────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
 
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Shell.Blazor.dll"]
