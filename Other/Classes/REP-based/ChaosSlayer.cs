/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class ChaosSlayer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public Core13LoC LOC = new();

    public string[] Variants =
    {
        "Chaos Slayer",
        "Chaos Slayer Cleric",
        "Chaos Slayer Berserker",
        "Chaos Slayer Mystic",
        "Chaos Slayer Thief"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetCS();

        Core.SetOptions(false);
    }

    public void GetCS(CSvariant variant = CSvariant.Mystic, bool rankUpClass = true)
    {
        if (Core.CheckInventory(Variants))
            return;

        LOC.Hero();
        Adv.BuyItem("newfinale", 891, $"Chaos Slayer {variant}", shopItemID: (int)variant);

        if (rankUpClass)
            Adv.rankUpClass($"Chaos Slayer {variant}");
    }
}

public enum CSvariant
{
    Berserker = 15402,
    Cleric = 15459,
    Mystic = 15401,
    Thief = 15403
}
