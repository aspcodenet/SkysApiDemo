FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore


COPY . .

RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:6.0

RUN apt-get update && apt-get install -y libgdiplus

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SkysApiDemo.dll"]