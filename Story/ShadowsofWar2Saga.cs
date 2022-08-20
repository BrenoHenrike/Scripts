//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class ShadowFlame2Saga
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();
        
        Core.SetOptions(false);
    }

    public void DoAll()
    {
        Story.PreLoad();
        
        Tyndarius();
        RuinedCrown();
        Timekeep();
        Placeholder();
    }

    public void Tyndarius()
    {
        if (Core.isCompletedBefore(8243))
            return;

        Adv.BestGear(GearBoost.Human);

        //War Medals
        Story.KillQuest(8125, "fireplanewar", "Shadowflame Soldier");

        // Mega War Medals
        Story.KillQuest(8126, "fireplanewar", "Shadowflame Soldier");

        // Shadowflame Un - slaught
        Story.KillQuest(8127, "fireplanewar", "Shadefire Onslaught");

        // Soldier On
        Story.KillQuest(8128, "fireplanewar", "Shadowflame Soldier");

        // Fanning the Flames
        Story.KillQuest(8129, "fireplanewar", "Shadowflame Soldier");
        Story.MapItemQuest(8129, "fireplanewar", 8523, 5);

        // Trailblazer
        Story.KillQuest(8130, "fireplanewar", "Shadefire Onslaught");

        // ShadowClaw
        Story.KillQuest(8131, "fireplanewar", "ShadowClaw");

        // In Their Element
        Story.KillQuest(8132, "fireplanewar", "Shadefire Elemental");

        // Cure the Fire
        Story.KillQuest(8133, "fireplanewar", "Living Shadowflame");
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
        Story.KillQuest(8138, "shadowfireplane", "Onslaught Knight");

        // Awaken Lady Fiamme
        Story.MapItemQuest(8139, "shadowfireplane", 8542);
        Bot.Sleep(5000);

        // Destroy the Barrier
        if (!Story.QuestProgression(8140))
        {
            Core.Join("shadowfireplane", "r6", "Left"); // for incase u start here
            Core.EnsureAccept(8140);
            Core.GetMapItem(8543);
            Core.KillMonster("shadowfireplane", "r6", "Left", "Shadow Wing", "Shadow Flamewing Defeated", 2);
            Core.KillMonster("shadowfireplane", "r6", "Left", "Shadowfire Summoner", "Shadowfire Summoner Defeated", 1);
            Core.EnsureComplete(8140);
        }

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
        Story.KillQuest(8192, "fireinvasion", new[] { "Living Shadowflame", "Shadefire Cavalry" });

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
        Story.KillQuest(8199, "wartraining", "Simulated Fire");

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

        //Colonel Vanguard 8233
        Story.KillQuest(8233, "fireavatar", "Shadefire Colonel");

        //In a Major Way 8234
        Story.KillQuest(8234, "fireavatar", "Shadefire Major");

        //Elemental Backup 8235
        Story.MapItemQuest(8235, "fireavatar", 8859, 4);

        //The Cavalry's Here 8236
        Story.KillQuest(8236, "fireavatar", "Shadefire Cavalry");

        //Path of Flame 8237
        Story.KillQuest(8237, "fireavatar", "Shadefire Colonel");

        //The Envoy of Fire 8238
        Story.KillQuest(8238, "fireavatar", "Elius");

        //Living in Shadowflame 8239
        Story.KillQuest(8239, "fireavatar", "Living Shadowflame");

        //Elemental of Surprise 8240
        Story.KillQuest(8240, "fireavatar", "Shadefire Elemental");

        //Well That’s New 8241
        Story.KillQuest(8241, "fireavatar", "Shadow Lava");

        //Thermal Energy 8242
        Story.KillQuest(8242, "fireavatar", "Shadow Lava");

        //Avatar of Fire 8243
        Story.KillQuest(8243, "fireavatar", new[] { "Avatar Tyndarius", "Fire Orb" });
    }

    public void RuinedCrown()
    {
        if (Core.isCompletedBefore(8787))
            return;

        if (!Bot.Quests.IsUnlocked(8778))
            Tyndarius();

        // 8778 Mental Damage Sponge
        Story.MapItemQuest(8778, "ruinedcrown", new[] { 10380, 10382, 10383 });

        // 8779 Scraping the Barrel
        Story.KillQuest(8779, "ruinedcrown", new[] { "Mana-Burdened Minion", "Mana-Burdened Knight" });

        // 8780 Fractals
        Story.MapItemQuest(8780, "ruinedcrown", 10384, 6);

        // 8781 Blind Retaliation
        Story.KillQuest(8781, "ruinedcrown", "Mana-Burdened Mage");

        // 8782 Deafening Silence
        Story.KillQuest(8782, "ruinedcrown", new[] { "Mana-Burdened Minion", "Mana-Burdened Knight" });

        // 8784 Stilled Mind (Yes 8784 before 8783)
        Story.MapItemQuest(8784, "ruinedcrown", 10385, 6);

        // 8783 Volatile Nature
        Story.KillQuest(8783, "ruinedcrown", "Frenzied Mana");

        // 8785 Heartache
        Story.KillQuest(8785, "ruinedcrown", "Mana-Burdened Mage");

        // 8786 Clouded Vision
        Story.MapItemQuest(8786, "ruinedcrown", 10386);
        Story.KillQuest(8786, "ruinedcrown", "Frenzied Mana");

        // 8787 Guilt Complex
        Story.KillQuest(8787, "ruinedcrown", "Calamitous Warlic");

        // 8788 Em-pathetic Connection (Merge Shop Quest)
        Core.EnsureAccept(8788);
        Core.HuntMonster("ruinedcrown", "Frenzied Mana", "Mana Residue", 8);
        Core.HuntMonster($"ruinedcrown", "Mana-Burdened Mage", "Mage’s Blood Sample", 8);
        Core.HuntMonster($"ruinedcrown", "Calamitous Warlic", "Warlic’s Favor");
        Core.EnsureComplete(8788);
        // I'm going to assume they will fix the ’ to a normal ' one day so this will need to be fixed then (:
    }

    public void Timekeep()
    {
        if (Core.isCompletedBefore(8813))
            return;

        if (!Bot.Quests.IsUnlocked(8803))
            RuinedCrown();

        // 8803|Mood Pendulum
        Story.MapItemQuest(8803, "Timekeep", new[] { 10455, 10456, 10457 });

        // 8804|Bug Issue
        Story.KillQuest(8804, "Timekeep", "Decaying Locust");

        // 8805|Advanced Age
        Story.KillQuest(8805, "Timekeep", "Distorted Imp");

        // 8806|One Singularity
        Story.MapItemQuest(8806, "Timekeep", 10458, 6);

        // 8807|Canaries on Edge
        Story.KillQuest(0880700, "Timekeep", "Mana-Burdened Mage");

        // 8808|Caution! Wet Floor
        Story.MapItemQuest(8808, "Timekeep", new[] { 10459, 10460 });

        // 8809|Reflection in the Puddle
        Story.KillQuest(8809, "Timekeep", "Mumbler");

        // 8810|Meniscus Point
        Story.KillQuest(8810, "Timekeep", new[] { "Distorted Imp", "Mana-Burdened Mage" });

        // 8811|Distractions
        Story.KillQuest(8811, "Timekeep", new[] { "Mumbler", "Decaying Locust" });

        // 8812|Dog Bites Back
        Story.KillQuest(8812, "Timekeep", "Mal-formed Gar");

        // 8813|Janitorial Duties
        Story.KillQuest(8813, "Timekeep", new[] { "Mal-formed Gar", "Mumbler", "Decaying Locust" });
    }

    public void Placeholder()
    {
        Core.Logger("last part of the saga hasnt been released yet. will update when released");
        return;
    }
}