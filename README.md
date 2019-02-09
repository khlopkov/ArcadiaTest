# Arcadia test task
#### Stack of technoogies
* Backend: ASP.NET Core MVC 2.2 + EntityFrameworkCore 2.2
* Frontend: HTML + CSS
* Database: Postgresql 10.5

Database scripts are situated in ./db folder of this repository

Database connection string is configured in ./ArcadiaTest/DataLayer/Repositories/PostgresContext.cs


#### Building with docker

```bash
	docker build . -t arcadia-test
	docker run -p 8080:80 --name arcadia-test -d arcadia-test:latest
```
