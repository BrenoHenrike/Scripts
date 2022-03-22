//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class DualWield
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailys Dailys = new CoreDailys();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        WeaponReflection();

        Core.SetOptions(false);
    }


    public void WeaponReflection()
    {
        if (Core.CheckInventory("Weapon Reflection"))
            return;

        Core.Logger("Checking if Your Acc is 8 Years Old");

        Core.BuyItem(Bot.Map.Name, 1317, "Golden 8th Birthday Candle");
        if (!Core.CheckInventory("Golden 8th Birthday Candle"))
        {
            Core.Logger("your acc isn't old enough.");
            return;
        }

        Core.AddDrop("Weapon Reflection");
        if (!Core.CheckInventory("Weapon Reflection"))
        {
            Core.EnsureAccept(5518);
            Core.HuntMonster("nostalgiaquest", "Skeletal Viking", "Reflected Glory", 5);
            Core.HuntMonster("nostalgiaquest", "Skeletal Warrior", "Divided Light", 5);
            Core.EnsureComplete(5518);
            Bot.Wait.ForPickup("Weapon Reflection");
        }
    }
}
