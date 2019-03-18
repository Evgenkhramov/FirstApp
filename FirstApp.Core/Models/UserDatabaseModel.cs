using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FirstApp.Core.Models
{

    [Table("Users")]
    public class UserDatabaseModel
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }
}
