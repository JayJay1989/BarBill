using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using SQLite;
namespace BarBill
{
    public class Beverage : INotifyPropertyChanged
    {
        [Unique, PrimaryKey]

        public BeverageType Type
        {
            get;
            set;
        }
        private double _price;
        public double Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }
        public string Icon
        {
            get;
            set;
        }
        private int _aantalGeconsumeerd;
        public int AantalGeconsumeerd
        {
            get => _aantalGeconsumeerd;
            set
            {
                SetProperty(ref _aantalGeconsumeerd, value);
                AantalGeconsumeerdIsGroterDanNul = _aantalGeconsumeerd > 0;
                FontSize = _aantalGeconsumeerd > 9 ? 15 : 18;
                Margin = _aantalGeconsumeerd > 9 ? new Thickness(2.5, 1.5, 0, 0) : new Thickness(6.5, 0, 0, 0);
            }
        }
        private bool _aantalGeconsumeerdIsGroterDanNul;
        public bool AantalGeconsumeerdIsGroterDanNul
        {
            get => _aantalGeconsumeerdIsGroterDanNul;
            set => SetProperty(ref _aantalGeconsumeerdIsGroterDanNul, value);
        }
        private double _fontSize;
        [Ignore]
        public double FontSize
        {
            get => _fontSize;
            set => SetProperty(ref _fontSize, value);
        }
        private Thickness _margin;
        [Ignore]
        public Thickness Margin
        {
            get => _margin;
            set => SetProperty(ref _margin, value);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }


    }
}
