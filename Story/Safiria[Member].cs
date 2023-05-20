/*
name: Safiria Story (Member)
description: This will finish the Safiria Story.
tags: story, quest, safiria, member
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Safiria
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
        if (Core.isCompletedBefore(1947))
            return;

        if (!Core.IsMember)
        {
            Core.Logger("Safiria Storyline Is Member Only. Skipping this Script");
            return;
        }

        Story.PreLoad(this);
        Core.AddDrop(new[] { "Djinn's Magic Trace", "Chronomancer's Magic Trace", "Darkblood's Magic Trace", "Dragon's Magic Trace", "Safiria's Blood Sample" });


        //The Stolen Ritual 1939
        Story.KillQuest(1939, "Safiria", "Chaos Lycan");

        //A Pound Of Flesh 1940
        Story.KillQuest(1940, "Safiria", "Blood Maggot");

        //Blood Of The Ancients 1941
        Story.KillQuest(1941, "Safiria", "Chaos Lycan");

        //Phinding Phylacteries 1942
        Story.MapItemQuest(1942, "Safiria", 962, 4);

        //The First Phylactery 1943
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(1943, "djinn", "Ultra-Tibicenas");

        //The Second Phylactery 1944
        Story.KillQuest(1944, "ultravoid", "Ultra Iadoa");

        //The Third Phylactery 1945
        Story.KillQuest(1945, "ultralionfang", "Ultra Lionfang");

        //The Final Phylactery 1946
        Story.KillQuest(1946, "ancienttrigoras", "Ancient Trigoras");

        //Ancient Vitae 1947
        Story.KillQuest(1947, "battledoom", "Shadow Safiria");
    }
}
