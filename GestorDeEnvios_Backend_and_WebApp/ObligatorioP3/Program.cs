using LogicaAccesoDatos;
using LogicaAccesoDatos.Repositorios;
using LogicaAplicacion.CasosUso.CUEnvio;
using LogicaAplicacion.CasosUso.CUUsuario;
using LogicaAplicacion.ICasosUso.ICUEnvio;
using LogicaAplicacion.ICasosUso.ICUUsuario;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;


namespace ObligatorioP3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");//DefaultConnection debe coincidir con el nombre designado en el JSON.
            // Registrar el DbContext en el contenedor de servicios
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //DI - REPOS
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

            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
