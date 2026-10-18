using TorneoGalactico.Api.Models;

namespace TorneoGalactico.Api.Rules
{
    public class BattleRules
    {
        public static Species DetermineWinner(Species a, Species b)
        {
            if (a.PowerLevel != b.PowerLevel)
                return a.PowerLevel > b.PowerLevel ? a : b;

            return string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase) <= 0 ? a : b;
        }
    }
}
