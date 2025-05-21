# 📦 Lernzielkontrolle – Docker Compose & .NET Microservices

## 📊 Teil 1 – Theoretische Fragen

### 1. Was ist eine `docker-compose.yml` Datei und wofür wird sie verwendet?
Eine `docker-compose.yml` Datei ist eine YAML-basierte Konfigurationsdatei, mit der mehrere Docker-Container als Services definiert und gemeinsam orchestriert werden können.

### 2. Nenne 5 zentrale Schlüsselwörter in einer `docker-compose.yml` Datei und erkläre sie.
- `version`: Gibt die Version des Compose-Dateiformats an.
- `services`: Definiert die einzelnen Container-Services.
- `image`: Gibt an, welches Image verwendet werden soll.
- `build`: Gibt den Pfad zu einem Dockerfile an, um ein eigenes Image zu bauen.
- `volumes`: Bindet Volumes für persistente Daten oder Source Code ein.

### 3. Was macht `docker compose up -d --build`?
Startet alle Container im Hintergrund (`-d`) und erstellt die Images neu aus dem Dockerfile (`--build`).

### 4. Wie geht man vor, um ein fremdes Image (z. B. redis oder postgres) korrekt in Compose zu verwenden?
Die Dokumentation auf [hub.docker.com](https://hub.docker.com) lesen, dort sind Image-Name, benötigte Ports, Umgebungsvariablen und Volumes dokumentiert. Anschließend diese Infos in die `docker-compose.yml` übertragen.

### 5. Was ist ein Multistage Build im Dockerfile?
Eine Technik, bei der mehrere `FROM`-Anweisungen verwendet werden. Der Build wird in mehreren Stufen durchgeführt. Am Ende enthält das finale Image nur die kompilierten Artefakte – das reduziert die Größe und erhöht die Sicherheit.

---

## 🛠️ Teil 2 – Praktische Aufgaben

### Aufgabe 1: Einfaches docker-compose Setup
```yaml
version: "3.9"
services:
  mongo:
    image: mongo
    ports:
      - "27017:27017"
    volumes:
      - mongo-data:/data/db

  webapi:
    build: ./WebApi
    ports:
      - "5001:8080"
    environment:
      - MoviesDatabaseSettings__ConnectionString=mongodb://mongo:27017
    depends_on:
      - mongo

volumes:
  mongo-data:
```

### Aufgabe 2: Dockerfile mit Multistage für .NET WebAPI
```dockerfile
# Build-Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o out

# Runtime-Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "WebApi.dll"]
```

### Aufgabe 3: dotnet-Befehle
```bash
dotnet new webapi -n WebApi
cd WebApi
dotnet build
dotnet run
```

### Aufgabe 4: Compose starten und stoppen
```bash
docker compose up -d --build     # Start mit Neubau
docker compose down              # Stoppen und entfernen
```

---