/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CoreSoC
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void CompleteCoreSoC()
    {
        if (Core.isCompletedBefore(7768))
        {
            Core.Logger("You have already completed Shadows Of Chaos storyline");
            return;
        }
        DualPlane();
        ChaosAmulet();
        LagunaBeach();
        Laguna();
        ShadowOff();
        BrightShadow();
        BrightChaos();
        BrightForest();
        BrightForestPast();
        BrightForestExtra();
    }

    public void DualPlane()
    {
        if (Core.isCompletedBefore(7571))
        {
            Core.Logger("You have already completed Dualplane storyline");
            return;
        }

        Story.PreLoad(this);

        //Check their Teeth 7561
        Story.KillQuest(7561, "dualplane", "Terrarsite");

        //More Investigation Needed 7562
        Story.KillQuest(7562, "dualplane", "Droognax");

        //Find Xiang 7563
        Story.MapItemQuest(7563, "dualplane", 7459);

        //Destroy her Creatures 7564
        Story.KillQuest(7564, "dualplane", "Terrarsite");

        //Time to un-decorate 7565
        Story.KillQuest(7565, "dualplane", new[] { "SpiderWing", "Terrarsite" });

        //Interrogation Time 7566
        Story.KillQuest(7566, "dualplane", "Droognax");

        //Get rid of it! 7567
        Story.KillQuest(7567, "dualplane", "Netherpit Lackey");

        //"Convince" the Bruiser 7568
        Story.KillQuest(7568, "dualplane", "Netherpit Bruiser");

        //They Never Learn 7569
        Story.KillQuest(7569, "dualplane", new[] { "Terrarsite", "Netherpit Lackey" });

        //Get the Mirror! 7570
        Story.MapItemQuest(7570, "dualplane", 7460);

        //Get Her! 7571
        Story.KillQuest(7571, "dualplane", "Xiang");
    }

    public void ChaosAmulet()
    {
        if (Core.isCompletedBefore(7688))
        {
            Core.Logger("You have already completed Chaos Amulet storyline");
            return;
        }

        Story.PreLoad(this);

        //Mega Shadow Medals
        if (!Bot.Quests.IsUnlocked(7687))
        {
            // Shadow Medals 7685
            if (!Bot.Quests.IsUnlocked(7686))
            {
                Core.EnsureAccept(7685);
                Core.HuntMonster("chaosamulet", "Shadowflame Scout", "Shadow Medal", 5);
                Core.EnsureComplete(7685);
            }
            Core.EnsureAccept(7686);
            Core.HuntMonster("chaosamulet", "Shadowflame Scout", "Mega Shadow Medal", 3);
            Core.EnsureComplete(7686);
        }

        //Defeat Goldun 7687
        Story.KillQuest(7687, "chaosamulet", "Goldun");

        //Goldun Wants Revenge 7688
        Story.KillQuest(7688, "chaosamulet", new[] { "Goldun", "Shadowflame Berserker" });
    }

    public void LagunaBeach()
    {
        if (Core.isCompletedBefore(7700))
        {
            Core.Logger("You have already completed Laguna Beach storyline");
            return;
        }
        Story.PreLoad(this);

        //Eyes on You 7690
        Story.KillQuest(7690, "lagunabeach", "Flying Fisheye");

        //Behind the Tentacles 7691
        Story.MapItemQuest(7691, "lagunabeach", 7636);

        //Overwhelming Minions 7692
        Story.KillQuest(7692, "lagunabeach", "ShadowChaos Brigand");

        //Ground Kelp? 7693
        Story.MapItemQuest(7693, "lagunabeach", 7637, 5);
        Story.KillQuest(7693, "lagunabeach", "Chaos Kelp");

        //Slug 'em! 7694
        Story.KillQuest(7694, "lagunabeach", "ShadowChaos Brigand");

        //Shadows and Chaos 7695
        Story.KillQuest(7695, "lagunabeach", "Flying Fisheye");

        //Follow the Trail 7696
        Story.MapItemQuest(7696, "lagunabeach", 7638, 3);
        Story.MapItemQuest(7696, "lagunabeach", 7639);

        //Clear the Tunnel 7697
        Story.KillQuest(7697, "lagunabeach", "ShadowChaos Brigand");

        //Blow it up! 7698
        Story.MapItemQuest(7698, "lagunabeach", 7640);
        Story.KillQuest(7698, "lagunabeach", "ShadowChaos Gunner");

        //The Heart of the Matter 7699
        Story.KillQuest(7697, "lagunabeach", "Heart of Chaos");

        // A Closer Look 7700
        Story.KillQuest(7700, "lagunabeach", "Flying Fisheye");
    }

    public void Laguna()
    {
        if (Core.isCompletedBefore(7712))
        {
            Core.Logger("You have already completed Laguna storyline");
            return;
        }

        Story.PreLoad(this);

        //Fight the Crew 7702
        Story.KillQuest(7702, "laguna", "ShadowChaos Brigand");

        //Reach the Ship 7703
        Story.MapItemQuest(7703, "laguna", 7675);

        //Get the Bombs 7704
        Story.KillQuest(7704, "laguna", "ShadowChaos Gunner");

        //Blow 'er Up! 7705
        Story.MapItemQuest(7705, "laguna", 7676);
        Story.KillQuest(7705, "laguna", "ShadowChaos Gunner");

        //Defeat the Captain 7706
        Story.KillQuest(7706, "laguna", "Captain Laguna");

        //More Pirates 7707
        Story.KillQuest(7707, "laguna", "ShadowChaos Brigand");

        //Steal for Scouts 7708
        Story.KillQuest(7708, "laguna", "Chaos Roe");

        //Make them Work 7709
        Story.KillQuest(7709, "laguna", "Chaos Burrower");

        //Find the Amulet 7710
        Story.MapItemQuest(7710, "laguna", 7678);
        Story.KillQuest(7710, "laguna", "ShadowChaos Brigand");

        //Retrieve the Amulet 7711
        Story.KillQuest(7711, "laguna", "Writhing Chaos");

        //Snack Time 7712
        Story.KillQuest(7712, "laguna", "Chaos Roe");
    }

    public void ShadowOff()
    {
        if (Core.isCompletedBefore(7732))
        {
            Core.Logger("You have already completed Shadowoff storyline");
            return;
        }

        Story.PreLoad(this);

        // Invasion!
        Story.KillQuest(7728, "Shadowoff", new[] { "Shadowflame Militia", "Shadowflame Paladin", "Shadowflame Scout", "Shadowflame Sorcerer" });

        //Rescue Needed
        Story.MapItemQuest(7729, "Shadowoff", 7699, 6);
        Story.KillQuest(7729, "Shadowoff", "Shadowflame Sorcerer");

        //Get their Intel
        Story.KillQuest(7730, "Shadowoff", "Shadowflame Militia");

        //Clue me in
        Story.KillQuest(7731, "Shadowoff", new[] { "Shadowflame Sorcerer", "Shadowflame Militia", "Shadowflame Scout" });

        //So Familiar
        Story.KillQuest(7732, "Shadowoff", "Shadowflame Paladin");
    }

    public void BrightShadow()
    {
        if (Core.isCompletedBefore(7738))
        {
            Core.Logger("You have already completed BrightShadow storyline");
            return;
        }

        ShadowOff();

        Story.PreLoad(this);

        //Protect the Light 7733
        Story.KillQuest(7733, "BrightShadow", "BrightFall light");

        //Fend Them Off 7734
        Story.KillQuest(7734, "BrightShadow", new[] { "BrightFall Guard", "ShadowFlame Paladin" });

        //Get The Keys 7735
        Story.KillQuest(7735, "BrightShadow", "BrightFall Guard");

        //Unlock The Door 7736
        Story.MapItemQuest(7736, "BrightShadow", 7701);

        //Defeat Gravelyn 7737
        Story.KillQuest(7737, "BrightShadow", "Gravelyn the Good");

        //Restoring Order 7738
        Story.KillQuest(7738, "BrightShadow", new[] { "Brightfall light", "Brightfall Guard", "Shadowflame Paladin" });
    }

    public void BrightChaos()
    {
        if (Core.isCompletedBefore(7750))
        {
            Core.Logger("You have already completed BrightChaos storyline");
            return;
        }
        BrightShadow();
        Story.PreLoad(this);

        //The Situation 7740
        Story.KillQuest(7740, "BrightChaos", "Shadowflame Militia");

        //A Study In Shadow 7741
        Story.KillQuest(7741, "BrightChaos", "Shadowflame Sorcerer");

        //Percision Strike 7742
        Story.KillQuest(7742, "BrightChaos", new[] { "Shadowflame Militia", "Shadowflame Sorcerer" });

        //Pants On Fire 7743
        Story.MapItemQuest(7743, "BrightChaos", 7731, 6);
        Story.KillQuest(7743, "BrightChaos", "Shadow Flame");

        //Scared of the Dark 7744
        Story.MapItemQuest(7744, "BrightChaos", 7732, 5);
        Story.KillQuest(7744, "BrightChaos", "Hidden Monster");

        //Go Bye-Bye 7745
        Story.KillQuest(7745, "BrightChaos", "Shadow Trap");

        //Essential Oils 7746
        Story.KillQuest(7746, "BrightChaos", "ShadowBeast");

        //What A Mess 7747
        Story.MapItemQuest(7747, "BrightChaos", 7733, 6);

        //Proscuitto! 7748
        Story.KillQuest(7748, "BrightChaos", "ShadowBeast");

        //A Blight On Us All 7749
        Story.KillQuest(7749, "BrightChaos", "Blight");

        //Unending Blight 7750
        Story.KillQuest(7750, "BrightChaos", "Blight");
    }

    public void BrightForest()
    {
        if (Core.isCompletedBefore(7758))
        {
            Core.Logger("You have already completed BrightForest storyline");
            return;
        }
        BrightChaos();
        Story.PreLoad(this);
        //Blood And Ashes 7756
        Story.KillQuest(7756, "BrightForest", new[] { "Shadow Flame", "ShadowFlame Scout" });

        //Drink and Fight 7757
        Story.KillQuest(7757, "BrightForest", new[] { "ShadowFlame Scout", "ShadowFlame Warrior" });

        //Oooommm 7758
        Story.MapItemQuest(7758, "BrightForest", 7754, 2);
        Story.MapItemQuest(7758, "BrightForest", 7755, 1);
    }

    public void BrightForestPast()
    {
        if (Core.isCompletedBefore(7766))
        {
            Core.Logger("You have already completed BrightForestPast storyline");
            return;
        }
        BrightForest();
        Story.PreLoad(this);

        //Slay the Wraiths 7759
        Story.KillQuest(7759, "BrightForestPast", "Time Wraith");

        //Spacetime 7760
        Story.MapItemQuest(7760, "BrightForestPast", 7753);
        Story.KillQuest(7760, "BrightForestPast", "Spacetime Energy");

        //Find Khasaanda 7761
        Story.MapItemQuest(7761, "BrightForestPast", 7756);

        //Find the Heart 7762
        Story.KillQuest(7762, "BrightForestPast", "Undead Minion");

        //Ooh, That Smell 7763
        Story.KillQuest(7763, "BrightForestPast", new[] { "Undead Minion", "Twisted Treeant" });

        //So Twisted 7764
        Story.KillQuest(7764, "BrightForestPast", "Twisted Treeant");

        //Purifying Flames 7765
        Story.MapItemQuest(7765, "BrightForestPast", 7757);
        Story.KillQuest(7765, "BrightForestPast", "Treeant");

        //Return to Iadoa 7766
        Story.MapItemQuest(7766, "BrightForestPast", 7758);
    }

    public void BrightForestExtra()
    {
        if (Core.isCompletedBefore(7768))
        {
            Core.Logger("You have already completed BrightForest Extra storyline");
            return;
        }

        Story.PreLoad(this);

        //The Dragon 7767
        Story.KillQuest(7767, "BrightForest", "ShadowFlame Dragon");

        //The Shadows Recede 7768
        Story.KillQuest(7768, "BrightForest", "ShadowFlame Dragon");
        Story.KillQuest(7768, "BrightForest", new[] { "Shadowflame Scout", "Shadowflame Warrior" });
    }
}
