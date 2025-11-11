# Gestion.Api

## Ejecutar con Docker

1. Asegúrate de tener [Docker](https://docs.docker.com/get-docker/) y [Docker Compose](https://docs.docker.com/compose/) instalados.
2. Desde la raíz del repositorio ejecuta:
   ```bash
   docker compose up --build
   ```
3. La API quedará disponible en `http://localhost:8080` y la instancia de SQL Server en el puerto `1433`.

El archivo [`docker-compose.yml`](docker-compose.yml) crea dos contenedores:

- **gestion-api**: construye la imagen de la aplicación .NET 8 utilizando el `Dockerfile` incluido.
- **gestion-sqlserver**: levanta una base de datos SQL Server 2022 Express con persistencia mediante el volumen `sqlserver-data` y un health check que garantiza que la base de datos esté lista antes de inicializar la API.

La conexión entre servicios se realiza mediante la cadena definida en `appsettings.json` y se puede sobreescribir mediante la variable de entorno `ConnectionStrings__GestionDB` si es necesario.
