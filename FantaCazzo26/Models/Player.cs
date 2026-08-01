using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantaCazzo26.Models;

[Table("players")]
public class Player
{
    [Key]
    public long Id { get; set; }

    public string Role { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Squad { get; set; } = string.Empty;

    public int SuggestedPrice { get; set; }

    public bool Sold { get; set; }
}