/*
name: Trobbolier Pet (Member Only)
description: This will finish the required quest to obtain all of the Trobbolier Pets.
tags: trobbolier-pet, member, friday-the-13th, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Seasonal/Friday13th/Story/CoreFriday13th.cs
using System.Text;
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class TrobbolierPet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFriday13th F13 = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        if (!Core.IsMember)
            return;

        F13.Wormhole();

        List<ItemBase> rewardOptions = Core.EnsureLoad(5067).Rewards;

        var logMessage = new StringBuilder("Inventory Check for Reward Options:\n");

        foreach (ItemBase item in rewardOptions)
        {
            string status = Core.CheckInventory(item.Name, toInv: false) ? "✔️" : "❌";
            logMessage.AppendLine($"- {item.Name}: {status}");
        }

        Bot.Log(logMessage);


        Bot.Drops.Add(rewardOptions.Select(item => item.Name).ToArray());
        Core.EquipClass(ClassType.Farm);

        foreach (ItemBase item in rewardOptions)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, toInv: false))
            {
                Core.EnsureAccept(5067);
                Core.HuntMonsterMapID("wormhole", 13, "Blue Trobbolier Fluff", 4, log: false);
                Core.HuntMonsterMapID("wormhole", 27, "Purple Trobbolier Fluff", 4, log: false);
                Core.HuntMonsterMapID("wormhole", 24, "Green Trobbolier Fluff", 4, log: false);
                Core.HuntMonsterMapID("wormhole", 11, "Red Trobbolier Fluff", 4, log: false);
                Core.EnsureCompleteChoose(5067, Core.QuestRewards(5067));
                Bot.Wait.ForPickup("*");
                Core.ToBank(rewardOptions
                            .Where(item => Bot.Inventory.Contains(item.Name))
                            .Select(item => item.Name)
                            .ToArray());
                Bot.Drops.Remove(item.ID);

            }
        }
    }

}
