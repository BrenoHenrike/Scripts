//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ShadowfallRise
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
        if (Core.isCompletedBefore(6592))
        {

            Core.Logger("You have already completed ShadowfallRise Storyline");
            return;
        }

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Solo);

        //Fluid Dynamics 6539
        Story.KillQuest(6539, "noxustower", "Slimeskull");

        //M-ossification 6540
        Story.KillQuest(6540, "noxustower", "Doomwood Treeant");

        //A-gore-able 6541
        Story.KillQuest(6541, "noxustower", "Sanguine Souleater");

        //Raw-r 6542
        Story.KillQuest(6542, "noxustower", "Lightguard Caster");

        //Light 'em Up 6543
        Story.KillQuest(6543, "noxustower", "Lightguard Caster");

        //Spellbound 6544
        Story.MapItemQuest(6544, "noxustower", 6018, 4);
        Story.KillQuest(6544, "noxustower", "Lightguard Caster");

        //Lupine Resurrection 6545
        Story.KillQuest(6545, "noxustower", "Lightguard Wolf");

        //Spying on the Light 6546
        Story.KillQuest(6546, "noxustower", "Lightguard Paladin");

        //Illusory Disguise 6547
        Story.KillQuest(6547, "noxustower", new[] { "Lightguard Caster", "Doomwood Treeant", "Slimeskull" });

        //Test the Disguise 6548
        Story.MapItemQuest(6548, "noxustower", 6019);

        //Get Alteon 6549
        Story.MapItemQuest(6549, "noxustower", 6020);

        //Hammer Time 6550
        Story.KillQuest(6550, "noxustower", "General Goldhammer");

        //Lightguard Medals 6560
        Story.KillQuest(6560, "lightguardwar", "Citadel Crusader");

        //Mega Lightguard Medals 6561
        Story.KillQuest(6561, "lightguardwar", "Citadel Crusader");

        //Bone Marrow 6562
        Story.KillQuest(6562, "lightguardwar", "Citadel Crusader");

        //Ooze it up 6563
        Story.KillQuest(6563, "lightguardwar", "Slimeskull");

        //Seize the Seige (Engine) 6564
        Story.KillQuest(6564, "lightguardwar", "Lightguard Engine");

        //Darken the Light 6565
        Story.KillQuest(6565, "lightguardwar", "Scorching Flame");

        //Get the Powder 6566
        Story.KillQuest(6566, "lightguardwar", "Citadel Crusader");

        //Destroy the Shield 6567
        Story.KillQuest(6567, "lightguardwar", "Sigrid Sunshield");

        //Such a Mess 6581
        Story.MapItemQuest(6581, "lumafortress", 6098, 8);
        Story.KillQuest(6581, "lumafortress", "Invasive Shadow");

        //Herbs Needed 6582
        Story.MapItemQuest(6582, "lumafortress", 6099, 7);
        Story.KillQuest(6582, "lumafortress", "Light Treeant");

        //Shine a Light 6583
        Story.KillQuest(6583, "lumafortress", "Light Elemental");

        //Hapless? 6584
        Story.KillQuest(6584, "lumafortress", "Hapless Skeleton");

        //How 'bout them Apples 6585
        Story.KillQuest(6585, "lumafortress", "Light Treeant");

        //Make an Offering 6586
        Story.MapItemQuest(6586, "lumafortress", 6100);
        Story.KillQuest(6586, "lumafortress", "Lightwing");

        //Test the Spell Again! 6587
        Story.MapItemQuest(6587, "lumafortress", 6101);

        //Gather the Light 6588
        Story.KillQuest(6588, "lumafortress", "Light Elemental");

        //Banish the Shadows 6589
        Story.KillQuest(6589, "lumafortress", "Living Shadow");

        //So Many Minions 6590
        Story.KillQuest(6590, "lumafortress", "Skeleton Minion");

        //Corrupted Light 6591
        Story.KillQuest(6591, "lumafortress", "Corrupted Luma");

        //Star Light Star Bright 6592
        Story.KillQuest(6592, "lumafortress", "Light Elemental");
    }
}