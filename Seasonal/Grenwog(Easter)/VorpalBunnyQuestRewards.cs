/*
name: Posty Mc Nobbins Quest Rewards
description: gest the quest rewards from all 4 quest from mc nobbins.
tags: mc nobbins, quest reward, farm, lucky day, luck, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class VorpalBunnyQuestRewards
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv => new();
    public CoreFarms Farm => new();

    string[] BunnySuit =
    {
        "Transforming Berzerker Bunny Helm",
        "Transforming Spear of the Berzerker Bunny",
        "Bunny on your Back"
    };


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Farm.BerserkerBunny(0, false);
        BerzerkerBunnyHelm();
        TransformingBunnySpear();
        BunnyonyourBack();

        Core.SetOptions(false);
    }


    public void BerzerkerBunnyHelm()
    {
        Core.AddDrop("Transforming Berzerker Bunny Helm");
        if (!Core.isSeasonalMapActive("grenwog") || Core.CheckInventory("Transforming Berzerker Bunny Helm"))
            return;

        Core.EnsureAccept(234);
        Core.HuntMonster("farm", "Treeant", "Wooden Egg");
        Core.EnsureComplete(234);
        Bot.Wait.ForPickup("Transforming Berzerker Bunny Helm");

    }

    public void TransformingBunnySpear()
    {
        Core.AddDrop("Transforming Spear of the Berzerker Bunny");
        if (!Core.isSeasonalMapActive("grenwog") || Core.CheckInventory("Transforming Spear of the Berzerker Bunny"))
            return;

        Core.EnsureAccept(235);
        Core.HuntMonster("boxes", "Grizzlespit", "Egg Shell");
        Core.EnsureComplete(235);
        Bot.Wait.ForPickup("Transforming Spear of the Berzerker Bunny");

    }

    public void BunnyonyourBack()
    {
        Core.AddDrop("Bunny on your Back");
        if (!Core.isSeasonalMapActive("grenwog") || Core.CheckInventory("Bunny on your Back"))
            return;

        Core.EnsureAccept(237);
        Core.HuntMonster("orctown", "General Porkon", "Quacked Egg");
        Core.EnsureComplete(237);
        Bot.Wait.ForPickup("Bunny on your Back");

    }
}
