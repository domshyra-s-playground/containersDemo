### Api

in the Dockerfile.debugNoHttps file add the proper keys 

`docker image build . -f Dockerfile.debugNoHttps -t api-demo` 

Assign it port 8001 in docker desktop.


### Web

`docker image build . -f Dockerfile.debugNoHttps -t web-demo`
Assign it port 3000 in docker desktop.