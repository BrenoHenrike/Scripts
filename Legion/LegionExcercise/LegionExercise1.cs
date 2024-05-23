/*
name: Legion Exercise Number 1
description: This script will complete "Legion Exercise Number 1" quest.
tags: legion exercise, 1, undead champion blade, legendary golden death blade
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class LegionExercise1
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public CoreLegion Legion = new();
    public CoreStory Story = new();

    private string[] Rewards = { "Undead Champion Blade", "Legendary Golden Death Blade" };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Exercise(Rewards);

        Core.SetOptions(false);
    }

    public void Exercise(string[] item)
    {
        if (Core.CheckInventory(item))
            return;
        Core.AddDrop(item);

        Legion.JoinLegion();
        Core.BuyItem("underworld", 216, "Undead Champion");

        Core.Logger("Disclaimer: Percentages are randomized, just made purely for fun. i cba making it an actualy %age");

        int Dice = Bot.Random.Next(1, 101);   // creates a number from 1 to 100
        //-------------------------------------------------------------------------------------------------------

        int i = 1;
        var displayPercentage = $"{(decimal)Dice / 100:P}";

        Core.Logger($"Potato Prediction Inc. Decided: {displayPercentage} is The Chance for Desired Rewards.");

        Core.EquipClass(ClassType.Farm);
        while (!Bot.ShouldExit && !Core.CheckInventory(new[] { "Undead Champion Blade", "Legendary Golden Death Blade" }))
        {
            Core.EnsureAccept(821);
            Core.HuntMonster("lair", "Water Draconian", "Flawless DracoHorn", 40, isTemp: false, publicRoom: false);
            Core.HuntMonster("lair", "Golden Draconian", "Golden DracoHeart", isTemp: true, publicRoom: false);
            Core.HuntMonster("lair", "Dark Draconian", "Dark DracoHeart", isTemp: true, publicRoom: false);
            Core.HuntMonster("lair", "Bronze Draconian", "Mammoth DracoHeart", isTemp: true, publicRoom: false);
            Core.HuntMonster("lair", "Water Draconian", "Water DracoHeart", isTemp: true, publicRoom: false);
            Core.HuntMonster("lair", "Venom Draconian", "Venom DracoHeart", isTemp: true, publicRoom: false);
            Core.HuntMonster("lair", "Purple Draconian", "Iron DracoHeart", isTemp: true, publicRoom: false);
            Core.EnsureComplete(821);
            Core.Logger($"Finished Quest {i++} Times");
        }

        Core.Logger($"Farming {item} Took {i++} Times");

        if (i++ > Dice)
            Core.Logger($"Perdiction: {displayPercentage} May have been a bit Low");
        if (i++ < Dice)
            Core.Logger($"Perdiction: {displayPercentage} Was waaaay to high... Congratulations!");

        Core.ToBank(Rewards);
    }

}
