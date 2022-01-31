//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaTroll
{
    public CoreBots Core => CoreBots.Instance;

    public int questStart = 0;

    public string OptionsStorage = "SagaTroll";
    public bool DontPreconfigure = true;

    public List<IOption> Options = new List<IOption>()
    {
        new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
    };

    public static readonly int[] qIDs = 
    {
        /* -- /Join CrossRoads --                              1226,1227,1228,1229,1231,1274,1275,1276,1277,1278,1369,1370,1371,1372,1373,1374,1419,1420,1421,1422,1423,1451,1452,1453,1454,1455,1464,1465,1466,1467,1468*/
            1226, /* 0  - Horc Stink!                        */
        /* -- /Join BloodTusk --                             */
            1227, /* 1  - The Time Grows Closer              */
        /* -- /Join CrossRoads --                            */
            1228, /* 2  - Like Calls to Like                 */
            1229, /* 3  - Incense Makes Sense                */
            0   , /* 4  - She Who Answers 1                  */
            1231, /* 5  - The Troll Inside                   */
            0   , /* 6  - She Who Answers 2 - cutscene       */
        /* -- /Join BloodTuskWar --                          */
            0   , /* 7  - Bloodtusk War                      */
        /* -- /Join RavineTemple --                          */
            1274, /* 8  - Guarded Secrets, Hidden Treasures  */
            1275, /* 9  - Evidence of Chaos                  */
            1276, /* 10 - Learn More of the Ore              */
            1277, /* 11 - Too Little, Too Late. Still Needed */
            1278, /* 12 - Alliance Defiance                  */
        /* -- /Join Alliance --                              */
            1369, /* 13 - The Headquartes of Good and Evil   */
            1370, /* 14 - Treat Nullification, Good and Bad  */
            1371, /* 15 - Trap the Keepers                   */
            1372, /* 16 - Find What is Hidden Inside         */
            1373, /* 17 - Chaorruption Annihilation          */
            1374, /* 18 - Alliance Demotion                  */
        /* -- /Join AncientTemple --                         */
            1419, /* 19 - Contain the Chaorruption           */
            1420, /* 20 - Ancient Ointment                   */
            1421, /* 21 - Anoint the Ancients                */
            1422, /* 22 - Serpents Do No Harm                */
            1423, /* 23 - Though Nature Bars the Way         */
        /* -- /Join OreCavern --                             */
            1451, /* 24 - Descent Into Darkness              */
            1452, /* 25 - Out of the Darkness                */
            1453, /* 26 - Shine a Light on Deception         */
            1454, /* 27 - Save Yourself, Save the Soldiers   */
            1455, /* 28 - Battle the Baas!                   */
        /* -- /Join DreamNexus --                            */
            1464, /* 29 - Know the Nexus                     */
            1465, /* 30 - Secure a Route Home                */
            1466, /* 31 - DreamDancers' Orbs                 */
            1467, /* 32 - Master the Flames                  */
            1468  /* 33 - Choose: Khasaanda Confrontation?   */
    };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        questStart = bot.Config.Get<int>("startQuest");

        for (int i = questStart; i < qIDs.Length; i++)
        {
            bot.Config.Set("startQuest", i);
            Core.Logger($"Starting {i}");
            Core.EnsureAccept(qIDs[i]);
            switch(i)
            {
                case 0: //Horc Stink!
                    Core.GetMapItem(523, map: "bloodtusk");
                    Core.SmartKillMonster(qIDs[i], "bloodtusk", "Trollola Plant");
                    Core.SmartKillMonster(qIDs[i], "crossroads", "Chinchilizard");
                    break;
                case 1: //The Time Grows Closer
                    Core.SmartKillMonster(qIDs[i], "crossroads", new[] { "Lemurphant", "Koalion" });
                    break;
                case 2: //Like Calls to Like
                    Core.SmartKillMonster(qIDs[i], "crossroads", new[] { "Chinchilizard", "Lemurphant" });
                    Core.SmartKillMonster(qIDs[i], "bloodtusk", "Crystal-Rock");
                    break;
                case 3: //Incense Makes Sense
                    Core.GetMapItem(521, 10, "crossroads");
                    Core.SmartKillMonster(qIDs[i], "crossroads", new[] { "Lemurphant", "Koalion" });
                    Core.SmartKillMonster(qIDs[i], "bloodtusk", "Trollola Plant");
                    break;
                case 4: //She Who Answers 1
                    Core.Join("crossroads");
                    Core.Jump("r11", "Down");
                    bot.SendPacket("%xt%zm%tryQuestComplete%74343%1230%-1%false%wvz%");
                    bot.Sleep(2000);
                    break;
                case 5: //The Troll Inside
                    Core.GetMapItem(524, 10, "crossroads");
                    Core.GetMapItem(522, 5);
                    Core.SmartKillMonster(qIDs[i], "crossroads", new[] { "Lemurphant", "Koalion"});
                    Core.SmartKillMonster(qIDs[i], "bloodtusk", "Crystal-Rock");
                    break;
                case 6: //She Who Answers 2 - cutscene
                    Core.Join("crossroads");
                    Core.Jump("CutE", "Left");
                    bot.SendPacket("%xt%zm%tryQuestComplete%76051%1240%-1%false%wvz%");
                    break;
                case 7: //Bloodtusk War
                    Core.Join("bloodtuskwar");
                    Core.Jump("r7", "Left");
                    bot.Player.Kill("Chaotic Troll");
                    Core.Jump("Cut1", "Left");
                    bot.Sleep(2000);
                    bot.SendPacket("%xt%zm%tryQuestComplete%76390%1272%-1%false%wvz%");
                    break;
                case 8: //Guarded Secrets, Hidden Treasures
                    Core.GetMapItem(553, map: "ravinetemple");
                    break;
                case 9: //Evidence of Chaos
                    Core.GetMapItem(554, 5, "ravinetemple");
                    Core.GetMapItem(555, 10);
                    Core.GetMapItem(556, 10);
                    break;
                case 10: //Learn More of the Ore
                    Core.SmartKillMonster(qIDs[i], "ravinetemple", "*");
                    break;
                case 11: //Too Little, Too Late. Still Needed
                    Core.GetMapItem(557, 10, "ravinetemple");
                    Core.SmartKillMonster(qIDs[i], "ravinetemple", "*");
                    break;
                case 12: //Alliance Defiance
                    Core.SmartKillMonster(qIDs[i], "ravinetemple", "*");
                    break;
                case 13: //The Headquartes of Good and Evil
                    Core.GetMapItem(679, map: "alliance");
                    Core.GetMapItem(680);
                    break;
                case 14: //Treat Nullification, Good and Bad
                    Core.SmartKillMonster(qIDs[i], "alliance", new[] { "Good Soldier", "Evil Soldier" });
                    break;
                case 15: //Trap the Keepers
                    Core.GetMapItem(675, 10, "alliance");
                    break;
                case 16: //Find What is Hidden Inside
                    Core.GetMapItem(676, map: "alliance");
                    break;
                case 17: //Chaorruption Annihilation
                    Core.SmartKillMonster(qIDs[i], "alliance", "Chaorrupted Evil Soldier|Chaorrupted Good Soldier");
                    break;
                case 18: //Alliance Demotion
                    Core.SmartKillMonster(qIDs[i], "alliance", new[] { "General Cynari", "General Tibias" });
                    break;
                case 19: //Contain the Chaorruption
                    Core.SmartKillMonster(qIDs[i], "ancienttemple", "Chaotic Vulture|Chaotic Horcboar");
                    break;
                case 20: //Ancient Ointment
                    Core.GetMapItem(706, 7, "ancienttemple");
                    Core.SmartKillMonster(qIDs[i], "ancienttemple", "Chaotic Vulture|Chaotic Horcboar");
                    break;
                case 21: //Anoint the Ancients
                    Core.SmartKillMonster(qIDs[i], "ancienttemple", "Chaos Troll Spirit|Chaos Horc Spirit");
                    break;
                case 22: //Serpents Do No Harm
                    Core.SmartKillMonster(qIDs[i], "ancienttemple", "Serpentress");
                    break;
                case 23: //Though Nature Bars the Way
                    Core.GetMapItem(707, map: "ancienttemple");
                    break;
                case 24: //Descent Into Darkness
                    Core.GetMapItem(717, map: "orecavern");
                    break;
                case 25: //Out of the Darkness
                    Core.GetMapItem(719, 5, "orecavern");
                    Core.SmartKillMonster(qIDs[i], "orecavern", "Crashroom");
                    break;
                case 26: //Shine a Light on Deception
                    Core.GetMapItem(718, 5, "orecavern");
                    break;
                case 27: //Save Yourself, Save the Soldiers
                    Core.SmartKillMonster(qIDs[i], "orecavern", "Chaorrupted Evil Soldier|Chaorrupted Good Soldier");
                    break;
                case 28: //Battle the Baas!
                    Core.SmartKillMonster(qIDs[i], "orecavern", "Naga Baas");
                    break;
                case 29: //Know the Nexus
                    Core.GetMapItem(734, map: "dreamnexus");
                    Core.GetMapItem(735);
                    Core.GetMapItem(736);
                    Core.GetMapItem(737);
                    break;
                case 30: //Secure a Route Home
                    Core.SmartKillMonster(qIDs[i], "dreamnexus", new[] { "Dark Wyvern", "Dark Wyvern", "Aether Serpent", "Aether Serpent" });
                    break;
                case 31: //DreamDancers' Orbs
                    Core.GetMapItem(738, 10, "dreamnexus");
                    Core.GetMapItem(739, 11);
                    break;
                case 32: //Master the Flames
                    Core.SmartKillMonster(qIDs[i], "dreamnexus", new[] { "Solar Phoenix", "Solar Phoenix"});
                    break;
                case 33: //Choose: Khasaanda Confrontation?
                    Core.SmartKillMonster(qIDs[i], "dreamnexus", "Khasaanda");
                    break;
            }
            Core.EnsureComplete(qIDs[i]);
            Core.Logger($"Finished {i}");
            Core.Rest();
        }

        Core.SetOptions(false);
    }
}
