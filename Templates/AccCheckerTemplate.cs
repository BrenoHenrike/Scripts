//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

public class AccCheckerTemplate
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();

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
        string[] BlacklistedServers =
        {
            "artix",
            "sir ver",
            "yorumi",
            "gravelyn",
            "galanoth",
            "class test realm"
        };

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
            else Core.Join("Whitemap-999999");

            Bot.Sleep(2000);

            //Insert things you wanted done for all accounts here


            //PUT YOUR SHIT HERE

            ///////////^^^^
        }

        string[] fileSetup()
        {
            string path = "Skua_Modules/options/TheFamily.txt";
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
    }
}