﻿using iZathfit.ModelsComponents;
using iZathfit.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iZathfit.Views.Pages.SubPagesHome
{
    /// <summary>
    /// Lógica de interacción para MantenimientosPage.xaml
    /// </summary>
    public partial class MantenimientosPage : UserControl
    {
        MantenimientosVM? _vm;
        public MantenimientosPage()
        {
            InitializeComponent();
            _vm = this.DataContext as MantenimientosVM;
            this.Loaded += MantenimientosPage_Loaded;
        }

        private void MantenimientosPage_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 800);
        }

        private void ActionToCommand_Click(object sender, RoutedEventArgs e)
        {
            var button = (Wpf.Ui.Controls.CardAction)sender;
            var contexto = button.DataContext as MenuItemCardsModel;
            if (contexto != null && contexto.Comando != null)
            {
                Wpf.Ui.Animations.Transitions.ApplyTransition(contenidowin, Wpf.Ui.Animations.TransitionType.FadeIn, 200);
                contexto.Comando.Invoke();
            }
        }
    }
}
