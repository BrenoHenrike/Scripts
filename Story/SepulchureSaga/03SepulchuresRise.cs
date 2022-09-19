//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class SepulchuresRise
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(6408))
        {

            Core.Logger("You have already completed SelpulchuresRise Storyline");
            return;
        }

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Solo);

        //Come On Get Ready! 6356
        Story.BuyQuest(6356, "valleyofdoom", 1599, "Valen Gear", 1);

        //Scout the Valley 6357
        Story.KillQuest(6357, "valleyofdoom", "Shadow Imp");

        //Gather Energy 6358
        Story.KillQuest(6358, "valleyofdoom", "Shadow Imp");

        //Do I Have To Spell It Out? 6359
        Story.MapItemQuest(6359, "valleyofdoom", 5873, 8);

        //Get Through the Shadows 6360
        Story.KillQuest(6360, "valleyofdoom", new[] { "Shadow Beast", "Shadow Person" });

        //Get the Key 6361
        Story.KillQuest(6361, "valleyofdoom", "Doom Guardian");

        //Confront the Champion of Darkness 6362
        Story.MapItemQuest(6362, "valleyofdoom", 5872);

        //Destroy the Arsenal 6363
        if (!Story.QuestProgression(6363))
        {
            Core.EnsureAccept(6363);
            Core.KillMonster("valleyofdoom", "r7", "Left", "Doom Star", "Doomstar Destroyed");
            Core.KillMonster("valleyofdoom", "r7", "Left", "Doom Scythe", "Doomscythe Destroyed");
            Core.KillMonster("valleyofdoom", "r7", "Left", "Doom Axe", "Doomaxe Destroyed");
            Core.KillMonster("valleyofdoom", "r8", "Left", "Doom Blade", "Doom Blade Destroyed");
            Core.KillMonster("valleyofdoom", "r8", "Left", "Doom Knight Armor", "Doom Knight Armor Destroyed");
            Core.EnsureComplete(6363);
        }

        //Defeat the Armor 6364
        if (!Story.QuestProgression(6364))
        {
            Core.EnsureAccept(6364);
            Core.KillMonster("valleyofdoom", "r8a", "Left", 3801);
            Core.EnsureComplete(6364);
        }

        //Secure the Chest 6365
        Story.MapItemQuest(6365, "guardiantower", 5871);

        //Get Some Goo 6366
        Story.KillQuest(6366, "guardiantower", "Slime");

        //Retrieve the Wand 6367
        Story.KillQuest(6367, "guardiantower", "Yargol Magebane");

        //The Wedding 6368
        Core.Logger("Autocompleted");

        //Gather the Shadows 6369
        Story.KillQuest(6369, "valleyofdoom", "Shadow Imp");

        //Choose Your Path 6370
        Story.MapItemQuest(6370, "ebonslate", 5895);

        //Mind Control 6371
        Story.KillQuest(6371, "ebonslate", "Mind Con-Troll");

        //Sp-Eye on You 6372
        Story.KillQuest(6372, "ebonslate", "Sp-Eye");

        //Lower the Drawbridge 6373
        Story.KillQuest(6373, "ebonslate", "Ebonslate Guard");
        Story.MapItemQuest(6373, "ebonslate", 5896);

        //Get the Key 6374
        Story.MapItemQuest(6374, "ebonslate", 5897);
        Story.KillQuest(6374, "ebonslate", new[] { "Lycan Brute", "Lycan Brute" });

        //Break Down the Door 6375
        Story.MapItemQuest(6375, "ebonslate", 5898);

        //Question the Knights 6376
        Story.KillQuest(6376, "ebonslate", "Ebonslate Knight");

        //Troll the Trolls 6377
        Story.KillQuest(6377, "ebonslate", "Mind Con-Troll");

        //Get Through the Barrier 6378
        Story.MapItemQuest(6378, "ebonslate", 5899);

        //Disarm the Knights 6379
        Story.KillQuest(6379, "ebonslate", "Ebonslate Knight");

        //Bruise the Bruiser 6380
        Story.KillQuest(6380, "ebonslate", "Ebonslate Bruiser");

        //Take Down Dethrix 6381
        if (Story.QuestProgression(6381))
        {
            Core.EnsureAccept(6381);
            Core.Join("ebonslate", "r11", "Left");
            Bot.Combat.Attack("Dethrix");
            Bot.Sleep(10000);
        }

        //Please Fix Me 6382
        Story.KillQuest(6382, "guardiantowerb", "Slime");

        //Get the Slime 6383
        Story.KillQuest(6383, "guardiantowerb", "Slime");

        //Reach for the Heart 6384
        Story.KillQuest(6384, "guardiantowerb", "Yargol Magebane");

        //Get More Shadows 6385
        Story.KillQuest(6385, "guardiantowerb", "Slime");

        //Garen's Key 6386
        Story.KillQuest(6386, "guardiantowerb", new[] { "Guardian Selby", "Guardian Garen" });

        //Selby's Key 6387
        Story.KillQuest(6387, "guardiantowerb", new[] { "Guardian Garen", "Guardian Selby" });

        //Get Rid of Bolton 6388
        Story.MapItemQuest(6388, "guardiantowerb", 5901);
        Story.KillQuest(6388, "guardiantowerb", "Guardian Bolton");

        //Get Rid of the Imps 6389
        Story.KillQuest(6389, "ebondungeon", "Ebonslate Imp");

        //Get Into the Dungeon 6390
        Story.MapItemQuest(6390, "ebondungeon", 5902);

        //Destroy the Guards 6391
        Story.KillQuest(6391, "ebondungeon", "Ebon Dungeon Guard");

        //Get the Key 6392
        Story.KillQuest(6392, "ebondungeon", "Elite Dungeon Guard");

        //Find Lynaria 6393
        Story.MapItemQuest(6393, "ebondungeon", new[] { 5903, 5904 });

        //Destroy Dethrix 6394
        Story.KillQuest(6394, "ebondungeon", "Dethrix");

        //Siphon the Power (Farming) 6395
        Story.KillQuest(6395, "ebondungeon", "Ebon Dungeon Guard");

        //He's Not Your Friend 6399
        Story.KillQuest(6399, "alteonfight", "King Alteon");

        //Defeat the Dragon 6400
        Story.MapItemQuest(6400, "alteonfight", 5910);

        //Power Boost 6401
        Story.KillQuest(6401, "darkplane", "Shadow Beast");

        //Take His Knights 6402
        Story.KillQuest(6402, "darkplane", "Guardian Knight");

        //Destroy the Light 6403
        Story.KillQuest(6403, "darkplane", "Light Spirit");

        //More Power! 6404
        Story.KillQuest(6404, "darkplane", "Shadow Beast");

        //Destroy the Force Field 6405
        Story.MapItemQuest(6405, "darkplane", 5911);

        //Defeat the Battalion 6406
        Story.KillQuest(6406, "darkplane", "Guardian Knight");

        //Destroy the Beast 6407
        Story.KillQuest(6407, "darkplane", "Victorious");

        //Learn from the Past 6408
        Story.KillQuest(6408, "darkplane", "Shadow Beast");

    }
}