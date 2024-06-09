using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobile_3ilPark.Models;

public class Rooms
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int? ParksId { get; set; }
    public DateTime? CreateDate { get; set; }

    // Constructor par défaut sans paramètres
    public Rooms()
    {
    }

    // Constructor avec paramètres pour faciliter l'initialisation
    public Rooms(string name, int? parksId, DateTime? createDate)
    {
        Name = name;
        ParksId = parksId;
        CreateDate = createDate;
    }
}