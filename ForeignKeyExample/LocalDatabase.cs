using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using SQLite;

/* Database class that uses the SQLite-Net Extensions
 * This class holds all the read & write operations & access to the SQLite database
 * 
 */ 
namespace ForeignKeyExample
{
    public class LocalDatabase
    {
        readonly SQLiteAsyncConnection db;
        public bool enableFKey = false;

        /* Upon creation. Drop the 2 tables, and create the 2 tables to reset the database.
         * Requires the .db3 file path to access the database
         */ 
		public LocalDatabase(string dbPath)
		{
            db = new SQLiteAsyncConnection(dbPath);
            db.ExecuteAsync("PRAGMA foreign_keys = ON");

            int result = db.ExecuteScalarAsync<int>("PRAGMA foreign_keys").Result;
            if(result == 1){
                enableFKey = true;
                System.Diagnostics.Debug.WriteLine("Foreign Key is working");
            }

            db.ExecuteAsync("CREATE TABLE if not exists `Asset` (`Id` INTEGER PRIMARY KEY AUTOINCREMENT, `Name` TEXT, `Comments` TEXT, `Address` TEXT, `LocationDescription` TEXT );");
            db.ExecuteAsync("CREATE TABLE if not exists `Component` ( `Id` INTEGER PRIMARY KEY AUTOINCREMENT, `Name` TEXT, `ParentAssetId` integer, FOREIGN KEY(`ParentAssetId`) REFERENCES `Asset`(`Id`));");

		}

		public Task<int> InsertAssetAsync(Asset asset)
		{
			return db.InsertAsync(asset);
		}

		/* Creates a new component.
		 * Map it to Parent Asset.
		 * Insert into database.
		 * Update Parent Asset in database.
         *
         */
		public void InsertComponentAsync(Component component)
        {
            db.InsertAsync(component);
        }

		/* Retrieves a list of Assets within the Asset Table.
         */
		public Task<List<Asset>> GetAssetsAsync(){
            return db.Table<Asset>().ToListAsync();
        }

		/* Retrieves an Asset from the Asset table via Asset Id.
         */
		public Task<Asset> GetAssetAsync(int id)
		{
			return db.Table<Asset>().Where(i => i.Id == id).FirstOrDefaultAsync();
		}

		/* Retrieves a list of Components from the Component Table by Asset ID
         */
		public Task<List<Component>> GetComponentsByAssetId(Asset asset){
            return db.Table<Component>().Where(i => i.ParentAssetId == asset.Id).ToListAsync();
        }

		/* Retrieves a list of Assets within the Asset Table.
         */
		public Task<List<Component>> GetComponentsAsync()
		{
			return db.Table<Component>().ToListAsync();
		}


        public Task<int> UpdateAssetAsync(Asset asset){

            return db.UpdateAsync(asset);
        }

		public Task<int> UpdateComponentAsync(Component component)
		{
			return db.UpdateAsync(component);
		}

		/* Save an Asset into the Asset Table
         */
		public Task<int> SaveAssetAsync(Asset asset)
		{
            //if Asset exists in database
			if (asset.Id != 0)
			{
				return db.UpdateAsync(asset);
			}
			else
			{
                //otherwise add to database
				return db.InsertAsync(asset);
			}
		}

		/* Delete an Asset within the Asset Table.
         */
		public Task<int> DeleteAssetAsync(Asset asset)
		{
			return db.DeleteAsync(asset);
		}

		/* Delete a Component within the Component Table.
         */
		public Task<int> DeleteComponentAsync(Component component)
		{
			return db.DeleteAsync(component);
		}

		/* Clear all information from the Database by dropping & recreating the Asset & Component Tables
         */
		public void Clear(){
			db.DropTableAsync<Asset>().Wait();
			db.DropTableAsync<Component>().Wait();

			db.CreateTableAsync<Asset>().Wait();
			db.CreateTableAsync<Component>().Wait();
        }

		/* Create a Sample Asset.
		 * Requires string name to name the Asset.
		 * Creates 4 sample Components to add to the Asset
         */
		public void CreateAsset(string name){

        }
	}
}
