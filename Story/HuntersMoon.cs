/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class HuntersMoon
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
        if (Core.isCompletedBefore(6810))
            return;

        Story.PreLoad(this);

        #region Marchosias Fight
        // Curse of the Moon 6699
        Story.KillQuest(6699, "marchosiasfight", "Marchosias");
        #endregion

        #region Hunter's Moon
        // Put out the Fire 6700
        Story.KillQuest(6700, "huntersmoon", "Dark Fire");

        // The Most Dire of Beasts 6701
        Story.KillQuest(6701, "huntersmoon", "Eclipsed One");

        // Find the Elder 6702
        Story.MapItemQuest(6702, "huntersmoon", 6195);

        // Send them Home 6703
        Story.MapItemQuest(6703, "huntersmoon", 6194, 6);

        // Who are the Blood Moon Clan? 6704
        Story.KillQuest(6704, "huntersmoon", "Blood Moon Hooligan");

        // Light the Torches 6705
        Story.KillQuest(6705, "huntersmoon", "Dark Fire");

        // Gather the Blood 6706
        Story.KillQuest(6706, "huntersmoon", "Eclipsed One");

        // Find the Skull! 6707
        Story.KillQuest(6707, "huntersmoon", "Marchiosas Acolyte");

        // Tree of Wisdom 6708
        Story.MapItemQuest(6708, "huntersmoon", 6193);

        // Shards of Moonlight 6709
        Story.MapItemQuest(6709, "huntersmoon", 6192, 6);

        // Moon-Struck 6710
        Story.KillQuest(6710, "huntersmoon", "Eclipsed One");

        // Offer the Skull 6711
        Story.MapItemQuest(6711, "huntersmoon", 6191);

        // The Fighting Spirit 6712
        Story.KillQuest(6712, "huntersmoon", "Spirit of Voland");
        #endregion

        #region Moon Cursed Lair
        // Moon Crystals 6799
        Story.KillQuest(6799, "mooncursedlair", "Shard of Moonlight");

        // Eclipsed Blood 6800
        Story.KillQuest(6800, "mooncursedlair", "Eclipsed One");

        // Heart of the Bear 6801
        Story.KillQuest(6801, "mooncursedlair", "Tainted Grizzly");

        // Forge of the Moon 6802
        Story.MapItemQuest(6802, "mooncursedlair", 6325);

        // Bear-ly Alive 6803
        Story.KillQuest(6803, "mooncursedlair", "Corrupted Bear");

        // Cleanse the Filth 6804
        Story.KillQuest(6804, "mooncursedlair", "Corrupted Sludge");

        // Search the Cave 6805
        Story.MapItemQuest(6805, "mooncursedlair", 6326);

        // Clear the Eclipse 6806
        Story.KillQuest(6806, "mooncursedlair", "Eclipsed One");

        // Interrogation 6807
        Story.KillQuest(6807, "mooncursedlair", "Blood Moon Minion");

        // Search for Clues 6808
        Story.KillQuest(6808, "mooncursedlair", "Matted Yuck");

        // Check the Nest 6809
        Story.MapItemQuest(6809, "mooncursedlair", 6327);

        // Defeat Marchosias! 6810
        Story.KillQuest(6810, "mooncursedlair", "Marchosias");
        #endregion
    }
}
