/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class MustyCave
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
        if (Core.isCompletedBefore(7049))
            return;

        Story.PreLoad(this);

        //Killer Robots! (7034)
        Story.KillQuest(7034, "redfurvalley", "Guard Drone|Harvester Drone");
        //Full Bellies (7035)
        Story.KillQuest(7035, "redfurvalley", "Harvester Drone");
        //They're Waaatching (7036)
        Story.KillQuest(7036, "redfurvalley", "Spy Drone");
        //Gotta Keep Hydrated (7037)
        Story.BuyQuest(7037, "nibbleon", 1553, "Makai-Sun");
        //Find Twilly (7038)
        Story.MapItemQuest(7038, "mustycave", 6588);
        //Free Twilly! (7039)
        Story.KillQuest(7039, "mustycave", "Guard Drone");
        Story.MapItemQuest(7039, "mustycave", 6589);
        //Free the Moglins (7040)
        Story.MapItemQuest(7040, "mustycave", 6590, 6);
        //Essence-ial (7041)
        Story.KillQuest(7041, "mustycave", "Harvester Drone");
        //Drones are a Drag (7042)
        Story.KillQuest(7042, "mustycave", "Spy Drone");
        //Souls for Controls (7043)
        Story.KillQuest(7043, "nibbleon", "Void Mage");
        //Investigation Time (7044)
        Story.MapItemQuest(7044, "mustycave", new[] { 6591, 6592, 6593, 6594 });
        //Get Revenge (7045)
        Story.KillQuest(7045, "mustycave", "Guard Drone|Harvester Drone");
        //Key the Guards (7046)
        Story.KillQuest(7046, "mustycave", "Guard Drone");
        //Time to Teleport (7047)
        Story.MapItemQuest(7047, "mustycave", 6595);
        //GET HIM! (7048)
        Story.KillQuest(7048, "mustycave", "Mogdring");
        //Doom Regin Doom's Reward (7049)
        if (!Story.QuestProgression(7049))
        {
            Core.EnsureAccept(7049);
            Core.HuntMonster("mustycave", "Mogdring", "Golden Gear", 5, false);
            Core.HuntMonster("mustycave", "Spy Drone", "Aura Core", 25, false);
            Core.HuntMonster("mustycave", "Guard Drone", "Dimension Stabilizer", 35, false);
            Core.EnsureComplete(7049);
        }
    }
}
