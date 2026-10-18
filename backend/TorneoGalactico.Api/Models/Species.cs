namespace TorneoGalactico.Api.Models
{
    public class Species
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PowerLevel { get; set; }
        public string SpecialAbility { get; set; } = string.Empty;
    }
}