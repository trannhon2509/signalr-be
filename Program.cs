using Microsoft.EntityFrameworkCore;
using signalr_be.ApplicationDb;
using signalr_be.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserDbConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("http://localhost:5174", "http://localhost:5173", "http://signalrwebsite.runasp.net/", "https://signalrfe.netlify.app/") // Địa chỉ của ứng dụng React của bạn
                         .AllowAnyMethod()
                         .AllowAnyHeader()
                         .AllowCredentials(); // Quan trọng khi sử dụng cookies hoặc thông tin xác thực
        });
});

var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI();

// Use CORS policy
app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();
app.MapHub<UserHub>("/userhub");

app.Run();
