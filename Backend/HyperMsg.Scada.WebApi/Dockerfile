FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["HyperMsg.Scada.WebApi.csproj", "HyperMsg.Scada.WebApi/"]
RUN dotnet restore "HyperMsg.Scada.WebApi/HyperMsg.Scada.WebApi.csproj"
COPY . .
WORKDIR "/src/HyperMsg.Scada.WebApi"
RUN dotnet build "HyperMsg.Scada.WebApi.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "HyperMsg.Scada.WebApi.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HyperMsg.Scada.WebApi.dll"]
