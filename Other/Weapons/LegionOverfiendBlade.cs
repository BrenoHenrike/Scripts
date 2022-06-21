//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Quests;

public class LegionBlade
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    string[] Rewards = { "Legion Overfiend Blade", "Dark Caster's Tome", "Eternal Legion Sword" };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Blade();

        Core.SetOptions(false);
    }

    public void Blade()
    {
        if (Core.CheckInventory(Rewards))
            return;

        Core.Logger("Checking for one of the required pets.");

        string item;
        int quant1;
        int quant2;
        int questID;

        if (Core.CheckInventory("Paragon Fiend Quest Pet"))
        {
            item = "Paragon Fiend Quest Pet";
            questID = 6748;
            quant1 = 1;
            quant2 = 1;
        }
        else if (Core.CheckInventory("Shogun Paragon Pet"))
        {
            item = "Shogun Paragon Pet";
            questID = 5751;
            quant1 = 8;
            quant2 = 10;
        }

        else if (Core.CheckInventory("Shogun Dage Pet"))
        {
            item = "Shogun Dage Pet";
            questID = 5752;
            quant1 = 1;
            quant2 = 1;
        }

        else if (Core.CheckInventory("Paragon Ringbearer"))
        {
            item = "Paragon Ringbearer";
            questID = 7071;
            quant1 = 1;
            quant2 = 1;
        }

        else
        {
            Core.Logger("You Don't Own the any of the Correct Pets, Stopping", stopBot: true);
            return;
        }

        Core.Logger($"You Own {item}");

        Quest QuestData = Core.EnsureLoad(questID);

        Core.AddDrop(Rewards);

        int i = 1;

        while (!Bot.ShouldExit() && !Core.CheckInventory(Rewards))
        {
            Core.EquipClass(ClassType.Farm);
            Core.EnsureAccept(questID);
            Core.HuntMonster("styx", "Sullen Soul", "Sullen Soul Received", quant1, log: false);
            Core.HuntMonster("styx", "Wrathful Soul", "Wrathful Soul Taken", quant2, log: false);
            Core.EnsureComplete(questID);
            Core.Logger($"Completed (Pet: {item}) \"{QuestData.Name}\" {i++} time[s]");
        }
    }
}
