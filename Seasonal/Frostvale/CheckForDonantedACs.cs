//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Seasonal/Frostvale/ChillysParticipation.cs
using Skua.Core.Interfaces;
using Skua.Core.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

public class CheckForDonatedACs
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private ChillysQuest CQ = new();
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();
        Core.BankingBlackList.Add("Treasure Potion");

        CheckACs();

        Core.SetOptions(false);
    }

    public void CheckACs()
    {
        var acc = fileSetup();

        Bot.Options.AutoRelogin = false;
        string logPath = "Skua_Modules/options/FrostvaleDonationLog.txt";
        bool firstTime = !File.Exists(logPath);
        List<string> ACs = new();
        List<string> oldACs = new();
        List<string> newACs = new();
        List<string> warnings = new();
        if (!firstTime)
            oldACs = File.ReadAllLines(logPath).ToList();
        string[] BlacklistedServers =
        {
            "artix",
            "sir ver",
            "yorumi",
            "gravelyn",
            "galanoth",
            "class test realm"
        };

        Bot.Events.ExtensionPacketReceived += ACsListener;
        for (int i = 0; i < acc.Length; i++)
        {
            string name = acc[i++];
            string pass = acc[i];

            if (Core.Username() != name)
            {
                if (Bot.Player.LoggedIn)
                {
                    Bot.Servers.Logout();
                    Bot.Sleep(Core.ActionDelay);
                }
                Bot.Servers.Login(name, pass);
                Bot.Sleep(3000);
                Bot.Servers.Connect(Bot.Servers.CachedServers.Where(x => !BlacklistedServers.Contains(x.Name.ToLower())).ToArray()[Bot.Random.Next(0, 8)]);
                Bot.Wait.ForMapLoad("battleon");
                while (!Bot.Player.Loaded) { }
            }
            else Core.Join("battleon-999999");

            Bot.Sleep(2000);

            Daily.WheelofDoom();
            Daily.MonthlyTreasureChestKeys();

            //Requierments:
            // Level 30
            Farm.Experience(30);

            // Two week old account
            string _output = Bot.Flash.GetGameObject("world.myAvatar.objData.dCreated")!;
            //"Fri Dec 3 08:32:00 GMT+0100 2021"
            string[] output = _output[1..^1].Split(' ');
            string[] time = output[3].Split(':');
            var creationDate = new DateTime(
                Int32.Parse(output[5]),
                Months.First(x => x.Key == output[1]).Value,
                Int32.Parse(output[2]),
                Int32.Parse(time[0]),
                Int32.Parse(time[1]),
                Int32.Parse(time[2]),
                DateTimeKind.Unspecified
                );
            double accountAgeInDays = DateTime.Now.Subtract(creationDate).TotalDays;
            if (accountAgeInDays < (double)14)
            {
                Core.Logger($"Account too young: {Core.Username()} ({accountAgeInDays.ToString()}/14 days) - Skipping");
                warnings.Add($"- {Core.Username()}: account is too young ({accountAgeInDays.ToString()}/14 days)");
                continue;
            }

            // Verified Email
            if (Bot.Flash.CallGameFunction<bool>("world.myAvatar.isEmailVerified"))
                // Participation Quest
                CQ.ChillysParticipation();
            else
            {
                Core.Logger($"Unverified Email: {Core.Username()} - Skipping");
                warnings.Add($"- {Core.Username()}: email is unverified ({Bot.Flash.GetGameObject("world.myAvatar.objData.strEmail")[1..^1]})");
                continue;
            }
        }
        Bot.Events.ExtensionPacketReceived -= ACsListener;

        List<string> writeACs = new();
        writeACs.AddRange(newACs);
        foreach (var p in oldACs)
        {
            string name = p.Split(':').First();
            if (!writeACs.Any(x => x.StartsWith(name)))
                writeACs.Add(p);
        }
        File.WriteAllLines(logPath, writeACs);

        if (newACs.Count() == 0)
            Bot.ShowMessageBox($"We checked {acc.Count() / 2} accounts, but none of them have gained any {(firstTime ? "ACs" : "more ACs since last time")}." +
            $"{(warnings.Count() > 0 ? "\n\nPlease be aware of the following things:\n" + String.Join('\n', warnings) : "")}",
            (Bot.Random.Next(1, 100) == 100 ? "No Maidens" : "No ACs"));
        else
        {
            Bot.ShowMessageBox($"{newACs.Count()} out of {acc.Count() / 2} accounts received ACs! Below you will find more details:\n\n" + String.Join('\n', ACs) +
            $"{(warnings.Count() > 0 ? "\n\nPlease be aware of the following things:\n" + String.Join('\n', warnings) : "")}", "Got ACs!");
        }

        string[] fileSetup()
        {
            string path = "Skua_Modules/options/FrostvaleDonations.txt";
            if (File.Exists(path))
                return File.ReadAllLines(path);

            Bot.ShowMessageBox("Your login details will be saved locally on your own device. We will not receive them.", "A heads up");

            int i = 1;
            string title = $"Please provide the login details for account #";
            string data = string.Empty;
            Dictionary<string, string> redo = new();

            while (!Bot.ShouldExit)
            {
                bool goRedo = redo.Count() != 0;

                var name = new InputDialogViewModel(title + i, "Account Name", false);
                if (goRedo)
                    name.DialogTextInput = redo.First().Key;
                if (isInvalid(name))
                    break;

                var pass = new InputDialogViewModel(title + i, "Account Password:", false);
                if (goRedo)
                    pass.DialogTextInput = redo.First().Value;
                if (isInvalid(pass))
                    break;

                var res = Bot.ShowMessageBox(
                    "Is this correct?\n\n" +
                    "Name:\t\t" + name.DialogTextInput + "\n" +
                    "Password:\t" + pass.DialogTextInput,
                    "Confirm that these are correct",
                    $"Yes, go to account #{i + 1}", "Yes, I am now done", "No"
                );

                redo = new();
                if (res.Text == "No")
                    redo.Add(name.DialogTextInput, pass.DialogTextInput);
                else
                {
                    data += $"{name.DialogTextInput}\n{pass.DialogTextInput}\n";
                    if (!res.Text.StartsWith("Yes, go"))
                        break;
                    i++;
                }
            }

            if (String.IsNullOrEmpty(data))
                Core.Logger("No input provided, stopping the bot.", messageBox: true, stopBot: true);

            File.WriteAllText(path, data[..^1]);
            Bot.Handlers.RegisterOnce(1, Bot => Bot.ShowMessageBox($"If you ever wish to edit things, the file can be found at:\n{Core.AppPath + "/" + path}", "File path"));
            return data[..^1].Split('\n');

            bool isInvalid(InputDialogViewModel input) =>
                Ioc.Default.GetRequiredService<IDialogService>().ShowDialog(input) != true ||
                String.IsNullOrEmpty(input.DialogTextInput) ||
                String.IsNullOrWhiteSpace(input.DialogTextInput);
        }

        void ACsListener(dynamic packet)
        {
            string type = packet["params"].type;
            dynamic data = packet["params"].dataObj;
            if (type is not null and "str")
            {
                string cmd = data[0];
                switch (cmd)
                {
                    case "server":
                        if (data[2] == null)
                            break;
                        string text = data[2].ToString();
                        if (text.Contains("AdventureCoins from other players. Happy Frostval!"))
                        {
                            int ac = Int32.Parse(text.Split(' ')[2]);
                            Core.Logger($"{Core.Username()} has received {ac} ACs!");
                            int acLog = Int32.Parse((oldACs.Find(x => x.StartsWith(Core.Username())) ?? "a:0").Split(':').Last()) + ac;

                            ACs.Add($"{Core.Username()}: +{ac} (received {acLog} ACs total)");
                            newACs.Add($"{Core.Username()}:{acLog}");
                        }
                        break;
                }
            }
        }
    }
    private Dictionary<string, int> Months = new()
    {
        { "Jan", 1 },
        { "Feb", 2 },
        { "Mar", 3 },
        { "May", 4 },
        { "Apr", 5 },
        { "Jun", 6 },
        { "Jul", 7 },
        { "Aug", 8 },
        { "Sep", 9 },
        { "Oct", 10},
        { "Nov", 11},
        { "Dec", 12}
    };
}