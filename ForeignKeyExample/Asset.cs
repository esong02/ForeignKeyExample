using SQLite;
using System.Collections.Generic;

/* Asset Model for Extended Database
 * Has a Many-One relationship with Component
 * 
 */

namespace ForeignKeyExample
{
    public class Asset
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }
        public string Address { get; set; }
        public string LocationDescription { get; set; }
    }
}
