FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ARG TOKEN_PAT

RUN dotnet nuget add source \
    https://nuget.pkg.github.com/cesarortiz93/index.json \
    --name github \
    --username cesarortiz93 \
    --password $TOKEN_PAT \
    --store-password-in-clear-text

COPY ["Shell.Blazor/Shell.Blazor.csproj", "Shell.Blazor/"]

RUN dotnet restore "Shell.Blazor/Shell.Blazor.csproj"

COPY . .

WORKDIR /src/MFE.Shell
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Shell.Blazor.dll"]