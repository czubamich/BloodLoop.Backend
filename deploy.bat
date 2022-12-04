docker image ls bloodloop-api
set /p Ver=Enter image tag: 

docker build -t bloodloop-api:%Ver% -f Dockerfile .

docker rename bloodloop-api bloodloop-api_backup

docker stop bloodloop-api_backup
docker run -d --name bloodloop-api -p 85:80 --network=bloodloop-net bloodloop-api:%Ver%

docker rm bloodloop-api_backup