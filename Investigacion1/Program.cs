using System.Text;
using Investigacion1.Features.Auth;
using Investigacion1.Features.Auth.Login;
using Investigacion1.Features.Auth.Refresh;
using Investigacion1.Features.Auth.Register;
using Investigacion1.Features.Usuarios.GetCurrentUser;
using Investigacion1.Features.Usuarios.GetUserById;
using Investigacion1.Features.Usuarios.GetUsers;
using Investigacion1.Features.Usuarios.Logout;
using Investigacion1.Features.WeatherForecast.GetWeatherForecast;
using Investigacion1.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("Investigacion1Db"));

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));
builder.Services.AddScoped<JwtTokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();
        if (jwt is null)
        {
            throw new InvalidOperationException("La configuración JWT no está definida.");
        }
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret)),
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", p => p.RequireRole(Investigacion1.Features.Usuarios.Role.Admin));
});

var app = builder.Build();

// Seed: crear Admin inicial si no existe ninguno
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Usuarios.Any(u => u.Role == Investigacion1.Features.Usuarios.Role.Admin))
    {
        db.Usuarios.Add(new Investigacion1.Features.Usuarios.Usuario
        {
            Nombre = "Admin",
            Email = "admin@example.com",
            Password = BCrypt.Net.BCrypt.HashPassword("admin123", 15),
            Role = Investigacion1.Features.Usuarios.Role.Admin,
            IsActive = true,
            SubscriptionExpirationDate = DateTime.UtcNow.AddYears(1),
        });
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGetWeatherForecastEndpoint();
app.MapRegisterEndpoint();
app.MapLoginEndpoint();
app.MapRefreshEndpoint();
app.MapLogoutEndpoint();
app.MapGetCurrentUserEndpoint();
app.MapGetUsersEndpoint();
app.MapGetUserByIdEndpoint();

app.Run();