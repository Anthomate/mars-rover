docker-compose up -d

docker inspect container_mariadb | Select-String "Health" 