/*
name: Blood Moon
description: This will complete the Blood Moon story.
tags: story, quest, blood, moon, maxius
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class BloodMoon
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BloodMoonSaga();

        Core.SetOptions(false);
    }

    public void BloodMoonSaga()
    {
        if (Core.isCompletedBefore(6067))
            return;

        BloodMoonMap();
        Maxius();
    }

    public void BloodMoonMap()
    {
        if (Core.isCompletedBefore(6058))
            return;

        Story.PreLoad(this);

        //Get Out! 6048
        Story.MapItemQuest(6048, "bloodmoon", 5451);
        Story.KillQuest(6048, "bloodmoon", "Darkness");

        //The Court of The King 6049
        Story.KillQuest(6049, "bloodmoon", "Constantin");

        //Hungry like the ... Lycan? 6050
        Story.MapItemQuest(6050, "bloodmoon", 5452);

        //The Sounds of Music? 6051
        Story.MapItemQuest(6051, "bloodmoon", new[] { 5453, 5454, 5455 });

        //Roll the Stones? 6052
        Story.MapItemQuest(6052, "bloodmoon", 5456, 2);

        //Creepy Spooky Monsters 6053
        Story.KillQuest(6053, "bloodmoon", "Spooky Fur");

        //Down the Hole 6054
        Story.MapItemQuest(6054, "bloodmoon", 5457);

        //Beat This 6055
        Story.KillQuest(6055, "bloodmoon", new[] { "Frankentacles", "Spidderbeast" });

        //Insane in the Brain? 6056
        Story.MapItemQuest(6056, "bloodmoon", 5458);

        //No Surprise 6057
        Story.KillQuest(6057, "bloodmoon", "Black Unicorn");

        //Killer Kitty 6058 
        Story.KillQuest(6058, "bloodmoon", "Bubble");
    }

    public void Maxius()
    {
        if (Core.isCompletedBefore(6067))
            return;

        Story.PreLoad(this);

        // //Ghoul, Ghoul, Ghoul 6063
        Story.KillQuest(6063, "maxius", "Ghoul Minion");

        //Get Him! 6064
        if (!Story.QuestProgression(6064))
        {
            // Bot.Events.CellChanged += CutSceneFixer;
            Core.EnsureAccept(6064);
            Core.HuntMonsterMapID("maxius", 6, "Count Maxius Defeated");
            Core.EnsureComplete(6064);
        }

        // //Minions Everywhere 6065
        Story.KillQuest(6065, "maxius", "Vampire Minion");

        //Get Barnabus! 6066
        if (!Story.QuestProgression(6066))
        {
            // Bot.Events.CellChanged += CutSceneFixer;
            Core.EnsureAccept(6066);
            Core.HuntMonster("maxius", "Barnabus", "Barnabus Defeated");
            Core.EnsureComplete(6066);
        }

        //An End To This Threat 6067
        if (!Story.QuestProgression(6067))
        {
            Bot.Wait.ForCellChange("Cut5");
            // Bot.Events.CellChanged += CutSceneFixer;
            Core.EnsureAccept(6067);
            Core.Jump("r6");
            Core.HuntMonsterMapID("maxius", 13, "Count Maxius Slain");
            Core.EnsureComplete(6067);
        }
    }


    private void CutSceneFixer(string map, string cell, string pad)
    {
        // /if more maps get stuck, just fillin the bit below.
        if (map == "maxius")
        {
            while (!Bot.ShouldExit && Bot.Player.Cell == "Cut2")
            {
                Core.Sleep(2500);
                Core.Jump("r3");
                Core.Sleep(2500);
            }

            while (!Bot.ShouldExit && Bot.Player.Cell == "Cut3")
            {
                Core.Sleep(2500);
                Core.Jump("r5");
                Core.Sleep(2500);
            }

            while (!Bot.ShouldExit && Bot.Player.Cell == "Cut5")
            {
                Core.Sleep(2500);
                Core.Jump("r6");
                Core.Sleep(2500);
            }
        }
        Bot.Events.CellChanged -= CutSceneFixer;
    }
}
