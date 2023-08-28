/*
name: Mysterious Egg
description: if you own the manticore cub pet, this will farm the Mysterious Egg
tags: mysterious egg, manticore cub pet, egg
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
using Skua.Core.Interfaces;

public class MysteriousEgg
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public Core7DD DD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetMysteriousEgg();

        Core.SetOptions(false);
    }

    public void GetMysteriousEgg()
    {
        if (Core.CheckInventory("Mysterious Egg"))
            return;

        if (Core.CheckInventory("Manticore Cub Pet") && !Core.CheckInventory("Mysterious Egg"))
        {
            Core.Logger("You own the \"Manticore Cub Pet\" and thus dont need to farm for the \"Mysterious Egg\".\n" +
                        "It's a lot quicker to use AQW's BuyBack function for the \"Mysterious Egg\". Please do so now\n" +
                        "https://www.aq.com > Account > Manage Account > left hand side, \"Buy Back\"\n" +
                        "Relogin and restart the script after hitting ok.",
                        messageBox: true, stopBot: true);
        }

        Core.AddDrop("Mysterious Egg");
        Core.EnsureAccept(6171);

        Core.KillMonster("pride", "r13", "Left", "Valsarian", "Key of Pride", isTemp: false);
        Core.KillMonster("gluttony", "Enter2", "Top", "Deflated Glutus", "Key of Gluttony", isTemp: false);
        Core.KillMonster("greed", "r16", "Left", "Goregold", "Key of Greed", isTemp: false);

        if (!Core.CheckInventory("Key of Sloth"))
        {
            DD.HazMatSuit();
            Core.HuntMonster("sloth", "Phlegnn", "Key of Sloth", isTemp: false);
        }

        Core.HuntMonster("lust", "Lascivia", "Key of Lust", isTemp: false);
        Bot.Quests.UpdateQuest(6000);
        Core.HuntMonster("maloth", "Maloth", "Key of Envy", isTemp: false);
        Core.HuntMonster("wrath", "Gorgorath", "Key of Wrath", isTemp: false);

        Core.EnsureComplete(6171, 42497);
        Bot.Wait.ForPickup("Mysterious Egg");
    }
}
