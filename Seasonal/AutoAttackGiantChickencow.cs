//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class AAGiantChickenCow
{
    public CoreBots Core => CoreBots.Instance;
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface Bot)
    {
        Core.SetOptions();

        KFC();

        Core.SetOptions(false);
    }

    public void KFC()
    {
        int Dice = Bot.Runtime.Random.Next(1, 999999);

        Core.AddDrop(Core.EnsureLoad(8605).Rewards.Select(x => x.Name).ToArray());

        Story.MapItemQuest(8603, "battleontown", 10031);

        if (!Core.CheckInventory(Core.EnsureLoad(8605).Rewards.Select(x => x.Name).ToArray()))
        {
            Core.EnsureAccept(8605);
            Core.Join("battleontown", "r9", "Right");
            Bot.Player.SetSpawnPoint();

            Core.Logger($"If you're reading this, I banged your mom {Dice} times last night.");
            Adv.BoostKillMonster("battleontown", "r9", "Right", "Giant ChickenCow", "Flaming Feather", 25);
            Core.EnsureComplete(8605);
        }
    }
}