﻿using Configuration.GlobalHelpers;
using iZathfit.Views.Windows;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace iZathfit.Components.Paginator
{
    /// <summary>
    /// Lógica de interacción para PaginatorComponent.xaml
    /// </summary>
    public partial class PaginatorComponent : UserControl, INotifyPropertyChanged
    {
        IGlobalHelpers? _helpers;
        public PaginatorComponent()
        {
            InitializeComponent();
            DataContext = this;
            _helpers = App.GetService<IGlobalHelpers>();
        }

        int _page = 1;
        int _TotalPages = 0;
        bool _CanGoNext = false;
        bool _CanGoPrevious = false;
        int _PageSize = 20;
        Visibility _visibleNumber = Visibility.Collapsed;

        public Visibility VisibleTextoNumber => VisibleNumber == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

        public Visibility VisibleNumber {
            get => _visibleNumber;
            private set { 
                if(value != _visibleNumber) { 
                    _visibleNumber= value;
                    change(nameof(VisibleNumber));
                    change(nameof(VisibleTextoNumber));
                }
            }
        }

        public int Page {
            get => _page;
            private set { 
              if(value != _page)
                {
                    _page = value;
                    change(nameof(Page));
                }  
            }
        }



        public int PageSize {
            get => _PageSize;
            set => _PageSize = value;
        }

        public string TotalPagesString => TotalPages.ToString();

        public int TotalPages
        {
            get => _TotalPages;
            private set
            {
                if (value != _TotalPages)
                {
                    _TotalPages = value;
                    change(nameof(TotalPages));
                    change(nameof(TotalPagesString));
                }
            }
        }

        public bool CanGoNext
        {
            get => _CanGoNext;
            private set {
                if (value != _CanGoNext) {
                    _CanGoNext = value;
                    change(nameof(CanGoNext));
                }
            }
        }


        public bool CanGoPrevious {
            get => _CanGoPrevious;
            private set
            {
                if(value != _CanGoPrevious)
                {
                    _CanGoPrevious = value;
                    change(nameof(CanGoPrevious));
                }
            }
        }

        public ObservableCollection<T> GetPaginationCollection<T>(ObservableCollection<T> OriginalCollection) where T : class
        {
            TotalPages = OriginalCollection.Count() % PageSize == 0 ? OriginalCollection.Count() / PageSize : (OriginalCollection.Count() / PageSize) + 1;
            TotalPages = TotalPages < 1 ? TotalPages + 1 : TotalPages;
            //situacion: principio debe activarse cangonext y page = 1, Si no deben activarse el que toca y page no cambia
            if (Page > 1 && CanGoPrevious == false)
            {
                CanGoPrevious = true;
            }
            else if (Page == 1)
            {
                Page = 1;
                CanGoPrevious = false;
                CanGoNext = TotalPages > 1;
            }
            App.GetService<MainWindow>()?.Alzeimer();
            return new ObservableCollection<T>(OriginalCollection.Skip((Page - 1) * PageSize).Take(PageSize));

        }

        public ObservableCollection<T> BuscarWithPagination<T>(IEnumerable<T> OriginalCollection) where T : class
        {
            TotalPages = OriginalCollection.Count() % PageSize == 0 ? OriginalCollection.Count() / PageSize : (OriginalCollection.Count() / PageSize) + 1;
            TotalPages = TotalPages < 1 ? TotalPages + 1: TotalPages;
            Page = 1;
            CanGoPrevious = false;
            CanGoNext = TotalPages > 1;
            return new ObservableCollection<T>(OriginalCollection.Skip((Page - 1) * PageSize).Take(PageSize));

        }

        public event EventHandler? ChangePageEvent;


        void change(string property)
        { 
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void btnPrevious_click(object sender, RoutedEventArgs e)
        { //<
            if (Page > 1)
            {
                Page -= 1;
                if (Page == 1) CanGoPrevious = false;
                if (ChangePageEvent != null)
                    ChangePageEvent(this, e);
            }

            
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            //>
            if (Page < TotalPages)
            {
                Page += 1;
                if (Page == TotalPages) CanGoNext = false;
                if (ChangePageEvent != null)
                    ChangePageEvent(this, e);
            }
            
        }

        private void NumberBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                VisibleNumber = Visibility.Collapsed;
                var numberbox = sender as Wpf.Ui.Controls.NumberBox;
                if (numberbox == null || _helpers == null) return;
                if (string.IsNullOrWhiteSpace(numberbox.Text)) return;
                if (!_helpers.IsNullOrWhiteSpaceAndNumber(numberbox.Text)) return;
                var page = Convert.ToInt32(numberbox.Text);               

                if (page < 1) {
                    Page = Page;
                    return;
                }
                if (page > TotalPages) {
                    Page = Page;
                    return;
                }
                if (page >= 1 && page != Page)
                {
                    Page = page;
                    if(ChangePageEvent != null)
                        ChangePageEvent(this, e);
                    return;
                }
                if(page <= TotalPages && page != Page)
                {
                    Page = page;
                    if (ChangePageEvent != null)
                        ChangePageEvent(this, e); 
                    return;
                }
            }
        }

        private void tbPageClick(object sender, MouseButtonEventArgs e)
        {            
            VisibleNumber = Visibility.Visible;
        }
    }
}
