/*
name: Gong Ji Zhanshi Set
description: Farms the Gong Ji Zhanshi set from Red Envelope Hunt quest.
tags: seasonal, akiba new year, gong ji zhanshi, red envelope hunt, akibacny
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;


public class GongJiZhanshiSet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoQuest();

        Core.SetOptions(true);
    }

    public void DoQuest()
    {

        QuestsIfNeeded();
        GetTheSet(5668, 1);
    }

    public void GetTheSet(int questID = 5668, int quant = 1)
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        foreach (ItemBase item in RewardOptions)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, quant))
            {
                Core.EnsureAccept(questID);
                Core.HuntMonster("akibacny", "Fiery Lantern", "Lucky Envelope", log: false);
                Core.EnsureComplete(questID, item.ID);
            }
        }
    }

    public void QuestsIfNeeded()
    {
        if (Core.isCompletedBefore(5667) || !Core.isSeasonalMapActive("akibacny"))
            return;

        Story.PreLoad(this);

        //Red Chicken Dance (5663)
        Story.KillQuest(5663, "akibacny", "Red Chicken");
        Story.MapItemQuest(5663, "akibacny", 5132);

        //Year of Fire FIght (5664)
        Story.KillQuest(5664, "akibacny", "Fiery Lantern");

        //HolyCud Crooner (5665)
        Story.KillQuest(5665, "akibacny", "Xingzhi");

        //Summon of the Spicy One (5666)
        Story.MapItemQuest(5666, "akibacny", 5133);

        //Too Hot to Handle? (5667)
        Story.KillQuest(5667, "akibacny", "Sriracha Holycud");
    }
}
