
# Minimal API mit .NET 8 und Docker

Dieses Projekt enthält das Grundgerüst für eine .NET 8 Minimal API mit Docker-Unterstützung.

## 1. Ziel
- Erstellen einer .NET 8 Minimal API.
- Containerisierung der Anwendung mit Docker und docker-compose.

## 2. Umgebung
- **Betriebssystem**: Ubuntu 22.04 (VM LP-22.04)
- **IDE**: Visual Studio Code

---

## ✅ Aufgabe 1: Installation .NET 8

### Prüfen ob .NET 8 installiert ist
```bash
dotnet --list-sdks
```

### Falls nicht vorhanden, installieren:
```bash
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0
```

### Installation verifizieren:
```bash
dotnet --list-sdks
```

---

## ✅ Aufgabe 2: GIT-Repository erstellen

1. Repository auf GitHub oder lokal erstellen mit dem Namen `min-api-with-mongo`.
2. Klonen:
   ```bash
   git clone https://github.com/<username>/min-api-with-mongo.git
   cd min-api-with-mongo
   ```

3. README.md hinzufügen:
   ```bash
   echo "# Minimal API with MongoDB" > README.md
   ```

---

## ✅ Aufgabe 3: Grundgerüst erstellen

1. .NET Web API erstellen:
   ```bash
   dotnet new web --name WebApi --framework net8.0
   ```

2. .gitignore erstellen:
   ```bash
   dotnet new gitignore
   ```

3. Projektstruktur prüfen:

   ```text
   min-api-with-mongo/
   ├── README.md
   ├── WebApi/
   │   ├── Program.cs
   │   ├── Properties/
   │   ├── ...
   └── .gitignore
   ```

4. Visual Studio Code öffnen:
   - VS Code starten und Ordner `min-api-with-mongo` öffnen.
   - Terminal in VS Code öffnen:
     ```bash
     cd WebApi
     dotnet run
     ```
   - URL in Browser öffnen (z. B. `http://localhost:5000` oder `http://localhost:5001`).
   - Ausgabe: `Hello World!`
   - Beenden mit `[Ctrl] + C`.

5. Datei `WebApi/Properties/launchSettings.json` anpassen:
   - **Lösche**:
     - `iisSettings`
     - Profile `IIS Express` und `https`
   - **Passe das http-Profil an**:
     ```json
     "profiles": {
       "WebApi": {
         "commandName": "Project",
         "applicationUrl": "http://localhost:5001",
         "dotnetRunMessages": true
       }
     }
     ```

---

## ✅ Aufgabe 4: Dockerfile erstellen

**Pfad:** `WebApi/Dockerfile`

```Dockerfile
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 5001
ENTRYPOINT ["dotnet", "WebApi.dll"]
```

---

## ✅ Aufgabe 5: docker-compose.yml

**Pfad:** `min-api-with-mongo/docker-compose.yml`

```yaml
services:
  webapi:
    build:
      context: ./WebApi
      dockerfile: Dockerfile
    ports:
      - "5001:5001"
    environment:
      - ASPNETCORE_URLS=http://0.0.0.0:5001
```

**Starten der Anwendung:**
```bash
docker compose up --build
```

**Testen im Browser:**
- `http://localhost:5001`

---

## ✅ Aufgabe 6: Git Commit & Push

### Benutzername und E-Mail konfigurieren (falls noch nicht gesetzt):
```bash
git config --global user.email "you@example.com"
git config --global user.name "Your Name"
```

### Änderungen committen und pushen:
```bash
git add .
git commit -m "Initial commit: Setup .NET 8 Minimal API with Docker"
git push origin main
```

🔁 **Tipp:** Nach jedem Arbeitsschritt erneut committen und pushen.

---

## ✅ Projekt abgeschlossen
Die Minimal API ist nun mit Docker lauffähig und via http://localhost:5001 erreichbar.
