FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

RUN apt-get -y update
RUN apt-get -y upgrade
RUN apt-get install -y ffmpeg

COPY /src/VideoRequestConsumer/VideoRequestConsumer.csproj ./
RUN dotnet restore 


COPY . ./

RUN dotnet publish "src/VideoRequestConsumer/VideoRequestConsumer.csproj" -c Debug -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build-env /app/out ./

ENTRYPOINT ["dotnet", "VideoRequestConsumer.dll"]