//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
using Skua.Core.Interfaces;

public class ElementalDragonSlayer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public Core7DD DD = new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        List<string> Rewards = Core.EnsureLoad(6171).Rewards.Select(x => x.Name).ToList();
        Rewards.Remove("Mysterious Egg");
        string[] SetItems = Rewards.ToArray();

        Bot.Drops.Add(SetItems);

        if (Core.CheckInventory(SetItems, toInv: false))
            return;

        for (int i = 0; i < SetItems.Length; i++)
        {
            if (!Core.CheckInventory(SetItems[i], toInv: false))
            {
                Core.Logger($"Farming \"{SetItems[i]}\"");

                //Mysterious Chest 6171
                Core.EnsureAccept(6171);

                Core.KillMonster("pride", "r13", "Left", "Valsarian", "Key of Pride", isTemp: false);
                Core.KillMonster("gluttony", "Enter2", "Top", "Deflated Glutus", "Key of Gluttony", isTemp: false);
                Core.KillMonster("greed", "r16", "Left", "Goregold", "Key of Greed", isTemp: false);

                if (!Core.CheckInventory("Key of Sloth"))
                {
                    DD.HazMatSuit();
                    Core.HuntMonster("sloth", "Phlegnn", "Key of Sloth", isTemp: false);
                }

                Core.HuntMonster("lust", "Lascivia", "Key of Lust", isTemp: false);
                Bot.Quests.UpdateQuest(6000);
                Core.HuntMonster("maloth", "Maloth", "Key of Envy", isTemp: false);
                Core.HuntMonster("wrath", "Gorgorath", "Key of Wrath", isTemp: false);

                Core.EnsureCompleteChoose(6171, new[] {SetItems[i]});
                Bot.Wait.ForPickup(SetItems[i]);
            }
        }
    }
}
