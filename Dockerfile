FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/PetClinic.Domain/PetClinic.Domain.csproj", "src/PetClinic.Domain/"]
COPY ["src/PetClinic.Infrastructure/PetClinic.Infrastructure.csproj", "src/PetClinic.Infrastructure/"]
COPY ["src/PetClinic.Application/PetClinic.Application.csproj", "src/PetClinic.Application/"]
COPY ["src/PetClinic.App.Web/PetClinic.App.Web.csproj", "src/PetClinic.App.Web/"]

RUN dotnet restore "src/PetClinic.App.Web/PetClinic.App.Web.csproj" -r linux-x64

COPY . .

WORKDIR "/src/src/PetClinic.App.Web"
RUN dotnet publish "PetClinic.App.Web.csproj" -c Release --no-restore \
    -r linux-x64 --self-contained false \
    -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble-chiseled-extra AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PetClinic.App.Web.dll"]
