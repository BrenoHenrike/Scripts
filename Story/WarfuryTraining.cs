//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class WarTraining
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Adv.BestGear(GearBoost.Human);
        DoALl();

        Core.SetOptions(false);
    }

    public void DoALl()
    {
        Story.PreLoad();
        StoryLine();
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(8204))
            return;

        //War Medals
        Story.KillQuest(8125, "fireplanewar", "Shadowflame Soldier|Shadefire Onslaught");

        // Mega War Medals
        Story.KillQuest(8126, "fireplanewar", "Shadowflame Soldier|Shadefire Onslaught");

        // Shadowflame Un - slaught
        Story.KillQuest(8127, "fireplanewar", "Shadefire Onslaught");

        // Soldier On
        Story.KillQuest(8128, "fireplanewar", "Shadowflame Soldier");

        // Fanning the Flames
        Story.KillQuest(8129, "fireplanewar", "Shadowflame Soldier|Shadefire Onslaught");
        Story.MapItemQuest(8129, "fireplanewar", 8523, 5);

        // Trailblazer
        Story.KillQuest(8130, "fireplanewar", "Shadefire Onslaught|Shadowflame Soldier");

        // ShadowClaw
        Story.KillQuest(8131, "fireplanewar", "ShadowClaw");

        // In Their Element
        Story.KillQuest(8132, "fireplanewar", "Shadefire Elemental");

        // Cure the Fire
        Story.KillQuest(8133, "fireplanewar", "Living Shadowflame|Shadefire Elemental");
        Story.MapItemQuest(8133, "fireplanewar", 8524, 5);

        // Human Torch
        Story.KillQuest(8134, "fireplanewar", "Living Shadowflame");

        // ShadowFlame Phedra
        Story.KillQuest(8135, "fireplanewar", "ShadowFlame Phedra");

        // Gather Fuel
        Story.KillQuest(8136, "shadowfireplane", "Living Shadowflame");

        // Attune to Play
        Story.MapItemQuest(8137, "shadowfireplane", 8544, 5);

        // Sparks Will Fly
        Story.KillQuest(8138, "shadowfireplane", "Onslaught Knight|Shadowfire Corporal");

        // Awaken Lady Fiamme
        Story.MapItemQuest(8139, "shadowfireplane", 8542);
        Bot.Sleep(5000);

        // Destroy the Barrier
        Story.KillQuest(8140, "shadowfireplane", new[] { "Shadowfire Summoner", "Shadow Wing" });
        Story.MapItemQuest(8140, "shadowfireplane", 8543);

        // Blaze a Path
        Story.KillQuest(8141, "shadowfireplane", new[] { "Onslaught Knight", "Shadowfire Corporal" });

        // Into the Tiger's Den
        Story.KillQuest(8142, "shadowfireplane", "Shadowfire Tiger");

        // One Final Push
        Story.KillQuest(8143, "shadowfireplane", "Shadowfire Corporal");

        // Defeat Elius
        Story.KillQuest(8144, "shadowfireplane", "Elius");

        // Elementary Defense
        Story.KillQuest(8179, "fireinvasion", "Onslaught Knight");

        // Tiger Burning Bright
        Story.KillQuest(8180, "fireinvasion", "Shadowfire Tiger");
        Story.MapItemQuest(8180, "fireinvasion", 8728, 3);

        // Crush the Cavalry
        Story.KillQuest(8181, "fireinvasion", "Shadefire Cavalry");

        // Capture the Corporals
        Story.KillQuest(8182, "fireinvasion", "Shadowfire Corporal");

        // Light up the Night
        Story.KillQuest(8183, "fireinvasion", "Shadefire Elemental");
        Story.MapItemQuest(8183, "fireinvasion", 8729, 6);

        // Major Malfunction
        Story.KillQuest(8184, "fireinvasion", "Shadefire Major");

        // Darkness in Swordhaven
        Story.KillQuest(8185, "fireinvasion", new[] { "Shadefire Elemental", "Shadowfire Tiger" });

        // Extinguish the Flames
        Story.KillQuest(8186, "fireinvasion", "Living Shadowflame");

        // ShadeFire Colonel Clash
        Story.KillQuest(8187, "fireinvasion", "Shadefire Colonel");

        // Defense of Embersea
        Story.KillQuest(8188, "fireinvasion", "Living Shadowflame");

        // A Chilling Conflict
        Story.KillQuest(8189, "fireinvasion", "Onslaught Knight");

        // General Dismay
        Story.KillQuest(8190, "fireinvasion", "Shadefire General");

        //A Fallen Friend
        Story.KillQuest(8191, "fireinvasion", "Shadowflame Kyron");

        // Fire Fighting
        Story.KillQuest(8192, "fireinvasion", new[] { "Living Shadowflame|Onslaught Knight|Onslaught Knight|Shadowfire Corporal", "Shadefire Cavalry" });

        // Shadefires of War
        Story.KillQuest(8193, "wartraining", "Simulated Shadefire");

        // Extinguish the Shadowflame
        Story.MapItemQuest(8194, "wartraining", 8746, 4);

        // A Major Pain in the Neck
        Story.KillQuest(8195, "wartraining", "Simulated Major");

        // The Envoy of Fire
        Story.KillQuest(8196, "wartraining", "Simulated Elius");

        // Putting Out Small Fires
        Story.KillQuest(8197, "wartraining", "Simulated Fire");

        // Element - ary
        Story.KillQuest(8198, "wartraining", "Simulated Elemental");

        // A Dragonslayer's Past
        Story.KillQuest(8199, "wartraining", "Simulated Fire|Simulated Elemental");

        // The Champion of Fire
        Story.KillQuest(8200, "wartraining", "Fire Champion");

        // March of the Warfury
        Story.KillQuest(8201, "wartraining", "Warfury Soldier");

        // Elite It to Beat It
        Story.KillQuest(8202, "wartraining", "Warfury Elite");

        //The Goddess of War
        Story.KillQuest(8203, "wartraining", "Varga");

        // Warfury Training
        Story.KillQuest(8204, "wartraining", "Warfury Soldier");
    }
}