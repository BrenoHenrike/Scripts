/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CoreShadowofDoom
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void DoAll()
    {
        ShadowBattleon();
        Camlan();
    }

    public void ShadowBattleon()
    {
        if (Core.isCompletedBefore(9427))
            return;

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Solo);

        // Mega Shadow Hunt Medal 9422
        Story.KillQuest(9422, "shadowbattleon", "Doomed Beast");

        // Early Autopsy 9423
        Story.KillQuest(9423, "shadowbattleon", "Doomed Beast");

        // Given Life and Purpose 9424
        Story.KillQuest(9424, "shadowbattleon", "Possessed Armor");

        // Adult Hatchling 9425
        Story.KillQuest(9425, "shadowbattleon", "Ouro Spawn");

        // Solidified Light 9426
        Story.KillQuest(9426, "shadowbattleon", "Tainted Wraith");

        //Enigmatic Entity 9427
        Story.KillQuest(9427, "shadowbattleon", "Mysterious Stranger");

    }

    public void Camlan()
    {
        ShadowBattleon();

        if (Core.isCompletedBefore(9443))
            return;

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Solo);

        Core.Logger("Cutscene will play, hunt will resume is a second.");
        // Cold Shells 9433
        Story.KillQuest(9433, "camlan", "Possessed Armor");

        // Equivalent Exchange 9434
        Story.KillQuest(9434, "camlan", "Ouro Spawn");
        Story.MapItemQuest(9434, "camlan", new[] { 12249, 12250 });

        // Mouth of the Snake Den 9435
        Story.KillQuest(9435, "camlan", new[] { "Ouro Spawn", "Possessed Armor" });
        Story.MapItemQuest(9435, "camlan", 12251);

        // Buried Human Pillars 9436
        Story.KillQuest(9436, "camlan", "Tainted Wraith");

        // Guileless Sneers 9437
        Story.MapItemQuest(9437, "camlan", new[] { 12252, 12253 });

        // Shadows of Aminion 9438
        Story.KillQuest(9438, "camlan", "Doomed Elf");

        // Come to Light 9439
        Story.MapItemQuest(9439, "camlan", new[] { 12255, 12256 });
        Story.KillQuest(9439, "camlan", "Ouro Spawn");

        // It's in the Blood 9440
        Story.KillQuest(9440, "camlan", "Doomed Elf");

        // Parental Pressure 9441
        Story.KillQuest(9441, "camlan", new[] { "Bellona", "Sleih" });

        // Get Lost in Me 9442
        Core.Logger("Good luck with this \"ultra\"!");
        Story.KillQuest(9442, "camlan", "Metamorphosis Maw");

        // Cocooned Gold 9443
        Story.KillQuest(9443, "camlan", new[] { "Bellona", "Sleih", "Metamorphosis Maw" });
    }
}






