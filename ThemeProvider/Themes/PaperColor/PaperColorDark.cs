// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.PaperColor;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the PaperColor Dark color palette with official hex values.
/// A clean, minimal dark theme inspired by Google's Material Design.
/// Based on the PaperColor theme by NLKNguyen.
/// </summary>
public class PaperColorDark : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Dark background colors
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#1c1c1c");
	public static readonly PerceptualColor BgAlt = PerceptualColor.FromRgb("#262626");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#1c1c1c");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#262626");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#303030");
	public static readonly PerceptualColor Selection = PerceptualColor.FromRgb("#4e4e4e");
	public static readonly PerceptualColor LineNumbers = PerceptualColor.FromRgb("#585858");

	// Light foreground colors for dark theme
	public static readonly PerceptualColor Fg0 = PerceptualColor.FromRgb("#d0d0d0");
	public static readonly PerceptualColor Fg1 = PerceptualColor.FromRgb("#bcbcbc");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#808080");

	// Material Design inspired dark colors
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#af005f");
	public static readonly PerceptualColor Pink = PerceptualColor.FromRgb("#d70087");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#ff8700");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#ffaf00");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#5faf00");
	public static readonly PerceptualColor Teal = PerceptualColor.FromRgb("#00afaf");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#0087d7");
	public static readonly PerceptualColor Purple = PerceptualColor.FromRgb("#af87d7");
	public static readonly PerceptualColor Brown = PerceptualColor.FromRgb("#8f8f00");
	public static readonly PerceptualColor Gray = PerceptualColor.FromRgb("#8a8a8a");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg0,         // Lightest
		Background,  // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Purple],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Teal],
		[SemanticMeaning.Information] = [Blue],
		[SemanticMeaning.Caution] = [Yellow],
		[SemanticMeaning.Warning] = [Orange],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Pink]
	};

	/// <summary>
	/// PaperColor Dark is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
