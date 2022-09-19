//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class NecroTowerStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }
    public void DoAll()
    {
        if (Core.isCompletedBefore(1101))
        {
            Core.Logger($"Story: Necro Tower - Complete");
            return;
        }

        Story.PreLoad(this);

        //A Brutal Mistake (1077)

        if (!Story.QuestProgression(1077))
        {
            Core.EnsureAccept(1077);
            Core.GetMapItem(429, 6, "lightguard");
            Core.KillMonster("doomwood", "r6", "Right", "*", "Backup Spare Extra Auxiliary Keyring");
            Core.EnsureComplete(1077);
        }

        //Bony Battalion (1064)
        Story.KillQuest(1064, "doomwood", new[] { "Doomwood Ectomancer", "Doomwood Bonemuncher", "Doomwood Soldier" });

        //Warrior Rez-queue (1065)
        Story.MapItemQuest(1065, "doomwood", 423, 5);

        //Bone-tired Backup (1066)
        if (!Story.QuestProgression(1066))
        {
            Core.EnsureAccept(1066);
            Core.KillMonster("doomwood", "r6", "Right", "*", "Warrior Reinforced");
            Core.EnsureComplete(1066);
        }

        //Reconnaissance Route (1067)
        Story.MapItemQuest(1067, "doomwood", 422);

        //Fight Against Shadowed Light (1068)
        Story.KillQuest(1068, "doomwood", "Undead Paladin");

        //Camouflage: Skelly-Style (1069)
        Story.KillQuest(1069, "doomwood", new[] { "Doomwood Bonemuncher", "Doomwood Ectomancer", "Doomwood Soldier" });

        //De(ad)ception (1070)
        if (!Story.QuestProgression(1070))
        {
            Core.EnsureAccept(1070);
            Core.GetMapItem(427, map: "doomundead");
            Core.KillMonster("doomundead", "r3", "Right", "*", "Light Knight Lifeforce", 5);
            Core.EnsureComplete(1070);
        }

        //Artix Stopped! (1089)
        Story.ChainQuest(1089);

        //Zorbak's Hideout (1081)
        Story.MapItemQuest(1081, "maul", 435);

        //Stink-tuary (1082)
        Story.MapItemQuest(1082, "maul", 434, 13);

        //The Infected (1083)
        Story.KillQuest(1083, "maul", new[] { "Slimeskull", "Personal Chopper" });

        //Chopping Spree (1084)
        if (!Story.QuestProgression(1084))
        {
            Core.EnsureAccept(1084);
            Core.KillMonster("maul", "r2", "Left", "*", "Body Part Donation", 10);
            Core.GetMapItem(436, 2, "maul");
            Core.EnsureComplete(1084);
        }

        //GraveStop the Creature (1085)
        Story.KillQuest(1085, "maul", "Creature Creation");

        //ID What You Did There (1087)
        if (!Story.QuestProgression(1087))
        {
            Core.EnsureAccept(1087);
            Core.KillMonster("necrotower", "r2", "Left", "Doomwood Treeant", "Pain-per", 5);
            Core.KillMonster("necrotower", "r2", "Left", "Slimeskull", "Toxic Goo", 5);
            Core.EnsureComplete(1087);
        }

        //The Ego and the ID (1088)
        Story.KillQuest(1088, "necrotower", new[] { "DoomWood Soldier", "DoomWood Soldier" });

        //An IDeal Seal (1090)
        Story.KillQuest(1090, "necrotower", "DoomWood Bonemuncher");

        //Need for Speed (Reading)! (1091)
        Story.MapItemQuest(1091, "necrotower", 438);

        //Necro Tower Elevator Minigame
        for (int i = 1092; i <= 1101; i++)
            Story.ChainQuest(i);
    }
}
