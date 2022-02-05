//cs_include Scripts/CoreBots.cs
using RBot;
public class AvatarOfDeathsScythe
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetAoDS();

        Core.SetOptions(false);
    }

    public void GetAoDS()
    {
        if (Core.CheckInventory("Avatar Of Death's Scythe"))
            return;

        Core.AddDrop("Avatar Of Death's Scythe");
        Core.EnsureAccept(4511);
        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("celestialrealm", "Underworld Hound", "Energy of Death", 500, false);
        Core.EnsureComplete(4511);
        Bot.Wait.ForPickup("Avatar Of Death's Scythe");
    }
}