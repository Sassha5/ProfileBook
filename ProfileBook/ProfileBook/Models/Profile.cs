using SQLite;
using System;

namespace ProfileBook.Models
{
    [Table("Profiles")]
    public class Profile : BaseModel
    {
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string ImagePath { get; set; }
    }
}
