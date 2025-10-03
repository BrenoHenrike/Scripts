using System;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Skua.Core.Interfaces;
using Skua.Core.Models.Auras;
using Skua.Core.Models.Players;
using Skua.Core.Models.Factions;
using Skua.Core.Models.Quests;
using Skua.Core.Models.Items;
using Skua.Core.Models.Shops;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Skills;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class CoreUltras
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    readonly ConcurrentDictionary<string, object> _cache = new();
    readonly ConcurrentDictionary<string, DateTime> _throttle = new();

    CancellationTokenSource _cts;
    Task _runSkills;

    public TimeSpan ThrottleDuration { get; set; } = TimeSpan.FromSeconds(3);
    public event Action<string, string> OnSignal;

    #region Settings

    int D1 = 250;
    int D2 = 700;
    int D3 = 1400;
    int D4 = 2800;

    public void Boot()
    {
        if (_runSkills?.Status == TaskStatus.Running)
            return;

        OnSignal += (category, message) => { Bot.Log($"[{category}] {message}"); };

        Bot.Events.ScriptStopping += OnScriptStopping;
        Bot.UltraBossHelper.DisableCounterAttack();

        _cts = new CancellationTokenSource();
        _runSkills = Task.Run(() => SkillsAsync(_cts.Token));

        StopAttack();

        if (Bot.Bank.Items is null)
            Bot.Bank.Load();
        Bot.Options.SafeTimings = true;
        Bot.Options.InfiniteRange = true;
        Bot.Options.SkipCutscenes = true;
        Bot.Lite.HidePlayers = true;

        Alert("CORE", "System online");
    }

    bool OnScriptStopping(Exception e)
    {
        Alert("CORE", "System offline");

        Bot.Lite.HidePlayers = false;
        Bot.Events.ExtensionPacketReceived -= ChargeListener;

        _cts?.Cancel();
        _runSkills?.Wait(TimeSpan.FromSeconds(2));

        OnSignal = null;

        _cache.Clear();
        _throttle.Clear();

        _cts?.Dispose();
        _runSkills?.Dispose();

        return true;
    }

    #endregion

    #region Items

    private void ForItemCore(string monsters, string map, int quantity, bool isTemp, bool useBestGear, bool alt, string? cell, string pad, bool priority, Action ensureInBank, Func<int> ownedCount, Action pickup, string itemLabel)
    {
        if (quantity <= 0) return;

        if (!string.IsNullOrWhiteSpace(map)) Join(map);

        ChooseBestCell(monsters, alt, cell, pad);
        if (useBestGear) ChooseBestGear(monsters);

        var m = monsters?.Split('|', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
        ensureInBank();

        if (ownedCount() >= quantity) return;

        Alert("FARMING", $"Killing {monsters} for {quantity}x {itemLabel}");
        EnableSkills();

        int i = 0;
        while (!Bot.ShouldExit)
        {
            //if (_chargeDetected) UsePotion();

            if (ownedCount() >= quantity)
            {
                Alert("SUCCESS", $"Acquired {quantity}x {itemLabel}");
                DisableSkills();
                StopAttack();
                return;
            }

            pickup();

            if (m.Length > 0)
            {
                if (priority)
                    KillWithPriority(m.Select(MonsterKey.FromName).ToArray());
                else
                    Kill(m[i++ % m.Length]);
            }
        }
    }

    public void ForItem(string monsters, string map, string name, int quantity = 1, bool isTemp = false, bool useBestGear = false, bool alt = false, string? cell = null, string pad = "Left", bool priority = false)
    {
        if (string.IsNullOrWhiteSpace(name) || quantity <= 0) return;
        ForItemCore(
            monsters, map, quantity, isTemp, useBestGear, alt, cell, pad, priority,
            ensureInBank: () => { if (!isTemp) InBank(name); },
            ownedCount: () => Owned(name, isTemp),
            pickup: () => PickupItems(name),
            itemLabel: name
        );
    }

    public void ForItem(string monsters, string map, int itemId, int quantity = 1, bool isTemp = false, bool useBestGear = false, bool alt = false, string? cell = null, string pad = "Left", bool priority = false)
    {
        if (itemId <= 0 || quantity <= 0) return;
        var itemLabel = GetDropItem(itemId)?.Name ?? $"Item#{itemId}";
        ForItemCore(
            monsters, map, quantity, isTemp, useBestGear, alt, cell, pad, priority,
            ensureInBank: () => { if (!isTemp) InBank(itemId); },
            ownedCount: () => Owned(itemId, isTemp),
            pickup: () => PickupItems(itemId),
            itemLabel: itemLabel
        );
    }

    public void EquipBestClass(List<(string name, int rank)> priorities)
    {
        if (priorities?.Count == 0) return;

        bool isMaxRank = IsCurrentClassMaxRank();

        foreach (var (name, rank) in priorities)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) || !Owned(name, 1)) continue;

                if (HasClassEquipped(name) && (isMaxRank || Bot.Player.CurrentClassRank >= rank))
                    return;

                if (!Bot.Inventory.IsEquipped(name))
                {
                    Bot.Inventory.EquipItem(name);
                    Bot.Sleep(D3);
                    return;
                }
            }
            catch { continue; }
        }
    }

    public void EquipBestClass(List<(int id, int rank)> priorities)
    {
        if (priorities?.Count == 0) return;

        bool isMaxRank = IsCurrentClassMaxRank();

        foreach (var (id, rank) in priorities)
        {
            try
            {
                var item = Bot.Inventory.Items.FirstOrDefault(i => i?.ID == id);
                if (item == null) continue;

                if (HasClassEquipped(item.Name) && (isMaxRank || Bot.Player.CurrentClassRank >= rank))
                    return;

                if (!Bot.Inventory.IsEquipped(id))
                {
                    Bot.Inventory.EquipItem(id);
                    Bot.Sleep(D3);
                    return;
                }
            }
            catch { continue; }
        }
    }

    public bool InBank(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || !Bot.Bank.Contains(name)) return false;
        try
        {
            StopAttack();
            Bot.Bank.ToInventory(name);
            Bot.Sleep(D2);
            return true;
        }
        catch { return false; }
    }

    public bool InBank(int id)
    {
        if (id <= 0 || !Bot.Bank.Contains(id)) return false;
        try
        {
            StopAttack();
            Bot.Bank.ToInventory(id);
            Bot.Sleep(D2);
            return true;
        }
        catch { return false; }
    }

    public bool ToBank(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || !Bot.Inventory.Contains(name)) return false;
        try
        {
            StopAttack();
            Bot.Inventory.ToBank(name);
            Bot.Sleep(D2);
            return true;
        }
        catch { return false; }
    }

    public bool ToBank(int id)
    {
        if (id <= 0 || !Bot.Inventory.Contains(id)) return false;
        try
        {
            StopAttack();
            Bot.Inventory.ToBank(id);
            Bot.Sleep(D2);
            return true;
        }
        catch { return false; }
    }

    public int Owned(string name, bool isTemp = false)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name)) return 0;
            return isTemp ? Bot.TempInv?.GetQuantity(name) ?? 0 : Bot.Inventory?.GetQuantity(name) ?? 0;
        }
        catch { return 0; }
    }

    public int Owned(int id, bool isTemp = false)
    {
        try
        {
            if (id <= 0) return 0;
            return isTemp ? Bot.TempInv?.GetQuantity(id) ?? 0 : Bot.Inventory?.GetQuantity(id) ?? 0;
        }
        catch { return 0; }
    }

    public bool Owned(string name, int quantity, bool isTemp = false) => Owned(name, isTemp) >= quantity;

    public bool Owned(int id, int quantity, bool isTemp = false) => Owned(id, isTemp) >= quantity;

    #endregion

    #region Find Item by Enhancement

    const int D = 500;

    public InventoryItem ChooseBestEnhancementFor(string itemGroup, params string[] priority)
    {
        if (priority?.Length == 0) return null;

        itemGroup = NormalizeItemGroup(itemGroup);
        bool isMember = Bot?.Player?.IsMember == true;

        foreach (var name in priority.Where(n => !string.IsNullOrWhiteSpace(n)))
        {
            var invHit = Find(Bot.Inventory.Items?.OfType<InventoryItem>(), name, isMember);
            if (TryEquip(invHit)) return invHit;

            var bankHit = Find(Bot.Bank.Items?.OfType<InventoryItem>(), name, isMember);
            if (bankHit != null)
            {
                ToBank(bankHit.Name);
                Bot.Sleep(D);
                if (TryEquip(Find(Bot.Inventory.Items?.OfType<InventoryItem>(), name, isMember)))
                    return Bot.Inventory.Items.FirstOrDefault(i => i != null && Bot.Inventory.IsEquipped(i.ID));
            }
            Bot.Sleep(D);
        }

        Alert("Enhancement", $"No {itemGroup} found with enhancements: {string.Join(", ", priority)}");
        return null;

        InventoryItem Find(System.Collections.Generic.IEnumerable<InventoryItem> src, string enhName, bool isMember) =>
            (src ?? System.Linq.Enumerable.Empty<InventoryItem>())
            .Where(i => i?.ItemGroup?.Equals(itemGroup, StringComparison.OrdinalIgnoreCase) == true &&
                        MatchesEnhancement(i, enhName))
            .FirstOrDefault(i => isMember || !i.Upgrade);

        bool TryEquip(InventoryItem it)
        {
            if (it == null) return false;
            if (Bot.Inventory.IsEquipped(it.ID)) return true;
            for (int t = 0; t < 3; t++)
            {
                Bot.Inventory.EquipItem(it.ID);
                Bot.Sleep(D);
                if (Bot.Inventory.IsEquipped(it.ID)) return true;
            }
            return false;
        }
    }

    private static bool MatchesEnhancement(InventoryItem i, string name) =>
        name != null && (EnhancementName(i?.EnhancementPatternID ?? -1)?.Equals(name, StringComparison.OrdinalIgnoreCase) == true ||
                         WeaponTrait(i?.ProcID ?? -1)?.Equals(name, StringComparison.OrdinalIgnoreCase) == true);

    public static string EnhancementName(int id) => id switch
    {
        1 => "Adventurer",
        2 => "Fighter",
        3 => "Thief",
        4 => "Armsman",
        5 => "Hybrid",
        6 => "Wizard",
        7 => "Healer",
        8 => "Spellbreaker",
        9 => "Lucky",
        10 => "Forge",
        11 => "Absolution",
        12 => "Avarice",
        23 => "Depths",
        24 => "Vainglory",
        25 => "Vim",
        26 => "Examen",
        27 => "Pneuma",
        28 => "Anima",
        29 => "Penitence",
        30 => "Lament",
        32 => "Hearty",
        _ => null
    };

    public static string WeaponTrait(int id) => id switch
    {
        2 => "Spiral Carve",
        3 => "Awe Blast",
        4 => "Health Vamp",
        5 => "Mana Vamp",
        6 => "Powerword Die",
        7 => "Lacerate",
        8 => "Smite",
        9 => "Valiance",
        10 => "Arcana's Concerto",
        11 => "Acheron",
        12 => "Elysium",
        13 => "Praxis",
        14 => "Dauntless",
        15 => "Ravenous",
        _ => null
    };

    private string NormalizeItemGroup(string g) => g?.ToLower() switch
    {
        "weapon" => "Weapon",
        "helm" or "he" => "he",
        "back" or "ba" or "cape" => "ba",
        "class" or "co" => "co",
        "pet" or "pe" => "pe",
        _ => g
    };

    #endregion

    #region Combat

    public record MonsterKey(int? MapId = null, string? Name = null, int? Id = null)
    {
        public static MonsterKey FromName(string name) => new(Name: name);
        public static MonsterKey FromId(int id) => new(Id: id);
        public static MonsterKey FromMapId(int mapId) => new(MapId: mapId);
    }

    private bool IsAliveByMapId(int? mapId = null, string? name = null, int? id = null)
    {
        var monsters = Bot.Monsters.MapMonsters ?? Enumerable.Empty<Monster>();

        if (mapId.HasValue)
            monsters = monsters.Where(m => m.MapID == mapId.Value);
        if (name != null)
            monsters = monsters.Where(m => m.Name?.Equals(name, StringComparison.OrdinalIgnoreCase) == true);
        if (id.HasValue)
            monsters = monsters.Where(m => m.ID == id.Value);

        return monsters.Any(m => m.Alive);
    }

    private bool IsAlive(MonsterKey key)
        => IsAliveByMapId(key.MapId, key.Name, key.Id);

    public void Attack(MonsterKey key)
    {
        if (key.Id.HasValue) Bot.Combat.Attack(key.Id.Value);
        else if (key.MapId.HasValue) Bot.Combat.Attack(key.MapId.Value);
        else if (key.Name != null) Bot.Combat.Attack(key.Name);
    }

    public void Kill(MonsterKey key)
    {
        if (!IsAlive(key)) return;
        EnsureMonsterSetup(key);
        Attack(key);
        Bot.Sleep(D1);
    }

    public void KillWithPriority(params MonsterKey[] keys)
    {
        foreach (var k in keys)
        {
            if (IsAlive(k))
            {
                EnsureMonsterSetup(k);
                Attack(k);
                Bot.Sleep(D1);
                return;
            }
        }
        Bot.Sleep(D1);
    }

    // --- overloads ---------------------------------------------------------------

    public void Kill(string name)
        => Kill(MonsterKey.FromName(name));

    public void Kill(params string[] names)
    {
        if (names == null || names.Length == 0) return;
        var keys = names
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .Select(MonsterKey.FromName)
            .ToArray();

        if (keys.Length == 0) return;
        KillWithPriority(keys);
    }

    public void Kill(int id)
        => Kill(MonsterKey.FromId(id));

    public void KillAtMapId(int mapId)
        => Kill(MonsterKey.FromMapId(mapId));

    public void KillWithPriority(string primaryName, string priorityName1)
        => KillWithPriority(MonsterKey.FromName(priorityName1), MonsterKey.FromName(primaryName));

    public void KillWithPriority(int primaryId, int priorityId1)
        => KillWithPriority(MonsterKey.FromId(priorityId1), MonsterKey.FromId(primaryId));

    public void KillWithPriorityAtMapId(int primaryMapId, int priorityMapId1)
        => KillWithPriority(MonsterKey.FromMapId(priorityMapId1), MonsterKey.FromMapId(primaryMapId));

    public void KillWithPriority(string primaryName, string priorityName1, string priorityName2)
        => KillWithPriority(MonsterKey.FromName(priorityName1), MonsterKey.FromName(priorityName2), MonsterKey.FromName(primaryName));

    public void KillWithPriority(int primaryId, int priorityId1, int priorityId2)
        => KillWithPriority(MonsterKey.FromId(priorityId1), MonsterKey.FromId(priorityId2), MonsterKey.FromId(primaryId));

    public void KillWithPriorityAtMapId(int primaryMapId, int priorityMapId1, int priorityMapId2)
        => KillWithPriority(MonsterKey.FromMapId(priorityMapId1), MonsterKey.FromMapId(priorityMapId2), MonsterKey.FromMapId(primaryMapId));

    // --- helpers ---------------------------------------------------------------

    private readonly HashSet<string> _preparedMonsters = new(StringComparer.OrdinalIgnoreCase);

    private void MonsterSetup(string monsterName)
    {
        if (string.IsNullOrWhiteSpace(monsterName)) return;

        string m = monsterName.ToLowerInvariant();

        if (m.Contains("ultra chaos harpy") || m.Contains("chaos harpy"))
            SetupChaosHarpy();
        else if (m.Contains("ultra xiang") || m.Contains("chaos lord xiang"))
            SetupChaosXiang();
        else if (m.Contains("doomkitten"))
            SetupDoomKitten();

        void SetupChaosHarpy()
        {
            Bot.Events.ExtensionPacketReceived += ChaosHarpyListener;
            const string Pot = "Shriekward Potion";
            if (Owned(Pot) < 1) BuyItem("mirrorportal", 774, Pot, 30);
            EquipConsumable(Pot);
        }

        void SetupChaosXiang()
        {
            var classes = new List<(string name, int rank)>
            {
                ("Dragon of Time", 10),
                ("Healer (Rare)", 1),
                ("Healer", 1)
            };
            EquipBestClass(classes);
        }

        void SetupDoomKitten()
        {
            var classes = new List<(string name, int rank)>
            {
                ("Dragon of Time", 10),
                ("Legion Revenant", 10),
                ("Blaze Binder", 10)
            };
            EquipBestClass(classes);
        }
    }

    private string? ResolveMonsterName(MonsterKey key)
    {
        if (!string.IsNullOrWhiteSpace(key.Name))
            return key.Name;

        var list = Bot.Monsters.MapMonsters ?? Enumerable.Empty<Monster>();
        if (key.Id.HasValue)
            return list.FirstOrDefault(m => m.ID == key.Id.Value)?.Name;
        if (key.MapId.HasValue)
            return list.FirstOrDefault(m => m.MapID == key.MapId.Value)?.Name;

        return null;
    }

    private void EnsureMonsterSetup(MonsterKey key)
    {
        var name = ResolveMonsterName(key);
        if (string.IsNullOrWhiteSpace(name)) return;

        if (_preparedMonsters.Add(name))
            MonsterSetup(name);
    }

    private void ResetMonsterSetupCache() => _preparedMonsters.Clear();

    #endregion

    #region Factions

    public List<Faction> GetAllFactions()
        => Bot?.Reputation?.FactionList ?? new List<Faction>();

    public int FactionRank(string name)
        => (Bot?.Reputation?.FactionList ?? new List<Faction>())
           .FirstOrDefault(f => f?.Name != null &&
                                name != null &&
                                f.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
           ?.Rank ?? 0;

    public bool FactionRank(string name, int minRank)
        => FactionRank(name) >= minRank;

    #endregion

    #region Potions & Scrolls

    public void UsePotion()
    {
        DisableSkills();
        try
        {
            Bot.Sleep(D1);
            Bot.Skills.UseSkill(5);
            Bot.Sleep(D2);
        }
        finally { EnableSkills(); }
    }

    public void GetScrollOfEnrage()
    {
        if (FactionRank("SpellCrafting") < 5) return;

        if (Owned("Scroll of Enrage") < 10)
        {
            ForItem("Undead Infantry", "underworld", "Mystic Parchment", 2);
            Join("dragonrune");
            Bot.Shops.Load(549);
            if (Owned("Zealous Ink") < 1)
                Bot.Shops.BuyItem(13286, 1639, 5);

            Join("spellcraft");
            Bot.Send.Packet("%xt%zm%crafting%1%spellOnStart%7%1555%Spell%"); Bot.Sleep(5000);
            Bot.Send.Packet("%xt%zm%crafting%1%spellComplete%7%2330%Enrage%"); Bot.Sleep(D3);
            Bot.Drops.Pickup("Scroll of Enrage");
        }
        EquipConsumable("Scroll of Enrage");
    }

    public void GetScrollOfDecay()
    {
        if (!Bot.Reputation.HasRank("SpellCrafting", 5)) return;

        while (Owned("Scroll of Decay") < 10)
        {
            ForItem("Undead Infantry", "underworld", "Mystic Parchment", 2);
            Join("dragonrune");
            Bot.Shops.Load(549);
            if (Owned("Zealous Ink") < 1)
                Bot.Shops.BuyItem("Zealous Ink", 5);

            Join("spellcraft");
            Bot.Drops.Add("Scroll of Decay");
            Bot.Send.Packet("%xt%zm%crafting%1%spellOnStart%7%1555%Spell%"); Bot.Sleep(5000);
            Bot.Send.Packet("%xt%zm%crafting%1%spellComplete%7%2331%Decay%");
        }

        EquipConsumable("Scroll of Decay");
    }

    public void GetDivineElixir()
    {
        ForItem("Xavier Lionfang", "poisonforest", "Divine Elixir");
        EquipConsumable("Divine Elixir");
        UsePotion();
    }

    public void UseAlchemyPotions(params string[] potionNames)
    {
        if (potionNames == null || potionNames.Length == 0) return;
        foreach (string potion in potionNames)
        {
            if (string.IsNullOrWhiteSpace(potion)) continue;
            Alert("DEBUG", $"Checking potion: {potion}");
            if (HasAura(potion, self: true))
            {
                Alert("DEBUG", $"{potion} aura already active, skipping");
                continue;
            }
            try
            {
                Alert("DEBUG", $"Buying {potion}");
                BuyAlchemyPotion(potion);
                Alert("DEBUG", $"Equipping {potion}");
                EquipConsumable(potion);
                if (Bot.Inventory.IsEquipped(potion))
                {
                    Alert("DEBUG", $"Using {potion}");
                    UsePotion();
                    Bot.Sleep(1000); //* Add a small delay*
                }
                else
                {
                    Alert("DEBUG", $"Failed to equip {potion}");
                }
            }
            catch (Exception ex)
            {
                Alert("ERROR", $"Exception with {potion}: {ex.Message}");
            }
        }
    }

    public void BuyAlchemyPotion(string name)
    {
        if (Owned(name) >= 1) return;

        switch (name)
        {
            case "Might Tonic":
                if (FactionRank("Alchemy") < 8) return;
                /*Join("alchemyacademy");
                Bot.Shops.Load(2036);
                if (Owned(61043) < 2)
                    Bot.Shops.BuyItem(61043, 8421, 2);
                if (Owned(11623) < 1)
                    Bot.Shops.BuyItem(11623, 8798, 10);*/
                BuyItem("Gold Voucher 500k", 2036, "alchemyacademy", 2);
                BuyItem("Might Tonic", 2036, "alchemyacademy", 10, calculateRemaining: false);
                break;
            case "Sage Tonic":
                if (FactionRank("Alchemy") < 8) return;
                /*Join("alchemyacademy");
                Bot.Shops.Load(2036);
                if (Owned(61043) < 2)
                    Bot.Shops.BuyItem(61043, 8421, 2);
                if (Owned(11635) < 1)
                    Bot.Shops.BuyItem(11635, 8800, 10);*/
                BuyItem("Gold Voucher 500k", 2036, "alchemyacademy", 2);
                BuyItem("Sage Tonic", 2036, "alchemyacademy", 10, calculateRemaining: false);
                break;
            case "Potent Malevolence Elixir":
                /*Join("alchemyacademy");
                Bot.Shops.Load(2036);
                if (Owned(61043) < 4)
                    Bot.Shops.BuyItem(61043, 8421, 4);
                if (Owned(11745) < 1)
                    Bot.Shops.BuyItem(11745, 9825, 8);*/
                BuyItem("Gold Voucher 500k", 2036, "alchemyacademy", 4);
                BuyItem("Potent Malevolence Elixir", 2036, "alchemyacademy", 8, calculateRemaining: false);
                break;
            case "Potent Battle Elixir":
                /*Join("alchemyacademy");
                Bot.Shops.Load(2036);
                if (Owned(61043) < 4)
                    Bot.Shops.BuyItem(61043, 8421, 4);
                if (Owned(11741) < 1)
                    Bot.Shops.BuyItem(11741, 9824, 8);*/
                BuyItem("Gold Voucher 500k", 2036, "alchemyacademy", 4);
                BuyItem("Potent Battle Elixir", 2036, "alchemyacademy", 8, calculateRemaining: false);
                break;
            case "Potent Honor Potion":
                if (FactionRank("Good") < 10) return;
                /*Join("alchemyacademy");
                Bot.Shops.Load(2036);
                if (Owned(61043) < 1)
                    Bot.Shops.BuyItem(61043, 8421, 1);
                if (Owned(11736) < 1)
                    Bot.Shops.BuyItem(11736, 8826, 5);*/
                BuyItem("Gold Voucher 500k", 2036, "alchemyacademy");
                BuyItem("Potent Honor Potion", 2036, "alchemyacademy", 5, calculateRemaining: false);
                break;
            default: return;
        }
    }

    public string GetBestTonicPotion()
    {
        var str = GetStatValue("STR");
        var intel = GetStatValue("INT");
        return str > intel ? "Might Tonic" : "Sage Tonic";
    }

    public string GetBestElixirPotion()
    {
        var str = GetStatValue("STR");
        var intel = GetStatValue("INT");
        return str > intel ? "Potent Battle Elixir" : "Potent Malevolence Elixir";
    }

    public void EquipConsumable(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return;

        try
        {
            DisableSkills();
            StopAttack();

            if (Owned(name) < 1) return;
            if (Bot.Inventory.IsEquipped(name)) return;

            WhiteMap();
            Bot.Inventory.EquipUsableItem(name);
            Bot.Sleep(D3);
            EnableSkills();
        }
        catch { }
    }

    #endregion

    #region Best Gear

    public record Gear(string Name, string Group, bool FromBank, double All, double Race);

    public void ChooseBestGear(string names)
    {
        if (Bot?.Monsters?.MapMonsters == null || Bot?.Inventory?.Items == null || Bot?.Bank?.Items == null) return;

        string race = GetMonsters(names)
            .Where(m => !string.IsNullOrWhiteSpace(m?.Race))
            .GroupBy(m => m.Race).OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault();
        if (string.IsNullOrWhiteSpace(race) || race.Equals("None", StringComparison.OrdinalIgnoreCase)) race = "allDmg";

        var items = GetItems(race).ToList();
        if (!items.Any()) return;

        var bestAll = items.Where(i => i.All > 0).GroupBy(i => i.Group).Select(g => g.OrderByDescending(i => i.All).First()).ToList();
        var bestRace = items.Where(i => i.Race > 0).GroupBy(i => i.Group).Select(g => g.OrderByDescending(i => i.Race).First()).ToList();

        var combo = (from a in bestAll
                     from r in bestRace
                     where a.Group != r.Group
                     orderby a.All + r.Race descending
                     select (a, r)).FirstOrDefault();

        if (combo.a != null)
        {
            EquipWithRetry(combo.a);
            Bot.Sleep(500);
            EquipWithRetry(combo.r);
        }
        else EquipWithRetry(items.OrderByDescending(i => Math.Max(i.Race, i.All)).First());
    }

    void EquipWithRetry(Gear g)
    {
        if (string.IsNullOrWhiteSpace(g.Name)) return;
        for (int t = 0; t < 3; t++)
        {
            if (g.FromBank) InBank(g.Name);
            Bot.Inventory.EquipItem(g.Name);
            Bot.Sleep(500);
            if (IsEquipped(g.Name)) break;
        }
    }

    bool IsEquipped(string name)
    {
        var inv = Bot?.Inventory?.Items;
        if (inv == null) return false;
        return inv.Any(i => i?.Name == name && (i.Equipped == true));
    }

    IEnumerable<Monster> GetMonsters(string s)
    {
        var all = Bot.Monsters.MapMonsters;
        if (string.IsNullOrWhiteSpace(s) || s == "*") return all;
        var set = new HashSet<string>(s.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()), StringComparer.OrdinalIgnoreCase);
        return all.Where(m => m?.Name != null && set.Contains(m.Name));
    }

    IEnumerable<Gear> GetItems(string race)
    {
        race = string.IsNullOrWhiteSpace(race) || race.Equals("None", StringComparison.OrdinalIgnoreCase) ? "allDmg" : race;
        var valid = new HashSet<string>(new[] { "Weapon", "he", "ba", "co", "pe" });
        var inv = Bot.Inventory.Items ?? Enumerable.Empty<InventoryItem>();
        var bank = Bot.Bank.Items ?? Enumerable.Empty<InventoryItem>();
        var bset = new HashSet<InventoryItem>(bank);
        bool mem = Bot?.Player?.IsMember == true;

        return inv.Concat(bank)
                  .Where(i => i != null && !string.IsNullOrWhiteSpace(i.ItemGroup) && valid.Contains(i.ItemGroup) && (!i.Upgrade || mem))
                  .Select(i => new Gear(i.Name ?? "", i.ItemGroup ?? "", bset.Contains(i),
                                        ParseMeta(i.Meta, "allDmg"), ParseMeta(i.Meta, race)))
                  .Where(g => g.All > 0 || g.Race > 0);
    }

    double ParseMeta(string meta, string key)
    {
        if (string.IsNullOrWhiteSpace(meta)) return 0;
        foreach (var t in meta.Split('\n', '\r', ','))
        {
            var p = t.Split(':'); if (p.Length != 2) continue;
            var k = p[0].Trim();
            if (!(k.Equals(key, StringComparison.OrdinalIgnoreCase) || (key == "allDmg" && k.Equals("dmgAll", StringComparison.OrdinalIgnoreCase)))) continue;
            return double.TryParse(p[1].Trim(), out var v) ? Math.Max(0, v - 1) : 0;
        }
        return 0;
    }

    #endregion

    #region Shop

    public bool BuyItem(string itemName, int shopId, string map, int quantity = 1, bool calculateRemaining = true, bool skipIfHaveEnough = true, bool considerBank = true)
    {
        if (string.IsNullOrWhiteSpace(itemName) || quantity <= 0) return false;

        try
        {
            if (!string.IsNullOrWhiteSpace(map) && Bot?.Map?.Name?.Equals(map, StringComparison.OrdinalIgnoreCase) != true)
            {
                Join(map);
                Bot.Sleep(D4);
                if (Bot?.Map?.Name?.Equals(map, StringComparison.OrdinalIgnoreCase) != true)
                {
                    Alert("Shop", $"Failed to join map: {map}");
                    return false;
                }
            }

            Bot.Shops.Load(shopId);
            Bot.Sleep(D4);

            if (Bot?.Shops?.Items?.Any() != true)
            {
                Alert("Shop", $"Failed to load shop: {shopId}");
                return false;
            }

            if (considerBank) { InBank(itemName); Bot.Sleep(D3); }

            int current = Owned(itemName);

            if (skipIfHaveEnough && current >= quantity) return true;

            int buyQuantity = calculateRemaining ? Math.Max(0, quantity - current) : quantity;
            if (buyQuantity == 0) return true;

            var item = Bot.Shops.Items.FirstOrDefault(i =>
                i?.Name?.Equals(itemName, StringComparison.OrdinalIgnoreCase) == true);

            if (item == null)
            {
                Alert("Shop", $"Item not found in shop: {itemName}");
                return false;
            }

            long totalCost = (long)item.Cost * buyQuantity;

            if (Bot.Player.Gold < totalCost)
            {
                Alert("Shop", $"Insufficient gold: need {totalCost}, have {Bot.Player.Gold}");
                return false;
            }

            if (Bot.Player.Level < item.Level)
            {
                Alert("Shop", $"Level too low: need {item.Level}, have {Bot.Player.Level}");
                return false;
            }

            if (Bot.Inventory.FreeSlots <= 0)
            {
                Alert("Shop", "No inventory space");
                return false;
            }

            int before = current;
            Bot.Shops.BuyItem(item.ID, item.ShopItemID, buyQuantity);
            Bot.Sleep(D3);

            int after = Owned(itemName);
            bool success = after > before;

            if (success)
                Alert("Shop", $"Purchased {after - before}x {itemName}");
            else
                Alert("Shop", $"Purchase failed for {itemName}");

            return success;
        }
        catch (Exception ex)
        {
            Alert("Shop", $"Error buying {itemName}: {ex.Message}");
            return false;
        }
    }

    #endregion

    #region Drops

    public bool HasDrop(string name) =>
        !string.IsNullOrWhiteSpace(name) &&
        Bot?.Drops?.CurrentDrops?.Any(d => string.Equals(d, name, StringComparison.OrdinalIgnoreCase)) == true;

    public bool HasDrop(int id) =>
        id > 0 && Bot?.Drops?.CurrentDropInfos?.Any(i => i?.ID == id) == true;

    public ItemBase GetDropItem(string name) =>
        string.IsNullOrWhiteSpace(name) ? null :
        Bot?.Drops?.CurrentDropInfos?.FirstOrDefault(i =>
            string.Equals(i?.Name, name, StringComparison.OrdinalIgnoreCase));

    public ItemBase GetDropItem(int id) =>
        id <= 0 ? null : Bot?.Drops?.CurrentDropInfos?.FirstOrDefault(i => i?.ID == id);

    public void PickupItems(params string[] names)
    {
        if (names?.Length == 0) return;
        foreach (var name in names.Where(n => !string.IsNullOrWhiteSpace(n) && HasDrop(n)))
        {
            Bot.Drops.Pickup(name);
            Bot.Sleep(D1);
        }
    }

    public void PickupItems(params int[] ids)
    {
        if (ids?.Length == 0) return;
        foreach (var id in ids.Where(i => i > 0 && HasDrop(i)))
        {
            Bot.Drops.Pickup(id);
            Bot.Sleep(D1);
        }
    }

    public bool WaitForDrop(string name, int timeout = 30000)
    {
        if (string.IsNullOrWhiteSpace(name)) return false;
        var sw = System.Diagnostics.Stopwatch.StartNew();
        while (!HasDrop(name) && sw.ElapsedMilliseconds < timeout)
            Bot.Sleep(D1);
        return HasDrop(name);
    }

    public bool WaitForDrop(int id, int timeout = 30000)
    {
        if (id <= 0) return false;
        var sw = System.Diagnostics.Stopwatch.StartNew();
        while (!HasDrop(id) && sw.ElapsedMilliseconds < timeout)
            Bot.Sleep(D1);
        return HasDrop(id);
    }

    public bool HasAnyDrop(params string[] names) =>
        names?.Any(n => !string.IsNullOrWhiteSpace(n) && HasDrop(n)) == true;

    public bool HasAnyDrop(params int[] ids) =>
        ids?.Any(i => i > 0 && HasDrop(i)) == true;

    #endregion

    #region Player

    public double GetHealthPercentage()
    {
        if (Bot?.Player == null || Bot.Player.MaxHealth <= 0) return 0;
        return (double)Bot.Player.Health / Bot.Player.MaxHealth * 100;
    }

    public double GetManaPercentage()
    {
        if (Bot?.Player == null || Bot.Player.MaxMana <= 0) return 0;
        return (double)Bot.Player.Mana / Bot.Player.MaxMana * 100;
    }

    public bool IsHealthLow(double percentage = 30)
    {
        return GetHealthPercentage() < percentage;
    }

    public bool IsManaLow(double percentage = 30)
    {
        return GetManaPercentage() < percentage;
    }

    public bool IsHealthHigh(double percentage = 90)
    {
        return GetHealthPercentage() > percentage;
    }

    public bool IsManaHigh(double percentage = 90)
    {
        return GetManaPercentage() > percentage;
    }

    public bool IsFullHealth()
    {
        if (Bot?.Player == null) return false;
        return Bot.Player.Health >= Bot.Player.MaxHealth;
    }

    public bool IsFullMana()
    {
        if (Bot?.Player == null) return false;
        return Bot.Player.Mana >= Bot.Player.MaxMana;
    }

    public bool IsFullHealthAndMana()
    {
        return IsFullHealth() && IsFullMana();
    }

    public bool IsDead()
    {
        if (Bot?.Player == null) return true; // Assume dead if can't check
        return Bot.Player.State == 0;
    }

    public bool IsIdle()
    {
        if (Bot?.Player == null) return false;
        return Bot.Player.State == 1;
    }

    public double GetDistanceTo(int x, int y)
    {
        if (Bot?.Player == null) return double.MaxValue;

        int deltaX = Bot.Player.X - x;
        int deltaY = Bot.Player.Y - y;
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    public bool IsInRangeOf(int x, int y, double range = 50)
    {
        return GetDistanceTo(x, y) <= range;
    }

    public string GetCurrentClassName()
    {
        return Bot?.Player?.CurrentClass?.Name ?? "No Class";
    }

    public bool HasClassEquipped(string className)
    {
        if (string.IsNullOrWhiteSpace(className)) return false;
        return Bot?.Player?.CurrentClass?.Name?.Equals(className, StringComparison.OrdinalIgnoreCase) == true;
    }

    public bool IsCurrentClassMaxRank()
    {
        if (Bot?.Player == null) return false;
        return Bot.Player.CurrentClassRank >= 10;
    }

    public bool IsInCell(string cellName)
    {
        if (string.IsNullOrWhiteSpace(cellName)) return false;
        return Bot?.Player?.Cell?.Equals(cellName, StringComparison.OrdinalIgnoreCase) == true;
    }

    public bool NeedsRest(double healthThreshold = 50, double manaThreshold = 50)
    {
        return IsHealthLow(healthThreshold) || IsManaLow(manaThreshold);
    }

    public bool ShouldRest()
    {
        if (Bot?.Player == null) return false;
        return !Bot.Player.InCombat && !IsFullHealthAndMana();
    }

    public string GetTargetName()
    {
        return Bot?.Player?.Target?.Name ?? string.Empty;
    }

    public double GetTargetHealthPercentage()
    {
        var target = Bot?.Player?.Target;
        if (target == null || target.MaxHP <= 0) return 0;
        return (double)target.HP / target.MaxHP * 100;
    }

    public bool IsTargetAlive()
    {
        return Bot?.Player?.Target?.Alive == true;
    }

    public bool IsTargetHealthLow(double percentage = 30)
    {
        return GetTargetHealthPercentage() < percentage;
    }

    public bool HasEnoughGold(int amount)
    {
        if (Bot?.Player == null) return false;
        return Bot.Player.Gold >= amount;
    }

    public PlayerStats GetPlayerStats()
    {
        return Bot?.Player?.Stats ?? new PlayerStats();
    }

    public int GetStatValue(string statName)
    {
        if (string.IsNullOrWhiteSpace(statName)) return 0;

        var stats = Bot?.Player?.Stats;
        if (stats == null) return 0;

        return statName.ToUpper() switch
        {
            "STR" or "STRENGTH" => stats.Strength,
            "WIS" or "WISDOM" => stats.Wisdom,
            "DEX" or "DEXTERITY" => stats.Dexterity,
            "END" or "ENDURANCE" => stats.Endurance,
            "INT" or "INTELLECT" => stats.Intellect,
            "LCK" or "LUCK" => stats.Luck,
            "AP" or "ATTACKPOWER" => stats.AttackPower,
            "SP" or "SPELLPOWER" => stats.SpellPower,
            _ => 0
        };
    }

    public float GetCriticalChance()
    {
        return Bot?.Player?.Stats?.CriticalChance ?? 0f;
    }

    public float GetCriticalMultiplier()
    {
        return Bot?.Player?.Stats?.CriticalMultiplier ?? 0f;
    }

    public float GetEvasionChance()
    {
        return Bot?.Player?.Stats?.EvasionChance ?? 0f;
    }

    public float GetHaste()
    {
        return Bot?.Player?.Stats?.Haste ?? 0f;
    }

    public bool IsReadyForCombat()
    {
        if (Bot?.Player == null) return false;
        return Bot.Player.Alive && Bot.Player.Loaded;
    }

    #endregion

    #region Map

    string _bestCell = null;
    string _bestPad = "Left";

    public void Join(string map, string cell = "Enter", string pad = "Spawn", bool publicRoom = false, int? roomNumber = null)
    {
        if (string.IsNullOrWhiteSpace(map) || Bot?.Map == null || Bot?.Player == null) return;

        string mapName = map.Split('-')[0].Trim();

        string target = publicRoom ? mapName
            : roomNumber.HasValue ? $"{mapName}-{roomNumber.Value}"
            : map.Contains("-") ? map
            : $"{mapName}-{GenerateRoomID()}";

        if (Bot.Map.Name?.Equals(mapName, StringComparison.OrdinalIgnoreCase) == true) return;

        while (!Bot.ShouldExit && Bot.Map.Name?.Equals(mapName, StringComparison.OrdinalIgnoreCase) != true)
        {
            StopAttack();
            try
            {
                Bot.Send.Packet($"%xt%zm%cmd%{Bot.Map.RoomID}%tfer%{Bot.Player.Username}%{target}%{cell}%{pad}%");
                Bot.Wait.ForMapLoad(mapName);
            }
            catch { break; }
        }
        ResetMonsterSetupCache();
    }

    public void ChooseBestCell(string monsterNames, bool alt = false, string setCell = null, string setPad = "Spawn")
    {
        if (Bot?.Monsters == null || Bot?.Map == null || Bot?.Player == null) return;

        var names = (monsterNames?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>())
            .Select(n => n?.Trim())
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .ToArray();

        bool wildcard = names.Length == 0 || (names.Length == 1 && names[0] == "*");
        string pad = string.IsNullOrWhiteSpace(setPad) ? "Left" : setPad;

        var monsters = (Bot.Monsters.MapMonsters ?? Enumerable.Empty<Monster>())
            .Where(m => m != null && !string.IsNullOrWhiteSpace(m.Cell))
            .Where(m => wildcard || names.Any(name =>
                (m.Name ?? "").Equals(name, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        if (monsters.Count == 0) return;

        string targetCell = !string.IsNullOrWhiteSpace(setCell) ? setCell
            : alt ? monsters.First().Cell
            : monsters.GroupBy(m => m.Cell)
                      .OrderByDescending(g => g.Count())
                      .First().Key;

        var mapCells = Bot.Map.Cells as IEnumerable<string> ?? Array.Empty<string>();
        if (!mapCells.Contains(targetCell)) return;

        _bestCell = targetCell;
        _bestPad = pad;

        if (!string.IsNullOrWhiteSpace(targetCell) &&
            !string.Equals(Bot.Player.Cell, targetCell, StringComparison.Ordinal))
        {
            try
            {
                Bot.Map.Jump(targetCell, pad);
                Bot.Wait.ForCellChange(targetCell);
                Bot.Player.SetSpawnPoint();
            }
            catch { }
        }
    }

    int GenerateRoomID()
    {
        // Creates a stable room ID (1000-99999) based on machine only
        // All accounts on same PC share the same room permanently
        string machineId;
        try
        {
            machineId = Microsoft.Win32.Registry.GetValue(
                @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography", "MachineGuid", null
            ) as string ?? Environment.MachineName;
        }
        catch { machineId = Environment.MachineName; }

        string seed = machineId;
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(seed));
        uint roomSeed = BitConverter.ToUInt32(hash, 0);
        return (int)(roomSeed % 99000) + 1000;
    }

    double GetLowestHpPercentage()
    {
        if (Bot?.Map?.PlayerNames?.Count == 0) return 100.0;

        double lowest = 100.0;

        foreach (var playerName in Bot.Map.PlayerNames.Where(p => !string.IsNullOrWhiteSpace(p)))
        {
            try
            {
                int hp = Bot.Flash.GetGameObject<int>($"world.uoTree.{playerName}.intHP");
                int maxHp = Bot.Flash.GetGameObject<int>($"world.uoTree.{playerName}.intHPMax");

                if (maxHp > 0 && hp >= 0)
                    lowest = Math.Min(lowest, (double)hp / maxHp * 100.0);
            }
            catch { }
        }

        return lowest;
    }

    bool IsArmyHealthLow(double percentage = 30.0) => GetLowestHpPercentage() < percentage;

    void WhiteMap() => Join("whitemap");

    #endregion

    #region Skills

    readonly int skillsDelay = 50;

    async Task SkillsAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                Skills();
            }
            catch { }

            try
            {
                await Task.Delay(skillsDelay, token);
            }
            catch (TaskCanceledException) { break; }
            catch
            {
                try
                {
                    await Task.Delay(1000, token);
                }
                catch (TaskCanceledException) { break; }
                catch { }
            }
        }
    }

    bool NotUltraDage() =>
        !string.Equals(Bot.Map.Name, "ultradage", StringComparison.OrdinalIgnoreCase);

    void Skills()
    {
        if (Bot?.Player == null) return;
        if (!Bot.Player.HasTarget) return;

        string className = Bot.Player.CurrentClass?.Name?.ToLower();
        if (string.IsNullOrWhiteSpace(className)) return;

        switch (className)
        {
            // Ultra classes
            case "legion revenant": LegionRevenantClass(); break;
            case "archpaladin": ArchPaladinClass(); break;
            case "stonecrusher": StoneCrusherClass(); break;
            case "lord of order": LordsOfOrderClass(); break;
            case "void highlord": VoidHighlordClass(); break;
            case "chaos avenger": ChaosAvengerClass(); break;
            case "lightcaster": LightCasterClass(); break;
            case "legion doomknight": LegionDoomKnightClass(); break;
            case "dragon of time": DragonOfTimeClass(); break;
            case "archmage": ArchmageClass(); break;
            case "verus doomknight": VerusDoomKnight(); break;

            // Chrono classes
            case "chrono dragonknight": case "chrono dataknight": ChronoDataKnightClass(); break;
            case "shadowstalker of time": case "shadowweaver of time": ShadowWeaverOfTimeClass(); break;
            case "continuum chronomancer": case "quantum chronomancer": QuantumChronomancerClass(); break;
            case "nechronomancer": case "necrotic chronomancer": NecroticChronomancerClass(); break;
            case "legion paladin": case "obsidian paladin chronomancer": ObsidianPaladinChronomancerClass(); break;

            // Common classes
            case "master ranger": MasterRangerClass(); break;
            case "dragonslayer general": DragonslayerGeneralClass(); break;
            case "cryomancer": CryomancerClass(); break;
            case "dragon knight": DragonKnightClass(); break;
            case "shaman": ShamanClass(); break;
            case "evolved shaman": EvolvedShamanClass(); break;
            case "dark legendary hero": DarkLegendaryHeroClass(); break;
            case "necromancer": NecromancerClass(); break;
            case "chrono assassin": ChronoAssassinClass(); break;
            case "guardian": GuardianClass(); break;
            case "great thief": GreatThiefClass(); break;

            // Basic classes
            case "mage": MageClass(); break;
            case "dragonslayer": DragonslayerClass(); break;

            default:
                // No rotation available - just return silently
                break;
        }
    }

    // --- ultra classes ---------------------------------------------------------------

    void LegionRevenantClass()
    {
        if (Cast(3)) return;
        if (Cast(2)) return;
        if (Cast(1)) return;
        if (Cast(4)) return;
    }

    void LordsOfOrderClass()
    {
        if ((IsHealthLow(80) || IsArmyHealthLow(80)) && NotUltraDage())
            if (Cast(2)) return;
        if (Cast(4)) return;
        if (Left("Empowerment", 1, true))
            if (Cast(1)) return;
        if (Left("Clarity", 1, true))
            if (Cast(3)) return;
    }

    void StoneCrusherClass()
    {
        var mode = GetMode("StoneCrusher");

        if (mode == "Ultra")
        {
            if (IsHealthLow(80) || IsArmyHealthLow(80) && HasAura("Magnitude", true))
                if (Cast(3)) return;
            if (Left("Dissonance", 1, true))
                if (Cast(2)) return;
            if (Cast(4)) return;
            if (Cast(1)) return;
        }
        else
        {
            if (IsHealthLow(80) || IsArmyHealthLow(80))
                if (Cast(3)) return;
            if (HasAura("Magnitude", true))
                if (Cast(4)) return;
            if (Left("Dissonance", 1, true))
                if (Cast(2)) return;
            if (Cast(1)) return;
        }
    }

    void ArchPaladinClass()
    {
        var mode = GetMode("ArchPaladin");

        if (mode == "Ultra")
        {
            if ((IsHealthLow(85) || IsArmyHealthLow(85)) && NotUltraDage())
                if (Cast(2)) return;
            if (!HasAura("Righteous Seal"))
                if (Cast(4)) return;
            if (Cast(3)) return;
            if (Cast(1)) return;
        }
        else
        {
            if ((IsHealthLow(85) || IsArmyHealthLow(85)) && NotUltraDage())
                if (Cast(2)) return;
            if (HasAura("Righteous Seal"))
                if (Cast(4)) return;
            if (Cast(3)) return;
            if (Cast(1)) return;
        }
    }

    void VoidHighlordClass()
    {
        if (HasAura("Unshackled", true))
            if (Cast(4)) return;
        if (IsHealthHigh(60))
            if (Cast(1)) return;
        if (Cast(2)) return;
        if (IsHealthHigh(60))
            if (Cast(3)) return;
    }

    void ChaosAvengerClass()
    {
        if (Cast(2)) return;
        if (Cast(4)) return;
        if (Cast(1)) return;
        if (Cast(3)) return;
    }

    void LightCasterClass()
    {
        if (IsHealthLow(85) || IsArmyHealthLow(85) || Left("Illuminated", 1, true))
            if (Cast(3)) return;
        if (Cast(4)) return;
        if (Cast(1)) return;
        if (Cast(2)) return;
    }

    void LegionDoomKnightClass()
    {
        if (Cast(4)) return;
        if (Cast(1)) return;
        if (Cast(2)) return;
        if (Cast(3)) return;
    }

    void DragonOfTimeClass()
    {
        if (IsHealthLow(95))
            if (Cast(2)) return;
        if (HasAura("Convergence", true))
            if (Cast(3)) return;
        if (IsHealthHigh(60))
            if (Cast(4)) return;
        if (Cast(1)) return;
    }

    void ArchmageClass()
    {
        if (IsManaLow(30))
            if (Cast(2)) return;
        if (HasAura("Arcane Flux", true) && IsHealthHigh(50))
            if (Cast(4)) return;
        if (Cast(1)) return;
        if (Cast(3)) return;
    }

    void VerusDoomKnight()
    {
        if (IsHealthLow(50))
            if (Cast(2)) return;
        if (Stacks("Doom", 10, true))
            if (Cast(4)) return;
        if (Cast(1)) return;
        if (Cast(2)) return;
        if (Cast(3)) return;
    }

    // --- chrono classes ---------------------------------------------------------------

    void ChronoDataKnightClass()
    {
        if (Stacks("Temporal Rift", 4, true))
            if (Cast(4)) return;
        if (Cast(1)) return;
        if (Cast(2)) return;
        if (Cast(3)) return;
    }

    void ShadowWeaverOfTimeClass()
    {
        if (IsHealthLow(50) || IsManaLow(30))
            if (Cast(3)) return;
        if (Stacks("Chaos Rift", 4, true))
            if (Cast(4)) return;
        if (Cast(1)) return;
        if (Cast(2)) return;
    }

    void QuantumChronomancerClass()
    {
        if (Stacks("Temporal Rift", 4, true))
            if (Cast(3)) return;
        if (HasAura("Quantum Restructure", true))
            if (Cast(4)) return;
        if (Cast(2)) return;
        if (Cast(1)) return;
    }

    void NecroticChronomancerClass()
    {
        if (Stacks("Chaos Rift", 4, true))
            if (Cast(3)) return;
        if (Left("Debilitated", 2))
            if (Cast(4)) return;
        if (Cast(2)) return;
        if (Cast(1)) return;
    }

    void ObsidianPaladinChronomancerClass()
    {
        if (IsHealthLow(50) || IsArmyHealthLow(50))
            if (Cast(3)) return;
        if (IsHealthLow(80) || IsArmyHealthLow(80))
            if (Cast(2)) return;
        if (Stacks("Temporal Rift", 4, true))
            if (Cast(4)) return;
        if (Cast(1)) return;
    }

    // --- common classes ---------------------------------------------------------------

    void MasterRangerClass()
    {
        if (HasAura("Vampiric Shot", true))
            if (Cast(3)) return;
        if (Stacks("Marks", 6, true))
            if (Cast(4)) return;
        if (Stacks("Marks", 3, true))
            if (Cast(2)) return;
        if (Cast(1)) return;
    }

    void DragonslayerGeneralClass()
    {
        if (HasAura("General's Dragonbane"))
            if (Cast(2)) return;
        if (HasAura("General's Dragonbane"))
            if (Cast(3)) return;
        if (Cast(4)) return;
        if (Cast(1)) return;
    }

    void CryomancerClass()
    {
        if (IsHealthLow(60) && HasAura("Polar Vortex", true))
            if (Cast(3)) return;
        if (HasAura("Frozen") && HasAura("Polar Vortex", true))
            if (Cast(2)) return;
        if (Cast(1)) return;
        if (Cast(4)) return;
    }

    void DragonslayerClass()
    {
        if (HasAura("Dragonbane") && !HasAura("Infected Wound"))
            if (Cast(2)) return;
        if (HasAura("Dragonbane") && !HasAura("Weakened"))
            if (Cast(3)) return;
        if (Cast(4)) return;
        if (Cast(1)) return;
    }

    void DragonKnightClass()
    {
        if (Cast(1)) return;
        if (HasAura("Flammable"))
            if (Cast(4)) return;
        if (Cast(2)) return;
        if (HasAura("Dumbfounded"))
            if (Cast(3)) return;
    }

    void ShamanClass()
    {
        if (Left("Elemental Embrace", 5))
            if (Cast(4)) return;
        if (HasAura("Elemental Embrace"))
            if (Cast(3)) return;
        if (HasAura("Scorched Spirit"))
            if (Cast(2)) return;
        if (Cast(1)) return;
    }

    void EvolvedShamanClass()
    {
        if (IsHealthLow(80) || IsArmyHealthLow(80))
            if (Cast(3)) return;
        if (Left("Elemental Grasp", 5))
            if (Cast(4)) return;
        if (Cast(2)) return;
        if (Cast(1)) return;
    }

    void DarkLegendaryHeroClass()
    {
        if (IsHealthLow(30) || IsArmyHealthLow(30))
            if (Cast(4)) return;
        if (Cast(3)) return;
        if (Cast(2)) return;
        if (Cast(1)) return;
    }

    void NecromancerClass()
    {
        if (IsManaLow(90) && IsHealthHigh(80) && !HasAura("Deadly Frenzy", true))
            if (Cast(3)) return;
        if (IsManaLow(30) && IsHealthHigh(80) && HasAura("Deadly Frenzy", true))
            if (Cast(3)) return;
        if (IsManaHigh(80) && IsHealthHigh(80))
            if (Cast(4)) return;
        if (HasAura("Deadly Frenzy", true))
            if (Cast(1)) return;
        if (Cast(2)) return;
    }

    void ChronoAssassinClass()
    {
        if (HasAura("Reverse Time", true))
        {
            if (Cast(4)) return;
        }
        else
        {
            if (Cast(3)) return;
            if (Cast(1)) return;
        }
        if (Cast(2)) return;

    }

    void GuardianClass()
    {
        if ((HasAura("Hypercritical", true) || HasAura("Void Imbue", true)) && Stacks("Guardian Spirit", 15, true))
            if (Cast(4)) return;
        if (IsManaLow(40))
            if (Cast(3)) return;
        if (Cast(1)) return;
        if (Cast(2)) return;
    }

    void GreatThiefClass()
    {
        if (HasAura("Hidden Blade", true))
            if (Cast(4)) return;
        if (Cast(3)) return;
        if (Cast(1)) return;
        if (Cast(2)) return;
    }

    // --- basic classes ---------------------------------------------------------------

    void MageClass()
    {
        if (Left("Arcane Shield", 1, true))
            if (Cast(4)) return;
        if (HasAura("Frozen Blood"))
            if (Cast(1)) return;
        if (HasAura("Scorched"))
            if (Cast(3)) return;
        if (Cast(2)) return;
    }

    // --- helpers ---------------------------------------------------------------

    public bool Cast(int index)
    {
        if (index < 1 || index > 4) return false;
        if (Bot?.Skills == null) return false;

        if (!Bot.Skills.CanUseSkill(index)) return false;

        try
        {
            Bot.Skills.UseSkill(index);
            return true;
        }
        catch { return false; }
    }

    public void DisableSkills()
    {
        try
        {
            _cts?.Cancel();
        }
        catch { }
        finally
        {
            _runSkills = null;
            _cts = null;
        }
    }

    public void EnableSkills()
    {
        if (_runSkills != null && !_runSkills.IsCompleted) return;

        try
        {
            _cts = new CancellationTokenSource();
            _runSkills = Task.Run(() => SkillsAsync(_cts.Token));
        }
        catch
        {
            _cts?.Dispose();
            _cts = null;
            _runSkills = null;
        }
    }

    private Dictionary<string, string> _classRotationMode = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    public void SetClassRotation(string className, string mode)
    {
        if (string.IsNullOrWhiteSpace(className) || string.IsNullOrWhiteSpace(mode))
            return;

        _classRotationMode[className] = mode;
        Alert("Rotation", $"{className} set to {mode} mode");
    }

    private string GetMode(string className) =>
        _classRotationMode.TryGetValue(className, out var mode) ? mode : "Default";

    #endregion

    #region Auras

    public IEnumerable<Aura> GetAuras(bool self) =>
        (self ? Bot?.Self?.Auras : Bot?.Target?.Auras) ?? Enumerable.Empty<Aura>();

    public Aura GetAuraByName(string auraName, bool self)
    {
        if (string.IsNullOrWhiteSpace(auraName)) return null;
        return GetAuras(self).FirstOrDefault(a => a != null &&
            !string.IsNullOrWhiteSpace(a.Name) &&
            auraName.Equals(a.Name, StringComparison.OrdinalIgnoreCase));
    }

    public bool HasAura(string auraName, bool self = false)
    {
        return GetAuraByName(auraName, self) != null;
    }

    public bool HasAnyAura(List<string> auraNames, bool self = false)
    {
        if (auraNames == null || auraNames.Count == 0) return false;
        foreach (string aura in auraNames)
        {
            if (!string.IsNullOrWhiteSpace(aura) && HasAura(aura, self))
                return true;
        }
        return false;
    }

    public int GetAuraStacks(string auraName, bool self = false)
    {
        if (string.IsNullOrWhiteSpace(auraName)) return 0;
        try
        {
            object v = self ? Bot?.Self?.GetAuraValue(auraName)
                            : Bot?.Target?.GetAuraValue(auraName);
            if (v == null) return 0;

            int rawValue = v is int i ? i
                 : v is long l ? (int)l
                 : v is double d ? (int)Math.Round(d)
                 : v is float f ? (int)Math.Round(f)
                 : int.TryParse(v.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var n) ? n
                 : 0;

            return rawValue + 1;
        }
        catch { return 0; }
    }

    public int GetAuraSecondsRemaining(string auraName, bool self = false)
    {
        if (string.IsNullOrWhiteSpace(auraName)) return 0;
        var aura = GetAuraByName(auraName, self);
        if (aura == null || aura._timeStamp <= 0 || aura.Duration <= 0) return 0;
        try
        {
            var applied = DateTimeOffset.FromUnixTimeMilliseconds(aura._timeStamp);
            var expires = applied.AddSeconds(aura.Duration);
            var remaining = (int)(expires - DateTimeOffset.Now).TotalSeconds;
            return Math.Max(0, remaining);
        }
        catch { return 0; }
    }

    public bool Stacks(string name, int quantity, bool self = false)
    {
        if (string.IsNullOrWhiteSpace(name) || quantity <= 0) return false;
        int stacks = GetAuraStacks(name, self);
        return stacks >= quantity;
    }

    public bool Left(string name, int duration, bool self = false)
    {
        if (string.IsNullOrWhiteSpace(name) || duration < 0) return false;
        int rem = GetAuraSecondsRemaining(name, self); // returns 0 if expired/missing
        return rem <= duration;
    }

    #endregion

    #region Listeners

    private volatile bool _chargeDetected;
    private int _chargeCount;

    void PulseCharge(int ms = 2000)
    {
        System.Threading.Interlocked.Increment(ref _chargeCount);
        _chargeDetected = true;
        Task.Run(async () =>
        {
            await Task.Delay(ms);
            if (System.Threading.Interlocked.Decrement(ref _chargeCount) <= 0)
            {
                _chargeCount = 0;
                _chargeDetected = false;
            }
        });
    }

    public void ChargeListener(dynamic packet)
    {
        try
        {
            if (packet?["params"]?.type?.ToString() != "json") return;
            dynamic data = packet["params"].dataObj;
            if (data?.cmd?.ToString() != "ct") return;

            var anims = data?.anims as System.Collections.IEnumerable;
            if (anims == null) return;

            foreach (var anim in anims)
            {
                if ((anim as dynamic)?.animStr?.ToString()?.Equals("Charge", StringComparison.OrdinalIgnoreCase) == true)
                {
                    PulseCharge(2000);
                    break;
                }
            }
        }
        catch { }
    }

    void ChaosHarpyListener(dynamic packet)
    {
        try
        {
            if (packet?["params"]?.type?.ToString() != "json") return;
            dynamic data = packet["params"].dataObj;
            if (data?.cmd?.ToString() != "ct") return;

            var anims = data?.anims as System.Collections.IEnumerable;
            if (anims == null) return;

            foreach (var anim in anims)
            {
                if ((anim as dynamic)?.animStr?.ToString()?.Equals("Charge", StringComparison.OrdinalIgnoreCase) == true)
                {
                    Task.Run(async () =>
                    {
                        DisableSkills();
                        await Task.Delay(500);
                        Bot.Skills.UseSkill(5);
                        await Task.Delay(500);
                        EnableSkills();
                    });
                    break;
                }
            }
        }
        catch { }
    }

    #endregion

    #region Experimental Ultras

    public void TauntCycle(string name, string monster, string aura, int checkDelay)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(monster) || string.IsNullOrWhiteSpace(aura)) return;
        if (Bot?.Combat == null) return;
        if (HasClassEquipped(name))
        {
            int effect = GetAuraSecondsRemaining(aura);
            Bot.Combat.Attack(monster);
            if (checkDelay > 0) Bot.Sleep(checkDelay);
            if (effect < 2) UsePotion();
        }
    }

    public void TauntCharge(string name, string monster, string aura, int checkDelay)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(monster)) return;
        if (Bot?.Combat == null) return;
        if (HasClassEquipped(name))
        {
            Bot.Combat.Attack(monster);
            if (checkDelay > 0) Bot.Sleep(checkDelay);
            if (_chargeDetected) UsePotion();
        }
    }

    public void KillWithPriority(string primaryName, int primaryMapId, string priorityName1, int priorityMapId1, string priorityName2, int priorityMapId2)
    {
        if (string.IsNullOrWhiteSpace(primaryName)) return;
        if (!string.IsNullOrWhiteSpace(priorityName1) && IsAliveByMapId(priorityMapId1, name: priorityName1)) KillByMapId(priorityMapId1, name: priorityName1);
        else if (!string.IsNullOrWhiteSpace(priorityName2) && IsAliveByMapId(priorityMapId2, name: priorityName2)) KillByMapId(priorityMapId2, name: priorityName2);
        else KillByMapId(primaryMapId, name: primaryName);
        Bot.Sleep(D1);
    }

    public void KillByMapId(int mapId, string? name = null, int? id = null)
    {
        if (Bot?.Combat == null) return;
        if (IsAliveByMapId(mapId, name, id))
        {
            Bot.Combat.Attack(mapId);
            Bot.Sleep(250);
        }
    }
    public void UltraWardenTaunter()
    {
        const string USED_THRESHOLDS_KEY = "warden.usedThresholds";

        if (Bot?.Combat == null || Bot?.Player == null) return;

        Bot.Combat.Attack("Ultra Warden");
        var t = Bot.Player.Target;
        if (t?.HP == null || t.HP <= 0 || t.MaxHP <= 0) return;

        int currentHp = t.HP;
        int maxHp = t.MaxHP;
        int currentThreshold = (currentHp * 100) / maxHp;
        int thresholdBand = (currentThreshold / 5) * 5;

        HashSet<int> usedThresholds;
        var usedObj = AppDomain.CurrentDomain.GetData(USED_THRESHOLDS_KEY);
        usedThresholds = usedObj as HashSet<int> ?? new HashSet<int>();

        if (!usedThresholds.Contains(thresholdBand))
        {
            double percentRemaining = ((double)currentHp / maxHp) * 100;
            usedThresholds.Add(thresholdBand);
            AppDomain.CurrentDomain.SetData(USED_THRESHOLDS_KEY, usedThresholds);

            while (MonsterAlive("Ultra Warden") && !HasAura("Focus") && !Bot.ShouldExit)
                UsePotion();
        }

        Bot.Sleep(150);
    }

    public void DrakathTaunter()
    {
        const string THRESHOLD_KEY = "drakath.lastThreshold";
        if (Bot?.Combat == null || Bot?.Player == null) return;

        var bands = new (int thr, int rng)[] {
            (18_000_000, 180_000), (16_000_000, 180_000), (14_000_000, 180_000),
            (12_000_000, 180_000), (10_000_000, 180_000), (8_000_000, 100_000),
            (6_000_000, 100_000), (4_000_000, 100_000), (2_000_000, 100_000)
        };

        Bot.Combat.Attack("Champion Drakath");
        var t = Bot.Player.Target;
        if (t?.HP == null || t.HP <= 0) return;

        int hp = t.HP;
        var lastThreshold = AppDomain.CurrentDomain.GetData(THRESHOLD_KEY) as int? ?? int.MaxValue;

        var matchingBand = Array.FindLast(bands, band =>
            hp <= (band.thr + band.rng) &&
            band.thr < lastThreshold);

        if (matchingBand != default)
        {
            Alert("Drakath", $"Triggering threshold at {hp:N0} HP (target: {matchingBand.thr:N0})");
            AppDomain.CurrentDomain.SetData(THRESHOLD_KEY, matchingBand.thr);

            int attempts = 0;
            while (MonsterAlive("Champion Drakath") && !HasAura("Focus") && !Bot.ShouldExit && attempts < 50)
            {
                UsePotion();
                Bot.Sleep(100);
                attempts++;
            }

            if (HasAura("Focus"))
                Alert("Drakath", $"Focus obtained at {Bot.Player.Target?.HP:N0} HP");
            else
                Alert("Drakath", $"Warning: Failed to get Focus (attempts: {attempts})");
        }

        Bot.Sleep(150);
    }

    // --- helpers ---------------------------------------------------

    public void WaitForArmy(int quantity, int bufferTimeMs = 3000, int tickMs = 500, int timeoutMs = 0)
    {
        if (Bot?.Map == null) return;

        int required = Math.Max(1, quantity) + 1;
        var sw = Stopwatch.StartNew();

        while (!Bot.ShouldExit &&
               Bot.Map.PlayerCount < required &&
               (timeoutMs <= 0 || sw.ElapsedMilliseconds < timeoutMs))
        {
            int others = Math.Max(0, Bot.Map.PlayerCount - 1);
            Alert("Army", $"Waiting for army: {others}/{quantity} players ready");

            if (!IsManaLow(50))
            {
                Bot.Skills.UseSkill(1);
                Bot.Skills.UseSkill(2);
                Bot.Skills.UseSkill(3);
                Bot.Player.Rest(true);
            }

            Bot.Sleep(tickMs);
        }

        if (Bot.ShouldExit) return;

        Alert("Army", $"All players ready ({Bot.Map.PlayerCount}), final buffing...");
        if (!IsManaLow(50))
        {
            Bot.Skills.UseSkill(1);
            Bot.Skills.UseSkill(2);
            Bot.Skills.UseSkill(3);
            Bot.Player.Rest(true);
        }

        Alert("Army", $"Waiting {bufferTimeMs}ms for coordination...");
        Bot.Sleep(bufferTimeMs);
        Alert("Army", "Ready to proceed!");
    }

    public bool MonsterAlive(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return false;
        if (Bot?.Monsters?.MapMonsters == null) return false;

        return Bot.Monsters.MapMonsters
            .Any(m => m?.Name?.Equals(name, StringComparison.OrdinalIgnoreCase) == true && m.Alive);
    }

    public void StopAttack()
    {
        if (Bot?.Combat == null || Bot?.Map == null || Bot?.Player == null) return;

        Bot.Combat.CancelAutoAttack();
        Bot.Combat.CancelTarget();

        var cellsList = Bot.Map.Cells ?? new List<string>();
        var monstersList = Bot.Monsters?.MapMonsters ?? new List<Monster>();

        string safeCell = cellsList
            .Where(c => !string.IsNullOrWhiteSpace(c) &&
                       !c.Equals("Wait", StringComparison.OrdinalIgnoreCase) &&
                       !c.Equals("Blank", StringComparison.OrdinalIgnoreCase) &&
                       !c.StartsWith("Cut", StringComparison.OrdinalIgnoreCase))
            .OrderBy(c => monstersList.Count(m => m?.Cell == c))
            .FirstOrDefault() ?? Bot.Player.Cell;

        string pad = string.IsNullOrWhiteSpace(Bot.Player.Pad) ? "Left" : Bot.Player.Pad;

        // hop until we're out
        while (!Bot.ShouldExit && Bot.Player.State == 2)
        {
            if (!string.Equals(Bot.Player.Cell, safeCell, StringComparison.Ordinal))
            {
                Bot.Map.Jump(safeCell, pad);
                Bot.Wait.ForCellChange(safeCell);
            }
            Bot.Sleep(D1);
        }
        Bot.Sleep(D3);
    }

    public void DontAttack()
    {
        if (Bot?.Combat == null || Bot?.Map == null || Bot?.Player == null) return;

        Bot.Combat.CancelAutoAttack();
        Bot.Combat.CancelTarget();

        DisableSkills();
        Bot.Sleep(5000);
        EnableSkills();
    }

    #endregion

    bool HasChanged<T>(string key, T newValue)
    {
        if (string.IsNullOrWhiteSpace(key)) return false;

        if (_cache.TryGetValue(key, out var prev) && prev is T p &&
            EqualityComparer<T>.Default.Equals(p, newValue))
            return false;

        _cache[key] = newValue;
        return true;
    }

    public bool Alert(string category, string message)
    {
        if (string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(message))
            return false;

        category = category.Trim();
        message = message.Trim();

        if (!HasChanged(category, message))
            return false;

        var key = $"{category}:{message}";
        var now = DateTime.UtcNow;

        if (_throttle.TryGetValue(key, out var last) && now - last < ThrottleDuration)
            return false;

        _throttle[key] = now;
        OnSignal?.Invoke(category, message);
        return true;
    }
}