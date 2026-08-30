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

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no está definida.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
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