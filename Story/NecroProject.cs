/*
name: Necro Project
description: This will finish the Necro Project story.
tags: story, quest, necroproject, sally 
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class NecroProject
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
        if (Core.isCompletedBefore(9901))
            return;

        Story.PreLoad(this);

        #region Useable Monsters
        string[] UseableMonsters = new[]
        {
    "Noxborg", // UseableMonsters[0],
	"Noxore", // UseableMonsters[1],
	"Noxustrian", // UseableMonsters[2],
	"Imperial Noxus", // UseableMonsters[3]
};
        #endregion Useable Monsters

        // 9897 | In-Field Study
        if (!Story.QuestProgression(9897))
        {
            Core.HuntMonsterQuest(9897,
("necrocavern", "Shadowstone Elemental", ClassType.Farm)
);
        }


        // 9898 | Technilliterate
        if (!Story.QuestProgression(9898))
        {
            Core.HuntMonsterQuest(9898,
("necroproject", UseableMonsters[0], ClassType.Solo)
);
        }


        // 9899 | Odd Tastes
        if (!Story.QuestProgression(9899))
        {
            Core.HuntMonsterQuest(9899,
("necroproject", UseableMonsters[1], ClassType.Solo)
);
        }


        // 9900 | My Toxic Pony
        if (!Story.QuestProgression(9900))
        {
            Core.HuntMonsterQuest(9900,
("necroproject", UseableMonsters[2], ClassType.Solo)
);
        }


        // 9901 | Hand-Me-Down
        if (!Story.QuestProgression(9901))
        {
            Core.HuntMonsterQuest(9901,
("necroproject", UseableMonsters[3], ClassType.Solo)
);
        }


    }
}
