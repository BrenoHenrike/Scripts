//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class Tutorial
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreStory Story = new CoreStory();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Badges();

        Core.SetOptions(false);
    }

    public void Badges()
    {
        Core.AddDrop("Venom Head", "Brutal Battle Blade", "Bonehead Bludgeon");

        Core.Join("oaklore");
        Core.Logger("Achievement - Combat");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%22%1%");//combat-
        Core.Logger("Achievement - Interact");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%23%1%");//interact- 
        Core.Logger("Achievement - Quest");
        if (Bot.Player.Level < 10)
        {
            Core.EnsureAccept(4007);
            Core.HuntMonster("oaklore", "Bone Berserker", "Bone Berserker Slain");
            Core.EnsureComplete(4007);
            Bot.Wait.ForPickup("Bonehead Bludgeon");
            Bot.Wait.ForPickup("Venom Head");
            Core.SellItem("Bonehead Bludgeon");
            Core.SellItem("Venom Head");
        }
        Core.SendPackets("xt%zm%setAchievement%93430%ia0%24%1%");//quest- 
        Core.Logger("Achievement - Skill");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%25%1%");//skill- 
        Farm.Gold(5000);
        Core.BuyItem("oaklore", 1072, "Brutal Battle Blade");
        Core.Equip("Brutal Battle Blade");
        if (Core.CheckInventory("Default Dagger"))
            Core.SellItem("Default Dagger");
        if (Core.CheckInventory("Default Staff"))
            Core.SellItem("Default Staff");
        if (Core.CheckInventory("Default Sword"))
            Core.SellItem("Default Sword");
        Adv.EnhanceEquipped(EnhancementType.Lucky);
        Core.Logger("Achievement - Shop");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%26%1%");//shop- 
        Core.SendPackets("%xt%zm%enhanceItemShop%93430%27956%1857%1073%"); //enhance do
        Core.Logger("Achievement - Enhance");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%27%1%"); //enhance2
        Core.Logger("Achievement - Rest");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%28%1%");//heal- 
        Core.Logger("Achievement - World");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%29%1%");//world 
        Core.Logger("Achievement - Emotions are bad");
        Core.SendPackets("%xt%zm%emotea%1%dance%"); //emote do
        Core.Logger("Achievement - Why do we have these");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%30%1%");//emote2
        Core.Logger("Achievement - Going to Paris");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%31%1%");//travel
        Core.Join("party");
    }
}