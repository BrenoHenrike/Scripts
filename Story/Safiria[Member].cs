/*
name: Safiria Story (Member)
description: This will finish the Safiria Story.
tags: story, quest, safiria, member
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class Safiria
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
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
        if (!Story.QuestProgression(1943))
        {
            if (Core.CheckInventory("Legion Revenant"))
                Core.Equip("Legion Revenant");
            else Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(1943);
            Adv.KillUltra("djinn", "r6", "Up", "Ultra-Tibicenas", "Djinn's Magic Trace", 5, isTemp: false);
            Core.EnsureComplete(1943);
        }

        //The Second Phylactery 1944
        if (!Story.QuestProgression(1944))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(1944);
            Core.HuntMonster("mqlesson", "Dragonoid", "Dragonoid of Hours", isTemp: false);
            Adv.KillUltra("ultravoid", "Frame2", "Left", "Ultra Iadoa", "Chronomancer's Magic Trace", 5, isTemp: false);
            Core.EnsureComplete(1944);
        }

        //The Third Phylactery 1945
        if (!Story.QuestProgression(1945))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(1945);
            Adv.KillUltra("ultralionfang", "Enter", "Spanw", "Ultra Lionfang", "Darkblood's Magic Trace", 5, isTemp: false);
            Core.EnsureComplete(1945);
        }

        //The Final Phylactery 1946
        if (!Story.QuestProgression(1946))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(1946);
            Adv.KillUltra("ancienttrigoras", "r2a", "Spawn", "Ancient Trigoras", "Dragon's Magic Trace", 5, isTemp: false);
            Core.EnsureComplete(1946);
        }

        //Ancient Vitae 1947
        Story.KillQuest(1947, "battledoom", "Shadow Safiria");
    }
}
