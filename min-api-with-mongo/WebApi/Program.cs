using MongoDB.Driver;


var builder = WebApplication.CreateBuilder(args);

var movieDatabaseConfigSection = builder.Configuration.GetSection("DatabaseSettings");
builder.Services.Configure<DatabaseSettings>(movieDatabaseConfigSection);

var app = builder.Build();

app.MapGet("/", () => "Minimal API nach Arbeitsauftrag 2");

// docker run --name mongodb -d -p 27017:27017 -v data:/data/db -e MONGO_INITDB_ROOT_USERNAME=gbs -e MONGO_INITDB_ROOT_PASSWORD=geheim mongo
app.MapGet("/check", (Microsoft.Extensions.Options.IOptions<DatabaseSettings> options) => {
    
    try
    {
        var mongoDbConnectionString = options.Value.ConnectionString;
        var mongoClient = new MongoClient(mongoDbConnectionString);
        var databaseNames = mongoClient.ListDatabaseNames().ToList();

        return "Zugriff auf MongoDB ok. Vorhandene DBs: " + string.Join(",", databaseNames);
    }
    catch (System.Exception e)
    {
        return "Zugriff auf MongoDB funktioniert nicht: " + e.Message;
    }         

});

app.Run();
