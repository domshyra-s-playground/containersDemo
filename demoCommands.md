# Demo contents 
this goes over what this repo demos

## single container (api) with localhost web
run the web from vscode, or from the command line `npm run start`
#### Api

in the Dockerfile.debugNoHttps file add the proper keys 

`docker image build . -f Dockerfile.debugNoHttps -t api-demo` 

Assign it port 8001 in docker desktop.

## both containers
#### Web

`docker image build . -f Dockerfile.debugNoHttps -t web-demo`
Assign it port 3000 in docker desktop. 

## docker compose 

What is it? (Show the docker compose file)

### docker compose-debug
`docker compose -f docker-compose.debug.yml up -d`







