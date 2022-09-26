//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;

public class CorruptDraconicParagon
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreSoW SoW = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(7428).Rewards;
        List<string> RewardsList = new List<string>();
        List<string> RewardList = RewardOptions.Select(x => x.Name).ToList();
        string[] Rewards = RewardList.ToArray();

        if (Core.CheckInventory(Rewards, toInv: false))
            return;

        Core.AddDrop(Rewards);
        SoW.DarkAlly();

        int count = 0;
        Core.CheckSpaces(ref count, Rewards);

        Core.EquipClass(ClassType.Solo);
        Core.RegisterQuests(7428);
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards, toInv: false))
            //Defeat the Underfiend 7428
            Core.HuntMonster("darkally", "Underfiend", "Underfiend Defeated");
        Core.ToBank("Corrupt Draconic Paragon Plate");
        Core.CancelRegisteredQuests();

    }
}
