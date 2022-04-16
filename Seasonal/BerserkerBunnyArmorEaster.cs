//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class BerserkerBunnyArmorEaster
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetBBA();

        Core.SetOptions(false);
    }

    public void GetBBA()
    {
        if (Core.CheckInventory("Berserker Bunny Armor"))
            return;

        if (!Core.CheckInventory("Berserker Bunny Armor"))
        {
            Core.EnsureAccept(236);
            Core.HuntMonster("greenguardwest", "Big Bad Boar", "Were Egg", log: false);
            Core.EnsureComplete(236);
            Bot.Wait.ForPickup("Berserker Bunny Armor");
        }
    }
}



