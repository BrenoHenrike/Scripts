/*
name: DeepTrobbleTreasureChest
description: Does the "Deep Trobble Treasure Chest" quest for the rewards [random drops]
tags: deep, trobble, treasure, chest, rewards, grim first mort on yer back, grim skull 'n flag, marauder's monkey mace, captain chinchillarrrr on yer back, captain chinchillarrrr pet, beleen's balloon cutelass, beleen's balloon cutelasses, gunpowder beach, gunpowder beach, pearl dust arm shuriken, astravian corsair, astravian corsair hat, astravian corsair hat, astravian corsair hat + locks, astravian corsair's coat, astravian corsair coat and blade
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
// using Skua.Core.Options;

public class DeepTrobbleTreasureChest
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Core.OneTimeMessage("read if u want", "drops P1 [section 1 on wiki] are random\n" +
        "drops P2[section 2 on wiki] are guaranteed  1 by 1 till u get them all", forcedMessageBox: true);

        DoQuest();

        Core.SetOptions(true);
    }

    public void DoQuest()
    {
        foreach (string map in new[] { "hiddendepths", "midnightwar", "kaijuwar", "cetoleonwar", "dragoncapital" })
            if (!Core.isSeasonalMapActive(map))
                return;

        RandomReward(9407);
    }

    private void RandomReward(int questID = 0000, int quant = 1)
    {
        int i = 1;

        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        string[] QuestRewards = RewardOptions.Select(x => x.Name).ToArray();

        Core.EquipClass(ClassType.Solo);
        Core.RegisterQuests(questID);
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                Core.Logger($"{Reward.Name} Found.");
            else
            {
                Core.FarmingLogger(Reward.Name, 1);
                while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name, quant, toInv: false))
                {

                    Core.HuntMonster("hiddendepths", "Aquamancer", "Aquamancer's Key");
                    Core.HuntMonster("midnightwar", "Flintfang", "Flintfang's Key");
                    Core.HuntMonster("kaijuwar", "Captain Kraylox", "Kraylox' Key");
                    Core.HuntMonster("cetoleonwar", "Nomura", "Nomura's Key");
                    Core.HuntMonster("dragoncapital", "Leviathanius", "Leviathanius' Key");

                    i++;

                    if (i % 5 == 0)
                    {
                        Core.JumpWait();
                        Core.ToBank(QuestRewards);
                    }
                }
            }
        }
        Core.CancelRegisteredQuests();
    }
}
