//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Hollowborn/HollowbornPaladin/CoreHollowbornPaladin.cs
//cs_include Scripts/Hollowborn/HollowbornDoomKnight/CoreHollowbornDoomKnight.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Evil/NecroticSwordOfDoom.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Chaos/DrakathArmorBot.cs
//cs_include Scripts/Chaos/AscendedDrakathGear.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Story/Artixpointe.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Story/BattleUnder.cs

using RBot;

public class ADKFalls
{    
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreHollowbornDoomKnight HDK = new CoreHollowbornDoomKnight();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        HDK.ADKFalls();

        Core.SetOptions(false);
    }
}