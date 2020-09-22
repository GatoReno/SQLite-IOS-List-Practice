using System;
using SQLite;

namespace IOSSQLite.models
{
    public class ContactModel
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
