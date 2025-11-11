using Gestion.Api.Services.Interfaces;
using Gestion.Api.Services;
using Microsoft.EntityFrameworkCore;
using ApplicationDbContext = Gestion.Api.Repository.ApplicationDbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Gestion.Api.Helpers;
using Microsoft.Extensions.Options;
using Gestion.Api.Configurations;
using Gestion.Api.Configurations.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("GestionDB"));
});

builder.Services.AddServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfig();

builder.Configuration.AddEncryptionOptions(builder);
var jwt = builder.Configuration.AddJwtOptions(builder);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
            ClockSkew = TimeSpan.Zero
        };

        //o.Events = new JwtBearerEvents
        //{
        //    OnAuthenticationFailed = context =>
        //    {
        //        Console.WriteLine($"? JWT Error: {context.Exception.Message}");
        //        if (context.Exception.InnerException != null)
        //            Console.WriteLine($"   ? Inner: {context.Exception.InnerException.Message}");
        //        return Task.CompletedTask;
        //    },
        //    OnChallenge = context =>
        //    {
        //        Console.WriteLine("?? OnChallenge: Token inválido o ausente");
        //        return Task.CompletedTask;
        //    },
        //    OnForbidden = context =>
        //    {
        //        Console.WriteLine("?? OnForbidden: Acceso denegado");
        //        return Task.CompletedTask;
        //    }
        //};
    });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly", p => p.RequireClaim("rolId", "1"));
//    options.AddPolicy("EmpleadoOrAdmin", p => p.RequireClaim("rolId", "1", "2"));
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

app.MapPost("/debug/jwt", (string token) =>
{
    try
    {
        var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token.Replace("Bearer ", ""));
        return Results.Ok(new
        {
            jwt.Issuer,
            Audience = jwt.Audiences,
            jwt.ValidTo,
            Claims = jwt.Claims.Select(c => new { c.Type, c.Value })
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});


app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
