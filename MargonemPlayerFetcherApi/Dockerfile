FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MargonemPlayerFetcherApi/MargonemPlayerFetcherApi.csproj", "MargonemPlayerFetcherApi/"]
RUN dotnet restore "MargonemPlayerFetcherApi/MargonemPlayerFetcherApi.csproj"
COPY . .
WORKDIR "/src/MargonemPlayerFetcherApi"
RUN dotnet build "MargonemPlayerFetcherApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MargonemPlayerFetcherApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MargonemPlayerFetcherApi.dll"]