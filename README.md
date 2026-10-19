# Torneo Galáctico

Aplicación web para gestionar las especies que participan en el Gran Torneo Galáctico. Permite registrar participantes, organizar combates, consultar sus resultados y ver un ranking de victorias.

## Funcionalidades

- Registrar especies con nombre, nivel de poder y habilidad especial.
- Consultar las especies registradas.
- Iniciar combates entre dos especies o generar un combate aleatorio.
- Guardar y consultar el historial de combates.
- Consultar el ranking, ordenado por victorias. En caso de empate, se ordena por nombre.

En cada combate gana la especie con mayor nivel de poder. Si ambas tienen el mismo nivel, gana la que aparece primero alfabéticamente (sin distinguir mayúsculas de minúsculas).

## Tecnologías

- **Frontend:** React, TypeScript y Vite.
- **Backend:** ASP.NET Core Web API con .NET 10.
- **Persistencia:** SQLite.
- **Acceso a datos:** Dapper.
- **Pruebas:** xUnit.
- **Ejecución en contenedor:** Docker y Docker Compose.

## Requisitos

Para ejecutar la aplicación completa con Docker:

- Docker Desktop (o Docker Engine) con Docker Compose.

Para ejecutar o probar los proyectos directamente en el equipo:

- .NET SDK 10.
- Node.js 22 o superior y npm.

## Instalación y ejecución con Docker

Desde la raíz del repositorio, ejecuta:

```bash
docker compose up --build
```

Cuando el contenedor esté listo:

- Aplicación web: [http://localhost:8080](http://localhost:8080)
- Documentación interactiva de la API: [http://localhost:8080/swagger](http://localhost:8080/swagger)

Para detener la aplicación, presiona `Ctrl+C`. Para iniciarla nuevamente en segundo plano:

```bash
docker compose up -d
```

Los datos de SQLite se guardan en el volumen `torneo-data` de Docker y se conservan entre reinicios del contenedor. Para detener y eliminar también ese volumen (y sus datos), usa `docker compose down -v`.

## Ejecución local

La API puede ejecutarse sin Docker desde la raíz del repositorio:

```bash
dotnet run --project backend/TorneoGalactico.Api
```

La configuración local utiliza SQLite y crea el archivo `torneo.db` al iniciar. Para ejecutar las pruebas:

```bash
dotnet test backend/TorneoGalactico.Tests
```

## API

La API está disponible bajo `/api`:

| Método | Ruta | Descripción |
| --- | --- | --- |
| `GET` | `/api/species` | Lista las especies registradas. |
| `POST` | `/api/species` | Registra una especie. |
| `GET` | `/api/battles` | Consulta el historial de combates. |
| `POST` | `/api/battles` | Inicia un combate entre dos especies y guarda el resultado. |
| `POST` | `/api/battles/random` | Inicia un combate aleatorio. |
| `GET` | `/api/ranking` | Consulta el ranking de especies. |

Ejemplo de registro de especie:

```json
{
  "name": "Dragón estelar",
  "powerLevel": 900,
  "specialAbility": "Aliento de plasma"
}
```

Ejemplo de inicio de combate:

```json
{
  "speciesAId": 1,
  "speciesBId": 2
}
```

## Estructura del proyecto

```text
backend/
  TorneoGalactico.Api/     API, reglas del torneo y acceso a SQLite
  TorneoGalactico.Tests/   Pruebas unitarias de las reglas de combate
frontend/                  Interfaz web en React y TypeScript
Dockerfile                 Construcción de frontend y backend
docker-compose.yml         Ejecución de la aplicación y persistencia
```