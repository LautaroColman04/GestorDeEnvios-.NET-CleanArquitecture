using LogicaAccesoDatos;
using LogicaAccesoDatos.Repositorios;
using LogicaAplicacion.CasosUso.CUEnvio;
using LogicaAplicacion.CasosUso.CUUsuario;
using LogicaAplicacion.ICasosUso.ICUEnvio;
using LogicaAplicacion.ICasosUso.ICUUsuario;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");//DefaultConnection debe coincidir con el nombre designado en el JSON.
                                                                                      // Registrar el DbContext en el contenedor de servicios
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddScoped<IRepositorioAgencia, RepositorioAgencia>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioEnvio, RepositorioEnvio>();
builder.Services.AddScoped<IRepositorioSeguimientoEnvio, RepositorioSeguimientoEnvio>();
builder.Services.AddScoped<IRepositorioEmpresa, RepositorioEmpresa>();
builder.Services.AddScoped<IRepositorioComentario, RepositorioComentario>();
builder.Services.AddScoped<IRepositorioAuditoria, RepositorioAuditoria>();

//DI - CASOS USO

builder.Services.AddScoped<ICUAltaUsuario, CUAltaUsuario>();
builder.Services.AddScoped<ICULogin, CULogin>();
builder.Services.AddScoped<ICUCreateEnvio, CUCreateEnvio>();
builder.Services.AddScoped<ICUFinalizarEnvio, CUFinalizarEnvio>();
builder.Services.AddScoped<ICUListarEnvios, CUListarEnvios>();
builder.Services.AddScoped<ICUListarEnviosEnProceso, CUListarEnviosEnProceso>();
builder.Services.AddScoped<ICUObtenerEnvio, CUObtenerEnvio>();
builder.Services.AddScoped<ICUCreateSeguimientoEnvio, CUCreateSeguimientoEnvio>();
builder.Services.AddScoped<ICUListarUsuarios, CUListarUsuarios>();
builder.Services.AddScoped<ICUObtenerUsuario, CUObtenerUsuario>();
builder.Services.AddScoped<ICUActualizarUsuario, CUActualizarUsuario>();
builder.Services.AddScoped<ICUEliminarUsuario, CUEliminarUsuario>();
builder.Services.AddScoped<ICUObtenerEnvioByNumTracking, CUObtenerEnvioByNumTracking>();
builder.Services.AddScoped<ICUCambiarPassword, CUCambiarPassword>();
builder.Services.AddScoped<ICUListarEnviosDelUsuario, CUListarEnviosDelUsuario>();
builder.Services.AddScoped<ICUListarEnviosUsuarioPorFecha, CUListarEnviosUsuarioPorFecha>();
builder.Services.AddScoped<ICUListarEnviosDelUsuarioPorComentario, CUListarEnviosDelUsuarioPorComentario>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JWT
//La clave debe ser almacenada en el json, o en el sistema operativo cuando estéen producción.
var clave = "UTzl^7yPl$5xrT6&{7RZCSG&O42MEK89$CW1XXRrN(>XqIp{W4s2S5$>KT$6CG!2M]'ZlrqH-t%eI4.X9W~u#qO+oX£+[?7QDAa";
var claveCodificada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        //Definir las verificaciones a realizar
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = claveCodificada,
        RoleClaimType = ClaimTypes.Role, // <-- Esto asegura que [Authorize(Roles = "...")] funcione
        NameClaimType = ClaimTypes.Email
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
