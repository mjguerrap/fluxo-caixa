FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 50965

ENV ASPNETCORE_URLS=http://+:50965

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FluxoCaixa/FluxoCaixa.csproj", "FluxoCaixa/"]

RUN dotnet restore "FluxoCaixa/FluxoCaixa.csproj"
COPY . .
# RUN dotnet ef database update
WORKDIR "/src/FluxoCaixa"
RUN dotnet build "FluxoCaixa.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FluxoCaixa.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
RUN mkdir -m a=rwx ./Database

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "FluxoCaixa.dll"]

