/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CastleTunnels
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
        if (Core.isCompletedBefore(5255))
            return;

        Story.PreLoad(this);

        //Explore the Tunnels 5244
        Story.MapItemQuest(5244, "CastleTunnels", 4597);
        Story.KillQuest(5244, "CastleTunnels", new[] { "Vampire Bat", "Blood Maggot" });

        //Find the Keys 5245
        Story.MapItemQuest(5245, "CastleTunnels", 4598, 4);

        //Open the Treasure Chests 5246
        Story.MapItemQuest(5246, "CastleTunnels", 4599, 4);

        //Gotta get that Blood 5247
        Story.KillQuest(5247, "CastleTunnels", "Blood Maggot|Vampire Bat");

        //Feed the Shrine 5248
        Story.MapItemQuest(5248, "CastleTunnels", 4600);

        //Beat those Ghouls 5249
        Story.KillQuest(5249, "CastleTunnels", new[] { "Vampire Ghoul", "Vampire Ghoul" });

        //Activate the Golden Scepter 5250
        Story.MapItemQuest(5250, "CastleTunnels", 4601);

        //Check out the Basement 5251
        Story.MapItemQuest(5251, "CastleTunnels", new[] { 4602, 4603 });

        //Get those Symbiotes 5252
        Story.KillQuest(5252, "CastleTunnels", new[] { "Blood Symbiote", "Blood Symbiote" });

        //Activate the Basement Scepter 5253
        Story.MapItemQuest(5253, "CastleTunnels", 4604);

        //Unlock that Chest! 5254
        Story.MapItemQuest(5254, "CastleTunnels", 4605);

        //Defeat the Blood Dragon! 5255
        Story.KillQuest(5255, "CastleTunnels", "Blood Dragon");

    }
}
