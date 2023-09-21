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
- Usuario: Tambien crear un newid() para el ID y pones tu usuario y password y si es activo o no (0 o 1)
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

### Autores
- David Legendre
- [LinkedIn](https://www.linkedin.com/in/david-legendre-albites-904a361a7/)
- Francois Renquifo
- [LinkedIn](https://www.linkedin.com/in/francois-renquifo-mercado-544141192/)

### Librerias
- Dapper
- WPF UI
- Community Toolkit
- Newtonsoft.Json
- Microsoft.Extension.Hosting
- Microsoft.Extension.Http

### Software Usado
- Visual Studio 2022 (.NET 7.0)
- SQL Server
- SQL Project Vs2022

### PoweredBY
C# - WPF - Dapper - WPF UI
