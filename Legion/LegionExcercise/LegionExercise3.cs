//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class LegionExercise3
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreLegion Legion = new CoreLegion();
    public CoreStory Story = new CoreStory();

    private string[] Rewards = { "Judgement Hammer", "Legion Token" };

    public void ScriptMain(ScriptInterface bot)
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
        Core.BuyItem("underworld", 216, "Undead Warrior");

        Core.Logger("Disclaimer: Percentages are randomized, just made purely for fun. i cba making it an actualy %age");

        int Dice = Bot.Runtime.Random.Next(1, 101);
        //-------------------------------------------------------------------------------------------------------

        int i = 1;
        var displayPercentage = $"{(decimal)Dice / 100:P}";

        Core.Logger($"Potato Prediction Inc. Decided: {displayPercentage} is The Chance for Desired Rewards.");

        while (!Core.CheckInventory(new[] { "Judgement Hammer" }))
        {
            Core.EnsureAccept(823);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("Uppercity", "Chaos Egg", "Chaos Egg", 24, publicRoom: false);
            Core.HuntMonster("Mobius", "Chaos Sp-Eye|Chaos Spider", "Chaorrupted Essence", 50, isTemp: false, publicRoom: false);
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
