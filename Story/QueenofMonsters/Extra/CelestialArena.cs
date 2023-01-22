/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs

using Skua.Core.Interfaces;

public class CelestialArenaQuests
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        Adv.GearStore();
        Arena1to10();
        Arena11to20();
        Arena21to29();
        Adv.GearStore(true);

    }

    public void Arena1to10()
    {
        if (Core.isCompletedBefore(6022))
            return;
        Adv.BestGear(GearBoost.dmgAll);
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6013, "CelestialArenaB", "Slork Construct");
        Story.KillQuest(6014, "CelestialArenaB", "Azkorath Construct");
        Story.KillQuest(6015, "CelestialArenaB", "Blessed Inquisitor");
        Story.KillQuest(6016, "CelestialArenaB", "Lich Ravager Construct");
        Story.KillQuest(6017, "CelestialArenaB", "Ring Guardian Construct");
        Story.KillQuest(6018, "CelestialArenaB", "Serepthys Construct");
        Story.KillQuest(6019, "CelestialArenaB", "Yaomo Construct");
        Story.KillQuest(6020, "CelestialArenaB", "Cerberus Construct");
        Story.KillQuest(6021, "CelestialArenaB", "Infernal Warrior Construct");
        Story.KillQuest(6022, "CelestialArenaB", "Infernal Warlord Construct");
    }
    public void Arena11to20()
    {
        if (Core.isCompletedBefore(6032))
            return;

        Adv.BestGear(GearBoost.dmgAll);
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6023, "CelestialArenaC", "Conquest Construct");
        Story.KillQuest(6024, "CelestialArenaC", "War Construct");
        Story.KillQuest(6025, "CelestialArenaC", "Death Construct");
        Story.KillQuest(6026, "CelestialArenaC", "Famine Construct");
        Story.KillQuest(6027, "CelestialArenaC", "Diabolical Warlord Construct");
        Story.KillQuest(6028, "CelestialArenaC", "Undead Raxgore Construct");
        Story.KillQuest(6029, "CelestialArenaC", "Blessed Karok");
        Story.KillQuest(6030, "CelestialArenaC", "Kezeroth Construct");
        Story.KillQuest(6031, "CelestialArenaC", "Shadow Lord Construct");
        Story.KillQuest(6032, "CelestialArenaC", "Desolich Construct");
    }
    public void Arena21to29()
    {
        if (Core.isCompletedBefore(6042))
            return;

        Adv.BestGear(GearBoost.dmgAll);
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6033, "CelestialArenaD", "Queen of Hope");
        Story.KillQuest(6034, "CelestialArenaD", "Malxas Construct");
        Story.KillQuest(6035, "CelestialArenaD", "Blessed Gladius");
        Story.KillQuest(6036, "CelestialArenaD", "High Celestial Priest");
        Story.KillQuest(6037, "CelestialArenaD", "Blessed Enfield");
        Story.KillQuest(6038, "CelestialArenaD", "Avatar of Spirits");
        Story.KillQuest(6039, "CelestialArenaD", "Avatar of Time");
        Story.KillQuest(6040, "CelestialArenaD", "Avatar of Life");
        Story.KillQuest(6041, "CelestialArenaD", "Fallen Abezeth");
        Story.KillQuest(6042, "CelestialArenaD", "Aranx");
    }
}
