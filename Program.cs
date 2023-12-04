using EvaluacionDavidLlopis.Filters;
using EvaluacionDavidLlopis.Middlewares;
using EvaluacionDavidLlopis.Models;
using EvaluacionDavidLlopis.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region SERVICEs
builder.Services
    .AddControllers((option) =>
    {
        _ = option.Filters.Add<ExceptionFilter>();
    })
    .AddJsonOptions((option) =>
    {
        option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services
    .AddDbContext<LigaContext>((option) =>
    {
        string? connectionString = builder.Configuration
            .GetConnectionString("DefaultConnection");

        _ = option.UseSqlServer(connectionString);
        //_ = option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });

#region TRANSIENTs
builder.Services.AddTransient<HashService>();
builder.Services.AddTransient<TokenService>();
#endregion TRANSIENTs

#region SECURITY
//builder.Services.AddDataProtection(); //para EncryptVersion

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["ClaveJWT"] ?? ""))
    });
#endregion SECURITY

#region CORS Policy
builder.Services
    .AddCors((options) =>
    {
        options.AddDefaultPolicy((builder) =>
        {
            _ = builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });
#endregion CORS Policy

#region SWAGGER
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
});
#endregion SWAGGER

#endregion SERVICEs

WebApplication app = builder.Build();

#region MIDDDLEWAREs
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseMiddleware<LogRequestMiddleware>();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
#endregion MIDDDLEWAREs

app.MapControllers();

app.Run();
