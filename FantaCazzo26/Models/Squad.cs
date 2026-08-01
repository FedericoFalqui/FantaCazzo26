using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantaCazzo26.Models;

[Table("squads")]
public class Squad
{
    [Key]
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string President { get; set; } = string.Empty;

    public int NumGk { get; set; }

    public int NumDef { get; set; }

    public int NumMid { get; set; }

    public int NumStr { get; set; }

    public int Credits { get; set; }
}