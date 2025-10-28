# GestorDeEnvios-.NET (Prototipo funcional)

Url HTTP Cliente: 
https://clientehttpp3lc-arbdffgba7fkg4ea.brazilsouth-01.azurewebsites.net/

Url MVC Administradores / Funcionarios: 
https://webappobligatoriop3lc-b4erb6athncsftf6.brazilsouth-01.azurewebsites.net/

TEST USERS:

ADMIN
Usuario: leomessi@gmail.com 
Password: leomessi1234 

FUNCIONARIO 
Usuario: sergioaguero@gmail.com 
Password: sergioaguero1234

CLIENTE
Usuario: fernandomuslera@gmail.com 
Password: fernandomuslera1234 

(Evita usar la funcionalidad cambiar password para que los demas puedan probar la app.)

Este proyecto es una aplicación web desarrollada en ASP.NET Core Razor Pages (.NET 8) que gestiona el proceso de envíos postales. Permite a los usuarios autenticados crear, listar, consultar, finalizar y realizar seguimientos de envíos, integrando funcionalidades de auditoría y control de acceso.

Características principales

	Buscar Envio por Numero de Tracking (ANONIMO):
	-Cualquiera que entre puede buscar un pedido sin iniciar sesion si cuenta con el numero de tracking.
	
	Gestión de Envíos (Rol ADMINISTRADOR / FUNCIONARIO):
	-Creación de envíos (con soporte para diferentes tipos y agencias).
	-Listado general y filtrado de envíos.
	-Visualización de detalles y finalización de envíos.

	Gestión de Usuarios (Rol ADMINISTRADOR):
	-El Admin podra dar de alta un usuario, editar sus datos, y darlo de baja.
	-Listado general y filtrado de Usuarios.
	-Visualización de detalles de usuario.

	Visualizacion de envios propios (Rol CLIENTE):
	-Listado general y filtrado de envíos.
	-Visualización de detalles

	Seguimiento de Envíos:
	-Agregado de comentarios y seguimientos a cada envío.
	-Visualización del historial de seguimiento.

	Autenticación y Autorización:
	-Inicio de sesión de usuarios.
	-Control de acceso a funcionalidades según el rol y estado de autenticación.

	Auditoría:
	-Registro de acciones relevantes (altas, errores, finalizaciones) para trazabilidad y control.

	Arquitectura en Capas:
	-Separación clara entre lógica de aplicación, lógica de negocio, acceso a datos y presentación.
	-Uso de DTOs y mapeadores para desacoplar las capas.

	Interfaz:
	Vistas Razor Pages con estilos modernos y mensajes de feedback para el usuario.

Tecnologías utilizadas
	-.NET 8 / C# 12
	-ASP.NET Core Razor Pages
	-Entity Framework Core
	-Autenticación JWT (para la API)
	-Patrón Repository y Unit of Work
	-DTOs y AutoMapper
	-Tailwind CSS (opcional, según estilos en las vistas)

Estructura del Proyecto
	-LogicaNegocio: Entidades y lógica de negocio.
	-LogicaAccesoDatos: Repositorios y acceso a datos.
	-LogicaAplicacion: Casos de uso y servicios de aplicación.
	-ObligatorioP3: Proyecto web Razor Pages.
	-ObligatorioWebApi: API RESTful para integración externa.

Cómo ejecutar
1.	Clona el repositorio.
2.	Configura la cadena de conexión en appsettings.json ().
3.	Ejecuta las migraciones de la base de datos (si aplica).
4.	Inicia el proyecto web (ObligatorioP3) desde Visual Studio 2022.
