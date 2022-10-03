//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class LegionExercise4
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreLegion Legion = new CoreLegion();
    public CoreStory Story = new CoreStory();

    private string[] Rewards = { "Corrupted Dragon Slayer", "Judgement Scythe", "PainSaw of Eidolon", "Soul Eater Advanced", "Legion Token" };

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

        while (!Bot.ShouldExit && !Core.CheckInventory(new[] { "Corrupted Dragon Slayer", "Judgement Scythe", "PainSaw of Eidolon", "Soul Eater Advanced" }))
        {
            Core.EnsureAccept(824);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("swordhavenundead", "Skeletal Ice Mage", "Frostbit Skull", 15);
            Core.HuntMonster("Marsh2", "Lesser Shadow Serpent", "Potent Viper's Blood");
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("Marsh2", "Soulseeker", "Soul Scythe", isTemp: false);
            Core.EnsureComplete(824);
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