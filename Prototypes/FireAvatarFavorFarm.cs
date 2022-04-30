//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class FireAvatarFavorFarm
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
        
        FAFavor();

        Core.SetOptions(false);
    }
    public void FAFavor()
    {
        if (Core.CheckInventory("Fire Avatar's Favor", 300))
            return;

        Core.AddDrop("Fire Avatar's Favor");
        Core.EquipClass(ClassType.Farm);
        while (!Bot.ShouldExit() || !Core.CheckInventory("Fire Avatar's Favor", 300))
        {
            Core.EnsureAccept(8244);

            Core.KillMonster("fireavatar", "r4", "Right", "*", "Onslaught Defeated", 6);
            Core.KillMonster("fireavatar", "r6", "Left", "*", "Elemental Defeated", 6);

            Core.EnsureComplete(8244);
            Bot.Wait.ForPickup("Fire Avatar's Favor");
        }
    
        
    }
}
