//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/SummerBreak/BeachParty.cs
using Skua.Core.Interfaces;

public class BeachPartyTokenItems
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public BeachPartyStory BP = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        TokenItems();

        Core.SetOptions(false);
    }

    public void TokenItems()
    {
        string[] rewards = Core.EnsureLoad(7010).Rewards.Where(x => Core.IsMember ? true : !x.Upgrade).Select(x => x.Name).ToArray();
        if (Core.CheckInventory(rewards, toInv: false))
            return;

        Core.AddDrop("Tiki Tokens");
        Core.AddDrop(rewards);
        BP.Storyline();

        Core.RegisterQuests(7010);
        while (!Bot.ShouldExit && !Core.CheckInventory(rewards, toInv: false))
        {
            Core.KillMonster("beachparty", "r3", "Left", "*", "Tiki Tokens", 5, false);
            Bot.Sleep(Core.ActionDelay);
        }
        Core.RegisterQuests();
        Core.ToBank(rewards);
    }
}
