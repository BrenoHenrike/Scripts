//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class OrbHunt
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        SagaName();

        Core.SetOptions(false);
    }

    public void SagaName()
    {
        if (Core.isCompletedBefore(8349))
            return;

        Story.PreLoad();

        // 8302|Fallen Nopperabo
        Story.KillQuest(8302, "queenreign", "Samurai Nopperabo");

        // 8303|Samurai of Jaaku
        Story.KillQuest(8303, "queenreign", "Shadow Samurai");

        // 8304|Constructing the Portal
        Story.MapItemQuest(8304, "queenreign", 9120);
        Story.KillQuest(8304, "queenreign", new[] { "Samurai Nopperabo", "Shadow Samurai" });

        // 8305|Into the Yokai Realm
        Story.KillQuest(8305, "queenreign", "Tsukumo-Gami");

        // 8306|Jaaku's Shadow
        Story.KillQuest(8306, "queenreign", "Jaaku's Shadow");

        // 8307|The Forces of Jaaku
        Story.KillQuest(8307, "queenreign", "Tsukumo-Gami");

        // 8308|The Wind Orb
        Story.KillQuest(8308, "queenreign", "Jaaku");

        // 8320|Goblin Down Water
        Story.KillQuest(8320, "queenreign", "Water Goblin");

        // 8321|Spawn of the Salt Sower
        Story.KillQuest(8321, "queenreign", "Sa-Laatan Spawn");

        // 8322|Faerie in Danger
        Story.KillQuest(8322, "queenreign", "Water Goblin");

        // 8323|Fertility Ward
        Story.MapItemQuest(8323, "queenreign", 9125, 5);

        // 8324|Path to Sa-Laatan
        Story.KillQuest(0008324, "queenreign", "Water Goblin");

        // 8325|The Water Orb
        Story.KillQuest(8325, "queenreign", "Sa-Laatan");

        // 8326|Water. Earth. Energy? Air.
        Story.KillQuest(8326, "queenreign", new[] { "Sa-Laatan", "Grou'luu", "Extriki", "Jaaku" });

        // 8328|Plant Stalk-ers
        Story.KillQuest(8328, "orbhunt", "Seed Stalker");

        // 8329|Rage Against the Mach-Wing
        Story.KillQuest(8329, "orbhunt", "Ragewing");

        // 8330|Tower Wards
        Story.MapItemQuest(8330, "orbhunt", 9156, 4);
        Story.MapItemQuest(8330, "orbhunt", 9157, 4);

        // 8331|The Final Barrier
        Story.KillQuest(8331, "orbhunt", "Seed Stalker");

        // 8332|The Fire Orb
        Story.KillQuest(8332, "orbhunt", "Chamat");

        // 8333|Lotus Be Done With This
        Story.KillQuest(8333, "orbhunt", "Lotus Spider");

        // 8334|Lightless Light
        Story.KillQuest(8334, "orbhunt", "Suffocated Light");

        // 8335|Suffocation Investigation
        Story.MapItemQuest(8335, "orbhunt", 9158, 4);

        // 8336|Gone With the Djinn
        Story.MapItemQuest(8336, "orbhunt", 9159);
        Story.KillQuest(8336, "orbhunt", "Lotus Spider");

        // 8337|The Light Orb
        Story.KillQuest(8337, "orbhunt", "Sek Duat I");

        // 8338|The Light Orb, Part II
        Story.KillQuest(8338, "orbhunt", "Horothotep");

        // 8339|Nax-ty Beasts
        Story.KillQuest(8339, "orbhunt", "Nax Beast");

        // 8340|Children of the Night
        Story.MapItemQuest(83834041, "orbhunt", 9160, 4);
        Story.KillQuest(8340, "orbhunt", "Hive");

        // 8341|We've Woken The Hive
        Story.KillQuest(8341, "orbhunt", "Hive");

        // 8342|Ritual Disruption
        Story.MapItemQuest(8342, "orbhunt", 9161, 4);
        Story.MapItemQuest(8342, "orbhunt", 1);
        Story.KillQuest(8342, "orbhunt", "Hive");

        // 8343|The Darkness Orb
        Story.KillQuest(8343, "orbhunt", "Kolyaban");

        // 8344|The Infernal Cold
        Story.KillQuest(8344, "orbhunt", "Ice Infernal");

        // 8345|Light a Fire
        Story.MapItemQuest(8345, "orbhunt", 9163, 4);

        // 8346|We Ani-must Win
        Story.KillQuest(8346, "orbhunt", "Animus of Ice");

        // 8347|Bones For Stones
        Story.KillQuest(8347, "orbhunt", new[] { "Seed Stalker", "Lotus Spider", "Hive", "Ice Infernal" });

        // 8348|The Ice Orb
        Story.KillQuest(8348, "orbhunt", "Quetzal");

        // 8349|A Hymn of Ice and Fire, and Light and Dark  
        Story.KillQuest(8349, "orbhunt", new[] { "Chamat", "Horothotep", "Kolyaban", "Quetzal" });
    }

}