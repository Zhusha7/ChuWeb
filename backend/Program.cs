using ChuWeb.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database context
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173") // Vite's default port
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Create/update database tables
    // using (var scope = app.Services.CreateScope())
    // {
    //     var dbContext = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
    //     // This will create the database if it doesn't exist and create all tables based on the model
    //     dbContext.Database.EnsureCreated();
    // }
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run(); 