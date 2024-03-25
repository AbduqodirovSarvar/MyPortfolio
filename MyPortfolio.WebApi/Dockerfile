#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5034
EXPOSE 4443

# Define environment variables
ENV ASPNETCORE_URLS=http://+:5034
ENV ConnectionStrings__SQLiteConnection=Data
ENV Source=/app/DB/Portfolio.db
ENV JWTConfiguration__ValidAudience=MyPortfolio
ENV JWTConfiguration__ValidIssuer=localhost
ENV JWTConfiguration__Secret=MyPortfolioSecretKeyorSecretWordForJwtSecurity

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MyPortfolio.WebApi/MyPortfolio.WebApi.csproj", "MyPortfolio.WebApi/"]
RUN dotnet restore "MyPortfolio.WebApi/MyPortfolio.WebApi.csproj"
COPY . .
WORKDIR "/src/MyPortfolio.WebApi"
RUN dotnet build "MyPortfolio.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyPortfolio.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Define network configuration
#ARG NETWORK=network_for_portfolio
#RUN --network=${NETWORK} echo "Network configuration added"

ENTRYPOINT ["dotnet", "MyPortfolio.WebApi.dll"]