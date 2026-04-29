FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj e restaurar dependências
COPY ["PizzariaWeb.csproj", "./"]
RUN dotnet restore "PizzariaWeb.csproj"

# Copiar tudo e buildar
COPY . .
RUN dotnet publish "PizzariaWeb.csproj" -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Instalar curl para healthcheck
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Copiar publicados
COPY --from=build /app/publish .

# Configurar variáveis de ambiente para Render
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80

# Healthcheck para Render
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost/ || exit 1

ENTRYPOINT ["dotnet", "PizzariaWeb.dll"]
