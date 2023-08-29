using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using BetfairDotNet.Models.Streaming;
using Spectre.Console;

namespace BetfairDotNet.Demo.StreamingAPI;


internal static class Display {

    private static List<RunnerCatalog> _runners = new();
    private static int _tableStartRow = 5;


    internal static void RenderMarketSnapshot(MarketCatalogue mc, MarketSnapshot ms) {
        if(_runners.Count == 0) {
            _runners = mc.Runners;

            AnsiConsole.Clear();
            AnsiConsole.Cursor.Hide();
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[bold]{mc.MarketStartTime.Value.ToShortTimeString()} {mc.Event?.Venue}: {mc.MarketName}[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine();
            foreach(var runner in _runners) {
                AnsiConsole.WriteLine(runner.RunnerName.PadRight(30) + "-".PadRight(30) + "-");
            }
        }
        else {
            UpdateLiveTable(ms);
        }
    }

    private static void UpdateLiveTable(MarketSnapshot ms) {
        var orderedSnaps = ms.RunnerSnapshots.Values.OrderBy(r => r.RunnerDefinition?.SortPriority ?? 0).ToList();

        const int width = 10;

        for(var i = 0; i < orderedSnaps.Count; i++) {
            var runner = orderedSnaps[i];
            if(runner.RunnerDefinition?.Status != RunnerStatusEnum.ACTIVE) continue;

            var backData = Enumerable.Range(0, 3).Select(index =>
                index < runner.ToBack.GetDepth() ?
                    new {
                        Price = $"[bold #92FAFF]{runner.ToBack[index]?.Price.ToString("F2").PadRight(width)}[/]",
                        Size = $"[grey]£{runner.ToBack[index]?.Size.ToString("F2").PadRight(width - 1)}[/]"
                    } :
                    new { Price = "-".PadRight(width), Size = "£-".PadRight(width - 1) }
            ).Reverse().ToArray();

            var layData = Enumerable.Range(0, 3).Select(index =>
                index < runner.ToLay.GetDepth() ?
                    new {
                        Price = $"[bold #F994A9]{runner.ToLay[index]?.Price.ToString("F2").PadRight(width)}[/]",
                        Size = $"[grey]£{runner.ToLay[index]?.Size.ToString("F2").PadRight(width - 1)}[/]"
                    } :
                    new { Price = "-".PadRight(width), Size = "£-".PadRight(width - 1) }
            ).ToArray();

            var backDisplay = string.Join(" ", backData.Select(b => b.Price));
            var layDisplay = string.Join(" ", layData.Select(l => l.Price));

            var backSizeDisplay = string.Join(" ", backData.Select(b => b.Size));
            var laySizeDisplay = string.Join(" ", layData.Select(l => l.Size));

            AnsiConsole.Cursor.SetPosition(0, _tableStartRow + (i * 2));
            AnsiConsole.MarkupLine($"{_runners[i].RunnerName,-30} {backDisplay}  {layDisplay}");

            AnsiConsole.Cursor.SetPosition(0, _tableStartRow + (i * 2) + 1);
            AnsiConsole.MarkupLine($"{"",-30} {backSizeDisplay}  {laySizeDisplay}");
        }
    }
}
