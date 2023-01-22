/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/JoinLegion[UndeadWarrior].cs
using Skua.Core.Interfaces;

public class LegionExercise2
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreLegion Legion = new CoreLegion();
    public CoreStory Story = new CoreStory();

    private string[] Rewards = { "Executioner's Judgement", "legion Token" };

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

        while (!Bot.ShouldExit && !Core.CheckInventory(new[] { "Executioner's Judgement" }))
        {
            Core.EnsureAccept(822);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("darkoviagrave", "Skeletal Fire Mage", "Charred Skull", 20, isTemp: false, publicRoom: false);
            Core.HuntMonster("mudluk", "Tiger Leech", "Intact Tiger Leech Hide", publicRoom: false);
            Bot.Sleep(2500);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("sewer", "Grumble", "Grumble's Curse", isTemp: false, publicRoom: false);
            Core.EnsureComplete(822);
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
