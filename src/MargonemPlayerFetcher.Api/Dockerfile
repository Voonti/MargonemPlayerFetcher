#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MargoFetcher.Api/MargoFetcher.Api.csproj", "MargoFetcher.Api/"]
RUN dotnet restore "MargoFetcher.Api/MargoFetcher.Api.csproj"
COPY . .
WORKDIR "/src/MargoFetcher.Api"
RUN dotnet build "MargoFetcher.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MargoFetcher.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MargoFetcher.Api.dll"]