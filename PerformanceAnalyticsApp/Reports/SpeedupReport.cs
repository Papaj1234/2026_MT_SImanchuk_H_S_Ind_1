using System.Globalization;
using PerformanceAnalytics.Dtos;
using PerformanceAnalytics.Interfaces;
using PerformanceAnalytics.Loading;
using PerformanceAnalyticsApp.Interfaces;
using Spectre.Console;

namespace PerformanceAnalyticsApp.Reports;

public sealed class SpeedupReport : IReport
{
	private readonly ISpeedupAnalytics analytics;

	public SpeedupReport(ISpeedupAnalytics analytics)
	{
		this.analytics = analytics;
	}

	public string Title => "Parallelism speedup (Sequential / Parallel)";

	public void Render(AnalyticsDataSet data)
	{
		var rows = this.analytics.Speedups(data);

		var matrixPanel = BuildPanel(
			"Matrices",
			rows.Where(r => r.Scope == "Matrix").ToList(),
			showSize: true);

		var networkPanel = BuildPanel(
			"Network",
			rows.Where(r => r.Scope == "Network").ToList(),
			showSize: false);

		var grid = new Grid();
		grid.AddColumn();
		grid.AddColumn();
		grid.AddRow(matrixPanel, networkPanel);

		AnsiConsole.Write(grid);
	}

	private static Panel BuildPanel(string title, IReadOnlyList<SpeedupRow> rows, bool showSize)
	{
		var table = new Table().Border(TableBorder.Rounded);

		table.AddColumn("Operation");
		if (showSize)
		{
			table.AddColumn(new TableColumn("Size").RightAligned());
		}

		table.AddColumn(new TableColumn("Seq, ms").RightAligned());
		table.AddColumn(new TableColumn("Par, ms").RightAligned());
		table.AddColumn(new TableColumn("Speedup").RightAligned());

		foreach (var r in rows)
		{
			var color = r.Speedup >= 1.0 ? "green" : "red";
			var speedup = $"[{color}]{r.Speedup.ToString("N2", CultureInfo.InvariantCulture)}x[/]";
			var seq = r.AvgSequentialMs.ToString("N1", CultureInfo.InvariantCulture);
			var par = r.AvgParallelMs.ToString("N1", CultureInfo.InvariantCulture);

			if (showSize)
			{
				table.AddRow(r.Operation, r.MatrixSize.ToString(CultureInfo.InvariantCulture), seq, par, speedup);
			}
			else
			{
				table.AddRow(r.Operation, seq, par, speedup);
			}
		}

		return new Panel(table)
			.Header($"[bold]{title}[/]")
			.Border(BoxBorder.Double);
	}
}