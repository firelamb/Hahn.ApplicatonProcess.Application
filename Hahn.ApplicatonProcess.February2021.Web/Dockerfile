FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

# Setup NodeJs
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_10.x | bash - && \
    apt-get install -y build-essential nodejs


# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser


FROM node:12.20.2-alpine3.12 AS client
WORKDIR /app 
COPY ["Hahn.ApplicatonProcess.February2021.Web", "."]
WORKDIR /app/ClientApp
RUN echo $(npm cache clean -f) 
RUN echo $(npm install)
RUN echo $(npm run build)



FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Hahn.ApplicatonProcess.February2021.Web/Hahn.ApplicatonProcess.February2021.Web.csproj", "Hahn.ApplicatonProcess.February2021.Web/"]
RUN dotnet restore "Hahn.ApplicatonProcess.February2021.Web/Hahn.ApplicatonProcess.February2021.Web.csproj"


COPY . .

WORKDIR "/src/Hahn.ApplicatonProcess.February2021.Web"
RUN dotnet build "Hahn.ApplicatonProcess.February2021.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hahn.ApplicatonProcess.February2021.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
#COPY --from=client /app/wwwroot ./wwwroot
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Hahn.ApplicatonProcess.February2021.Web.dll"]
