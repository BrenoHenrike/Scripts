//cs_include Scripts/CoreBots.cs
using RBot;
public class BurningBladeOfAzabeth
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetBBoA();

        Core.SetOptions(false);
    }

    public void GetBBoA()
    {
        if (Core.CheckInventory("Burning Blade of Azabeth"))
            return;

        Core.EquipClass(ClassType.Solo);
        if (!Bot.Options.AggroMonsters)
            Bot.Schedule(10000, b => {
                b.Options.AggroMonsters = false;
            });
        Bot.Options.AggroMonsters = true;
        Core.HuntMonster("celestialareand", "Aranx", "Burning Blade of Azabeth", isTemp: false);
        Bot.Wait.ForPickup("Burning Blade of Azabeth");
    }
}