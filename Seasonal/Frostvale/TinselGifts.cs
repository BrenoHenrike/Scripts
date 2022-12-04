//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class TinselGifts
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        TinselWeapon();
        TinselHelm();
        TinselArmor();
        TinselCape();
    }

    public void TinselWeapon()
    {
        if (!Core.isSeasonalMapActive("frostdeep"))
            return;

        string[] rewards = Core.QuestRewards(914); //Tinsel's Weapon Gift

        if (Core.CheckInventory(rewards))
            return;

        Core.AddDrop(rewards);

        Core.RegisterQuests(914);
        while (!Bot.ShouldExit && !(Core.CheckInventory(rewards)))
        {
            Core.HuntMonster("frostdeep", "Ancient Maggot", "Tinsel's Sword Bow", log: false);
        }
        Core.CancelRegisteredQuests();
        Core.ToBank(rewards);
    }

    public void TinselHelm()
    {
        if (!Core.isSeasonalMapActive("icevolcano"))
            return;

        string[] rewards = Core.QuestRewards(915); //Tinsel's Helm & Pet Gift

        if (Core.CheckInventory(rewards))
            return;

        Core.AddDrop(rewards);

        Core.RegisterQuests(915);
        while (!Bot.ShouldExit && !(Core.CheckInventory(rewards)))
        {
            Core.HuntMonster("icevolcano", "Ice Symbiote", "Tinsel's Helm Bow", log: false);
        }
        Core.CancelRegisteredQuests();
        Core.ToBank(rewards);
    }

    public void TinselArmor()
    {
        if (!Core.isSeasonalMapActive("goldenruins"))
            return;

        string[] rewards = Core.QuestRewards(1517); //Tinsel's Armor Gift

        if (Core.CheckInventory(rewards))
            return;

        Core.AddDrop(rewards);

        Core.RegisterQuests(1517);
        while (!Bot.ShouldExit && !(Core.CheckInventory(rewards)))
        {
            Core.HuntMonster("goldenruins", "Golden Warrior", "Tinsel's Armor Bow", log: false);
        }
        Core.CancelRegisteredQuests();
        Core.ToBank(rewards);
    }

    public void TinselCape()
    {
        if (!Core.isSeasonalMapActive("icerise"))
            return;

        string[] rewards = Core.QuestRewards(2554); //Tinsel's Cape Gift

        if (Core.CheckInventory(rewards))
            return;

        Core.AddDrop(rewards);

        Core.RegisterQuests(2554);
        while (!Bot.ShouldExit && !(Core.CheckInventory(rewards)))
        {
            Core.HuntMonster("icerise", "Arctic Direwolf", "Tinsel's Cape Bow", log: false);
        }
        Core.CancelRegisteredQuests();
        Core.ToBank(rewards);
    }
}