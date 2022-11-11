//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CoreSepulchure
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Core.RunCore();

        Core.SetOptions(false);
    }

    public void CompleteSS()
    {
        Alden();
        Lynaria();
        SepulchuresRise();
        ShadowfallRise();
    }

    #region Alden Quests
    public void Alden()
    {
        if (Core.isCompletedBefore(6342))
            return;

        Story.PreLoad(this);

        // Who goes there? 6332
        Core.EquipClass(ClassType.Farm);
        Story.MapItemQuest(6332, "scarsgarde", 5860);

        // The Taint Spreads 6333
        Story.MapItemQuest(6333, "scarsgarde", 5864, 6);
        Story.KillQuest(6333, "scarsgarde", "VenomWing");
        
        // Beauty Twisted 6334
        Story.KillQuest(6334, "scarsgarde", "Garde Grif");
        
        // Element of Surprise 6335
        Story.MapItemQuest(6335, "scarsgarde", 5865, 5);
        Story.KillQuest(6335, "scarsgarde", "Tree");
        
        // (Take the) Watch Out 6336
        Story.KillQuest(6336, "scarsgarde", new[] { "Garde Watch", "Garde Pikeman" });
        
        // False Hoods 6337
        Story.KillQuest(6337, "scarsgarde", new[] { "Garde Watch", "Garde Watch", "Garde Watch", "Garde Watch", "Garde Watch", "Garde Watch", "Garde Watch" });
        
        // Pass for Real 6338
        Story.MapItemQuest(6338, "scarsgarde", new[] { 5866, 5867 });
        Story.KillQuest(6338, "scarsgarde", new[] { "Garde Knight", "Garde Pikeman", "Garde Knight" });
        
        // Hidden in Plain Sight 6339
        Story.MapItemQuest(6339, "scarsgarde", 5868, 8);
        Story.MapItemQuest(6339, "scarsgarde", 5869);
        
        // Stay Strong Keep Steady 6340
        Story.KillQuest(6340, "scarsgarde", new[] { "Garde Knight", "Garde Pikeman" });
        
        // The Final Fight 6341
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6341, "scarsgarde", "Garde Captain");
        
        // Arm the Army 6342
        Story.KillQuest(6342, "scarsgarde", new[] { "Garde Watch", "Garde Pikeman", "Garde Knight" });
    }
    #endregion

    #region Lynaria Quests
    public void Lynaria()
    {
        if (Core.isCompletedBefore(6353))
            return;

        Story.PreLoad(this);
        
        // Who goes there? 6343
        Core.EquipClass(ClassType.Farm);
        Story.MapItemQuest(6343, "scarsgarde", 5861);
        
        // The Taint Spreads 6344
        Story.MapItemQuest(6344, "scarsgarde", 5864, 6);
        Story.KillQuest(6344, "scarsgarde", "VenomWing");
        
        // Beauty Twisted 6345
        Story.KillQuest(6345, "scarsgarde", "Garde Grif");
        
        // Element of Surprise 6346
        Story.MapItemQuest(6346, "scarsgarde", 5865, 5);
        Story.KillQuest(6346, "scarsgarde", "Tree");
        
        // (Take the) Watch Out 6347
        Story.KillQuest(6347, "scarsgarde", new[] { "Garde Watch", "Garde Pikeman" });
        
        // False Hoods 6348
        Story.KillQuest(6348, "scarsgarde", new[] { "Garde Watch", "Garde Watch", "Garde Watch", "Garde Watch", "Garde Watch", "Garde Watch", "Garde Watch" });
        
        // Pass for Real 6349
        Story.MapItemQuest(6349, "scarsgarde", new[] { 5866, 5867 });
        Story.KillQuest(6349, "scarsgarde", new[] { "Garde Knight", "Garde Pikeman", "Garde Knight" });
        
        // Hidden in Plain Sight 6350
        Story.MapItemQuest(6350, "scarsgarde", 5868, 8);
        Story.MapItemQuest(6350, "scarsgarde", 5869);
        
        // Stay Strong Keep Steady 6351
        Story.KillQuest(6351, "scarsgarde", new[] { "Garde Knight", "Garde Pikeman" });
        
        // The Final Fight 6352
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6352, "scarsgarde", "Garde Captain");
        
        // Arm the Army 6353
        Story.KillQuest(6353, "scarsgarde", new[] { "Garde Watch", "Garde Pikeman", "Garde Knight" });
    }
    #endregion

    #region Sepulchure's Rise
    public void SepulchuresRise()
    {
        if (Core.isCompletedBefore(6408))
            return;

        Story.PreLoad(this);
        
        // Come On Get Ready! 6356
        Core.EquipClass(ClassType.Farm);
        Story.BuyQuest(6356, "valleyofdoom", 1599, "Valen Gear", 1);
        
        // Scout the Valley 6357
        Story.KillQuest(6357, "valleyofdoom", "Shadow Imp");
        
        // Gather Energy 6358
        Story.KillQuest(6358, "valleyofdoom", "Shadow Imp");
        
        // Do I Have To Spell It Out? 6359
        Story.MapItemQuest(6359, "valleyofdoom", 5873, 8);
        
        // Get Through the Shadows 6360
        Story.KillQuest(6360, "valleyofdoom", new[] { "Shadow Beast", "Shadow Person" });
        
        // Get the Key 6361
        Story.KillQuest(6361, "valleyofdoom", "Doom Guardian");
        
        // Confront the Champion of Darkness 6362
        Story.MapItemQuest(6362, "valleyofdoom", 5872);
        
        // Destroy the Arsenal 6363
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
        
        // Defeat the Armor 6364
        Core.EquipClass(ClassType.Solo);
        if (!Story.QuestProgression(6364))
        {
            Core.EnsureAccept(6364);
            Core.KillMonster("valleyofdoom", "r8a", "Left", 3801);
            Core.EnsureComplete(6364);
        }
        
        // Secure the Chest 6365
        Story.MapItemQuest(6365, "guardiantower", 5871);
        
        // Get Some Goo 6366
        Story.KillQuest(6366, "guardiantower", "Slime");
        
        // Retrieve the Wand 6367
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6367, "guardiantower", "Yargol Magebane");
        
        // The Wedding 6368
        Core.Logger("Autocompleted");
        
        // Gather the Shadows 6369
        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(6369, "valleyofdoom", "Shadow Imp");
        
        // Choose Your Path 6370
        Story.MapItemQuest(6370, "ebonslate", 5895);
        
        // Mind Control 6371
        Story.KillQuest(6371, "ebonslate", "Mind Con-Troll");
        
        // Sp-Eye on You 6372
        Story.KillQuest(6372, "ebonslate", "Sp-Eye");
        
        // Lower the Drawbridge 6373
        Story.MapItemQuest(6373, "ebonslate", 5896);
        Story.KillQuest(6373, "ebonslate", "Ebonslate Guard");
        
        // Get the Key 6374
        Story.MapItemQuest(6374, "ebonslate", 5897);
        Story.KillQuest(6374, "ebonslate", new[] { "Lycan Brute", "Lycan Brute" });
        
        // Break Down the Door 6375
        Story.MapItemQuest(6375, "ebonslate", 5898);
        
        // Question the Knights 6376
        Story.KillQuest(6376, "ebonslate", "Ebonslate Knight");
        
        // Troll the Trolls 6377
        Story.KillQuest(6377, "ebonslate", "Mind Con-Troll");
        
        // Get Through the Barrier 6378
        Story.MapItemQuest(6378, "ebonslate", 5899);
        
        // Disarm the Knights 6379
        Story.KillQuest(6379, "ebonslate", "Ebonslate Knight");
        
        // Bruise the Bruiser 6380
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6380, "ebonslate", "Ebonslate Bruiser");
        
        // Take Down Dethrix 6381
        if (Story.QuestProgression(6381))
        {
            Core.EnsureAccept(6381);
            Core.Join("ebonslate", "r11", "Left");
            Bot.Combat.Attack("Dethrix");
            Bot.Sleep(10000);
        }
        
        // Please Fix Me 6382
        Story.KillQuest(6382, "guardiantowerb", "Slime");
        
        // Get the Slime 6383
        Story.KillQuest(6383, "guardiantowerb", "Slime");
        
        // Reach for the Heart 6384
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6384, "guardiantowerb", "Yargol Magebane");
        
        // Get More Shadows 6385
        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(6385, "guardiantowerb", "Slime");
        
        // Garen's Key 6386
        Story.KillQuest(6386, "guardiantowerb", new[] { "Guardian Selby", "Guardian Garen" });
        
        // Selby's Key 6387
        Story.KillQuest(6387, "guardiantowerb", new[] { "Guardian Garen", "Guardian Selby" });
        
        // Get Rid of Bolton 6388
        Story.MapItemQuest(6388, "guardiantowerb", 5901);
        Story.KillQuest(6388, "guardiantowerb", "Guardian Bolton");
        
        // Get Rid of the Imps 6389
        Story.KillQuest(6389, "ebondungeon", "Ebonslate Imp");
        
        // Get Into the Dungeon 6390
        Story.MapItemQuest(6390, "ebondungeon", 5902);
        
        // Destroy the Guards 6391
        Story.KillQuest(6391, "ebondungeon", "Ebon Dungeon Guard");
        
        // Get the Key 6392
        Story.KillQuest(6392, "ebondungeon", "Elite Dungeon Guard");
        
        // Find Lynaria 6393
        Story.MapItemQuest(6393, "ebondungeon", new[] { 5903, 5904 });
        
        // Destroy Dethrix 6394
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6394, "ebondungeon", "Dethrix");
        
        // Siphon the Power (Farming) 6395
        Story.KillQuest(6395, "ebondungeon", "Ebon Dungeon Guard");
        
        // He's Not Your Friend 6399
        Story.KillQuest(6399, "alteonfight", "King Alteon");
        
        // Defeat the Dragon 6400
        Story.MapItemQuest(6400, "alteonfight", 5910);
        
        // Power Boost 6401
        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(6401, "darkplane", "Shadow Beast");
        
        // Take His Knights 6402
        Story.KillQuest(6402, "darkplane", "Guardian Knight");
        
        // Destroy the Light 6403
        Story.KillQuest(6403, "darkplane", "Light Spirit");
        
        // More Power! 6404
        Story.KillQuest(6404, "darkplane", "Shadow Beast");
        
        // Destroy the Force Field 6405
        Story.MapItemQuest(6405, "darkplane", 5911);
        
        // Defeat the Battalion 6406
        Story.KillQuest(6406, "darkplane", "Guardian Knight");
        
        // Destroy the Beast 6407
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6407, "darkplane", "Victorious");
        
        // Learn from the Past 6408
        Story.KillQuest(6408, "darkplane", "Shadow Beast");
    }
    #endregion

    #region Shadowfall Rise
    public void ShadowfallRise()
    {
        if (Core.isCompletedBefore(6592))
            return;

        Story.PreLoad(this);
        
        // Fluid Dynamics 6539
        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(6539, "noxustower", "Slimeskull");
        
        // M-ossification 6540
        Story.KillQuest(6540, "noxustower", "Doomwood Treeant");
        
        // A-gore-able 6541
        Story.KillQuest(6541, "noxustower", "Sanguine Souleater");
        
        // Raw-r 6542
        Story.KillQuest(6542, "noxustower", "Lightguard Caster");
        
        // Light 'em Up 6543
        Story.KillQuest(6543, "noxustower", "Lightguard Caster");
        
        // Spellbound 6544
        Story.MapItemQuest(6544, "noxustower", 6018, 4);
        Story.KillQuest(6544, "noxustower", "Lightguard Caster");
        
        // Lupine Resurrection 6545
        Story.KillQuest(6545, "noxustower", "Lightguard Wolf");
        
        // Spying on the Light 6546
        Story.KillQuest(6546, "noxustower", "Lightguard Paladin");
        
        // Illusory Disguise 6547
        Story.KillQuest(6547, "noxustower", new[] { "Lightguard Caster", "Doomwood Treeant", "Slimeskull" });
        
        // Test the Disguise 6548
        Story.MapItemQuest(6548, "noxustower", 6019);
        
        // Get Alteon 6549
        Story.MapItemQuest(6549, "noxustower", 6020);
        
        // Hammer Time 6550
        Story.KillQuest(6550, "noxustower", "General Goldhammer");
        
        // Lightguard Medals 6560
        Story.KillQuest(6560, "lightguardwar", "Citadel Crusader");
        
        // Mega Lightguard Medals 6561
        Story.KillQuest(6561, "lightguardwar", "Citadel Crusader");
        
        // Bone Marrow 6562
        Story.KillQuest(6562, "lightguardwar", "Citadel Crusader");
        
        // Ooze it up 6563
        Story.KillQuest(6563, "lightguardwar", "Slimeskull");
        
        // Seize the Seige (Engine) 6564
        Story.KillQuest(6564, "lightguardwar", "Lightguard Engine");
        
        // Darken the Light 6565
        Story.KillQuest(6565, "lightguardwar", "Scorching Flame");
        
        // Get the Powder 6566
        Story.KillQuest(6566, "lightguardwar", "Citadel Crusader");
        
        // Destroy the Shield 6567
        Story.KillQuest(6567, "lightguardwar", "Sigrid Sunshield");
        
        // Such a Mess 6581
        Story.MapItemQuest(6581, "lumafortress", 6098, 8);
        Story.KillQuest(6581, "lumafortress", "Invasive Shadow");
        
        // Herbs Needed 6582
        Story.MapItemQuest(6582, "lumafortress", 6099, 7);
        Story.KillQuest(6582, "lumafortress", "Light Treeant");
        
        // Shine a Light 6583
        Story.KillQuest(6583, "lumafortress", "Light Elemental");
        
        // Hapless? 6584
        Story.KillQuest(6584, "lumafortress", "Hapless Skeleton");
        
        // How 'bout them Apples 6585
        Story.KillQuest(6585, "lumafortress", "Light Treeant");
        
        // Make an Offering 6586
        Story.MapItemQuest(6586, "lumafortress", 6100);
        Story.KillQuest(6586, "lumafortress", "Lightwing");
        
        // Test the Spell Again! 6587
        Story.MapItemQuest(6587, "lumafortress", 6101);
        
        // Gather the Light 6588
        Story.KillQuest(6588, "lumafortress", "Light Elemental");
        
        // Banish the Shadows 6589
        Story.KillQuest(6589, "lumafortress", "Living Shadow");
        
        // So Many Minions 6590
        Story.KillQuest(6590, "lumafortress", "Skeleton Minion");
        
        // Corrupted Light 6591
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6591, "lumafortress", "Corrupted Luma");
        
        // Star Light Star Bright 6592
        Story.KillQuest(6592, "lumafortress", "Light Elemental");
    }
    #endregion
}
