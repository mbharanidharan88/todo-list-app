#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 6000
EXPOSE 6001
ENV ASPNETCORE_ENVIRONMENT=development
ENV ASPNETCORE_URLS=http://+:6000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TodoList.API/TodoList.API.csproj", "TodoList.API/"]
COPY ["TodoList.Data/TodoList.Data.csproj", "TodoList.Data/"]
COPY ["TodoList.Domain/TodoList.Domain.csproj", "TodoList.Domain/"]
COPY ["TodoList.Business/TodoList.Business.csproj", "TodoList.Business/"]
RUN dotnet restore "TodoList.API/TodoList.API.csproj"
COPY . .
WORKDIR "/src/TodoList.API"
RUN dotnet build "TodoList.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoList.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoList.API.dll"]