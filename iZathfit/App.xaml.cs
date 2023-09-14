// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Configuration;
using Configuration.GlobalHelpers;
using Domain.Genero;
using Domain.Ocupaciones;
using Domain.PadecimientosEnfermedades;
using Domain.Persona;
using Domain.Plan;
using Domain.PlanDuracion;
using Domain.Rol;
using Domain.Servicios;
using Domain.TipoIdentificacion;
using Domain.Usuario;
using iZathfit.Helpers;
using iZathfit.ServicesSystem;
using iZathfit.Views.Pages;
using iZathfit.Views.Pages.Mantenimiento;
using iZathfit.Views.Pages.Negocio;
using iZathfit.Views.Pages.Negocio.Ventanas.ViewModels;
using iZathfit.Views.Pages.SubPagesHome;
using iZathfit.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Genero;
using Services.Login;
using Services.Ocupacion;
using Services.PadecimientosEnfermedades;
using Services.Persona;
using Services.Plan;
using Services.PlanDuracion;
using Services.Rol;
using Services.Servicios;
using Services.TipoIdentificacion;
using Services.Usuario;
using System.Windows.Threading;

namespace iZathfit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
        // https://docs.microsoft.com/dotnet/core/extensions/generic-host
        // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
        // https://docs.microsoft.com/dotnet/core/extensions/configuration
        // https://docs.microsoft.com/dotnet/core/extensions/logging
        private static readonly IHost _host = Host
            .CreateDefaultBuilder()
            
            .ConfigureServices((context, services) =>
            {
                services.AddHttpClient();
                services.AddHostedService<ApplicationHostService>();
               

                services.AddTransient<HomePage>();
                services.AddTransient<MantenimientosPage>();
                services.AddTransient<ServiciosPage>();
                services.AddTransient<MantenimientoPersonas>();
                services.AddTransient<MantenimientoUsuarios>();
                services.AddTransient<MantenimientoOcupaciones>();
                services.AddTransient<MantenimientoTipoIdentificacion>();
                services.AddTransient<MantenimientoPadecimientosEnfermedades>();
                services.AddTransient<PlanDuracionPage>();
                services.AddTransient<PlanesPage>();

                services.AddScoped<MainWindow>();
                services.AddScoped<LoginPage>();
                services.AddScoped<Home>();
                services.AddScoped<SettingPage>();
                services.AddScoped<IExceptionHelperService, ExceptionsHelperService>();
                services.AddScoped<IGeneralConfiguration, GeneralConfiguration>();
                services.AddScoped<LoginService>();
                services.AddScoped<CryptoService>();
                services.AddScoped<IGeneroRepository, GeneroRepository>();
                services.AddScoped<IPersonaRepository, PersonaRepository>();
                services.AddScoped<IGeneroService, GeneroService>();
                services.AddScoped<ILoginService, LoginService>();
                services.AddScoped<IUsuarioRepository, UsuarioRepository>();
                services.AddScoped<IOcupacionService, OcupacionService>();
                services.AddScoped<IOcupacionRepository, OcupacionRepository>();
                services.AddScoped<IPersonaService, PersonaService>();
                services.AddScoped<IUsuarioService, UsuarioService>();
                services.AddScoped<ITipoIdentificacionRepository, TipoIdentificacionRepository>();
                services.AddScoped<ITipoIdentificacionService, TipoIdentificacionService>();
                services.AddScoped<IGlobalHelpers, GlobalHelpers>();
                services.AddScoped<IRolRepository, RolRepository>();
                services.AddScoped<IRolService, RolService>();
                services.AddScoped<IPadecimientosEnfermedadesRepository, PadecimientosEnfermedadesRepository>();
                services.AddScoped<IPadecimientosEnfermedadesService, PadecimientosEnfermedadesService>();
                services.AddScoped<IServiciosRepositoryI, ServiciosRepository>();
                services.AddScoped<IServiciosService, ServiciosService>();
                services.AddScoped<ServiciosViewModel>();
                services.AddScoped<IPlanDuracionRepository, PlanDuracionRepository>();
                services.AddScoped<IPlanDuracionService, PlanDuracionService>();
                services.AddScoped<PlanDuracionFormViewModel>();
                services.AddScoped<IPlanRepository, PlanRepository>();
                services.AddScoped<IPlanService, PlanService>();

                services.AddSingleton<localDialogService>();
                services.AddSingleton<GlobalService>();


            }).Build();

        /// <summary>
        /// Gets registered service.
        /// </summary>
        /// <typeparam name="T">Type of the service to get.</typeparam>
        /// <returns>Instance of the service or <see langword="null"/>.</returns>
        public static T? GetService<T>()
            where T : class
        {
            return _host.Services.GetService(typeof(T)) as T;
        }

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        private void OnStartup(object sender, StartupEventArgs e)
        {
            _host.Start();
        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();

            _host.Dispose();
        }

        /// <summary>
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
        }
    }
}
