using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

/* Component Model for Extended Database
 * Has a One-Many relationship with Asset
 * 
 */

namespace ForeignKeyExample
{
    public class Component
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        //ForeignKey(typeof(Asset))]
        public int ParentAssetId { get; set; }
    }
}
