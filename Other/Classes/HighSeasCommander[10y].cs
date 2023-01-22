/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class HighSeasCommander
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreAdvanced Adv => new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetHSC();

        Core.SetOptions(false);
    }

    public void GetHSC(bool rankUpClass = true)
    {
        if (Core.CheckInventory("HighSeas Commander"))
            return;

        if (!Core.HasAchievement(27, "ip14"))
        {
            Core.Logger("This bot requiers you to have an account of 10 years or older.", messageBox: true);
            return;
        }

        UnlockFarm();

        Core.Logger("Farming for HighSeas Commander");
        Core.BuyItem(Bot.Map.Name, 1647, "Enchanted Pirate");
        Core.AddDrop("HighSeas Commander", "Enchanted Token");
        Core.EnsureAccept(6921);
        if (!Core.CheckInventory("Enchanted Token", 50))
        {
            Core.RegisterQuests(6922);
            Core.Logger($"Farming Enchanted Tokens ({Bot.Inventory.GetQuantity("Enchanted Token")}/50)");

            while (!Core.CheckInventory("Enchanted Token", 50))
            {
                Core.HuntMonster("natatorium", "Merdraconian", "Merdraconian Defeated", 5);
                Core.HuntMonster("natatorium", "Anglerfish", "Anglerfish Defeated", 5);
                Core.HuntMonster("natatorium", "Nessie", "Nessie Defeated");
                Bot.Wait.ForPickup("Enchanted Token");
            }
            Core.CancelRegisteredQuests();
        }
        Core.EnsureComplete(6921);
        Bot.Wait.ForPickup("HighSeas Commander");

        if (rankUpClass)
            Adv.rankUpClass("HighSeas Commander");
    }

    public void UnlockFarm()
    {
        if (Core.isCompletedBefore(6920))
            return;

        Story.PreLoad(this);

        //6918 | Sweep the Deck
        Story.KillQuest(6918, "highseas", "Skeleton Crew");

        //6919 | Troubles In Deep Waters
        Story.KillQuest(6919, "highseas", "Pirate Crew");

        //6920 | The Captain Wants a Word
        Story.KillQuest(6920, "highseas", "Capt. Beard");

    }
}
