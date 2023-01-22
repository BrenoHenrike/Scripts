/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Yokai.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class DragonShinobi
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new CoreAdvanced();
    public YokaiQuests Yokai = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        GetDSS();

        Core.SetOptions(false);
    }

    public void GetDSS(bool rankUpClass = true)
    {
        if (Core.CheckInventory(59476))
        {
            Adv.rankUpClass("DragonSoul Shinobi");
            return;
        }

        Yokai.Quests();
        Core.AddDrop("Dragon Shinobi Token");

        Core.EquipClass(ClassType.Solo);
        Core.RegisterQuests(7924);
        while (!Bot.ShouldExit && !Core.CheckInventory("Dragon Shinobi Token", 300))
            Core.HuntMonster("shadowfortress", "1st Head Of Orochi", "Perfect Orochi Scales", 10, isTemp: false);
        Core.CancelRegisteredQuests();

        Core.BuyItem("shadowfortress", 1968, 59476);
        if (rankUpClass)
        {
            Adv.GearStore();
            Core.Equip("DragonSoul Shinobi");
            Adv.rankUpClass("DragonSoul Shinobi");
            Adv.GearStore(true);
        }
    }
}
