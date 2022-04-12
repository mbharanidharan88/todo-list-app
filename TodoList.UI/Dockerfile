#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 7000
EXPOSE 7001
ENV ASPNETCORE_ENVIRONMENT=development
ENV ASPNETCORE_URLS=http://+:7000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TodoList.UI/TodoList.UI.csproj", "TodoList.UI/"]
COPY ["TodoList.Domain/TodoList.Domain.csproj", "TodoList.Domain/"]
COPY ["TodoList.Service/TodoList.Service.csproj", "TodoList.Service/"]
COPY ["TodoList.Data/TodoList.Data.csproj", "TodoList.Data/"]
COPY ["TodoList.ResourceLibrary/TodoList.ResourceLibrary.csproj", "TodoList.ResourceLibrary/"]
RUN dotnet restore "TodoList.UI/TodoList.UI.csproj"
COPY . .
WORKDIR "/src/TodoList.UI"
RUN dotnet build "TodoList.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoList.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoList.UI.dll"]