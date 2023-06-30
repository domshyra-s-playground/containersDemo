# Container Demo
This is a fork of my main site to containerize it as a demo. 


## Getting Started Locally

### API
might have to run `dotnet dev-certs https --trust` for api

Spotify will only work with a usersecrets file containing 

```json
{
  "Spotify:ClientId": "SpotifyClientId",
  "Spotify:ClientSecret": "SpotifyClientSecret"
}
```

run the following commands to add secrets.

`dotnet user-secrets init`

`dotnet user-secrets set "Spotify:ClientId" "SpotifyClientId"`

`dotnet user-secrets set "Spotify:ClientSecret" "SpotifyClientSecret"`

inorder to use the rating features on play list run `dotnet ef database update`

in vscode use ".NET core launch"


### Web
run `npm install`
in vscode use "Launch Chrome against localhost" or run `npm start` 





## Getting Started Docker

modify your `.env` file to add secrets for spotify.

for dev you'll need to set up a `dev-cert` for the api to enable https.

navigate to the Api folder and run the following commands. `cd Api` 

You'll want to pick a password for `your_cert_password` and use it for all three commands.

`dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\api.pfx -p your_cert_password`

`dotnet dev-certs https --trust` unless it's already trusted on your machine.

`dotnet user-secrets -p api.csproj set "Kestrel:Certificates:Development:Password" "your_cert_password"`

add `your_cert_password` to the env file. 

Navigate back to the root directory. `cd ..`

Run docker desktop, or however you get the docker daemon running. 

run the following commmand `docker compose -f docker-compose.debug.yml --env-file .env build` to build the containers, then run `docker compose -f docker-compose.debug.yml up` to start the containers.