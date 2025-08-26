FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
 
WORKDIR /src
 
COPY ["WorkFlow.csproj", "."]
 
RUN dotnet restore "WorkFlow.csproj"
 
COPY . .
RUN dotnet publish "WorkFlow.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
 
WORKDIR /app
 
COPY --from=build /app/publish .
# ? This is a Dockerfile instruction, not a terminal command
 
ENV ASPNETCORE_ENVIRONMENT=Development
 
ENV ASPNETCORE_URLS=http://+:88
EXPOSE 88
 
ENTRYPOINT ["dotnet", "WorkFlow.dll"]