using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BarBill
{
    public class BeverageConsumedDatabase
    {
        static SQLiteAsyncConnection Database;
        public static readonly AsyncLazy<BeverageConsumedDatabase> Instance = new AsyncLazy<BeverageConsumedDatabase>(async () =>
        {
            BeverageConsumedDatabase instance = new BeverageConsumedDatabase();
            CreateTableResult result = await Database.CreateTableAsync<BeverageConsumed>();
            return instance;
        });

        public BeverageConsumedDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabaseConstants.Path, Constants.DatabaseConstants.Flags);

        }
        public void DoFirstRunActions(List<Beverage> pBeverages)
        {

            foreach (Beverage beverage in pBeverages)
            {
                Save(new BeverageConsumed { Price = beverage.Price, AantalGeconsumeerd = 0, Type = beverage.Type });
            }
        }
        public Task<List<BeverageConsumed>> GetAllAsync()
        {
            return Database.Table<BeverageConsumed>().ToListAsync();
        }

        public Task<int> GetCountAsync()
        {
            return Database.Table<BeverageConsumed>().CountAsync();
        }

        public void UpdateDrink(BeverageType pBeverageType, int pAantalGeconsumeerd)
        {
            Database.QueryAsync<BeverageConsumed>("UPDATE [BeverageConsumed] SET [AantalGeconsumeerd] = " + pAantalGeconsumeerd + " WHERE [BeverageType] = " + pBeverageType);
        }
        public void Save(BeverageConsumed pBeverageConsumed)
        {
            Database.InsertAsync(pBeverageConsumed);
        }

        public Task<int> DeleteItemAsync(BeverageConsumed pBeverage)
        {
            return Database.DeleteAsync(pBeverage);
        }
        public void DeleteAll()
        {
            Database.DeleteAllAsync<BeverageConsumed>();
        }
    }
}
