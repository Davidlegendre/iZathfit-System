# iZathfit-System
Sistema de Gestion de Paquetes y Saldos iZathfit

# Primeros Pasos

## Descripcion
- El sistema se realiza la gestion de los paquetes (planes, servicios, tiempo, personas, etc) y saldos (los pagos de los clientes y muestra resumen por identificacion), esta realizado en WPF usando MVVM.

## Como Ejectutar el Sistema
1. Descarga el ejecutable en los Releases de Github
2. Coloca la variable de entorno del sistema en windows: "CONBDZATHFIT", el cual es la conexion a sql server
3. Coloca el ConnectionString en la variable de entorno: tiene que ser de SQL Server en donde este, ya sea local o nube
4. Descarga el script de SQL Server y Ejecutarlo en SQL Server (SSMS)
5. Coloca los datos Iniciales en las tablas:
- Genero ('Masculino', 'M'), ('Femenino', 'F')
- Rol ('Dueño', 'DUNO'),('Desarrollador','DEVP'),('Cliente','CLNT'),('Administrador', 'ADMN')
- Ocupacion: el que desees
- TipoIdentity ('Documento Nacional de Indentidad', 'DNI'), ('Carnet de Extranjeria', 'CE'), ('Pasaporte','PS')
- Persona: Crear un newid() para generar tu uniqueidentifier para el registro y pones tus datos
- Usuario: Tambien crear un newid() para el ID y pones tu usuario y si es activo o no (0 o 1)
- El password lo cambias con tu email en forgot password para que puedas crear un nuevo password encriptado

6. Iniciar

Puedes Ver ejemplos en la carpeta de iZathFitBD en Scripts e InitialData.sql
En Tabla Persona, ingresar los campos:
- IdPersona, Nombres, 
- Apellidos,Direccion,Email,Fech_Nacimiento,Identificacion,Telefono,idRol, 
- idGenero, idtipoIdentificacion, idOcupacion, NumeroEmergencia1

Ya que son obligatorios

## Puedo Crear mi Propia Version?
Si, Puedes Descargarte el proyecto y crear tu propia version, ya sea mejorandolo o construyendo encima de la base.
Solo recuerda que necesitas saber estos temas:
- C#11 (.NET Core)
- SQL
- Dapper
- MVVM
- Inyeccion de Dependencias
- Clean Arquitecture
- Events
- Procedures
- Linq
- Types de Usuario en SQL Server
- SQL Projects Vs2022
- XAML
- GitHub
- Task o Thread
- HttpClientFactory

### Notas Finales
Recuerda que este Sistema esta basado en una empresa, por lo que si quieres aplicar cambios, tienes que cambiar el logo y Nombre, y algunas cosas referentes a tu negocio.
Recuerda que ni el logo ni el Nombre: Modo Fit Centro Medico Deportivo son libres, tienen derechos de autor, así que cambialos si no quieres problemas legales. (el icono de la app tambien).

### Autores
- David Legendre
- [LinkedIn](https://www.linkedin.com/in/david-legendre-albites-904a361a7/)
- Francois Renquifo
- [LinkedIn](https://www.linkedin.com/in/francois-renquifo-mercado-544141192/)

### Capturas
![image](https://github.com/Davidlegendre/iZathfit-System/blob/main/Capturas/login.jpg)
![image](https://github.com/Davidlegendre/iZathfit-System/blob/main/Capturas/logindark.jpg)
![image](https://github.com/Davidlegendre/iZathfit-System/blob/main/Capturas/main.jpg)
![image](https://github.com/Davidlegendre/iZathfit-System/blob/main/Capturas/maindev.jpg)


### Librerias
- Dapper
- WPF UI
- Community Toolkit
- Newtonsoft.Json
- Microsoft.Extension.Hosting
- Microsoft.Extension.Http
- Emoji.Wpf

### Software Usado
- Visual Studio 2022 (.NET 7.0)
- SQL Server
- SQL Project Vs2022

### PoweredBY
C# - WPF - Dapper - WPF UI
