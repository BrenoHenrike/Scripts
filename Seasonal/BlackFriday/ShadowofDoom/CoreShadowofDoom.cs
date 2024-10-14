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
    public string[] UMLotusTomb { get; private set; }

    public CoreShadowofDoom()
    {
        UMLotusTomb = new[]
        {
            "Doomed Elf", // UMLotusTomb[0],
            "Umbral Armor", // UMLotusTomb[1],
            "Umbral Serpent", // UMLotusTomb[2],
            "Umbral Tomb Hound", // UMLotusTomb[3],
            "Apophis Chantress", // UMLotusTomb[4]
        };
    }
    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void DoAll(bool ReturnEarly = false)
    {
        ShadowBattleon();
        Camlan(ReturnEarly);
        LotusTomb();
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

    public void Camlan(bool ReturnEarly = false)
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
        Story.MapItemQuest(9437, "camlan", new[] { 12252, 12253, 12254 });

        // Shadows of Aminion 9438
        Story.KillQuest(9438, "camlan", "Doomed Elf");

        // Come to Light 9439
        Story.MapItemQuest(9439, "camlan", new[] { 12255, 12256 });
        Story.KillQuest(9439, "camlan", "Ouro Spawn");

        // It's in the Blood 9440
        Story.KillQuest(9440, "camlan", "Doomed Elf");

        // Parental Pressure 9441
        if (!Story.QuestProgression(9441))
        {
            Core.EnsureAccept(9441);
            Core.HuntMonster("camlan", "Sleih", "Cracked Light Crystal");
            Core.HuntMonster("camlan", "Bellona", "Crushed Light Pendant");
            Core.EnsureComplete(9441);
        }

        // Get Lost in Me 9442
        Core.Logger("Good luck with this \"ultra\"!");
        Story.KillQuest(9442, "camlan", "Metamorphosis Maw");

        if (ReturnEarly)
            return;

        // Cocooned Gold 9443
        if (!Story.QuestProgression(9443))
        {
            Core.EnsureAccept(9443);
            Core.HuntMonster("camlan", "Sleih", "Sleih's Changeling Records");
            Core.HuntMonster("camlan", "Bellona", "Bellona's Edict of War");
            Core.HuntMonster("camlan", "Metamorphosis Maw", "Alchemic Snake Scale");
            Core.EnsureComplete(9443);
        }
    }

    public void LotusTomb()
    {


        if (Core.isCompletedBefore(9920))
            return;

        Camlan(true);

        Story.PreLoad(this);

        // 9912 | A Herd of Black Sheep
        if (!Story.QuestProgression(9912))
        {
            Core.HuntMonsterQuest(9912,
("lotustomb", UMLotusTomb[0], ClassType.Solo)
);
        }


        // 9913 | Apep's Minders
        Story.MapItemQuest(9913, "lotustomb", 13731);
        Story.KillQuest(9913, "lotustomb", UMLotusTomb[1]);


        // 9914 | Primeval Discord
        if (!Story.QuestProgression(9914))
        {
            Core.HuntMonsterQuest(9914,
("lotustomb", UMLotusTomb[1], ClassType.Solo),
        ("lotustomb", UMLotusTomb[0], ClassType.Solo)
);
        }


        // 9915 | Hissing Hatchlings
        if (!Story.QuestProgression(9915))
        {
            Core.HuntMonsterQuest(9915,
("lotustomb", UMLotusTomb[2], ClassType.Farm)
);
        }


        // 9916 | How the Mighty Have Fallen
        Story.MapItemQuest(9916, "lotustomb", new[] { 13732, 13733 });


        // 9917 | Dog Water
        if (!Story.QuestProgression(9917))
        {
            Core.HuntMonsterQuest(9917,
("lotustomb", UMLotusTomb[3], ClassType.Farm)
);
        }


        // 9918 | Poor Costume Choice
        Story.MapItemQuest(9918, new[] { (13734, 1, "lotustomb"), (13735, 6, "lotustomb") });


        // 9919 | Chaotic Little Joys
        if (!Story.QuestProgression(9919))
        {
            Core.HuntMonsterQuest(9919,
("lotustomb", UMLotusTomb[2], ClassType.Farm),
        ("lotustomb", UMLotusTomb[3], ClassType.Farm)
);
        }


        // 9920 | Rite of Apophis
        if (!Story.QuestProgression(9920))
        {
            Core.HuntMonsterQuest(9920,
("lotustomb", UMLotusTomb[4], ClassType.Solo)
);
        }


    }
}






