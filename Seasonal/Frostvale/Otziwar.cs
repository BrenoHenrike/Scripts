/*
name: Otziwar
description: Does the quests in /join otziwar
tags: otziwar, story, huntress merge
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Otziwar
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        OtziwarStory();

        Core.SetOptions(false);
    }

    public void OtziwarStory()
    {
        if (Core.isCompletedBefore(8451))
            return;

        Story.PreLoad(this);

        // Sluagh Medals 8446 (8447 is mega and not neede)
        if (!Story.QuestProgression(0000))
        {
            Core.EnsureAccept(8446);
            Core.HuntMonster("otziwar", "Sluagh Warrior", "Ancient Fragments", 3);
            Core.EnsureComplete(8446);
        }

        // Glacial Archaeology 8448
        Story.KillQuest(8448, "otziwar", "Sluagh Warrior");

        // Calcium Dating 8449
        Story.KillQuest(8449, "otziwar", "Gauden Hound");
        
        // Circling Crows 8450
        Story.KillQuest(8450, "otziwar", "Sluagh Mellori");

        
        // Powder Snow 8451         
        Story.KillQuest(8451, "otziwar", "Huntress Valais");
    }
}
