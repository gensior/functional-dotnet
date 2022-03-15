using SampleWeb;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



app.MapGet("/", () => "Hello World!");

PeopleEndpoint.Configure(app, PeopleEndpoint.getPerson);

app.Run();


