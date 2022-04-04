#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

RUN apt-get update && apt-get install -y --no-install-recommends gss-ntlmssp

EXPOSE 80
EXPOSE 443
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0
ENV NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED true
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS {\"endpointCredentials\": [{\"endpoint\":\"$ArtifactsLocation\", \"username\":\"na\", \"password\":\"$ArtifactsAccessToken\"}]}

RUN wget -qO- https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh | bash

WORKDIR /src
COPY ["Tfg.TFGTemplateDemo.IntegrationApi/Tfg.TFGTemplateDemo.IntegrationApi.csproj", "Tfg.TFGTemplateDemo.IntegrationApi/"]
COPY nuget.config ./

RUN dotnet restore "Tfg.TFGTemplateDemo.IntegrationApi/Tfg.TFGTemplateDemo.IntegrationApi.csproj" --configfile nuget.config
COPY . .
WORKDIR "/src/Tfg.TFGTemplateDemo.IntegrationApi"

RUN rm -rf obj bin out

RUN dotnet build "Tfg.TFGTemplateDemo.IntegrationApi.csproj" -c $BuildConfig -o /app/build

FROM build AS publish
RUN dotnet publish "Tfg.TFGTemplateDemo.IntegrationApi.csproj" -c $BuildConfig -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tfg.TFGTemplateDemo.IntegrationApi.dll"]