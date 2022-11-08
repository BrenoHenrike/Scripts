//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Harvest
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(136))
            return;

        Story.PreLoad(this);

        //Scout the Cornycopia (130)
        Story.MapItemQuest(130, "harvest", 36);

        //Unboiling Water (131)
        if (!Story.QuestProgression(131))
        {
            Core.EnsureAccept(131, 420);
            Core.GetMapItem(31, 1, "harvest");
            Core.EnsureComplete(420);
            Core.EnsureComplete(131);
        }

        //The Corn has Ears (132)
        if (!Story.QuestProgression(132))
        {
            Core.EnsureAccept(132, 421);
            Core.HuntMonster("harvest", "Corn Stalker", "Corn Stalker Ears", 8);
            Core.EnsureComplete(421);
            Core.EnsureComplete(132);
        }

        //An Apple a Day (133)
        if (!Story.QuestProgression(133))
        {
            Core.EnsureAccept(133, 422);
            Core.HuntMonster("harvest", "Bad Apple", "Worm", 5);
            Core.EnsureComplete(422);
            Core.EnsureComplete(133);
        }

        //Whine n' Cheese (134)
        if (!Story.QuestProgression(134))
        {
            Core.EnsureAccept(134, 423);
            Core.HuntMonster("harvest", "Grapes of Wrath", "Whine", 8);
            Core.EnsureComplete(423);
            Core.EnsureComplete(134);
        }

        //Fruit of the Loot (135)
        Story.KillQuest(135, "harvest", "*");
        Story.MapItemQuest(135, "harvest", 37);

        //The Turdraken (136)
        Story.KillQuest(136, "harvest", "Turdraken");
    }
}