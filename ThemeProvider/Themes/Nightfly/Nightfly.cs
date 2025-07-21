// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nightfly;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Nightfly color palette with official hex values.
/// Based on the Nightfly theme by bluz71.
/// </summary>
public class Nightfly : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#011627");
	public static readonly PerceptualColor Selection = PerceptualColor.FromRgb("#1d3b53");
	public static readonly PerceptualColor Foreground = PerceptualColor.FromRgb("#d6deeb");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#637777");
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#fc514e");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#f78c6c");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#e3d18a");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#addb67");
	public static readonly PerceptualColor Teal = PerceptualColor.FromRgb("#4db5bd");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#82aaff");
	public static readonly PerceptualColor Purple = PerceptualColor.FromRgb("#c792ea");
	public static readonly PerceptualColor Cyan = PerceptualColor.FromRgb("#7fdbca");
	public static readonly PerceptualColor White = PerceptualColor.FromRgb("#ffffff");
	public static readonly PerceptualColor Gray1 = PerceptualColor.FromRgb("#1e2030");
	public static readonly PerceptualColor Gray2 = PerceptualColor.FromRgb("#2c3043");
	public static readonly PerceptualColor Gray3 = PerceptualColor.FromRgb("#506477");
	public static readonly PerceptualColor Gray4 = PerceptualColor.FromRgb("#7e8294");
	public static readonly PerceptualColor Gray5 = PerceptualColor.FromRgb("#a1aab8");

	public static Collection<PerceptualColor> Neutrals =>
	[
		White,       // Lightest
		Foreground,
		Gray5,
		Gray4,
		Gray3,
		Comment,
		Selection,
		Gray2,
		Gray1,
		Background,  // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Purple],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Green],
		[SemanticMeaning.Information] = [Cyan],
		[SemanticMeaning.Caution] = [Orange],
		[SemanticMeaning.Warning] = [Yellow],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Purple]
	};

	/// <summary>
	/// Nightfly is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
