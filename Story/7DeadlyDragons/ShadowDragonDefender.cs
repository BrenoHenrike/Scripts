//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/7DeadlyDragons/MysteriousEgg.cs
using RBot;

public class GetSDD
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public MysteriousEgg Egg = new MysteriousEgg();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        ShadowDragonDefender();

        Core.SetOptions(false);
    }

    public void ShadowDragonDefender()
    {
        if (Core.CheckInventory("Shadow Dragon Defender"))
            return;

        Story.PreLoad();

        Core.AddDrop("Shadow Dragon Defender", "Manticore Cub Pet");

        if (!Core.CheckInventory("Manticore Cub Pet"))
        {
            Egg.GetMysteriousEgg();
            Story.KillQuest(6901, "volcano", "Lava Golem");
            Story.KillQuest(6902, "embersea", "Living Lava");
            Story.KillQuest(6903, "ashfallcamp", "Lava Dragoblin");
            Story.KillQuest(6904, "gilead", "Water Elemental");
            Story.KillQuest(6905, "crossroads", "Koalion");
            Story.KillQuest(6906, "mountain", "Giant Scorpion");
            Story.MapItemQuest(6907, "void", 6453);
            if (!Story.QuestProgression(6908))
            {
                Core.EnsureAccept(6908);
                Core.HuntMonster("void", "Void Elemental", "Void Energy", 8);
                Core.EnsureAccept(6908);
            }
            Story.MapItemQuest(6909, "void", 6454);
            Story.KillQuest(6910, "void", "Void Bear");
            Story.KillQuest(6911, "void", "Void Elemental");
            Story.KillQuest(6912, "void", "Void Dragon");
            Story.MapItemQuest(6913, "mysteriousegg", 6455, GetReward: false);
            Story.ChainQuest(6914);
        }
        Core.BuyItem("mysteriousegg", 1728, "Shadow Dragon Defender");
    }

}