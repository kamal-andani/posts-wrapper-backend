using zum_rails.Interfaces;
using zum_rails.Middleware;
using zum_rails.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(typeof(Program));
// Inject application related custom services
builder.Services.AddScoped<IThirdPartyClient, ThirdPartyClient>();
builder.Services.AddScoped<IPostsService, PostsService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//middleware to handle exceptions
app.UseMiddleware<ExceptionMiddleware>();

// Configure CORS
app.UseCors(appbuilder =>
    appbuilder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200","http://localhost:4200"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
