FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS base

WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080;

FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build

COPY ["ResidentApi/", "/ResidentApi/"]
COPY ["ResidentApi.BusinessLogic/", "/ResidentApi.BusinessLogic/"]

WORKDIR /ResidentApi
RUN dotnet restore "ResidentApi.csproj" && \
    dotnet publish "ResidentApi.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
COPY --from=build /app/publish .
USER 1000
ENTRYPOINT ["dotnet", "ResidentApi.dll"]