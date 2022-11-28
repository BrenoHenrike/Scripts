//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/IsleOfFotia/CoreIsleOfFotia.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
// using Skua.Core.Options;

public class CreepyUndeadMonkeyQuest
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreIsleOfFotia Fotia => new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.Add("Twisted Monkey Paw");
        Core.SetOptions();

        RandomReward(4106);

        Core.SetOptions(false);
    }

    private void RandomReward(int questID, int quant = 1)
    {
        if (!Core.IsMember || !Core.isSeasonalMapActive("twig"))
            return;

        int i = 0;

        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        string[] QuestRewards = RewardOptions.Select(x => x.Name).ToArray();

        if (!Core.CheckInventory("Golden Bough"))
        {
            Bot.Drops.Add("Golden Bough");
            Core.EquipClass(ClassType.Farm);

            Core.EnsureAccept(3010);
            Core.HuntMonster("UnderRealm", "Underworld Soul", "Souls Released", 8);
            Core.EnsureComplete(3010);
        }

        Core.EquipClass(ClassType.Farm);
        Bot.Quests.UpdateQuest(3010);
        Core.RegisterQuests(questID);
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                Core.Logger($"{Reward.Name} Found.");
            else
            {
                Core.FarmingLogger(Reward.Name, 1);
                while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name, toInv: false))
                {

                    Core.HuntMonster("alliance", "Good Soldier", "Good Soldier's Face");
                    Core.HuntMonster("alliance", "Evil Soldier", "Evil Soldier's Skull");
                    Core.HuntMonster("neverlore", "Whablobble", "Whablobble Tongue");
                    Core.HuntMonster("thespan", "Minx Fairy", "Minx Fairy Wings");
                    Core.HuntMonster("battlecon", "BrutalCorn", "Evil Con Corny");
                    Core.HuntMonster("crossroads", "Lemurphant", "Lemurphant's Peanuts");
                    Core.HuntMonster("arcangrove", "Gorillaphant", "Fresh Gorilla Paw");
                    Core.HuntMonster("arcangrove", "Gorillaphant", "Bananas in pajamas");
                    Core.HuntMonster("battlefowl", "Chickencow", "Chickencow Head");
                    Core.HuntMonster("mafic", "Scoria Serpent", "Scoria Serpent Charmer");
                    Core.HuntMonster("underrealm", "Grief", "Grief's Tears");
                    Core.HuntMonster("deepchaos", "Kathool", "Kathool… All of him");
                    Core.HuntMonster("twig", "Sweetish Fish", "Candy from a Sweetish Fish");

                    i++;

                    if (i % 5 == 0)
                    {
                        Core.JumpWait();
                        Core.ToBank(QuestRewards);
                    }
                }
            }
        }
    }
}