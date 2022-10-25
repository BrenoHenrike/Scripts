//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Friday13th/CoreFriday13th.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Shops;


public class OdditiesMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreFriday13th CoreFriday13th => new();

    string[] MergeShop1Items =
    {
        "Gothic Musician's Beard",
        "Bloodmoon Musician",
        "Gothic Decorator's Hair",
        "Gothic Decorator's Beard",
        "Gothic Decorator",
        "Unicorn Commander's TopHat + Locks",
        "Unicorn Commander's TopHat",
        "Unicorn Commander Musician",
        "Goth Pirate TopHat + Locks",
        "Goth Pirate TopHat + Beard",
        "Goth Pirate Musician"
    };

    string[] MergeShop2Items =
    {
        "Spirit Katana of Wrath",
        "Slasher of Wrath",
        "Ancient Chains of Binding",
        "Spirit Hunter Hood",
        "Spirit Hunter Hat + Locks",
        "Spirit Hunter Hat",
        "Dread Spirit Hunter",
        "Gothic Thief's Wrap",
        "Gothic Thief's Locks",
        "Gothic Thief's Hair",
        "Gothic Thief",
        "Sally's Plushie",
        "Neo Necromantress' Fur Cloak",
        "Neo Necromantress' Braid",
        "Neo Necromantress",
        "Magical Johnson's Hat + Locks",
        "Magical Johnson's Hat",
        "Magical Johnson",
        "Mysterious Johnson's Hat + Locks",
        "Mysterious Johnson's Hat",
        "Mysterious Johnson"
    };


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreFriday13th.Oddities();

        MergeShopFabyo("all");
        MergeShopOddities("all");

        Core.ToBank(MergeShop1Items);
        Core.ToBank(MergeShop2Items);

        Core.SetOptions(false);
    }

    public void MergeShopFabyo(string item = "all")
    {
        // Merge Shop: Fabyo's Spooky Merge

        if (Core.CheckInventory(MergeShop1Items))
            return;

        if (!Core.CheckInventory(MergeShop1Items))
        {
            foreach (string MergeItem in MergeShop1Items)
            {
                if (!Core.CheckInventory(MergeItem))
                {
                    Core.Logger($"Started farming for {MergeItem}");
                    MergeMatsFabyo();
                    Core.BuyItem("oddities", 2135, MergeItem);
                    Core.ToBank(MergeItem);
                }
            }
        }
    }

    public void MergeShopOddities(string item = "all")
    {
        //Merge Shop: Oddities Merge Shop

        if (Core.CheckInventory(MergeShop2Items))
            return;

        if (item == "all" && !Core.CheckInventory(MergeShop2Items))
        {
            foreach (string MergeItem in MergeShop2Items)
            {
                if (!Core.CheckInventory(MergeItem, toInv: false))
                {
                    Core.Logger($"Started farming for {MergeItem}");
                    if (MergeItem == "Magical Johnson" && !Core.CheckInventory("Magical Johnson"))
                    {
                        MergeMatsOdd();
                        Core.BuyItem("oddities", 2134, "Mysterious Johnson");
                        Bot.Sleep(Core.ActionDelay);
                        Core.BuyItem("oddities", 2134, MergeItem);
                    }

                    if (MergeItem == "Magical Johnson's Hat" && !Core.CheckInventory("Magical Johnson's Hat"))
                    {
                        MergeMatsOdd();
                        Core.BuyItem("oddities", 2134, "Mysterious Johnson's Hat");
                        Bot.Sleep(Core.ActionDelay);
                        Core.BuyItem("oddities", 2134, MergeItem);
                    }

                    if (MergeItem == "Magical Johnson's Hat + Locks" && !Core.CheckInventory("Magical Johnson's Hat + Locks"))
                    {
                        MergeMatsOdd();
                        Core.BuyItem("oddities", 2134, "Mysterious Johnson's Hat + Locks");
                        Bot.Sleep(Core.ActionDelay);
                        Core.BuyItem("oddities", 2134, MergeItem);
                    }

                    MergeMatsOdd();
                    Core.BuyItem("oddities", 2134, MergeItem);
                    Core.ToBank(MergeItem);
                }
            }
        }
    }

    public void MergeMatsOdd()
    {
        List<ShopItem> shopdata = Bot.Shops.Items;

        foreach (string MergeItem in MergeShop2Items)
        {
            Core.Join("oddities");
            Bot.Shops.Load(2134);
            List<ItemBase> Requirements = shopdata.First(i => i.Name == MergeItem).Requirements;

            int CursedDollQuant = 0;
            if (Requirements.Where(x => x.Name == "Cursed Doll Tassel").Count() > 0)
                CursedDollQuant = Requirements.First(x => x.Name == "Cursed Doll Tassel").Quantity;

            int OldCoinQuant = 0;
            if (Requirements.Where(x => x.Name == "Odd Coin").Count() > 0)
                CursedDollQuant = Requirements.First(x => x.Name == "Odd Coin").Quantity;

            int EctoplasmicTokenQuant = 0;
            if (Requirements.Where(x => x.Name == "Ectoplasmic Token").Count() > 0)
                CursedDollQuant = Requirements.First(x => x.Name == "Ectoplasmic Token").Quantity;

            int SpookyFabricScrapQuant = 0;
            if (Requirements.Where(x => x.Name == "Spooky Fabric Scrap").Count() > 0)
                CursedDollQuant = Requirements.First(x => x.Name == "Spooky Fabric Scrap").Quantity;

            int EerieEmbellishmentQuant = 0;
            if (Requirements.Where(x => x.Name == "Eerie Embellishment").Count() > 0)
                CursedDollQuant = Requirements.First(x => x.Name == "Eerie Embellishment").Quantity;

            int VoucherQuant = 0;
            if (Requirements.Where(x => x.Name == "Gold Voucher 100k").Count() > 0)
                CursedDollQuant = Requirements.First(x => x.Name == "Gold Voucher 100k").Quantity;


            if (Core.CheckInventory("Cursed Doll Tassel", CursedDollQuant) && Core.CheckInventory("Odd Coin", OldCoinQuant) && Core.CheckInventory("Ectoplasmic Token", EctoplasmicTokenQuant) && Core.CheckInventory("Spooky Fabric Scrap", SpookyFabricScrapQuant) && Core.CheckInventory("Eerie Embellishment", EerieEmbellishmentQuant) && Core.CheckInventory("Gold Voucher 100k", VoucherQuant))
                return;

            Core.AddDrop("Cursed Doll Tassel", "Odd Coin", "Ectoplasmic Token", "Ectoplasmic Token", "Eerie Embellishment");

            Core.RegisterQuests(8667);
            Core.EquipClass(ClassType.Solo);
            while (!Bot.ShouldExit && !Core.CheckInventory("Cursed Doll Tassel", CursedDollQuant))
            {
                Core.KillMonster("Oddities", "r2", "Left", "*", "Chipped Wood", 7);
                Core.KillMonster("Oddities", "r6", "Left", "*", "Fuzz Tuff", 7);
                Core.KillMonster("Oddities", "r10", "Left", "Cursed Spirit", "Doll Eyes", 7);
                Bot.Wait.ForPickup("Cursed Doll Tassel");
            }
            Core.CancelRegisteredQuests();

            Core.RegisterQuests(8674);
            while (!Bot.ShouldExit && !Core.CheckInventory("Odd Coin", OldCoinQuant))
            {
                Core.KillMonster("Oddities", "r6", "Left", "*", "Frankensteined Teddy");
                Bot.Wait.ForPickup("Odd Coin");
            }
            Core.CancelRegisteredQuests();

            Core.RegisterQuests(8675);
            while (!Bot.ShouldExit && !Core.CheckInventory("Ectoplasmic Token", EctoplasmicTokenQuant))
            {
                Core.KillMonster("Oddities", "r10", "Left", "Cursed Spirit", "Doll Eye", 5);
                Bot.Wait.ForPickup("Ectoplasmic Token");
            }
            Core.CancelRegisteredQuests();

            Core.RegisterQuests(8676);
            Core.EquipClass(ClassType.Farm);
            while (!Bot.ShouldExit && !Core.CheckInventory("Spooky Fabric Scrap", SpookyFabricScrapQuant))
            {
                Core.KillMonster("Oddities", "r4", "Left", "*", "Cursed Cloth Roll", 13);
                Bot.Wait.ForPickup("Spooky Fabric Scrap");
            }
            Core.CancelRegisteredQuests();

            Core.RegisterQuests(8677);
            Core.EquipClass(ClassType.Farm);
            while (!Bot.ShouldExit && !Core.CheckInventory("Eerie Embellishment", EerieEmbellishmentQuant))
            {
                Core.KillMonster("Oddities", "r4", "Left", "*", "Freaky Fripperies", 13);
                Bot.Wait.ForPickup("Eerie Embellishment");
            }
            Core.CancelRegisteredQuests();

            while (!Bot.ShouldExit && !Core.CheckInventory("Gold Voucher 100k", VoucherQuant))
                Core.BuyItem("Oddities", 2135, "Gold Voucher 100k", VoucherQuant);
        }
    }
    public void MergeMatsFabyo()
    {
        Core.Join("oddities");
        Bot.Shops.Load(2135);
        List<ShopItem> shopdata = Bot.Shops.Items;

        foreach (string MergeItem in MergeShop1Items)
        {
            List<ItemBase> Requirements = shopdata.First(i => i.Name == MergeItem).Requirements;

            int CursedDollQuant = 0;
            if (Requirements.Where(x => x.Name == "Cursed Doll Tassel").Count() > 0)
                CursedDollQuant = Requirements.First(x => x.Name == "Cursed Doll Tassel").Quantity;

            int OldCoinQuant = 0;
            if (Requirements.Where(x => x.Name == "Odd Coin").Count() > 0)
                CursedDollQuant = Requirements.First(x => x.Name == "Odd Coin").Quantity;

            int EctoplasmicTokenQuant = 0;
            if (Requirements.Where(x => x.Name == "Ectoplasmic Token").Count() > 0)
                CursedDollQuant = Requirements.First(x => x.Name == "Ectoplasmic Token").Quantity;

            int SpookyFabricScrapQuant = 0;
            if (Requirements.Where(x => x.Name == "Spooky Fabric Scrap").Count() > 0)
                CursedDollQuant = Requirements.First(x => x.Name == "Spooky Fabric Scrap").Quantity;

            int EerieEmbellishmentQuant = 0;
            if (Requirements.Where(x => x.Name == "Eerie Embellishment").Count() > 0)
                CursedDollQuant = Requirements.First(x => x.Name == "Eerie Embellishment").Quantity;

            int VoucherQuant = 0;
            if (Requirements.Where(x => x.Name == "Gold Voucher 100k").Count() > 0)
                CursedDollQuant = Requirements.First(x => x.Name == "Gold Voucher 100k").Quantity;

            if (Core.CheckInventory("Cursed Doll Tassel", CursedDollQuant) && Core.CheckInventory("Odd Coin", OldCoinQuant) && Core.CheckInventory("Ectoplasmic Token", EctoplasmicTokenQuant) && Core.CheckInventory("Spooky Fabric Scrap", SpookyFabricScrapQuant) && Core.CheckInventory("Eerie Embellishment", EerieEmbellishmentQuant) && Core.CheckInventory("Gold Voucher 100k", VoucherQuant))
                return;

            Core.AddDrop("Cursed Doll Tassel", "Odd Coin", "Ectoplasmic Token", "Ectoplasmic Token", "Eerie Embellishment");

            Core.RegisterQuests(8667);
            Core.EquipClass(ClassType.Solo);
            while (!Bot.ShouldExit && !Core.CheckInventory("Cursed Doll Tassel", CursedDollQuant))
            {
                Core.KillMonster("Oddities", "r2", "Left", "*", "Chipped Wood", 7);
                Core.KillMonster("Oddities", "r6", "Left", "*", "Fuzz Tuff", 7);
                while (!Bot.ShouldExit && Bot.Player.Cell != "r10") //cutscene fuckery
                {
                    Bot.Sleep(Core.ActionDelay);
                    Core.Jump("r10");
                }
                Core.KillMonster("Oddities", "r10", "Left", "Cursed Spirit", "Doll Eyes", 7); //low DR
                Bot.Wait.ForPickup("Cursed Doll Tassel");
            }
            Core.CancelRegisteredQuests();

            Core.RegisterQuests(8674);
            while (!Bot.ShouldExit && !Core.CheckInventory("Odd Coin", OldCoinQuant))
            {
                Core.KillMonster("Oddities", "r6", "Left", "*", "Frankensteined Teddy");
                Bot.Wait.ForPickup("Odd Coin");
            }
            Core.CancelRegisteredQuests();

            Core.RegisterQuests(8675);
            while (!Bot.ShouldExit && !Core.CheckInventory("Ectoplasmic Token", EctoplasmicTokenQuant))
            {
                Core.KillMonster("Oddities", "r10", "Left", "Cursed Spirit", "Doll Eye", 5);
                Bot.Wait.ForPickup("Ectoplasmic Token");
            }
            Core.CancelRegisteredQuests();

            Core.RegisterQuests(8676);
            Core.EquipClass(ClassType.Farm);
            while (!Bot.ShouldExit && !Core.CheckInventory("Spooky Fabric Scrap", SpookyFabricScrapQuant))
            {
                Core.KillMonster("Oddities", "r4", "Left", "*", "Cursed Cloth Roll", 13);
                Bot.Wait.ForPickup("Spooky Fabric Scrap");
            }
            Core.CancelRegisteredQuests();

            Core.RegisterQuests(8677);
            Core.EquipClass(ClassType.Farm);
            while (!Bot.ShouldExit && !Core.CheckInventory("Eerie Embellishment", EerieEmbellishmentQuant))
            {
                Core.KillMonster("Oddities", "r4", "Left", "*", "Freaky Fripperies", 13);
                Bot.Wait.ForPickup("Eerie Embellishment");
            }
            Core.CancelRegisteredQuests();

            while (!Bot.ShouldExit && !Core.CheckInventory("Gold Voucher 100k", VoucherQuant))
                Core.BuyItem("Oddities", 2135, "Gold Voucher 100k", VoucherQuant);
        }
    }
}
