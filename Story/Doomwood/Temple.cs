//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Temple
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(1148))
            return;
        
        Story.PreLoad(this);

        // Level 1 1123
        if (!Story.QuestProgression(1123))
        {
            Core.EnsureAccept(1123);
            Core.KillMonster("temple", "r2", "up", "SlimeSkull", "Slimeskull Trophy", 5);
            Core.KillMonster("temple", "r2", "up", "Doomwood Bonemuncher", "Munched Boneshard", 5);
            Core.KillMonster("temple", "r2", "up", "Shelleton", "Shelleton Shrapnel", 5);
            Core.EnsureComplete(1123);
        }

        // Level 2 1124
        if (!Story.QuestProgression(1124))
        {
            Core.EnsureAccept(1124);
            Core.GetMapItem(456, 6, "temple");
            Core.KillMonster("temple", "r3", "up", "Undead Mage", "Necrotic Rune", 10);
            Core.KillMonster("temple", "r3", "up", "Doomwood Ectomancer", "Ecto-Covered Rune", 3);
            Core.EnsureComplete(1124);
        }

        // Level 3 1125
        if (!Story.QuestProgression(1125))
        {
            Core.EnsureAccept(1125);
            Core.KillMonster("temple", "r4", "up", "Ghoul", "Ghoulish Gear", 1);
            Core.KillMonster("temple", "r4", "up", "Lich", "Haunted Habiliment", 1);
            Core.EnsureComplete(1125);
        }        

        // Level 4 1126
        if (!Story.QuestProgression(1126))
        {
            Core.EnsureAccept(1126);
            Core.GetMapItem(457, 8, "temple");
            Core.KillMonster("temple", "r5", "up", "Skeletal Fire Mage", "Flame Extinguished", 10);
            Core.EnsureComplete(1126);
        }

        // Level 5 1127
        Story.KillQuest(1127, "temple", "Doomwood Ectomancer");

        // Level 6 1128
        Story.KillQuest(1128, "temple", new[] { "Doomwood Bonemuncher", "Sanguine Souleater" });

        // Level 7 1129
        Story.MapItemQuest(1129, "temple", 458, 12);

        // Level 8 1130
        Story.KillQuest(1130, "temple", new[] { "Skeletal Fire Mage", "Doomwood Ectomancer" });

        // Level 9 1131
        Story.MapItemQuest(1131, "temple", 459, 8);
        Story.KillQuest(1131, "temple", "Ghoul");

        // Level 10 1132
        Story.KillQuest(1132, "temple", "Doomwood Ectomancer");

        // Level 11 1133
        Story.MapItemQuest(1133, "temple", 460, 10);
        Story.KillQuest(1133, "temple", "Sanguine Souleater");

        // Level 12 1134
        Story.KillQuest(1134, "temple", new[] { "SlimeSkull", "Doomwood Ectomancer" });

        // Level 13 1135
        Story.MapItemQuest(1135, "temple", 461, 12);

        // Level 14 1137
        Story.KillQuest(1137, "temple", "Doomwood Soldier");

        // Level 15 1138
        Story.KillQuest(1138, "temple", "Ghoul");

        // Level 16 1139
        Story.MapItemQuest(1139, "temple", 462, 10);

        // Level 17 1140
        Story.KillQuest(1140, "temple", new[] { "Doomwood Bonemuncher", "Skeleton" });

        // Level 18 1141
        Story.KillQuest(1141, "temple", new[] { "Undead Mage", "Skeletal Fire Mage" });

        // Level 19 1142
        Story.MapItemQuest(1142, "temple", 463, 6);
        Story.KillQuest(1142, "temple", new[] { "Shelleton", "Skeleton" });

        // Level 20 1143
        Story.KillQuest(1143, "temple", "Lich");

        // Defy the Dracolich 1144
        Story.KillQuest(1144, "temple", "Dracolich");

        // Restore the Tome 1145
        Story.MapItemQuest(1145, "temple", 464, 10);

        // Recover the Pages 1146
        Story.KillQuest(1146, "temple", "Doomwood Bonemuncher");

        // Reconstruct the Codex 1147
        Story.KillQuest(1147, "temple", "Cryptkeeper Lich");

        // Galvanize the Guardian 1148
        if (!Story.QuestProgression(1148))
        {
            Core.EnsureAccept(1148);
            Core.KillMonster("temple", "r2", "up", "Shelleton", "Basilisk's Scale");
            Core.KillMonster("temple", "r2", "up", "Shelleton", "Scroll of Magic Inversion");
            Core.Jump("enter", "spawn");
            Core.BuyItem("temple", 287, "Scroll of Cure Petrification");
            Core.EnsureComplete(1148);
        }
    }
}
