using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

namespace ProfileBook.Models
{
    [Table("Profiles")]
    public class Profile
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public byte[] ImageData { get; set; }
        [Ignore]
        public ImageSource ImageSource
        {
            get 
            {
                if (ImageData != null) return ImageSource.FromStream(() => new MemoryStream(ImageData));
                else return ImageSource.FromFile(Constants.DefaultProfileImage);
            }
        }
    }
}
