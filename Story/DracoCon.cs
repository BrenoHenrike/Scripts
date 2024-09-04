/*
name: Draco Con Story
description: This will finish the Draco Con story.
tags: story, quest, dracocon, draco, con
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DracoCon
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
        if (Core.isCompletedBefore(5372))
            return;

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // Zorbak's Legion: Chapter 1 (5357)
        Story.KillQuest(5357, "dracocon", new[] { "Zombie Tog", "Zombie Dravir" });

        // Zorbak's Legion: Eggstreme Plan (5358)
        Story.KillQuest(5358, "dracocon", "Zombie Dravir");

        // Zorbak's Legion: Drac Attack (5359)
        Story.KillQuest(5359, "dracocon", new[] { "Angry Wisp", "Red Spirit" });

        // Zorbak's Legion: Undead vs Undead (5360)
        Story.KillQuest(5360, "dracocon", new[] { "Undead Soldier", "Zombie Warrior" });

        // Zorbak's Legion: Tiny Terrors (5361)
        Story.MapItemQuest(5361, "dracocon", 4723, 5);
        Story.KillQuest(5361, "dracocon", "Villager");

        // Eggcelent Hunt (5362)
        Story.MapItemQuest(5362, "dracocon", 4724, 13);

        // "Fresh" Dragon Chow (5363)
        Story.KillQuest(5363, "dracocon", "Zombie Warrior");

        // Bling, Baby, Bling (5364)
        Story.MapItemQuest(5364, "dracocon", 4725, 5);
        Story.KillQuest(5364, "dracocon", "Battle Gem");

        // Class 101: Slaying for Dummies (5365)
        Story.KillQuest(5365, "dracocon", "Dragon Training Dummy");

        // Class 102: Breath of Fresh Fire (5366)
        Story.KillQuest(5366, "dracocon", "Angry Wisp");

        // Class 103: Tog Eat Tog World (5367)
        Story.KillQuest(5367, "dracocon", "Zombie Tog");

        // Final Exam Demonstration (5368)
        if (!Story.QuestProgression(5368))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(5368, "dracocon", "Strong Drag");
            Core.EquipClass(ClassType.Farm);
        }

        // Smack The Sheep! (5369)
        Story.MapItemQuest(5369, "dracocon", 4726);

        // Hear No Evil (5370)
        Story.MapItemQuest(5370, "dracocon", 4727, 4);

        // Safe and (No) Sound (5371)
        Story.MapItemQuest(5371, "dracocon", 4728, 6);

        // Through the Fire and Bones (5372)
        if (!Story.QuestProgression(5372))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(5372, "dracocon", new[] { "Drummer", "Guitarist", "Keyboardist", "Singer" });
            Core.EquipClass(ClassType.Farm);
        }
    }
}


