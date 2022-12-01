//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/Paladin.cs
//cs_include Scripts/Story/XansLair.cs
using Skua.Core.Interfaces;

public class ArchPaladin
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreBLOD BLOD = new CoreBLOD();
    public Paladin Pal = new Paladin();
    public XansLair Xan = new XansLair();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAP();

        Core.SetOptions(false);
    }

    public void GetAP(bool rankUpClass = true)
    {
        if (Core.CheckInventory(36920))
            return;

        Story.PreLoad(this);

        Farm.GoodREP();
        if (!Core.IsMember && !Bot.Quests.IsUnlocked(5467))
        {
            // A Strong Base
            if (!Story.QuestProgression(5463))
            {
                Story.BuyQuest(5463, "temple", 288, "Stone Paladin Armor");
                Farm.Gold(500000);
                Story.BuyQuest(5463, "darkthronehub", 1308, "Exalted Paladin Seal");
            }

            // Proof of Valor
            if (!Story.QuestProgression(5464))
            {
                Core.EnsureAccept(5464);
                BLOD.UnlockMineCrafting();
                Farm.BattleUnderB("Undead Energy", 1000);
                Core.EquipClass(ClassType.Solo);
                Bot.Quests.UpdateQuest(3008);
                Adv.KillUltra("doomvault", "r5", "Left", "Binky", "Binky's Uni-horn", isTemp: false, publicRoom: true);
                Core.HuntMonster("banished", "Desterrat Moya", "Desterrat Moya Tentacle", publicRoom: true);
                Core.HuntMonster("dreadhaven", "Dreadhaven General", "Dreadhaven Helm");
                Adv.GearStore();
                Core.KillDoomKitten("DoomKitten Claw", isTemp: true);
                Adv.GearStore(true);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("vordredboss", "Vordred", "Vordred's Skull", isTemp: false, publicRoom: true);
                Core.EnsureComplete(5464);
            }

            // Mastering the Arcane
            if (!Story.QuestProgression(5465))
            {
                Core.EnsureAccept(5465);
                Core.EquipClass(ClassType.Solo);

                Core.KillMonster("xantown", "r8", "Left", "Xan", "Pyromancer Artifact", isTemp: false);
                if (Bot.Map.Name == "xantown")
                    Core.Jump("r12", "Left"); // map is aggro af this is a safe cell.

                Core.HuntMonster("dragonheart", "Proto-Air Dracolich", "Zephyrus Manifesto", isTemp: false);
                Core.HuntMonster("northstar", "Karok the Fallen", "Karok's Glaceran Gem", isTemp: false, publicRoom: true);
                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("thirdspell", "Mana Phoenix", "Nightmare Kibble", 200, false);
                Core.HuntMonster("thunderfang", "Lightning Ball", "Condensed Energy", isTemp: false);
                Core.KillMonster("downward", "r11", "Right", "Crystal Mana Construct", "Crystallized Mana Catalyst", isTemp: false);
                Core.HuntMonster("farm", "Treeant", "Just the Perfect Stick", isTemp: false);
                Core.EnsureComplete(5465);
            }

            // For Those Who Have Fallen
            if (!Story.QuestProgression(5466))
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
            Story.ChainQuest(Core.CheckInventory("Silver Paladin") ? 5475 : 5471);
            // Legendary|Silver Paladin's Oath
            Story.ChainQuest(Core.CheckInventory("Silver Paladin") ? 5476 : 5472);
            // Legendary|Silver Paladin's Pledge
            Story.ChainQuest(Core.CheckInventory("Silver Paladin") ? 5477 : 5473);
            // Aptitude Test
            if (!Story.QuestProgression(Core.CheckInventory("Silver Paladin") ? 5478 : 5474))
            {
                Core.EnsureAccept(Core.CheckInventory("Silver Paladin") ? 5478 : 5474);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("bosschallenge", "Colossal Primarch", "Primarch's Hilt", isTemp: false, publicRoom: true);
                Farm.Gold(500000);
                Core.BuyItem("darkthronehub", 1308, "Exalted Paladin Seal");
                Core.HuntMonster("timevoid", "Unending Avatar", "Condensed Mana", isTemp: false, publicRoom: true);
                Core.EnsureComplete(Core.CheckInventory("Silver Paladin") ? 5478 : 5474);
            }
        }

        // Commandment
        if (!Story.QuestProgression(5467))
        {
            Core.EnsureAccept(5467);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("brightfall", "Painadin Overlord", "Skill Observed", isTemp: false, publicRoom: true);
            Core.HuntMonster("citadel", "Grand Inquisitor", "Spirit of Vindication", isTemp: false);
            Core.HuntMonster("alliance", "Good Lieutenant", "Radiant Blade Enhancement", isTemp: false);
            Core.EnsureComplete(5467);
        }

        // Hymn of Light
        if (!Story.QuestProgression(5468))
        {
            Core.EnsureAccept(5468);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("poisonforest", "Xavier Lionfang", "Divine Elixir", isTemp: false);
            Core.HuntMonster("ultraalteon", "Ultra Alteon", "Prayer of Salvation", isTemp: false, publicRoom: true);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("newfinale", "Chaos Healer", "Acolyte's Braille", isTemp: false);
            Core.HuntMonster("skytower", "Dove", "Innocence", 25, false);
            Core.EnsureComplete(5468);
        }

        // Righteous Seal
        if (!Story.QuestProgression(5469))
        {
            Core.AddDrop("Scroll of Ethereal Slumber");
            Core.EnsureAccept(5469);
            Xan.DoAll();
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("xancave", "Shurpu Ring Guardian", "Fists of Fire", isTemp: false, publicRoom: true);
            if (!Core.CheckInventory("Scroll of Ethereal Slumber"))
            {
                if (!Core.CheckInventory("Archmage Ink"))
                {
                    Farm.SpellCraftingREP(8);
                    if (Core.CheckInventory("Mystic Shards", 2))
                        Core.BuyItem("spellcraft", 549, "Archmage Ink", 1);
                    else
                    {
                        if (!Core.CheckInventory("Arcane Quill"))
                        {
                            Farm.Gold(100000);
                            Core.BuyItem("spellcraft", 693, "Gold Voucher 100k");
                            Core.BuyItem("spellcraft", 693, 17391, 1, 8846); //Arcane Quill x1
                        }
                        Core.BuyItem("spellcraft", 622, "Archmage Ink", 1);
                    }
                }
                if (!Core.CheckInventory("Archmage Ink"))
                {
                    Core.HuntMonster("underworld", "Skull Warrior", "Mystic Shards", 2, false);
                    Core.BuyItem("dragonrune", 549, "Archmage Ink", 1);
                }
                Core.ChainComplete(2347);
                Bot.Wait.ForPickup("Scroll of Ethereal Slumber");
                Core.SellItem("Archmage Ink", all: true);
                Core.SellItem("Mystic Shards", all: true);
            }
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("onslaughttower", "Golden Caster", "Holy Magic Attunement", isTemp: false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("palace", "Pettivox", "Ring of Mana Transposition", isTemp: false);
            Core.EnsureComplete(5469);
            Core.SellItem("Scroll of Ethereal Slumber", all: true);
        }

        // Sacred Magic: Eden
        if (!Story.QuestProgression(5470))
        {
            Core.EnsureAccept(5470);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("seraph", "Adventus", "Sacred Tome", isTemp: false);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("marsh", "Marsh Tree", "Paladaffodil", 25, false);
            Core.KillMonster("wanders", "r2", "Up", "Lotus Spider", "Spirit Lotus", 25, false);
            Core.HuntMonster("bloodtusk", "Trollola Plant", "Radiant Magnolia", 25, false);
            Core.HuntMonster("gaiazor", "Wisterrora", "Cyanoblossom", 25, false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("gaiazor", "Nevanna", "BrightOak Forest Sapling", isTemp: false);
            Core.HuntMonster("infernalspire", "Malxas", "Forbidden Demon Seal", isTemp: false);
            Core.EnsureComplete(5470);
        }

        Core.BuyItem("darkthronehub", 1303, "ArchPaladin", shopItemID: 21833);
        if (rankUpClass)
        {
            Adv.GearStore();
            Core.Equip("ArchPaladin");
            Adv.rankUpClass("ArchPaladin");
            Adv.GearStore(true);
        }
    }
}
