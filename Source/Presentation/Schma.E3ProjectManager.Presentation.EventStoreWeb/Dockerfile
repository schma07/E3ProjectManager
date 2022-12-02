#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Source/Presentation/Schma.E3ProjectManager.Presentation.EventStoreWeb/Schma.E3ProjectManager.Presentation.EventStoreWeb.csproj", "Source/Presentation/Schma.E3ProjectManager.Presentation.EventStoreWeb/"]
COPY ["Source/Core/Schma.E3ProjectManager.Core.Application/Schma.E3ProjectManager.Core.Application.csproj", "Source/Core/Schma.E3ProjectManager.Core.Application/"]
COPY ["Source/Schma.E3ProjectManager.Common/Schma.E3ProjectManager.Common.csproj", "Source/Schma.E3ProjectManager.Common/"]
COPY ["Source/Core/Schma.E3ProjectManager.Core.Domain/Schma.E3ProjectManager.Core.Domain.csproj", "Source/Core/Schma.E3ProjectManager.Core.Domain/"]
COPY ["Source/Infrastructure/Schma.E3ProjectManager.Infrastructure/Schma.E3ProjectManager.Infrastructure.csproj", "Source/Infrastructure/Schma.E3ProjectManager.Infrastructure/"]
COPY ["Source/Infrastructure/Schma.E3ProjectManager.Infrastructure.Auditing/Schma.E3ProjectManager.Infrastructure.Auditing.csproj", "Source/Infrastructure/Schma.E3ProjectManager.Infrastructure.Auditing/"]
COPY ["Source/Infrastructure/Schma.E3ProjectManager.Infrastructure.Resources/Schma.E3ProjectManager.Infrastructure.Resources.csproj", "Source/Infrastructure/Schma.E3ProjectManager.Infrastructure.Resources/"]
RUN dotnet restore "Source/Presentation/Schma.E3ProjectManager.Presentation.EventStoreWeb/Schma.E3ProjectManager.Presentation.EventStoreWeb.csproj"
COPY . .
WORKDIR "/src/Source/Presentation/Schma.E3ProjectManager.Presentation.EventStoreWeb"
RUN dotnet build "Schma.E3ProjectManager.Presentation.EventStoreWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Schma.E3ProjectManager.Presentation.EventStoreWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Schma.E3ProjectManager.Presentation.EventStoreWeb.dll"]