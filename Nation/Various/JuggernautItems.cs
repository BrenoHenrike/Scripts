//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class JuggernautItemsofNulgath
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.AddRange(Rewards);
        Core.SetOptions();

        JuggItems();

        Core.SetOptions(false);
    }

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

    public void JuggItems()
    {
        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop(Rewards);

        Nation.FarmUni13();

        int i = 1;
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards, toInv: false))
        {
            Nation.FarmDiamondofNulgath(13);
            Nation.FarmDarkCrystalShard(50);
            Nation.FarmTotemofNulgath(3);
            Nation.FarmGemofNulgath(20);
            Nation.FarmVoucher(false);
            Nation.SwindleBulk(50);

            Core.EnsureAccept(837);
            Core.HuntMonster("underworld", "Undead Bruiser", "Undead Bruiser Rune");
            Core.EnsureCompleteChoose(837);
            Core.Logger($"Completed x{i++}");
        }
    }
}