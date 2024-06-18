/*
name: Treasure Chest Spam
description: Wastes (with a warning) your extra ACs on opening Treasure Chests
tags: treasure, chest, key, dark, box, waste, AC
*/
//cs_include Scripts/CoreBots.cs
using CommunityToolkit.Mvvm.DependencyInjection;
using Skua.Core.Interfaces;
using Skua.Core.ViewModels;

public class TreasureChestSpam
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        WhalingTime();
    }

    public void WhalingTime(bool? stopForDarkBoxAndKey = null)
    {
        Bot.Bank.Open();
        var rewards = Core.EnsureLoad(1238).Rewards.Select(x => x.ID);
        if (Core.CheckInventory(rewards.ToArray(), toInv: false))
            return;

        Core.Logger("Pay attention to the popup's, they are crucial to this bot");
        var accept = Bot.ShowMessageBox("This bot will use up a lot of your ACs (which costs real money).\n" +
        "We are not liable for any loss of ACs whilst using this bot.\n\n" +
        "Do you wish to proceed?",
        "Your AC is about to be spend", "Yes", "No", "Just crunch the numbers");
        if (accept == null || accept.Text == "No")
            return;

        int obtainedCount = Bot.Inventory.Items.Concat(Bot.Bank.Items).Count(item => rewards.Contains(item.ID));
        int maxKeys = rewards.Count() - obtainedCount;

        if (Core.CheckInventory(new[] { "Dark Key", "Dark Box" }, toInv: false))
            stopForDarkBoxAndKey = false;
        else if (stopForDarkBoxAndKey == null)
        {
            float oneEst = 100 / ((rewards.Count() - obtainedCount) / ((float)rewards.Count()));
            float twoEst = 100 / ((obtainedCount + oneEst) / (float)rewards.Count());

            var result = Bot.ShowMessageBox(
                "Do you wish for the bot to stop after the Dark Key and Dark Box have been obtained?\n\n" +
                $"Estimation:\t{Math.Ceiling(oneEst)} times for one\t\t({Math.Ceiling(oneEst) * 200} ACs)\n" +
                $"Estimation:\t{Math.Ceiling(twoEst)} times for both\t({Math.Ceiling(twoEst) * 200} ACs)\n" +
                $"Guaranteed:\t{maxKeys} times for both\t({maxKeys * 200} ACs)",
                "Dark Items?", accept.Text != "Just crunch the numbers" ? new[] { "Yes", "No" } : new[] { "OK" });

            if (result == null)
                return;

            stopForDarkBoxAndKey = result.Text == "Yes";
        }

        var goForbroke = Bot.ShowMessageBox("Do you wish to use select how many keys to use?\n" +
        "Or maybe you wanna go for broke",
        "Mode Selector", "Select Amount", "GO FOR BROKE!");

        int amount = 0;
        int currentAC = Bot.Flash.GetGameObject<int>("world.myAvatar.objData.intCoins");
        if (goForbroke.Text == "Select Amount")
        {
            InputDialogViewModel diag = new("Input Amount", "How many keys would you like to buy and use?", true);
            while (!Bot.ShouldExit)
            {
                if (Ioc.Default.GetRequiredService<IDialogService>().ShowDialog(diag) == true)
                {
                    amount = int.Parse(diag.DialogTextInput);
                    if (amount <= 0)
                        Bot.ShowMessageBox("Please provide a number greater than zero (0) in order to continue.", "Invalid input");
                    else
                    {
                        int afterAC = currentAC - (amount * 200);
                        if (afterAC < 0)
                        {
                            Bot.ShowMessageBox(
                                $"Your input: {amount} keys\n" +
                                $"This will cost you {amount * 200} ACs. (200 ACs per key)\n" +
                                $"This would leave you with {afterAC} ACs afterwards, which is impossible.\n\n" +
                                "Please choose a different amount of keys",
                                $"Calculating based on {amount} keys"
                            );
                        }
                        else
                        {
                            bool? acceptResult = Bot.ShowMessageBox(
                                $"Your input: {amount} keys\n" +
                                $"This will cost you {amount * 200} ACs. (200 ACs per key)\n" +
                                $"This would leave you with {afterAC} ACs afterwards.\n\n" +
                                "Do you wish to proceed?",
                                $"Calculating based on {amount} keys", true
                            );
                            if (acceptResult == true)
                                break;
                        }
                    }
                }
                else Bot.Stop(false);
            }
        }
        else if (goForbroke.Text == "GO FOR BROKE!")
            amount = currentAC / 200;

        amount = amount > maxKeys ? maxKeys : amount;
        Core.KillMonster("swordhavenundead", "Left", "Right", "*", "Treasure Chest", amount > 250 ? 250 : amount, false);

        Core.Join("battleon");
        Bot.Shops.Load(314);

        for (int i = 0; i < amount; i++)
        {
            if (!Core.CheckInventory("Treasure Chest", 1))
                Core.KillMonster("swordhavenundead", "Left", "Right", "*", "Treasure Chest", (amount - i) > 250 ? 250 : (amount - i), false);

            Bot.Shops.BuyItem(8939, 6483);

            Core.Sleep();
            var preKeyItems = Bot.Inventory.Items;

            Core.ChainComplete(1238);
            Core.Sleep();

            var newItems = Bot.Inventory.Items.Except(preKeyItems);
            foreach (var item in newItems)
                Core.Logger("New Item: " + item.Name);

            if (stopForDarkBoxAndKey == true && Core.CheckInventory(new[] { "Dark Key", "Dark Box" }))
            {
                Bot.ShowMessageBox("Dark Box and Dark Key obtained!", "Dark Items!");
                break;
            }

            if (Core.CheckInventory(rewards.ToArray(), toInv: false))
            {
                Bot.ShowMessageBox("All Treasure Chest items obtained!", "You maxed out the Treasure Chests");
                break;
            }
        }
    }
}
