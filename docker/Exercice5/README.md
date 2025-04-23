docker-compose up -d

docker exec -it container_a ping container_b
docker exec -it container_b ping container_a