//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class NorthlandsMonk
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        GetNlMonk();

        Core.SetOptions(false);
    }

    public void GetNlMonk(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Northlands Monk"))
        {
            Adv.rankUpClass("Northlands Monk");
            return;
        }

        Core.AddDrop("Northlands Monk");

        Core.EquipClass(ClassType.Solo);
        while (!Bot.ShouldExit && !Core.CheckInventory("DNorthlands Monk"))
            Core.HuntMonster("frozensoul", "FrozenSoul Queen", "PeNorthlands Monk", isTemp: false);

        if (rankUpClass)
        {
            Adv.GearStore();
            Core.Equip("Northlands Monk");
            Adv.rankUpClass("Northlands Monk");
            Adv.GearStore(true);
        }
    }
}
