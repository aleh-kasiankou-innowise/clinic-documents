FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Innowise.Clinic.Documents.Api/NuGet.Config", "./"]
COPY ["Innowise.Clinic.Documents.Api/Innowise.Clinic.Documents.Api.csproj", "Innowise.Clinic.Documents.Api/"]
COPY ["Innowise.Clinic.Documents.Services/Innowise.Clinic.Documents.Services.csproj", "Innowise.Clinic.Documents.Services/"]
RUN dotnet restore "Innowise.Clinic.Documents.Api/Innowise.Clinic.Documents.Api.csproj"
COPY . .
WORKDIR "/src/Innowise.Clinic.Documents.Api"
RUN dotnet build "Innowise.Clinic.Documents.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Innowise.Clinic.Documents.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Innowise.Clinic.Documents.Api.dll"]
