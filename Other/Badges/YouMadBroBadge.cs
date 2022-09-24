//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class YouMadBroBadge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        Farm.AlchemyREP();

        while (!Bot.ShouldExit && !Core.HasAchievement(15))
        {
            Core.AddDrop("Dragon Scale", "Ice Vapor");
            Core.Logger("Farming Reagents.\n" +
            "buying takes 60m / 30 achemy Packets.\n" +
            "meaning youd have to Farm gold each go around.");
            Core.FarmingLogger("Dragon Scale", 30);
            Core.FarmingLogger("Ice Vapor", 30);
            while (!Core.CheckInventory(11475, 30) || !Core.CheckInventory(11478, 30))
                Core.KillMonster("lair", "Enter", "Spawn", "*", log: false);
            Core.Logger($"Buying Runestones");
            Adv.BuyItem("alchemy", 395, "Dragon Runestone", 30, 10, 8845);
            Adv.BuyItem("alchemy", 395, "Dragon Runestone", 30, 1, 8844);
            //the 2nd buy is for if its close but not at max stack, it wont buy the full 30.
            Core.Join("alchemy");
            Core.Logger($"Beginning RNG");
            Farm.AlchemyPacket("Dragon Scale", "Ice Vapor", trait: CoreFarms.AlchemyTraits.hOu, P2w: true);
        }
    }
}