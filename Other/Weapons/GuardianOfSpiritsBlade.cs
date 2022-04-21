//cs_include Scripts/CoreBots.cs
using RBot;
public class GuardianOfSpiritsBlade
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetGoSB();

        Core.SetOptions(false);
    }

    public void GetGoSB()
    {
        if (Core.CheckInventory("Guardian of Spirits' Blade"))
            return;

        Core.AddDrop("Guardian of Spirits' Blade");
        Core.EnsureAccept(4510);
        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("celestialrealm", "Fallen Knight", "Trapped Spirits", 500, false);
        Core.EnsureComplete(4510);
        Bot.Wait.ForPickup("Guardian of Spirits' Blade");
    }
}