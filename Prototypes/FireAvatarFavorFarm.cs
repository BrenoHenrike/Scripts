//cs_include Scripts/CoreBots.cs
using RBot;

public class FireAvatarFavorFarm
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions(); 
        
        FAFavor();

        Core.SetOptions(false);
    }
    public void FAFavor(int quant = 300)
    {
        if (Core.CheckInventory("Fire Avatar's Favor", quant))
            return;

        Core.AddDrop("Fire Avatar's Favor");
        Core.RegisterQuests(8244);
        Core.EquipClass(ClassType.Farm);
        while (!Bot.ShouldExit() && !Core.CheckInventory("Fire Avatar's Favor", quant))
        {
            Core.KillMonster("fireavatar", "r4", "Right", "*", "Onslaught Defeated", 6);
            Core.KillMonster("fireavatar", "r6", "Left", "*", "Elemental Defeated", 6);

            Bot.Wait.ForPickup("Fire Avatar's Favor");
        }
    
        
    }
}
