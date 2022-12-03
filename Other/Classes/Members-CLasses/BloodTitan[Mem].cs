//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class BloodTitan
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Getclass();

        Core.SetOptions(false);
    }

    public void Getclass(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Blood Titan") || !Core.IsMember)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(2908);
        while (!Bot.ShouldExit && !Core.CheckInventory("Blood Titan Token", 425))
            Core.HuntMonster("dwarfhold", "Albino Bat", "Bat Wing", 3);
        Core.BuyItem("classhalla", 617, "Blood Titan");

        if (rankUpClass)
            Adv.rankUpClass("Blood Titan");
    }
}