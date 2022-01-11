using System.ComponentModel.DataAnnotations;

namespace SkysApiDemo
{
    public class PlayerListDTO
    {
        public int Id { get; set; }
        public string Namn { get; set; }
        public int Jersey { get; set; }
        public int Age { get; set; }
        public string Born { get; set; }
    }
}