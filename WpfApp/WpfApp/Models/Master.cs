using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class Master
    {
        [Key]
        public int Id { get; set; }
        public byte[] Image { get; set; }
        [Required]
        public string Surname { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string MiddleName { get; set; }

 
        public TatooTypes Type { get; set; }
        public int Experience { get; set; }

        public string FullName { get => $"{Surname} {Name} {MiddleName}"; }
        public string FullExperience { get => $"Стаж: {Experience} лет"; }
        public string FullType { get => $"Стаж: {Type}"; }

        public Master()
        {
            Id = -1;
          
        }

        public Master(int id, byte[] image, string surname, string name, string middleName, TatooTypes type, int experience)
        {
            Id = id;
            Image = image;
            Surname = surname;
            Name = name;
            MiddleName = middleName;
            Type = type;
            Experience = experience;

        }


        public string ForSearch()
        {
            return $"{Surname} {Name} {MiddleName} {Type}".ToLower();
        }
    }
}
