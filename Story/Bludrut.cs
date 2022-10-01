//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Bludrut
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(1693))
            return;

        Story.PreLoad(this);

        // [[[ bludrut ]]]

        //Stone Endurance 100
        Story.KillQuest(100, "bludrut", "Rattlebones");

        //Heart of Stone 101
        Story.KillQuest(101, "bludrut", "Rock Elemental");

        // [[[ bludrut2 ]]]

        //Fire Endurance 102
        Story.KillQuest(102, "bludrut2", "Shadow Creeper");

        if (Core.IsMember)
            //Burning Heart 103
            Story.KillQuest(103, "bludrut2", "Fire Elemental");

        // [[[ bludrut3 ]]]

        if (Core.IsMember)
        {
            //Water Endurance 104
            Story.KillQuest(104, "bludrut3", "Siren");

            //Liquid Soul 105
            Story.KillQuest(105, "bludrut3", "Ice Elemental");
        }

        // [[[ bludrut4 ]]]

        if (Core.IsMember)
        {
            //Dark Endurance 106
            Story.KillQuest(106, "bludrut4", "Shadow Serpent");

            //Heart of Darkness 107
            Story.KillQuest(107, "bludrut4", "Evil Elemental");
        }

        if (Core.IsMember)
        {

            //Lost Memories 1685
            Story.MapItemQuest(1685, "bludrut", 891);

            //Remembering... 1686
            Story.KillQuest(1686, "bludrut", "Rattlebones");

            //A Personal Effect 1687
            Story.KillQuest(1687, "bludrut2", "Shadow Creeper");

            //The Unintentional Tomb 1688
            Story.MapItemQuest(1688, "bludrut2", 892);

            //The Spirit Speaks 1689
            if (!Story.QuestProgression(1689))
            {
                Core.EnsureAccept(1689);
                Core.HuntMonster("bludrut2", "Shadow Creeper", "Shadow Creeper Tallow", 5);
                Core.HuntMonster("bludrut", "Rattlebones", "Rattlebones' Bones", 5);
                Core.HuntMonster("bludrut", "Rock Elemental", "Rock Elemental Spark");
                Core.EnsureComplete(1689);
            }

            //Unseen Monsters 1690
            Story.MapItemQuest(1690, "bludrut3", 894);

            //Stop IT! 1691
            Story.KillQuest(1691, "bludrut3", "IT");

            //Sad Farewells 1692
            Story.MapItemQuest(1692, "bludrut2", 893);

            //Ectoamber 1693
            Story.KillQuest(1693, "bludrut", "Rattlebones");
        }

    }
}
