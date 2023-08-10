using Cqrs.Repositories.Queries;
using testAPI.Contracts;
using testAPI.Services;

var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("Server=.;Database=testAPIDataBase;Trusted_Connection=True;");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString),
//    ServiceLifetime.Transient);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddScoped<Itest, testService>();
//builder.Services.AddScoped<IQueryFactory, QueryFactory>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
