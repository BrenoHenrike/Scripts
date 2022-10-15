//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class UnderWorldPirateCasterQuset
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        QuestFarming();

        Core.SetOptions(false);
    }

    public void QuestFarming()
    {
        if (Core.CheckInventory("Underworld Pirate Caster's Pet"))
        {
            Core.Logger("You Don't Have \"Underworld Pirate Caster's Pet\". Pet is required for doing the quests.");
            return;
        }

        string[] Rewards = (Core.EnsureLoad(7086).Rewards.Select(i => i.Name).ToArray());
        Core.AddDrop(Rewards);
      
        while(!Bot.ShouldExit && !Core.CheckInventory(Rewards, toInv: false))
        {
            //Underworld Pirate Caster’s Pet Chest 7086
            Core.EnsureAccept(7086);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("styx", "Cerberus", "Cerberus Conquered");
            Core.EnsureComplete(7086);
        }
        Core.ToBank(Rewards);

    }
}
