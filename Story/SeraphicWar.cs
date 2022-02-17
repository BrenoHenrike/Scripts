//cs_include Scripts/CoreBots.cs
using RBot;

public class SeraphicWar_Story
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        SeraphicWar_Questline();

        Core.SetOptions(false);
    }

    public void SeraphicWar_Questline()
    {
        if (Core.isCompletedBefore(7428))
            return;

        Core.EquipClass(ClassType.Solo);


        //Get the Parts
        Core.KillQuest(6238, "worldsoul", new[] { "Dwakel Infiltrator", "Dwakel Infiltrator", "Dwakel Infiltrator", "Dwakel Infiltrator" });
        //Get the Water
        Core.KillQuest(6239, "worldsoul", "Divine Water Elemental");
        //Get the Fire
        Core.KillQuest(6240, "worldsoul", "Divine Fire Elemental");
        //Gather Armaments
        Core.MapItemQuest(6241, "worldsoul", 5681, 3);
        Core.KillQuest(6241, "worldsoul", "Skeletal Squatter");
        //Plutonium For Power
        Core.KillQuest(6242, "worldsoul", "Radioactive Hydra");
        //Defeat the Undead
        Core.MapItemQuest(6243, "worldsoul", 5680);
        Core.KillQuest(6243, "worldsoul", "Legion Dreadmarch|Legion Shadowpriest");
        //Remove the Ward
        Core.MapItemQuest(6244, "worldsoul", 5682);
        Core.KillQuest(6244, "worldsoul", "Legion Dreadmarch|Legion Shadowpriest");
        //Defeat the Guardian
        Core.KillQuest(7428, "worldsoul", "Core Guardian");
    }
}