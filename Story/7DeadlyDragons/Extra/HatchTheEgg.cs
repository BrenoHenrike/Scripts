//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Other/MysteriousEgg.cs
using Skua.Core.Interfaces;

public class HatchTheEgg
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public MysteriousEgg Egg = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Hatch();

        Core.SetOptions(false);
    }

    public void Hatch()
    {
        if (Core.isCompletedBefore(6914) && Core.CheckInventory("Manticore Cub Pet"))
            return;

        Egg.GetMysteriousEgg();

        Story.PreLoad(this);

        //6901 | Feel the Heat
        Story.KillQuest(6901, "volcano", "Lava Golem");

        //6902 | Need Lava
        Story.KillQuest(6902, "embersea", "Living Lava");

        //6903 | Baby Food
        Story.KillQuest(6903, "ashfallcamp", "Lava Dragoblin");

        //6904 | Thirsty!
        Story.KillQuest(6904, "gilead", "Water Elemental");

        //6905 | Dress Up Time
        Story.KillQuest(6905, "crossroads", "Koalion");

        //6906 | Complete your Costume
        Story.KillQuest(6906, "mountain", "Giant Scorpion");

        //6907 | Find the Void Larva
        Story.MapItemQuest(6907, "void", 6453);

        //6908 | Void Energy Needed
        if (!Story.QuestProgression(6908))
        {
            Core.EnsureAccept(6908);
            while (!Bot.ShouldExit && !Core.CheckInventory(48632, 8))
            {
                Core.Join("void", "r11", "Left");
                Bot.Kill.Monster("Void Elemental");
            }
            Core.EnsureComplete(6908);
        }

        //6909 | Go Find Mariel
        Story.MapItemQuest(6909, "void", 6454);

        //6910 | Void... Parchment ?
        Story.KillQuest(6910, "void", "Void Bear");

        //6911 | Magical Ink
        Story.KillQuest(6911, "void", "Void Elemental");

        //6912 | A Soul for a Spell
        Bot.Quests.UpdateQuest(904);
        Story.KillQuest(6912, "void", "Void Dragon");

        //6913 | Cast the Spell!
        if (!Story.QuestProgression(6913) || !Core.CheckInventory("Mysterious Egg"))
        {
            Core.EnsureAccept(6913);
            Core.GetMapItem(6455, 1, "mysteriousegg");
            Core.EnsureComplete(6913);
            Bot.Wait.ForPickup("Mysterious Egg");
        }

        //6914 | The Egg Hatches
        if (!Story.QuestProgression(6914) || !Core.CheckInventory("Manticore Cub Pet"))
        {
            Core.ChainComplete(6914);
            Bot.Wait.ForPickup("Manticore Cub Pet");
        }
    }
}