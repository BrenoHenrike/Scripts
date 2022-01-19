//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs

//cs_include Scripts/CoreFile(Or folder)/Filename.cs

using RBot;

public class Tutorial
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Badges();

        Core.SetOptions(false);
    }

    public void Badges()
    {
        Core.Join(map: "oaklore");

        Core.Logger("Achievement - Combat");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%22%1%");//combat-
        Bot.Sleep(700);
        Core.Logger("Achievement - Interact");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%23%1%");//interact- 
        Bot.Sleep(700);
        Core.Logger("Achievement - Quest");
        Core.KillQuest(QuestID: 4007, MapName: "oaklore", MonsterName: "Bone Berserker", hasFollowup: false);
        Core.SendPackets("xt%zm%setAchievement%93430%ia0%24%1%");//quest- 
        Bot.Sleep(700);
        Core.Logger("Achievement - Skill");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%25%1%");//skill- 
        Bot.Sleep(700);
        Farm.Gold(5000);
        Core.BuyItem(map: "oaklore", shopID: 1072, itemName: "Brutal Battle Blade", shopItemID: 27956);
        Core.Logger("Achievement - Shop");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%26%1%");//shop- 
        Bot.Sleep(700);
        Farm.Experience(2);
        Core.SendPackets("%xt%zm%enhanceItemShop%93430%27956%1857%1073%"); //enhance do
        Bot.Sleep(700);
        Core.Logger("Achievement - Enhance");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%27%1%"); //enhance2
        Bot.Sleep(700);
        Core.Logger("Achievement - Rest");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%28%1%");//heal- 
        Bot.Sleep(700);
        Core.Logger("Achievement - World");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%29%1%");//world 
        Bot.Sleep(700);
        Core.Logger("Achievement - Emotions are bad");
        Core.SendPackets("%xt%zm%emotea%1%dance%"); //emote do
        Bot.Sleep(700);
        Core.Logger("Achievement - Why do we have these");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%30%1%");//emote2
        Bot.Sleep(700);
        Core.Logger("Achievement - Going to Paris");
        Core.SendPackets("%xt%zm%setAchievement%93430%ia0%31%1%");//travel
        Bot.Sleep(2500);
    }
}