var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(opts => 
{
    var t = builder.Configuration.GetConnectionString("DataBase");
    opts.Connection(builder.Configuration.GetConnectionString("DataBase")!);
}).UseLightweightSessions();
//builder.Services.AddMA;
//add services to the container
var app = builder.Build();
app.MapCarter();
//configure the http request pipeline
app.Run();
