using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: ExportFont("Samantha.ttf", Alias = "CustomFont")]
namespace BarBill
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //TODO:
        // schrijf data weg naar ergens? zijnde in app zelf met historiek, zijnde naar google Docs,...

        public ObservableCollection<Beverage> Beverages { get; } = new ObservableCollection<Beverage>();
        private ObservableCollection<Beverage> Other { get; } = new ObservableCollection<Beverage>();
        //private BeverageDatabase _BeverageDatabase;
        private BeverageConsumedDatabase _BeverageConsumedDatabase;
        public MainPage()
        {
            InitializeComponent();
            //_BeverageDatabase = new BeverageDatabase();
            _BeverageConsumedDatabase = new BeverageConsumedDatabase();
            UpdateTotalInTitle();
            FillAndAssignDatasourceBeverages();
        }
        private void FillAndAssignDatasourceBeverages(bool pReset = false)
        {
            if (pReset)
            {
                Beverages.Clear();
            }
            BeverageView.ItemsSource = Beverages;
            Beverages.Add(new Beverage { Icon = "🍺", Price = 2.5, Type = BeverageType.Beer });
            Beverages.Add(new Beverage { Icon = "☕", Price = 2.5, Type = BeverageType.Coffee });
            Beverages.Add(new Beverage { Icon = "🥤", Price = 2.5, Type = BeverageType.Soda });
            Beverages.Add(new Beverage { Icon = "🍹", Price = 9, Type = BeverageType.Cocktail });
            Beverages.Add(new Beverage { Icon = "☕🥃", Price = 7.5, Type = BeverageType.CoffeeSpecial });
            Beverages.Add(new Beverage { Icon = "💉", Price = 3.5, Type = BeverageType.Shooter });
            Beverages.Add(new Beverage { Icon = "💧", Price = 2.5, Type = BeverageType.Water });
            Beverages.Add(new Beverage { Icon = "❔", Price = 0.0, Type = BeverageType.Other });
            //if (pReset)
            //{
            //    Beverages.Clear();
            //}
            //List<Beverage> allBeverages = _BeverageDatabase.GetAllAsync().Result;
            //foreach (BeverageConsumed beverageConsumed in _BeverageConsumedDatabase.GetAllAsync().Result)
            //{
            //    allBeverages = allBeverages.Where(pX => pX.Type == beverageConsumed.Type).Select(c => { c.AantalGeconsumeerd = beverageConsumed.AantalGeconsumeerd; return c; }).ToList();
            //}
            //Total += allBeverages.Select(pX => pX.Price).Sum();
            //BeverageView.ItemsSource = allBeverages;
        }
        private async void AddBeverage(object sender, EventArgs args)
        {
            Beverage beverage = ((Button)sender).BindingContext as Beverage;
            if (beverage.Type == BeverageType.Other)
            {
                Beverage otherBeverage = new Beverage() { Type = BeverageType.Other };
                string prijs = await DisplayPromptAsync("Prijs", "Hoeveel kost het?");
                if (string.IsNullOrWhiteSpace(prijs))
                {
                    return;
                }
                otherBeverage.Price = double.Parse(prijs);
                beverage.Price = double.Parse(prijs);
                Other.Add(otherBeverage);
            }
            Total += beverage.Price;
            beverage.AantalGeconsumeerd++;
            UpdateTotalInTitle();
            _BeverageConsumedDatabase.UpdateDrink(beverage.Type, beverage.AantalGeconsumeerd);
        }
        private async void RemoveBeverage(object sender, EventArgs args)
        {
            Behaviors.LongPressBehavior longPressBehavior = sender as Behaviors.LongPressBehavior;
            Button pressedButton = longPressBehavior.PressedButton;
            Beverage beverage = pressedButton.BindingContext as Beverage;
            if (beverage.AantalGeconsumeerdIsGroterDanNul == false)
            {
                return;
            }
            bool removeBeverage = await DisplayAlert("Remove", $"Remove {beverage.Type}?", "👍", "👎");
            if (removeBeverage)
            {
                if (beverage.Type == BeverageType.Other)
                {
                    beverage.Price = Other.Last().Price;
                    Other.Remove(Other.Last());
                }
                Total -= beverage.Price;
                beverage.AantalGeconsumeerd--;
                UpdateTotalInTitle();
                _BeverageConsumedDatabase.UpdateDrink(beverage.Type, beverage.AantalGeconsumeerd);
            }

        }
        private void UpdateTotalInTitle()
        {
            lblTotal.Text = "€" + Total;
        }
        private async void Reset(object sender, EventArgs args)
        {

            bool reset = await DisplayAlert("Reset", $"Drinks paid?", "👍", "👎");
            if (reset)
            {
                Total = 0;
                UpdateTotalInTitle();
                FillAndAssignDatasourceBeverages(true);
                _BeverageConsumedDatabase.DeleteAll();
            }

        }
        public double Total { get; private set; } = 0.0;
    }
}
