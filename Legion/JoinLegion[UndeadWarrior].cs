//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using RBot;
using System.Windows.Forms;

public class JoinLegion
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        JoinLegionQuests();

        Core.SetOptions(false);
    }

    public void JoinLegionQuests()
    {
        if (Story.isCompletedBefore(793))
            return;

        Core.BuyItem("underworld", 215, "Undead Warrior");
        DialogResult SellUW = MessageBox.Show(
                                "Do you want the bot to sell the \"Undead Warrior\" armor after it has succesfully joined the legion. This will return 1080 AC to you",
                                "Sell \"Undead Warrior\"?",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

        Core.AddDrop("Ravaged Champion Soul");

        // Undead Champion Initiation
        Story.KillQuest(789, "greenguardwest", "Black Knight");
        // Mourn the Soldiers
        if (!Story.QuestProgression(790))
        {
            Core.EnsureAccept(790);
            Core.HuntMonster("dwarfhold", "Chaos Drow", "Chaos Drow slain");
            Core.HuntMonster("swordhavenundead", "Skeletal Soldier", "Skeletal Soldier slain");
            Core.HuntMonster("pirates", "Fishman Soldier", "Fishman Soldier slain");
            Core.HuntMonster("willowcreek", "Dwakel Soldier", "Dwarkel Soldier slain");
            Core.EnsureComplete(790);
        }
        // Understanding Undead Champions
        Story.KillQuest(791, "battleunderb", "Undead Champion");
        // Player vs Power
        if (!Story.QuestProgression(792))
        {
            if (!Core.CheckInventory("Combat Trophy", 200))
                Farm.BludrutBrawlBoss(quant: 200);
            Story.ChainQuest(792);
        }
        // Fail to the King
        Story.KillQuest(793, "prison", "King Alteon's Knight");

        if (SellUW == DialogResult.Yes)
            Core.SellItem("Undead Warrior");
    }
}