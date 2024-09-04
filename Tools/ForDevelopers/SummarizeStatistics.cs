/*
name: SummarizeStatistics
description: null
tags: null
*/
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Globalization;
using Skua.Core.Interfaces;
using Skua.Core.Models;
using Skua.Core.Options;
using CommunityToolkit.Mvvm.DependencyInjection;

public class SummarizeStats
{
    public List<IOption> Options = new()
    {
        new Option<int>("topAll", "Top X of all time", "", 5),
        new Option<int>("top31", "Top X of the last 31 days", "", 5),
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        ReadData(Bot);
    }
#nullable enable
    private void ReadData(IScriptInterface Bot)
    {
        string? path = null;//@"C:\Users\jesse\Downloads\Skua Script Statistics Form.csv\Skua Script Statistics Form.csv";
        if (path == null)
        {
            _fileDialog = Ioc.Default.GetRequiredService<IFileDialogService>();
            path = _fileDialog.OpenFile(ClientFileSources.SkuaScriptsDIR, "Statistics File (*.csv)|*.csv");
            if (path == null)
                return;
        }
        string[][] file = File.ReadAllLines(path)[3..].Select(d => d[1..^1].Split("\",\"")).ToArray();
        List<DataObject> data = new();
        foreach (string[] ar in file)
            data.Add(new(ar));

        var lastItem = data.Last();
        var days31 = data.Where(x => (lastItem.TimeStamp - x.TimeStamp).TotalDays <= 31);
        var days7 = data.Where(x => (lastItem.TimeStamp - x.TimeStamp).TotalDays <= 7);
        var hours24 = data.Where(x => (lastItem.TimeStamp - x.TimeStamp).TotalHours <= 24);

        Bot.Log("Basic Statistics\n");
        Bot.Log("Dataset size:\t\t" + data.Count);
        Bot.Log("First datapoint:\t\t" + data.First().TimeStamp);
        Bot.Log("Last datapoint:\t\t" + lastItem.TimeStamp);
        Bot.Log("User Count (Total):\t" + data.DistinctBy(x => x.UserID).Count());
        Bot.Log("User Count (31 Days):\t" + days31.DistinctBy(x => x.UserID).Count());
        Bot.Log("User Count (7 Days):\t" + days7.DistinctBy(x => x.UserID).Count());
        Bot.Log("User Count (24h):\t\t" + hours24.DistinctBy(x => x.UserID).Count());
        Bot.Log("Amount of bots started (Total):\t" + data.Where(x => x.Start == true).Count());
        Bot.Log("Amount of bots started (31 Days):\t" + days31.Where(x => x.Start == true).Count());
        Bot.Log("Amount of bots started (7 Days):\t" + days7.Where(x => x.Start == true).Count());
        Bot.Log("Amount of bots started (24h):\t" + hours24.Where(x => x.Start == true).Count());

        int cTop = Bot.Config!.Get<int>("topAll");
        Bot.Log($"\nTop {cTop} scripts (All Time)");
        var topAll = data.Where(x => !string.IsNullOrEmpty(x.ScriptPath)).GroupBy(x => x.ScriptPath?.Replace('\\', '/').Replace(".cs", "")).OrderByDescending(x => x.Count());
        for (int i = 0; i < cTop;)
        {
            var script = topAll.Skip(i++).First();
            Bot.Log($"#{i}\tx{script.Count()}\t{script.Key}");
        }

        int c31 = Bot.Config!.Get<int>("top31");
        Bot.Log($"\nTop {c31} scripts (31 Days)");
        var top31 = days31.Where(x => !string.IsNullOrEmpty(x.ScriptPath)).GroupBy(x => x.ScriptPath?.Replace('\\', '/').Replace(".cs", "")).OrderByDescending(x => x.Count());
        for (int i = 0; i < c31;)
        {
            var script = top31.Skip(i++).First();
            Bot.Log($"#{i}\tx{script.Count()}\t{script.Key}");
        }
    }
    private IFileDialogService? _fileDialog;

    private class DataObject
    {
        public DateTime TimeStamp { get; set; }
        public int UserID { get; set; }
        public string? ScriptPath { get; set; }
        public bool? Start { get; set; }
        public int? InstanceID { get; set; }

        public DataObject(string[] input)
        {
            string[] parts = input[0].Split(' ');
            TimeStamp = DateTime.ParseExact(
                $"{parts[0]} {(parts[1].Split(':')[0].Length == 1 ? 0 + parts[1] : parts[1])} {parts[2]}",
                "yyyy/MM/dd hh:mm:ss tt",
                CultureInfo.CreateSpecificCulture("en-US")
            ).AddHours(1);

            UserID = int.Parse(input[1]);
            ScriptPath = string.IsNullOrEmpty(input[2]) ? null : input[2];
            Start = (input[3]) switch
            {
                "Start" => true,
                "Stop" => false,
                _ => null
            };
            InstanceID = string.IsNullOrEmpty(input[4]) ? null : int.Parse(input[4]);
        }
    }
}
