using System.ComponentModel.DataAnnotations;

namespace SkysApiDemo.Data;

public class Player
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    public int Jersey { get; set; }
    public int Age { get; set; }
    [MaxLength(100)]
    public string Born { get; set; }
    [MaxLength(100)]
    public string UserEmail { get; set; }
}