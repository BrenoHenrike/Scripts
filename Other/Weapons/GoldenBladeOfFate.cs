//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class MysteriousStranger
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GoldenBladeofFate();

        Core.SetOptions(false);
    }

    public void GoldenBladeofFate()
    {
        if (Core.CheckInventory("Golden Blade of Fate"))
            return;

        if (!Core.isCompletedBefore(5679))
        {
            Core.Logger("Doing for the Golden Blade of Fate");

            // The Lost Teacher
            Core.KillQuest(5669, "tutor", "Horc Tutor Trainer");

            // Big Gold Coins
            Core.KillQuest(5670, "prison", "Piggy Drake");

            // Light as a Feather
            Core.KillQuest(5671, "lavarun", "Phedra");

            // Shard Shard Shard
            Core.KillQuest(5672, "chaoscrypt", "Chaorrupted Armor");

            // White Scales, Light Scales
            Core.KillQuest(5673, "j6", "Sketchy Frogzard");

            // The Stench of Defeat
            Core.MapItemQuest(5674, "orcpath", 5143, 3);

            // If you can't stand the heat...
            Core.KillQuest(5675, "lair", "Red Dragon");

            // The Depths of Despair
            Core.MapItemQuest(5676, "well", 5144);

            // All Things Green and Small...
            Core.KillQuest(5677, "cellar", "GreenRat");

            // Doom... Or Redemption?
            Core.KillQuest(5678, "sepulchure", "Dark Sepulchure");
        }

        Core.MapItemQuest(5679, "yulgar", 5145);
        Bot.Wait.ForPickup("Golden Blade of Fate");
    }
}