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

    public readonly int[] SkillOrder = { 2, 4, 3, 1 };

    public void ScriptMain(ScriptInterface Bot)
    {
        Core.SetOptions();

        KFC();

        Core.SetOptions(false);
    }

    private void KFC()
    {
        Core.AddDrop(Core.EnsureLoad(8605).Rewards.Select(x => x.Name).ToArray());


        if (!Story.QuestProgression(8603))
        {
            Core.EnsureAccept(8603);
            Core.GetMapItem(10031, 1, "battleontown");
            Core.EnsureComplete(8603);
        }
        if (!Core.CheckInventory(Core.EnsureLoad(8605).Rewards.Select(x => x.Name).ToArray()))
        {
            Core.EnsureAccept(8605);
            Core.Join("battleontown", "r9", "Right");
            Bot.Player.SetSpawnPoint();

            Adv.BoostKillMonster("battleontown", "r9", "Right", "Giant ChickenCow", "Flaming Feather", 25);
            Core.EnsureComplete(8605);
        }

    }
}