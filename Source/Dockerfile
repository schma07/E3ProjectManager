#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Source/Presentation/Schma.E3ProjectManager.Presentation.Web/Schma.E3ProjectManager.Presentation.Web.csproj", "Presentation/Schma.E3ProjectManager.Presentation.Web/"]
COPY ["Source/Core/Schma.E3ProjectManager.Core.Application/Schma.E3ProjectManager.Core.Application.csproj", "Core/Schma.E3ProjectManager.Core.Application/"]
COPY ["Source/Schma.E3ProjectManager.Common/Schma.E3ProjectManager.Common.csproj", "Schma.E3ProjectManager.Common/"]
COPY ["Source/Core/Schma.E3ProjectManager.Core.Domain/Schma.E3ProjectManager.Core.Domain.csproj", "Core/Schma.E3ProjectManager.Core.Domain/"]
COPY ["Source/Infrastructure/Schma.E3ProjectManager.Infrastructure.Resources/Schma.E3ProjectManager.Infrastructure.Resources.csproj", "Infrastructure/Schma.E3ProjectManager.Infrastructure.Resources/"]
COPY ["Source/Infrastructure/Schma.E3ProjectManager.Infrastructure.Shared/Schma.E3ProjectManager.Infrastructure.Shared.csproj", "Infrastructure/Schma.E3ProjectManager.Infrastructure.Shared/"]
COPY ["Source/Infrastructure/Schma.E3ProjectManager.Infrastructure/Schma.E3ProjectManager.Infrastructure.csproj", "Infrastructure/Schma.E3ProjectManager.Infrastructure/"]
COPY ["Source/Infrastructure/Schma.E3ProjectManager.Infrastructure.Auditing/Schma.E3ProjectManager.Infrastructure.Auditing.csproj", "Infrastructure/Schma.E3ProjectManager.Infrastructure.Auditing/"]
COPY ["Source/Presentation/Schma.E3ProjectManager.Presentation.Framework/Schma.E3ProjectManager.Presentation.Framework.csproj", "Presentation/Schma.E3ProjectManager.Presentation.Framework/"]
RUN dotnet restore "Presentation/Schma.E3ProjectManager.Presentation.Web/Schma.E3ProjectManager.Presentation.Web.csproj"
COPY . .
WORKDIR "/src/Source/Presentation/Schma.E3ProjectManager.Presentation.Web"
RUN dotnet build "Schma.E3ProjectManager.Presentation.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Schma.E3ProjectManager.Presentation.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Schma.E3ProjectManager.Presentation.Web.dll"]