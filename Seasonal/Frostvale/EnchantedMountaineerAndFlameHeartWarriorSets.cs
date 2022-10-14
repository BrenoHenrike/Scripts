//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Frostvale/Frostvale.cs
using Skua.Core.Interfaces;
public class EnchantedMountaineerAndFlameHeartWarriorSets
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public Frostvale Frostvale = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        QuestFarming();

        Core.SetOptions(false);
    }

    public void QuestFarming()
    {
        if (!Core.isSeasonalMapActive("BrightLights"))
            return;
        Frostvale.BrightLights();

        string[] Rewards = (Core.EnsureLoad(8176).Rewards.Select(i => i.Name).ToArray());
        Core.AddDrop(Rewards);

        int i = 0;

        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards, toInv: false))
        {
            //Bright Lights Festival Rewards 8176
            Core.EnsureAccept(8176);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("BrightLights", "Fire Imp", "Fallen Stars", 25, log: false);
            Core.EnsureComplete(8176);
            i++;

            if (i % 5 == 0)
            {
                Core.JumpWait();
                Core.ToBank(Rewards);
            }
        }
        Core.ToBank(Rewards);

    }
}
// Enchanted Mountaineer
// Enchanted Mountaineer's Hood + Goggles
// Enchanted Moutaineer's Masked Hood + Goggles
// Enchanted Mountaineer's Backpack
// Enchanted Mountaineer's Ice Axe
// Dual Enchanted Ice Axes 
// FlameHeart Warrior
// FlameHeart Warrior's Hair
// FlameHeart Warrior's Locks
// FlameHeart Warrior's Flaming Locks
// FlameHeart Warrior's Flaming Hair
// FlameHeart Warrior's Helm
// FlameHeart Warrior's Helm + Locks
// FlameHeart Warrior's Wings
// FlameHeart Warrior's Fire Wings
// FlameHeart Warrior's Closed Wings

