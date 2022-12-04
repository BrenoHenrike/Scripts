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

        Core.AddDrop(rewards);

        foreach (string s in rewards)
        {
            Core.EnsureAccept(914);
            if (Core.CheckInventory(s, toInv: false))
                continue;

            Core.FarmingLogger(s, 1);
            Core.HuntMonster("frostdeep", "Ancient Maggot", "Tinsel's Sword Bow", log: false);
            Core.EnsureCompleteChoose(914, new string[] { s });
        }
        Core.ToBank(rewards);
    }

    public void TinselHelm()
    {
        if (!Core.isSeasonalMapActive("icevolcano"))
            return;

        string[] rewards = Core.QuestRewards(915); //Tinsel's Helm & Pet Gift

        Core.AddDrop(rewards);

        foreach (string s in rewards)
        {
            Core.EnsureAccept(914);

            if (Core.CheckInventory(s, toInv: false))
                continue;
            
            Core.FarmingLogger(s, 1);
            Core.HuntMonster("frostdeep", "Ancient Maggot", "Tinsel's Sword Bow", log: false);
            Core.EnsureCompleteChoose(914, new string[] { s });
        }
        Core.ToBank(rewards);
    }

    public void TinselArmor()
    {
        if (!Core.isSeasonalMapActive("goldenruins"))
            return;

        string[] rewards = Core.QuestRewards(1517); //Tinsel's Armor Gift

        Core.AddDrop(rewards);

        foreach (string s in rewards)
        {
            Core.EnsureAccept(914);
            if (Core.CheckInventory(s, toInv: false))
                continue;
            
            Core.FarmingLogger(s, 1);
            Core.HuntMonster("frostdeep", "Ancient Maggot", "Tinsel's Sword Bow", log: false);
            Core.EnsureCompleteChoose(914, new string[] { s });
        }
        Core.ToBank(rewards);
    }

    public void TinselCape()
    {
        if (!Core.isSeasonalMapActive("icerise"))
            return;

        string[] rewards = Core.QuestRewards(2554); //Tinsel's Cape Gift

        Core.AddDrop(rewards);

        foreach (string s in rewards)
        {
            Core.EnsureAccept(914);
            if (Core.CheckInventory(s, toInv: false))
                continue;

            Core.FarmingLogger(s, 1);
            Core.HuntMonster("frostdeep", "Ancient Maggot", "Tinsel's Sword Bow", log: false);
            Core.EnsureCompleteChoose(914, new string[] { s });
        }
        Core.ToBank(rewards);
    }
}