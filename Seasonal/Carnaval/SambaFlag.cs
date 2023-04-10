/*
name: SambasFlag Story
description: This will finish the SambasFlag Story.
tags: story, quest, sambasflag
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class SambasFlag
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(9115))
            return;

        if (!Core.isSeasonalMapActive("Sambaflag"))
            return;

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // Bald Spots 9110
        Story.KillQuest(9110, "bloodtusk", new[] { "Jungle Vulture", "Rhison" });

        // Work Out Glow 9111
        Story.KillQuest(9111, "dwarfhold", new[] { "Gemrald", "Glow Worm" });

        // Burned Forevermore 9112
        Story.KillQuest(9112, "fotia", new[] { "Fotia Spirit", "Femme Cult Worshiper" });

        // Calm Sea and Prosperous Voyage 9113
        Story.KillQuest(9113, "shipwreck", new[] { "Gilded Merdraconian", "Gilded Crystal Undead" });

        // Chaos Cleanse 9114
        Story.KillQuest(9114, "falguard", new[] { "Chaonslaught Caster", "Chaonslaught Cavalry" });

        // Fly the Standard 9115
        Story.KillQuest(9115, "sambaflag", "Flag Bearer");
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(9115, "sambaflag", "Master Of Ceremonies");
    }
}
