FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 50965

ENV ASPNETCORE_URLS=http://+:50965
ENV ASPNETCORE_URLS=http://+:50965

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FluxoCaixa/FluxoCaixa.csproj", "FluxoCaixa/"]
COPY ["Database/FluxoCaixa.db", "Database/"]

RUN dotnet restore "FluxoCaixa/FluxoCaixa.csproj"
COPY . .
WORKDIR "/src/FluxoCaixa"
RUN dotnet build "FluxoCaixa.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FluxoCaixa.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FluxoCaixa.dll"]