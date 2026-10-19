using TorneoGalactico.Api.Models;
using TorneoGalactico.Api.Rules;

namespace TorneoGalactico.Tests;

public class BattleTests
{
    private static Species BuildSpecie(string name, int power) =>
        new() { Id = 1, Name = name, PowerLevel = power, SpecialAbility = "x" };

    [Fact]
    public void Gana_el_de_mayor_poder()
    {
        var a = BuildSpecie("Alienigena", 90);
        var b = BuildSpecie("Hada", 50);

        Assert.Same(a, BattleRules.DetermineWinner(a, b));
    }

    [Fact]
    public void El_orden_de_los_argumentos_no_cambia_el_resultado()
    {
        var a = BuildSpecie("Robot", 90);
        var b = BuildSpecie("Vampiro", 50);

        Assert.Same(a, BattleRules.DetermineWinner(b, a));
    }

    [Theory]
    [InlineData(10, 20, "B")]
    [InlineData(20, 10, "A")]
    public void Gana_el_mayor_poder_en_varios_casos(int powerA, int powerB, string expected)
    {
        var a = BuildSpecie("A", powerA);
        var b = BuildSpecie("B", powerB);

        Assert.Equal(expected, BattleRules.DetermineWinner(a, b).Name);
    }

    [Fact]
    public void En_empate_gana_el_nombre_alfabeticamente_primero()
    {
        var a = BuildSpecie("Dragon", 70);
        var b = BuildSpecie("Humano", 70);

        Assert.Equal("Dragon", BattleRules.DetermineWinner(a, b).Name);
        Assert.Equal("Dragon", BattleRules.DetermineWinner(b, a).Name);
    }

    [Fact]
    public void El_desempate_ignora_mayusculas_y_minusculas()
    {
        var a = BuildSpecie("elfo", 70);
        var b = BuildSpecie("Enano", 70);

        Assert.Equal("elfo", BattleRules.DetermineWinner(a, b).Name);
        Assert.Equal("elfo", BattleRules.DetermineWinner(b, a).Name);
    }
}