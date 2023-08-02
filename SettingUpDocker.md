# Adding docker files

in vscode us command "Docker: Add" to both the API proj and the Web project

## Web
Select `node.js` as the project. 

The web layer should be good to go and look something like this

```dockerfile
FROM node:lts-alpine
ENV NODE_ENV=production
WORKDIR /usr/src/app
COPY ["package.json", "package-lock.json*", "npm-shrinkwrap.json*", "./"]
RUN npm install --production --silent && mv node_modules ../
COPY . .
EXPOSE 3000
RUN chown -R node /usr/src/app
USER node
CMD ["npm", "start"]
```

### Debug
Run the `Docker Node.js Launch` task from VSCode to start the container in debug mode.
## API
Select `.Net` as the project. 
The api layer should be good to go and look something like this

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7167

ENV ASPNETCORE_URLS=http://+:7167

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Api.csproj", "./"]
RUN dotnet restore "Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
```

### Debug
Run the `Docker .NET Launch` task from VSCode to start the container in debug mode.



# Docker compose Locally

### Web

### Api
The api layer is a bit different. We will get a CORS error to fix that we will need a nginx proxy to work inside the docker instance. We will also need to add a few things to the docker file to get it to work with the proxy.


`dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\api.pfx -p your_cert_password`
`dotnet dev-certs https --trust`
`dotnet user-secrets -p api.csproj set "Kestrel:Certificates:Development:Password" "your_cert_password"`


# Docker Compose Production (TODO)
`docker compose build`
`docker-compose push`