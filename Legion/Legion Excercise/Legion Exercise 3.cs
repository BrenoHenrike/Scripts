//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/JoinLegion[UndeadWarrior].cs
//cs_include Scripts/Story/NecroDungeon.cs

using RBot;

public class LegionExercise3
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreFarms Farm = new CoreFarms();
    public CoreLegion Legion = new CoreLegion();
    public JoinLegion JoinLegion = new JoinLegion();
    public CoreStory Story = new CoreStory();

    string[] Rewards = { "Judgement Hammer", "Legion Token" };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Core.AcceptandCompleteTries = 5;
        if (!Core.CheckInventory("Undead Champion"))
            JoinLegion.JoinLegionQuests();

        Exercise(Rewards);
        Core.ToBank(Rewards);

        Core.SetOptions(false);
    }

    public void Exercise(string[] item = null)
    {
        Core.Logger("Disclaimer: Percentages are randomized, just made purely for fun. i cba making it an actualy %age");

        Random rnd = new Random();
        int Dice = rnd.Next(1, 101);   // creates a number from 1 to 100

        //-------------------------------------------------------------------------------------------------------

        if (item != null)
        {
            if (Core.CheckInventory(item)) { return; }
            Core.AddDrop(item);
        }

        if (item == null)
        {
            if (Core.CheckInventory(Rewards)) { return; }
            Core.AddDrop(Rewards);
        }

        int i = 1;

        var displayPercentage = $"{(decimal)Dice / 100:P}";


        Core.Logger($"Potato Prediction Inc. Decided: {displayPercentage} is The Chance for Desired Rewards.");


        while (!Core.CheckInventory(new[] { "Undead Champion Blade", "Legendary Golden Death Blade" }))
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
    }
}
