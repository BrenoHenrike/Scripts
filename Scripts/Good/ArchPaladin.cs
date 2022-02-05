//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Good/Paladin.cs
//cs_include Scripts/Story/XansLair.cs
//cs_include Scripts/Story/DoomVault.cs
using RBot;

public class ArchPaladin
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreBLOD BLOD = new CoreBLOD();
    public CoreDailys Daily = new CoreDailys();
    public Paladin Pal = new Paladin();
    public XansLair Xan = new XansLair();
    public DoomVaultA DVA = new DoomVaultA();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetAP();

        Core.SetOptions(false);
    }

    public void GetAP(bool rankUpClass = true)
    {
        if (Core.CheckInventory("ArchPaladin"))
            return;

        Farm.GoodREP();
        if (!Core.IsMember && !Bot.Quests.IsUnlocked(5467))
        {
            // A Strong Base
            if (!Core.QuestProgression(5463))
            {
                Core.BuyQuest(5463, "temple", 288, "Stone Paladin Armor");
                Farm.Gold(500000);
                Core.BuyQuest(5463, "darkthronehub", 1308, "Exalted Paladin Seal");
            }
            // Proof of Valor
            if (!Core.QuestProgression(5464))
            {
                Core.EnsureAccept(5464);
                BLOD.UnlockMineCrafting();
                DVA.StoryLine();
                Farm.BattleUnderB("Undead Energy", 1000);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("doomvault", "Binky", "Binky's Uni-horn", isTemp: false, publicRoom: true);
                Core.HuntMonster("banished", "Desterrat Moya", "Desterrat Moya Tentacle", publicRoom : true);
                Core.HuntMonster("dreadhaven", "Dreadhaven General", "Dreadhaven Helm");

                string[] DOTClasses = {
                    "ShadowStalker of Time",
                    "ShadowWeaver of Time",
                    "ShadowWalker of Time",
                    "Infinity Knight",
                    "Interstellar Knight",
                    "Void HighLord",
                    "Dragon of Time",
                    "Timeless Dark Caster",
                    "Frostval Barbarian",
                    "Blaze Binder",
                    "DeathKnight"
                };
                foreach (string Class in DOTClasses)
                {
                    if (Core.CheckInventory(Class))
                    {
                        Bot.Player.EquipItem("Class");
                        Farm.rankUpClass(Class);
                        break;
                    }
                }
                Core.HuntMonster("doomkitten", "DoomKitten", "DoomKitten Claw", publicRoom: true);

                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("vordredboss", "Vordred", "Vordred's Skull", isTemp: false, publicRoom : true);
                Core.EnsureComplete(5464);
            }
            // Mastering the Arcane
            if (!Core.QuestProgression(5465))
            {
                Core.EnsureAccept(5465);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("xantown", "Xan", "Pyromancer Artifact", isTemp: false);
                Core.HuntMonster("dragonheart", "Proto-Air Dracolich", "Zephyrus Manifesto", isTemp: false, publicRoom: true);
                Core.HuntMonster("northstar", "Karok the Fallen", "Karok's Glaceran Gem", isTemp: false, publicRoom: true);
                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("thirdspell", "Mana Phoenix", "Nightmare Kibble", 200, false);
                Core.HuntMonster("thunderfang", "Lightning Ball", "Condensed Energy", isTemp: false);
                Core.KillMonster("downward", "r11", "Right", "Crystal Mana Construct", "Crystallised Mana Catalyst", isTemp: false);
                Core.HuntMonster("farm", "Treeant", "Just the Perfect Stick", isTemp: false);
            }
            // For Those Who Have Fallen
            if (!Core.QuestProgression(5466))
            {
                Core.EnsureAccept(5466);
                Core.BuyItem("castle", 88, "Holy Hand Grenade");
                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("manor", "Bird of Paradise", "Feather of Paradise", 20, false);
                Core.KillMonster("doomwood", "r6", "Right", "*", "Shoelace of a Fallen Paladin", 77, false);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("fotia", "Amia the Cult Leader", "Eternity Flame", isTemp: false);
                Core.EnsureComplete(5466);
            }
        }
        else if (Core.IsMember && !Bot.Quests.IsUnlocked(5467))
        {
            if (!Core.CheckInventory("Silver Paladin"))
                Pal.GetPaladin(false);
            // Legendary|Silver Paladin's Vow
            Core.ChainQuest(Core.CheckInventory("Silver Paladin") ? 5475 : 5471);
            // Legendary|Silver Paladin's Oath
            Core.ChainQuest(Core.CheckInventory("Silver Paladin") ? 5476 : 5472);
            // Legendary|Silver Paladin's Pledge
            Core.ChainQuest(Core.CheckInventory("Silver Paladin") ? 5477 : 5473);
            // Aptitude Test
            if (!Core.QuestProgression(Core.CheckInventory("Silver Paladin") ? 5478 : 5474, FollowupIDOverwrite: 5467))
            {
                Core.EnsureAccept(Core.CheckInventory("Silver Paladin") ? 5478 : 5474);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("bosschallenge", "Colossal Primarch", "Primarch's Hilt", isTemp: false, publicRoom: true);
                Farm.Gold(500000);
                Core.BuyItem("darkthronehub", 1308, "Exalted Paladin Seal");
                Core.HuntMonster("timevoid ", "Unending Avatar", "Condensed Mana", isTemp: false, publicRoom : true);
                Core.EnsureComplete(Core.CheckInventory("Silver Paladin") ? 5478 : 5474);
            }
        }
        
        // Commandment
        if (!Core.QuestProgression(5467))
        {
            Core.EnsureAccept(5467);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("brightfall", "Painadin Overlord", "Skill Observed", isTemp: false, publicRoom : true);
            Core.HuntMonster("citadel", "Grant Inquisitor", "Spirit of Vindication", isTemp: false);
            Core.HuntMonster("alliance", "Good Lieutenant", "Radiant Blade Enhancement", isTemp: false);
            Core.EnsureComplete(5467);
        }
        // Hymn of Light
        if (!Core.QuestProgression(5468))
        {
            Core.EnsureAccept(5468);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("poisonforest", "Xavier Lionfang", "Divine Elixir", isTemp: false);
            Core.HuntMonster("ultraalteon", "Ultra Alteon", "Prayer of Salvation", isTemp: false, publicRoom: true);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("newfinale ", "Chaos Healer", "Acolyte's Braille", isTemp: false);
            Core.HuntMonster("skytower ", "Doves", "Innocence", 25, false);
            Core.EnsureComplete(5468);
        }
        // Righteous Seal
        if (!Core.QuestProgression(5469))
        {
            Core.EnsureAccept(5469);
            Xan.DoAll();
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("xancave", "Shurpu Ring Guardian", "Fists of Fire", isTemp: false, publicRoom: true);
            if (!Core.CheckInventory("Scroll of Ethereal Slumber"))
            {
                if (!Core.CheckInventory("Archmage Ink"))
                {
                    if (Core.CheckInventory("Mystic Shards", 2))
                        Core.BuyItem("spellcraft", 549, "Archmage Ink", 1, 5);
                    else
                    {
                        if (!Core.CheckInventory("Arcane Quill"))
                        {
                            Core.BuyItem("spellcraft", 693, "Gold Voucher 500k");
                            Core.BuyItem("spellcraft", 693, "Arcane Quill", 1, 10, 8847);
                        }
                        Core.BuyItem("spellcraft", 622, "Archmage Ink", 1, 5);
                    }
                }
                Core.AddDrop("Scroll of Ethereal Slumber");
                Core.ChainComplete(2347);
            }
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("onslaughttower", "Golden Caster", "Holy Magic Attunement", isTemp: false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("palace", "Pettivox", "Ring of Mana Transposition", isTemp: false);
            Core.EnsureComplete(5469);
        }
        // Sacred Magic: Eden
        if (!Core.QuestProgression(5470, hasFollowup: false))
        {
            Core.EnsureAccept(5470);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("seraph", "Adventus", "Sacred Tome", isTemp: false);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("marsh", "Marsh Trees", "Paladaffodil", 25, false);
            Core.HuntMonster("wanders", "Lotus Spiders", "Spirit Lotus", 25, false);
            Core.HuntMonster("bloodtusk", "Trollola Plants", "Radiant Magnolia", 25, false);
            Core.HuntMonster("gaiazor", "Wisterrora", "Cyanoblossom", 25, false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("gaiazor", "Nevanna", "BrightOak Forest Sapling", isTemp: false);
            Core.HuntMonster("infernalspire", "Malxas", "Forbidden Demon Seal", isTemp: false);
            Core.EnsureComplete(5470);
        }

        Core.BuyItem("darkthronehub", 1303, "ArchPaladin", shopItemID: 21833);
        if (rankUpClass)
            Farm.rankUpClass("ArchPaladin");
    }
}
