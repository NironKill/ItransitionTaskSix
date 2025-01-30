FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Presentation/JointPresentation/JointPresentation.csproj", "Presentation/JointPresentation/"]
COPY ["Infrastructure/JointPresentation.Persistence/JointPresentation.Persistence.csproj", "Infrastructure/JointPresentation.Persistence/"]
COPY ["Core/JointPresentation.Domain/JointPresentation.Domain.csproj", "Core/JointPresentation.Domain/"]
COPY ["Core/JointPresentation.Application/JointPresentation.Application.csproj", "Core/JointPresentation.Application/"]
RUN dotnet restore "Presentation/JointPresentation/JointPresentation.csproj"

COPY . .
WORKDIR "/src/Presentation/JointPresentation"
RUN dotnet build "JointPresentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JointPresentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JointPresentation.dll"]