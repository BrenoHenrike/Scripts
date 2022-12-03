//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class TinselGifts
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        TinselWeapon(914);
        TinselHelm(915);
        TinselArmor(1517);
        TinselCape(2554);

        Core.SetOptions(false);
    }

    public void TinselWeapon(int questID)
    {
        if (!Core.isSeasonalMapActive("frostdeep"))
            return;

        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                continue;

            Core.EnsureAccept(questID);
            Core.HuntMonster("frostdeep", "Ancient Maggot", "Tinsel's Sword Bow", log: false);
            Core.EnsureComplete(questID, Reward.ID);
            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
    }

    public void TinselHelm(int questID)
    {
        if (!Core.isSeasonalMapActive("icevolcano"))
            return;
        
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                continue;
            Core.FarmingLogger(Reward.Name, 1);

            Core.EnsureAccept(questID);
            Core.HuntMonster("icevolcano", "Ice Symbiote", "Tinsel's Helm Bow", log: false);
            Core.EnsureComplete(questID, Reward.ID);
            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
    }

    public void TinselArmor(int questID)
    {
        if (!Core.isSeasonalMapActive("goldenruins"))
            return;
        
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                continue;

            Core.EnsureAccept(questID);
            Core.HuntMonster("goldenruins", "Golden Warrior", "Tinsel's Armor Bow", log: false);
            Core.EnsureComplete(questID, Reward.ID);
            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
    }

    public void TinselCape(int questID)
    {
        if (!Core.isSeasonalMapActive("icerise"))
            return;
        
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                continue;

            Core.EnsureAccept(questID);
            Core.HuntMonster("icerise", "Arctic Direwolf", "Tinsel's Cape Bow", log: false);
            Core.EnsureComplete(questID, Reward.ID);
            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
    }
}