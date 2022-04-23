using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace BarBill
{
    public class BeverageConsumed : INotifyPropertyChanged
    {
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
        private int _aantalGeconsumeerd;
        public int AantalGeconsumeerd
        {
            get => _aantalGeconsumeerd;
            set
            {
                SetProperty(ref _aantalGeconsumeerd, value);
                AantalGeconsumeerdIsGroterDanNul = _aantalGeconsumeerd > 0;
            }
        }
        private bool _aantalGeconsumeerdIsGroterDanNul;
        public bool AantalGeconsumeerdIsGroterDanNul
        {
            get => _aantalGeconsumeerdIsGroterDanNul;
            set => SetProperty(ref _aantalGeconsumeerdIsGroterDanNul, value);
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
