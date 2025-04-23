docker build -t nginx-volume .

docker run -d -p 8080:80 -v ${PWD}:/usr/share/nginx/html --name nginx-volume-site nginx-volume