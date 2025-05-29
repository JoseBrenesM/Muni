using Abstracciones.Modelos.Autenticacion;
using Autorizacion.Abstracciones.BW;
using Autorizacion.Abstracciones.DA;
using Autorizacion.BW;
using Autorizacion.DA.Repositorios;
using Autorizacion.DA;
using BC;
using Microsoft.AspNetCore.Authentication.Cookies;
using Autorizacion.Middleware;

var builder = WebApplication.CreateBuilder(args);
// Config de la autorizacion de seguridad
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options =>
    {
        options.LoginPath = "/Index";
        options.AccessDeniedPath = "/Cuenta/AccesoDenegado";
    });

// Registrar el IHttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Registrar el AuthHandler
builder.Services.AddTransient<AuthHandler>();
builder.Services.AddSession();

// Configurar el HttpClient con el AuthHandler
builder.Services.AddHttpClient("Cliente")
    .AddHttpMessageHandler<AuthHandler>();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IRepositorioDapper, RepositorioDapper>();
builder.Services.AddTransient<ISeguridadDA, SeguridadDA>();
builder.Services.AddTransient<IAutorizacionBW, AutorizacionBW>();

builder.Services.AddScoped<Configuracion>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
// Seguridad
app.AutorizacionClaims();
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
