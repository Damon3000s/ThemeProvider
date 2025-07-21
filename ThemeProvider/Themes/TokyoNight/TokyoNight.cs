// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.TokyoNight;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Tokyo Night color palette with official hex values.
/// Based on the popular Tokyo Night theme by Enkia.
/// </summary>
public class TokyoNight : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#1a1b26");
	public static readonly PerceptualColor BackgroundHighlight = PerceptualColor.FromRgb("#24283b");
	public static readonly PerceptualColor Terminal = PerceptualColor.FromRgb("#1d202f");
	public static readonly PerceptualColor Foreground = PerceptualColor.FromRgb("#c0caf5");
	public static readonly PerceptualColor ForegroundDark = PerceptualColor.FromRgb("#a9b1d6");
	public static readonly PerceptualColor ForegroundGutter = PerceptualColor.FromRgb("#3b4261");
	public static readonly PerceptualColor Dark3 = PerceptualColor.FromRgb("#545c7e");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#565f89");
	public static readonly PerceptualColor Dark5 = PerceptualColor.FromRgb("#737aa2");
	public static readonly PerceptualColor Blue0 = PerceptualColor.FromRgb("#3d59a1");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#7aa2f7");
	public static readonly PerceptualColor Cyan = PerceptualColor.FromRgb("#7dcfff");
	public static readonly PerceptualColor Blue1 = PerceptualColor.FromRgb("#2ac3de");
	public static readonly PerceptualColor Blue2 = PerceptualColor.FromRgb("#0db9d7");
	public static readonly PerceptualColor Blue5 = PerceptualColor.FromRgb("#89ddff");
	public static readonly PerceptualColor Blue6 = PerceptualColor.FromRgb("#b4f9f8");
	public static readonly PerceptualColor Blue7 = PerceptualColor.FromRgb("#394b70");
	public static readonly PerceptualColor Magenta = PerceptualColor.FromRgb("#bb9af7");
	public static readonly PerceptualColor Magenta2 = PerceptualColor.FromRgb("#ff007c");
	public static readonly PerceptualColor Purple = PerceptualColor.FromRgb("#9d7cd8");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#ff9e64");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#e0af68");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#9ece6a");
	public static readonly PerceptualColor Green1 = PerceptualColor.FromRgb("#73daca");
	public static readonly PerceptualColor Green2 = PerceptualColor.FromRgb("#41a6b5");
	public static readonly PerceptualColor Teal = PerceptualColor.FromRgb("#1abc9c");
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#f7768e");
	public static readonly PerceptualColor Red1 = PerceptualColor.FromRgb("#db4b4b");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Foreground,         // Lightest
		Background,         // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Magenta],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Green],
		[SemanticMeaning.Information] = [Cyan],
		[SemanticMeaning.Caution] = [Orange],
		[SemanticMeaning.Warning] = [Yellow],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red1],
		[SemanticMeaning.Debug] = [Purple]
	};

	/// <summary>
	/// Tokyo Night is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
