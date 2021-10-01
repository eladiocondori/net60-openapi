//If we don't have the dependencies here Rider will complain although dotnet run works
//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//We need to register controllers so that the generated controllers work
builder.Services.AddControllers();
//And these are the actual implementations for the generated code interfaces
builder.Services.AddScoped<OpenApi.Example.IQueriesController, MinApi.Domain.ExampleQueries>();
builder.Services.AddScoped<OpenApi.Example.ICommandsController, MinApi.Domain.ExampleCommands>();

var app = builder.Build();

app.MapGet("/", () => "Hello World! I'm example from generated OpenAPI code. Visit /swagger for the spec UI.");

//As we are using static OpenApi spec we only need the SwaggerUI registration
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/openapi.json", "Example API V1");
});
//And in order to serve the static json file we'll need this
app.UseStaticFiles();

//Routing and endpoints is needed for the generated controller code
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();