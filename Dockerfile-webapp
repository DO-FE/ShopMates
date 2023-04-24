#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ShopMates.WepApp/ShopMates.WepApp.csproj", "ShopMates.WepApp/"]
RUN dotnet restore "ShopMates.WepApp/ShopMates.WepApp.csproj"
COPY . .
WORKDIR "/src/ShopMates.WepApp"
RUN dotnet build "ShopMates.WepApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopMates.WepApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopMates.WepApp.dll"]