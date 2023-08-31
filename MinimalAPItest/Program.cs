using MinimalAPItest;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddScoped

var app = builder.Build();


app.MapGet("/", () => CarViewer.CarViewer1());

app.Run();
