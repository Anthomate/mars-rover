docker build -t static-web-server .

docker run -p 8080:80 static-web-server