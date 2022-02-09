//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;
using System.Windows.Forms;

public class JoinLegion
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        JoinLegionQuests();

        Core.SetOptions(false);
    }

    public void JoinLegionQuests()
    {
        if (Bot.Quests.IsUnlocked(3043))
            return;

        Core.BuyItem("underworld", 215, "Undead Warrior");
        DialogResult SellUW = MessageBox.Show(
                                "Do you want the bot to sell the \"Undead Warrior\" armor after it has succesfully joined the legion. This will return 1080 AC to you",
                                "Sell \"Undead Warrior\"?",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

        Core.AddDrop("Ravaged Champion Soul");

        // Undead Champion Initiation
        Core.KillQuest(789, "greenguardwest", "Black Knight");
        // Mourn the Soldiers
        Core.KillQuest(790, "dwarfhold", "Chaos Drow");
        Core.KillQuest(790, "swordhavenundead", "Skeletal Soldier");
        Core.KillQuest(790, "pirates", "Fishman Soldier");
        Core.KillQuest(790, "willowcreek", "Dwakel Soldier");
        // Understanding Undead Champions
        Core.KillQuest(791, "battleunderb", "Undead Champion");
        // Player vs Power
        if (!Core.QuestProgression(792))
        {
            if (!Core.CheckInventory("Combat Trophy", 200))
                Farm.BludrutBrawlBoss(quant: 200);
            Core.ChainComplete(792);
        }
        // Fail to the King
        Core.KillQuest(793, "prison", "King Alteon's Knight");

        if (SellUW == DialogResult.Yes)
            Core.SellItem("Undead Warrior");
    }
}