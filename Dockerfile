FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /usr/src/app

COPY src src
COPY BeatEcoprove.sln .

RUN dotnet restore
RUN dotnet publish src/BeatEcoprove.Api/BeatEcoprove.Api.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /usr/src/app/out .

ENTRYPOINT ["dotnet", "BeatEcoprove.Api.dll"]