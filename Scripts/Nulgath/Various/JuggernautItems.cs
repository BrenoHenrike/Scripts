//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class JuggernautItemsofNulgath
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNulgath Nulgath = new CoreNulgath();

    public readonly string[] Rewards =
    {
        "Oblivion of Nulgath",
        "Ungodly Reavers of Nulgath",
        "Warlord of Nulgath",
        "Arcane of Nulgath",
        "Dimensional Champion of Nulgath",
        "Crystal Phoenix Blade of Nulgath",
        "Overfiend Blade of Nulgath",
        "Battlefiend Blade of Nulgath",
        "Dark Makai of Nulgath",
        "Nulgath Armor",
        "Polish Hussar",
        "Polish Hussar Helm",
        "Polish Hussar Spear",
        "Polish Hussar Wings",
        "Void Cowboy",
        "Void Cowboy Hat",
        "Void Cowboy Morph",
        "Void Cowboy's Mask + Locks",
        "Void Cowboy's Pistol",
        "Dual Void Cowboy Pistols"
    };
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Core.AddDrop(Nulgath.bagDrops);
        Core.AddDrop(Rewards);

        Nulgath.FarmUni13();

        int i = 1;
        while(!Core.CheckInventory(Rewards, toInv: false))
        {
            Nulgath.FarmDiamondofNulgath(13);
            Nulgath.FarmDarkCrystalShard(50);
            Nulgath.FarmTotemofNulgath(3);
            Nulgath.FarmGemofNulgath(20);
            Nulgath.FarmVoucher(false);
            Nulgath.SwindleBulk(50);

            Core.EnsureAccept(837);
            Core.HuntMonster("underworld", "Undead Bruiser", "Undead Bruiser Rune");
            Core.EnsureCompleteChoose(837);
            Core.Logger($"Completed x{i++}");
        }

        Core.SetOptions(false);
    }
}