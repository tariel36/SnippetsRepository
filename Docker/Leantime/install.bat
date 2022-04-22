docker compose up -d
ping -n 15 127.0.0.1 > nul
docker exec -i nutadev-mysql mysql -uroot -proot -e "update mysql.user set host = '%' where user='root';"
ping -n 15 127.0.0.1 > nul
docker container restart nutadev-mysql
ping -n 15 127.0.0.1 > nul
docker exec -i nutadev-mysql mysql -uroot -proot -e "ALTER USER 'root'@'' IDENTIFIED WITH mysql_native_password BY 'root';"
docker exec -i nutadev-mysql mysql -uroot -proot -e "create database leantime;"