//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/BloodMoon.cs
using Skua.Core.Interfaces;

public class VampiricKnightAndBloodGuardian
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public BloodMoon BloodMoon = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        string[] AllRewards = { "Vampiric Knight's Cane", "Vampiric Knight's Sword", "Vampiric Knight", "Vampiric Knight Cape", "Ceremonial Assistant Pet", "Blood Guardian Armor", "Blood Guardian Shag", "Blood Guardian's Sword", "Rubies" };
        string[] Quest1Rewards = { "Vampiric Knight's Cane", "Vampiric Knight's Sword" };
        string[] Quest2Rewards = { "Vampiric Knight", "Vampiric Knight Cape" };
        string[] Quest3Rewards = { "Ceremonial Assistant Pet", "Blood Guardian Armor", "Blood Guardian Shag", "Blood Guardian's Sword" };

        if (Core.CheckInventory(AllRewards, toInv: false))
            return;

        BloodMoon.BloodMoonSaga();
        Bot.Drops.Add(AllRewards);

        Core.EquipClass(ClassType.Farm);

        Core.RegisterQuests(6068);
        while (!Bot.ShouldExit && !Core.CheckInventory(Quest1Rewards, toInv: false))
        {
            //Lycan Medals 6068
            Core.HuntMonster("BloodWarVamp", "Lunar Blazebinder", "Lycan Medal", 5);
            Core.Jump("Wait", "Spawn");
            Core.ToBank(Quest1Rewards);
        }
        Core.CancelRegisteredQuests();

        Core.RegisterQuests(6069);
        while (!Bot.ShouldExit && !Core.CheckInventory(Quest2Rewards, toInv: false))
        {
            //Mega Lycan Medals 6069
            Core.HuntMonster("BloodWarVamp", "Lunar Blazebinder", "Mega Lycan Medal", 3);
            Core.Jump("Wait", "Spawn");
            Core.ToBank(Quest2Rewards);
        }
        Core.CancelRegisteredQuests();

        Core.RegisterQuests(6070);
        while (!Bot.ShouldExit && !Core.CheckInventory(Quest3Rewards, toInv: false))
        {
            //Blackened Incense 6072
            Core.HuntMonster("BloodWarVamp", "Lunar Blazebinder", "Blackened Incense", 5);
            Core.Jump("Wait", "Spawn");
            Core.ToBank(Quest3Rewards);
        }
        Core.CancelRegisteredQuests();


    }
}
