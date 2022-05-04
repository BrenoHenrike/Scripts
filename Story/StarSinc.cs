//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs

using RBot;
public class StarSinc
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        
        StarSincQuests();
        
        Core.SetOptions(false);
    }
    
    public void StarSincQuests()
    {
        if (Core.isCompletedBefore(4412))
            return;
            
        Story.PreLoad();

        //4400 | Weaken his Powers
        Story.KillQuest(4400, "starsinc", "Star Sprites");

        //4401 | Light and Dark
        Story.KillQuest(4401, "starsinc", "Star Sprites");

        //4402 | Paintings Give Him Strength
        Story.MapItemQuest(4402, "starsinc", 3607, 4);

        //4403 | Learning the Land
        Story.MapItemQuest(4403, "starsinc", 3608);

        //4004 | Slay the Light and Dark
        Story.KillQuest(4004, "starsinc", new[] {"Infernal Imp", "Living Star"});

        //4405 | Chaos Fragments
        Story.KillQuest(4405, "starsinc", "Chaorrupted Wolf");

        //4406 | Kill Them All
        Story.KillQuest(4406, "starsinc", "Star Sprites");

        //4407 | Get Rid of Those Guards
        Story.KillQuest(4407, "starsinc", "Fortress Guard");

        //4408 | Breach the Gate
        Story.KillQuest(4408, "starsinc", "Fortress Guard");

        //4409 | Defeat the Prime Dominus
        Story.KillQuest(4409, "starsinc", "Prime Defeated");

        //4410 | Place the Beacons
        Story.MapItemQuest(4410, "starsinc", 3609, 6);

        //4412 | Retrieve the Core
        Story.KillQuest(4412, "starsinc", "Final");

        //4413 | Become one with the Universe
        Story.KillQuest(4413, "starsinc", "Living Star");

        //4414 | Becoming the Star Dominus
        if (!Story.QuestProgression(4414))
        {
            Core.EnsureAccept(4414);
            Core.KillMonster("battleunderb", "Enter", "Spawn", "*", "Bone dust");
            Core.HuntMonster("starsinc", "Living Star", "Living Star Essence", 100, false);
            Farm.BludrutBrawlBoss(quant: 15);
            Core.EnsureComplete(4414);
        }

        //4415 | Your Hardest Task
        Story.KillQuest(4415, "starsinc", "Empowered Prime");
    }
}