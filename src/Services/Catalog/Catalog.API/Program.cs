

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opts => 
{
    opts.Connection(builder.Configuration.GetConnectionString("DataBase")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment()) 
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
//builder.Services.AddMA;
//add services to the container
builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("DataBase")!);

var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options => { }/*this empty option indicate that we use customHandler*/);
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
//configure the http request pipeline
app.Run();
