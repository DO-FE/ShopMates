#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ShopMates.Admin/ShopMates.Admin.csproj", "ShopMates.Admin/"]
RUN dotnet restore "ShopMates.Admin/ShopMates.Admin.csproj"
COPY . .
WORKDIR "/src/ShopMates.Admin"
RUN dotnet build "ShopMates.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopMates.Admin.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopMates.Admin.dll"]