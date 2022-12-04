//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Other/MergeShops/CelestialChampMerge.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/CelestialArena.cs
using Skua.Core.Interfaces;
public class BurningBladeOfAbezeth
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
    //public CelestialChampion CC = new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetBBoA();

        Core.SetOptions(false);
    }

    public void GetBBoA()
    {
        if (Core.CheckInventory("Burning Blade Of Abezeth")) //Other method until it's fixed
            return;

        // CC.BuyAllMerge("Burning Blade Of Abezeth", mergeOptionsEnum.select);
        Core.EquipClass(ClassType.Solo);
        Bot.Quests.UpdateQuest(6042);
        Core.HuntMonster("celestialarenad", "Aranx", "Champion Sash", 20, isTemp: false);
        Adv.BuyItem("celestialarena", 1474, "Burning Blade Of Abezeth");
        Adv.EnhanceItem("Burning Blade Of Abezeth", EnhancementType.Lucky);
    }
}
