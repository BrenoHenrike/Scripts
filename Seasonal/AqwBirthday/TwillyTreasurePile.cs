/*
name: Twilly's Treasure Pile
description: This will do the Twilly's Treasure Pile quest and obtain all of the rewards.
tags: twillys-treasure-pile, seasonal, aqw-anniversary
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class TwillyTreasurePile
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        string[] HouseItems = { "Zard Rider Statue", "Twilly's Treasure Pile", "20th Anniversary Balloon" };
        List<string> RewardsList = Core.EnsureLoad(8925).Rewards.Select(x => x.Name).ToList();
        //house Items
        RewardsList.Remove("Zard Rider Statue");
        RewardsList.Remove("Twilly's Treasure Pile");
        RewardsList.Remove("20th Anniversary Balloon");
        string[] Rewards = RewardsList.ToArray();

        if (Core.CheckInventory(Rewards, toInv: false) && CheckHouseInventory(HouseItems) && Core.isSeasonalMapActive("yulgarparty"))
            return;

        Bot.Drops.Add(Rewards);
        Bot.Drops.Add(HouseItems);

        bool CheckHouseInventory(string[] itemNames)
        {
            foreach (string item in itemNames)
            {
                if (!Bot.House.Contains(item))
                    return false;
                else
                    continue;
            }
            return true;
        }

        Core.EquipClass(ClassType.Solo);
        //Twilly's Treasure Pile 8925
        Core.RegisterQuests(8925);
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards) && !CheckHouseInventory(HouseItems))
        {
            int i = 0;
            Core.HuntMonster("yulgarparty", "Treasure Pile", "Twilly's Treasure Defeated");
            i++;
            if (i % 5 == 0)
            {
                Core.JumpWait();
                Core.ToBank(Rewards);
            }
        }
        Core.CancelRegisteredQuests();
        Core.JumpWait();
        Core.ToBank(Rewards);
        Core.ToBank(HouseItems);
    }
}
