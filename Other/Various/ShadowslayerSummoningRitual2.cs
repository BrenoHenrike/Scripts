/*
name: ShadowslayerSummoningRitual2
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Story/GiantTaleStory.cs
//cs_include Scripts/Farm/BuyScrolls.cs
//cs_include Scripts/Story/ShadowSlayerK.cs
//cs_include Scripts/Other\Various\ShadowslayerSummoningRitual.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class ShadowslayerSummoningRitual2
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreDailies Daily = new();
    public CoreAdvanced Adv = new();
    public ShadowSlayerK ShadowStory = new();
    public Core7DD DD = new();
    public BuyScrolls Scroll = new();
    public ShadowslayerSummoningRitual SSR = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        LunateSigil();

        Core.SetOptions(false);
    }

    public void LunateSigil(int quant = 30)
    {
        //Get "Sparkly Shadowslayer Relic"
        SSR.GetAll(true);

        Core.AddDrop("Lunate Sigil");
        Core.FarmingLogger("Lunate Sigil", quant);
        while (Core.CheckInventory("Lunate Sigil", 30))
        {
            Core.HuntMonsterQuest(9846, new (string? mapName, string? monsterName, ClassType classType)[] {
        ("darkoviaforest", "Lich Of The Stone", ClassType.Solo),
        ("chaoscave", "Dracowerepyre", ClassType.Solo),
        ("maul", "Creature Creation ", ClassType.Solo),
        ("firewar", "Uriax", ClassType.Solo),
        ("borgars", "Burglinster", ClassType.Solo),
        ("techdungeon", "Kalron the Cryptborg", ClassType.Solo),
        });
            Bot.Wait.ForPickup("Lunate Sigil");
        }

    }
}
