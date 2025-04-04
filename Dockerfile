FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="italo_ariel_dev77@outlook.com"
WORKDIR /src


COPY *.sln .
COPY src/AgendaRoom.API/AgendaRoom.API.csproj src/AgendaRoom.API/
COPY src/AgendaRoom.Application/AgendaRoom.Application.csproj src/AgendaRoom.Application/
COPY src/AgendaRoom.Domain/AgendaRoom.Domain.csproj src/AgendaRoom.Domain/
COPY src/AgendaRoom.Infrastructure/AgendaRoom.Infrastructure.csproj src/AgendaRoom.Infrastructure/


RUN dotnet restore


COPY src/. ./src/

WORKDIR /src/src/AgendaRoom.API
RUN dotnet publish -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "AgendaRoom.API.dll"]
