/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ShadowBattleon
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Shadowbattleon();
        Core.SetOptions(false);
    }

    public void Shadowbattleon()
    {
        if (Core.isCompletedBefore(9427))
            return;

        Story.PreLoad(this);

        //Shadow Hunt Medal (9421)
        Story.KillQuest(9421, "shadowbattleon", "Doomed Troll");

        // Mega Shadow Hunt Medal (9422)
        Story.KillQuest(9422, "shadowbattleon", "Doomed Troll");

        // Early Autopsy (9423)
        Story.KillQuest(9423, "shadowbattleon", "Doomed Troll");

        // Given Life and Purpose (9424)
        Story.KillQuest(9424, "shadowbattleon", "Possessed Armor");

        // Adult Hatching (9425)
        Story.KillQuest(9425, "shadowbattleon", "Ouro Spawn");

        // Solidified Light (9426)
        Story.KillQuest(9426, "shadowbattleon", "Tainted Wraith");


        if (!Story.QuestProgression(9427))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(9427);
            //Enigmatic Entity (9427)
            Core.HuntMonster("shadowbattleon", "Mysterious Stranger", "Stranger Defeated");
            Core.EnsureComplete(9427);
        }
    }
}
