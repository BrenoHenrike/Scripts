//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class JoinLegion
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public string OptionsStorage = "JoinLegion";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<string>("-", "This bot will spend 1200 AC on", " ", " "),
        new Option<string>("-", "\"Undead Warrior\" to join the Legion", " ", " "),
        new Option<bool>("AcceptBuy", "May the bot do this for you?", " ", false),
        new Option<string>("-", " ", " ", " "),
        new Option<string>("-", "Do you wish for the bot to sell the", " ", " "),
        new Option<string>("-", "\"Undead Warrior\" armor after its done?", " ", " "),
        new Option<string>("-", "This will give you back 1080 AC", " ", " "),
        new Option<string>("-", "of the initial 1200", " ", " "),
        new Option<bool>("AcceptSell", "May the bot do this for you?", " ", false),
        new Option<string>("-", " ", " ", " "),
        new Option<string>("-", " ", " ", " "),
        new Option<string>("-", "Close this window in order to start the bot", " ", " ")
    };
    public bool DontPreconfigure = true;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        JoinLegionQuests();

        Core.SetOptions(false);
    }

    public void JoinLegionQuests()
    {
        if (Bot.Quests.IsUnlocked(3043))
            return;

        Bot.Config.Configure();

        if(!Core.CheckInventory("Undead Warrior"))
        {
            if (!Bot.Config.Get<bool>("AcceptBuy"))
                Core.Logger("You have declined the option to buy \"Undead Warrior\", the bot cannot continue.", messageBox: true, stopBot: true);
            if (Bot.GetGameObject<int>("world.myAvatar.objData.intCoins") < 1200)
                Core.Logger("You dont have enough AC to buy \"Undead Warrior\", the bot cannot continue.", messageBox: true, stopBot: true);
            Core.BuyItem("underworld", 215, "Undead Warrior");
        }

        Core.AddDrop("Ravaged Champion Soul");

        // Undead Champion Initiation
        Core.KillQuest(789, "greenguardwest", "Black Knight");
        // Mourn the Soldiers
        Core.KillQuest(790, "dwarfhold", "Chaos Drow");
        Core.KillQuest(790, "swordhavenundead", "Skeletal Soldier");
        Core.KillQuest(790, "pirates", "Fishman Soldier");
        Core.KillQuest(790, "willowcreek", "Dwakel Soldier");
        // Understanding Undead Champions
        Core.KillQuest(791, "battleunderb", "Undead Champion");
        // Player vs Power
        if (!Core.QuestProgression(792))
        {
            if (!Core.CheckInventory("Combat Trophy", 200))
                Farm.BludrutBrawlBoss(quant: 200);
            Core.ChainComplete(792);
        }
        // Fail to the King
        Core.KillQuest(793, "prison", "King Alteon's Knight", hasFollowup: false);

        if (Bot.Config.Get<bool>("AcceptSell"))
            Core.SellItem("Undead Warrior");
    }
}