﻿using iZathfit.Views.Windows;
using Models;
using Models.DTOS;
using Services.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.ViewModels.Windows
{
    public partial class LoginVM : ObservableObject
    {
        readonly localDialogService localDialogService;
        readonly ILoginService loginService;
        public LoginVM(localDialogService localDialog, ILoginService loginService)
        {
            localDialogService = localDialog;
            this.loginService = loginService;
        }

        [RelayCommand]
        void close()
        {            
            App.GetService<MainWindow>().Close();
        }

        public async Task verificarusuario(string User, string Password)
        {
            if (string.IsNullOrWhiteSpace(User) || string.IsNullOrWhiteSpace(Password))
            {
                localDialogService.ShowDialog(new Models.ModelsCommons.DialogModel()
                {
                    Title = "Login Incorrecto",
                    canShowCancelButton = false,
                    Message = "Usuario o Password incorrecto"
                });
                return;
            }

            var user = await loginService.Login(User, Password);

            if (user == null)
            {
                localDialogService.ShowDialog(new Models.ModelsCommons.DialogModel()
                {
                    Title = "Login Incorrecto",
                    canShowCancelButton = false,
                    Message = "Usuario o Password incorrecto"
                });
                return;
            }

            if (UsuarioLogeado != null)
                UsuarioLogeado.Invoke(this, user);

        }

        public void clean(TextBox txtuser, PasswordBox txtpass) {
            txtuser.Clear();
            txtpass.Clear();
            txtuser.Focus();
        }

        public event EventHandler<UsuarioSistema>? UsuarioLogeado;

    }
}