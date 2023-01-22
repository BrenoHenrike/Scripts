/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class BloodMoon
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

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

        Core.EquipClass(ClassType.Farm);

        //Get Out! 6048
        Story.MapItemQuest(6048, "bloodmoon", 5451);
        Story.KillQuest(6048, "bloodmoon", "Darkness");


        //The Court of The King 6049
        if (!Story.QuestProgression(6049))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(6049, "bloodmoon", "Constantin");
            Core.EquipClass(ClassType.Farm);
        }

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
        if (!Story.QuestProgression(6057))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(6057, "bloodmoon", "Black Unicorn");
        }

        //Killer Kitty 6058 
        if (!Story.QuestProgression(6058))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(6058, "bloodmoon", "Bubble");
        }
    }

    public void Maxius()
    {
        Core.EquipClass(ClassType.Farm);

        //Ghoul, Ghoul, Ghoul 6063
        Story.KillQuest(6063, "maxius", "Ghoul Minion");

        //Get Him! 6064
        if (!Story.QuestProgression(6064))
        {
            Bot.Events.CellChanged += CutSceneFixer;
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(6064);
            Core.KillMonster("maxius", "r3", "Left", "Count Maxius", "Count Maxius Defeated");
            Core.EnsureComplete(6064);
        }

        //Minions Everywhere 6065
        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(6065, "maxius", "Vampire Minion");

        //Get Barnabus! 6066
        if (!Story.QuestProgression(6066))
        {
            Bot.Events.CellChanged += CutSceneFixer;
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(6066);
            Core.KillMonster("maxius", "r3", "Left", "Barnabus", "Barnabus Defeated");
            Core.EnsureComplete(6066);
        }

        //An End To This Threat 6067
        if (!Story.QuestProgression(6067))
        {
            Bot.Events.CellChanged += CutSceneFixer;
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(6067);
            Core.KillMonster("maxius", "r6", "Left","Count Maxius", "Count Maxius Slain");
            Core.EnsureComplete(6067);
        }
    }


    private void CutSceneFixer(string map, string cell, string pad)
    {

        // /if more maps get stuck, just fillin the bit below.
        if (map == "maxius")
        {
            if (!Core.isCompletedBefore(6064))
            {
                while (!Bot.ShouldExit && Bot.Player.Cell != "r3")
                {
                    Bot.Sleep(2500);
                    Core.Jump("r3");
                    Bot.Sleep(2500);
                }
            }
            else if (!Core.isCompletedBefore(6066))
                while (!Bot.ShouldExit && Bot.Player.Cell != "r5")
                {
                    Bot.Sleep(2500);
                    Core.Jump("r5");
                    Bot.Sleep(2500);
                }
            else if (!Core.isCompletedBefore(6067))
                while (!Bot.ShouldExit && Bot.Player.Cell != "r6")
                {
                    Bot.Sleep(2500);
                    Core.Jump("r6");
                    Bot.Sleep(2500);
                }
        }
        Bot.Events.CellChanged -= CutSceneFixer;
    }
}
