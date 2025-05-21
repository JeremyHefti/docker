
# Minimal API mit .NET 8, MongoDB und Docker (Teil 2)

Dieses Dokument erweitert das Projekt aus Auftrag 1 um eine MongoDB-Anbindung inkl. Docker Compose Orchestrierung.

---

## ✅ Aufgabe 1: MongoDB-Container starten

```bash
docker volume create mongodb-data

docker run -d \
  --name mongodb \
  -v mongodb-data:/data/db \
  -p 27017:27017 \
  -e MONGO_INITDB_ROOT_USERNAME=gbs \
  -e MONGO_INITDB_ROOT_PASSWORD=geheim \
  mongo
```

Vergewissern Sie sich, dass der Container läuft:
```bash
docker ps
```

---

## ✅ Aufgabe 2: C# Extension für VS Code

1. Öffne Visual Studio Code.
2. Öffne den Extensions Marketplace (`Ctrl + Shift + X`).
3. Suche nach **C# for Visual Studio Code (powered by OmniSharp)**.
4. Installieren.

---

## ✅ Aufgabe 3: MongoDB Driver installieren und API-Endpunkt erweitern

### NuGet-Paket installieren

```bash
cd WebApi
dotnet add package MongoDB.Driver
```

### Datei `Program.cs` erweitern

```csharp
app.MapGet("/", () => "Minimal API Version 1.0");

app.MapGet("/check", () =>
{
    try
    {
        var client = new MongoDB.Driver.MongoClient("mongodb://gbs:geheim@localhost:27017");
        var dbs = client.ListDatabaseNames().ToList();
        return Results.Ok("Zugriff auf MongoDB ok. Datenbanken: " + string.Join(", ", dbs));
    }
    catch (Exception ex)
    {
        return Results.Problem("Fehler beim Zugriff auf MongoDB: " + ex.Message);
    }
});
```

Starte die Anwendung:

```bash
dotnet run
```

Test im Browser: [http://localhost:5001/check](http://localhost:5001/check)

---

## ✅ Aufgabe 4: Verbindung über appsettings.json konfigurieren

### `DatabaseSettings.cs` erstellen

**Pfad:** `WebApi/DatabaseSettings.cs`

```csharp
public class DatabaseSettings
{
    public string ConnectionString { get; set; } = "";
}
```

### `appsettings.json` erweitern

```json
{
  "AllowedHosts": "*",
  "DatabaseSettings": {
    "ConnectionString": "mongodb://gbs:geheim@localhost:27017"
  }
}
```

### `Program.cs` anpassen

```csharp
var movieDatabaseConfigSection = builder.Configuration.GetSection("DatabaseSettings");
builder.Services.Configure<DatabaseSettings>(movieDatabaseConfigSection);

app.MapGet("/check",
    (Microsoft.Extensions.Options.IOptions<DatabaseSettings> options) =>
{
    try
    {
        var mongoDbConnectionString = options.Value.ConnectionString;
        var client = new MongoDB.Driver.MongoClient(mongoDbConnectionString);
        var dbs = client.ListDatabaseNames().ToList();
        return Results.Ok("Zugriff auf MongoDB ok. Datenbanken: " + string.Join(", ", dbs));
    }
    catch (Exception ex)
    {
        return Results.Problem("Fehler beim Zugriff auf MongoDB: " + ex.Message);
    }
});
```

---

## ✅ Aufgabe 5: docker-compose.yml erweitern

**Pfad:** `min-api-with-mongo/docker-compose.yml`

```yaml
version: '3.8'

services:
  mongodb:
    image: mongo
    container_name: mongodb
    restart: always
    environment:
      MONGO_INITDB_ROOT_USERNAME: gbs
      MONGO_INITDB_ROOT_PASSWORD: geheim
    volumes:
      - mongodb-data:/data/db
    ports:
      - "27017:27017"

  webapi:
    build:
      context: ./WebApi
      dockerfile: Dockerfile
    ports:
      - "5001:5001"
    depends_on:
      - mongodb
    environment:
      DatabaseSettings__ConnectionString: "mongodb://gbs:geheim@mongodb:27017"

volumes:
  mongodb-data:
```

---

## ✅ Anwendung starten

```bash
docker compose up --build
```

Teste im Browser: [http://localhost:5001/check](http://localhost:5001/check)

Die API sollte "Zugriff auf MongoDB ok." zurückgeben und die Datenbanken anzeigen.
