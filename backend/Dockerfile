FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["PersonApi.csproj", "./"]
RUN dotnet restore "PersonApi.csproj"
COPY . .
RUN dotnet build "PersonApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "PersonApi.dll"]
