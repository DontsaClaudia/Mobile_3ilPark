using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobile_3ilPark.Models
{
    public class Parks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Address { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public Parks() { }

        public Parks(int id, string name, string address, DateTime createDate)
        {
            Id = id;
            Name = name;
            Address = address;
            CreateDate = createDate;
        }
    }
}