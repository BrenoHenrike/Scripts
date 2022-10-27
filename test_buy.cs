//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreBots2.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class TestBuy
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Example();

        Core.SetOptions(false);
    }

    public void Example()
    {
        Core.HuntMonster("mobius", "Slugfit", "Mystic Quills", 20, false);
        Core.BuyItemTest("dragonrune", 549, "Ember Ink", 50, 0, true);
        Core.Logger($"Before: {Core.CheckInventory("Ember Ink", 50)}");
        Core.Relogin();
        Core.Logger($"After: {Core.CheckInventory("Ember Ink", 50)}");

        Core.BuyItemTest("dragonrune", 549, "Ember Ink", 50, 0, false);
        Core.Logger($"Before2: {Core.CheckInventory("Ember Ink", 50)}");
        Core.Relogin();
        Core.Logger($"After2: {Core.CheckInventory("Ember Ink", 50)}");
    }
}
