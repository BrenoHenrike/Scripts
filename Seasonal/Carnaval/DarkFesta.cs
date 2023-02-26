/*
name: Dark Festa
description: Completes Batista's Quests in darkfesta.
tags: dark festa, batista, carnaval, seasonal, story
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DarkFesta
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Storyline();
        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(6774) || !Core.isSeasonalMapActive("darkfesta"))
            return;

        Story.PreLoad(this);

        //Gather the Flames (6762)
        Story.KillQuest(6762, "darkfesta", "Mula Sem Cabeça");

        //Branching Out (6763)
        Story.KillQuest(6763, "darkfesta", "Bamboo Treeant");

        //Torch the Torches (6764)
        Story.MapItemQuest(6764, "darkfesta", 6291, 6);

        //Explore Boitata's Lair (6765)
        Story.MapItemQuest(6765, "darkfesta", 6292, 6);
        Story.MapItemQuest(6765, "darkfesta", 6293);

        //Destroy the Growth (6766)
        Story.MapItemQuest(6766, "darkfesta", 6294);

        //Reduce the Darkness (6767)
        Story.KillQuest(6767, "darkfesta", "Darkness Elemental");

        if (!Story.QuestProgression(6768))
        {
            //Just a Little Snack (6769)
            Core.EnsureAccept(6768, 6769);
            Core.HuntMonster("darkfesta", "Bamboo Treeant", "Bamboo Leaves", 8);
            Core.EnsureComplete(6769);
            Core.EnsureComplete(6768);
        }

        //Scales for Feathers (6770)
        Story.MapItemQuest(6770, "darkfesta", 6295, 4);

        //Time for Fire (6771)
        Story.KillQuest(6771, "darkfesta", "Mula Sem Cabeça");

        //A Shot in the... Dark (6772)
        Story.MapItemQuest(6772, "darkfesta", 6296);

        //Reduce the Shadow (6773)
        Story.KillQuest(6773, "darkfesta", "Infectious Shadow");

        //Light Him Up (6774)
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6774, "darkfesta", "Dark Boitata");
    }
}
