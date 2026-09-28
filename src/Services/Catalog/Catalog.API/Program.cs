var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
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
