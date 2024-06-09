using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobile_3ilPark.Models
{
    public class Computers
    {
        [Key]

        public int Id { get; set; }
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Caption { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int TotalPhysicalMemory { get; set; }
        public int? RoomId { get; set; }
        public Rooms? Room { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
    }
}




