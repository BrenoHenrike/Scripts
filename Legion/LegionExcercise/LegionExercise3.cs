/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class LegionExercise3
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreLegion Legion = new CoreLegion();
    public CoreStory Story = new CoreStory();

    private string[] Rewards = { "Judgement Hammer", "Legion Token" };

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

        int Dice = Bot.Random.Next(1, 101);
        //-------------------------------------------------------------------------------------------------------

        int i = 1;
        var displayPercentage = $"{(decimal)Dice / 100:P}";

        Core.Logger($"Potato Prediction Inc. Decided: {displayPercentage} is The Chance for Desired Rewards.");

        while (!Bot.ShouldExit && !Core.CheckInventory(new[] { "Judgement Hammer" }))
        {
            Core.EnsureAccept(823);
            Core.EquipClass(ClassType.Farm);
            if (!Core.CheckInventory("Chaos Egg", 24))
            {
                Core.Logger($"Hunting Chaos Egg for Chaos Egg, (24) [Temp = false]");
                while (!Core.CheckInventory("Chaos Egg", 24))
                    Core.HuntMonster("Uppercity", "Chaos Egg", publicRoom: false, log: false);
            }
            Core.HuntMonster("Mobius", "Chaos Sp-Eye", "Chaorrupted Essence", 50, isTemp: false, publicRoom: false);
            Bot.Sleep(2500);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("Underworld", "Dreadfiend Of Nulgath", "Darkness Core", publicRoom: false);
            Core.EnsureComplete(823);
            Core.Logger($"Finished Quest {i++} Times");
        }

        Core.Logger($"{Rewards} Aquired");
        Core.Logger($"Farming Took {i++} Times");

        if (Dice > i++)
            Core.Logger($"Perdiction: {Dice} was Higher Congratulations");

        Core.Logger($"Perdiction: {Dice} was lower sorry it took so long");

        Core.ToBank(Rewards);
    }
}
