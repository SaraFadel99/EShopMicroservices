
using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opts => 
{
    opts.Connection(builder.Configuration.GetConnectionString("DataBase")!);
}).UseLightweightSessions();
//builder.Services.AddMA;
//add services to the container
var app = builder.Build();
app.MapCarter();
//configure the http request pipeline
app.Run();
