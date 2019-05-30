﻿using SQLite;

namespace FirstApp.Core.Models
{
    [Table("Users")]
    public class UserDatabaseModel
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public double UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[] Password { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string PhotoURL { get; set; }
        public LoginType TypeUserLogin { get; set; }
    }
}
