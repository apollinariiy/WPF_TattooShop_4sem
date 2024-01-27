using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp.Services;

namespace WpfApp.Models
{
    public class Sketch
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public TatooTypes Type { get; set; }
        public byte[] Image { get; set; }


        public Sketch() => Id = -1;

        public Sketch(byte[] image, TatooTypes type)
        {
            Id = -1;
            Type = type;
            Image = image;
        }

        [NotMapped]
        public bool IsFavorite
        {
            get
            {
                var currentUser = Manager.CurrentUser as User;
                return DataConnection.GetFavourites().Any(f => f.User == currentUser && f.Sketch == this);
            }
        }
    }

    public enum TatooTypes
    {
        Abstraction,
        BlackWork,
        Minimalism,
        Handpook,
        None
    }
}
