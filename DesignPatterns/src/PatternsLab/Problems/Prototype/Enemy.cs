using System.Runtime.CompilerServices;

namespace PatternsLab.Problems.Prototype;

public class Weapon
{
    public required string Name { get; set; }
    public int Damage { get; set; }
}

public abstract class Enemy
{
    private string _modelData;

    public string Name { get; set; }=string.Empty;
    public int Health { get; set; }
    public Weapon Weapon { get; set; } =null!;
    public List<string> Abilities { get; set; } = new();
    public string ModelId => _modelData;

    protected Enemy()
    {
        Console.WriteLine("   ...loading 3D model (slow)...");
        Thread.Sleep(500);
        _modelData = "MODEL_" + Guid.NewGuid().ToString("N")[..6];
    }

    public abstract Enemy Clone ();
}

public class Orc : Enemy
{
    public Orc()
    {
        Name = "Orc";
        Health = 100;
        Weapon = new Weapon { Name = "Axe", Damage = 25 };
        Abilities.Add("Rage");
    }

    public override Enemy Clone()
    {
       var copy=(Orc)MemberwiseClone();
       copy.Weapon=new Weapon
       {
           Name=Weapon.Name ,
           Damage=Weapon.Damage 
       };
       copy.Abilities=new List<string>(Abilities);
       return copy;
    }
}

public class Elf : Enemy
{
    public Elf()
    {
        Name = "Elf";
        Health = 70;
        Weapon = new Weapon { Name = "Bow", Damage = 18 };
        Abilities.Add("Stealth");
    }

    public override Enemy Clone()
    {
        var copy=(Elf)MemberwiseClone();
         copy.Weapon=new Weapon
         {
             Name=Weapon.Name,Damage=Weapon.Damage
         };
         copy.Abilities=new List<string>(Abilities);
         return copy;
    }
}

public static class EnemyCopyHelper
{
    public static Enemy CopyEnemy(Enemy e)
    {
        return e.Clone();
    }
}
