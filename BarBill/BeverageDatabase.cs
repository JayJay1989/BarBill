using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BarBill
{
    public class BeverageDatabase
    {
        static SQLiteAsyncConnection Database;
        public static readonly AsyncLazy<BeverageDatabase> Instance = new AsyncLazy<BeverageDatabase>(async () =>
        {
            BeverageDatabase instance = new BeverageDatabase();
            CreateTableResult result = await Database.CreateTableAsync<Beverage>();
            return instance;
        });

        public BeverageDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabaseConstants.Path, Constants.DatabaseConstants.Flags);

            if (Settings.FirstRun)
            {
                Save(new Beverage { Icon = "🍺", Price = 2.5, Type = BeverageType.Beer });
                Save(new Beverage { Icon = "🍻", Price = 2.5, Type = BeverageType.StrongBeer });
                Save(new Beverage { Icon = "☕", Price = 2.5, Type = BeverageType.Coffee });
                Save(new Beverage { Icon = "🥤", Price = 2.5, Type = BeverageType.Soda });
                Save(new Beverage { Icon = "🍹", Price = 9, Type = BeverageType.Cocktail });
                Save(new Beverage { Icon = "☕🥃", Price = 7.5, Type = BeverageType.CoffeeSpecial });
                Save(new Beverage { Icon = "🍔", Price = 0.0, Type = BeverageType.Other });
                Save(new Beverage { Icon = "💉", Price = 3.5, Type = BeverageType.Shooter });
                Save(new Beverage { Icon = "💧", Price = 2.5, Type = BeverageType.Water });
                Save(new Beverage { Icon = "❔", Price = 0.0, Type = BeverageType.Other });
                BeverageConsumedDatabase beverageConsumedDatabase = new BeverageConsumedDatabase();
                beverageConsumedDatabase.DoFirstRunActions(GetAllAsync().Result);
                Settings.FirstRun = false;
            }
        }
        public Task<List<Beverage>> GetAllAsync()
        {
            return Database.Table<Beverage>().ToListAsync();
        }

        public Task<int> GetCountAsync()
        {
            return Database.Table<Beverage>().CountAsync();
        }

        public void Save(Beverage pBeverage)
        {
            Database.InsertAsync(pBeverage);
        }

        public Task<int> DeleteItemAsync(Beverage pBeverage)
        {
            return Database.DeleteAsync(pBeverage);
        }
        public void DeleteAll()
        {
            Database.DeleteAllAsync<Beverage>();
        }
    }
}
