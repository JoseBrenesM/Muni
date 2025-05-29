
using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelos;
using BW;
using DA;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var tokenConfig = builder.Configuration.GetSection("Token").Get<TokenConfiguracion>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenConfig.Issuer,
            ValidAudience = tokenConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Key))
        };
    });
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("https://localhost:7238")
            .AllowAnyMethod()
            .AllowAnyHeader());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();
builder.Services.AddScoped<IHorarioDA, HorarioDA>();
builder.Services.AddScoped<IHorarioBW, HorarioBW>();
builder.Services.AddScoped<IUsuarioDA, UsuarioDA>();
builder.Services.AddScoped<IUsuarioBW, UsuarioBW>();
builder.Services.AddScoped<IPersonaDA, PersonaDA>();
builder.Services.AddScoped<IPersonaBW, PersonaBW>();
builder.Services.AddScoped<IReporteDA, ReporteDA>();
builder.Services.AddScoped<IReporteBW, ReporteBW>();
builder.Services.AddScoped<IRolesDA, RolesDA>();
builder.Services.AddScoped<IRolesBW, RolesBW>();
builder.Services.AddScoped<IRolesxUsuarioDA, RolesxUsuarioDA>();
builder.Services.AddScoped<IRolesxUsuarioBW, RolesxUsuarioBW>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
