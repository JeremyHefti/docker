
# Minimal API mit MongoDB – Teil 3

© 2025 gbssg.ch | Modul 347

## 1. Ziele

- Sie unterscheiden GET, POST, PUT und DELETE-Requests.
- Sie erstellen CRUD-Endpunkte für ein Web-API.
- Sie testen ein Web-API mit einem REST-Client.

---

## 2. Klasse für Filme erstellen

Erstelle unter `min-api-with-mongo/WebApi` ein neues File `Movie.cs` mit folgendem Inhalt:

```csharp
using MongoDB.Bson.Serialization.Attributes;

public class Movie
{
    [BsonId]
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public int Year { get; set; }
    public string Summary { get; set; } = "";
    public string[] Actors { get; set; } = Array.Empty<string>();
}
```

> `BsonId` markiert das Feld `Id` als das primäre `_id`-Feld in MongoDB.

---

## 3. Endpunkte für REST vorbereiten

Ergänze in `Program.cs` folgende Routen:

```csharp
// Insert Movie
app.MapPost("/api/movies", (Movie movie) => {
    throw new NotImplementedException();
});

// Get all Movies
app.MapGet("/api/movies", () => {
    throw new NotImplementedException();
});

// Get Movie by id
app.MapGet("/api/movies/{id}", (string id) => {
    throw new NotImplementedException();
});

// Update Movie
app.MapPut("/api/movies/{id}", (string id, Movie movie) => {
    throw new NotImplementedException();
});

// Delete Movie
app.MapDelete("/api/movies/{id}", (string id) => {
    throw new NotImplementedException();
});
```

---

## 4. Beispiel-Code für GET ohne Datenbankanbindung

Implementiere den `/api/movies/{id}`-Endpunkt testweise mit folgendem Code:

```csharp
app.MapGet("/api/movies/{id}", (string id) => {
    if (id == "1")
    {
        var myMovie = new Movie()
        {
            Id = "1",
            Title = "Ein Quantum Trost",
        };
        return Results.Ok(myMovie);
    }
    else
    {
        return Results.NotFound();
    }
});
```

---

## 5. Testen mit REST-Client

1. Installiere die VS Code Extension **REST Client**.
2. Erstelle im Projektverzeichnis die Datei `testing.http` mit folgendem Inhalt:

```http
### Test GET Movie by Id
GET http://localhost:5001/api/movies/1
```

3. Klicke auf `Send Request`, um die API zu testen.
4. Teste auch mit anderen Ids (z.B. `/2`).

---

## 6. Hilfsmittel

- [https://gbssg.gitlab.io/m347/](https://gbssg.gitlab.io/m347/)
- Internet

---

## 7. Erwartete Resultate

- Funktionierende CRUD-Endpunkte
- Getestet mit REST-Client
- Screenshots und Kommentare dokumentieren das Vorgehen (in Word)
- Zeit: **30 Minuten**
