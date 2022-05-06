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
                if (!Core.CheckInventory(48632, 8))
                    Core.KillMonster("void", "r11", "Left", "Void Elemental", "Void Energy", 8);
                Core.EnsureComplete(6908);
            }
            Story.MapItemQuest(6909, "void", 6454);
            Story.KillQuest(6910, "void", "Void Bear");
            Story.KillQuest(6911, "void", "Void Elemental");
            Story.KillQuest(6912, "void", "Void Dragon");

            if (!Story.QuestProgression(6913) || !Core.CheckInventory("Mysterious Egg"))
            {
                Core.EnsureAccept(6913);
                Core.GetMapItem(6455, 1, "mysteriousegg");
                Core.EnsureComplete(6913);
                Bot.Wait.ForPickup("Mysterious Egg");
            }

            if (!Story.QuestProgression(6914) || !Core.CheckInventory("Manticore Cub Pet"))
            {
                Core.EnsureAccept(6914);
                Core.ChainComplete(6914);
                Core.EnsureComplete(6914);
                Bot.Wait.ForPickup("Manticore Cub Pet");
            }
        }
        Core.BuyItem("mysteriousegg", 1728, "Shadow Dragon Defender");
    }

}