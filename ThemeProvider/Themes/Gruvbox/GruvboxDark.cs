// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Gruvbox;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Gruvbox Dark color palette with official hex values.
/// Based on the Gruvbox theme by morhetz.
/// </summary>
public class GruvboxDark : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Dark colors
	public static readonly PerceptualColor DarkHard = PerceptualColor.FromRgb("#1d2021");
	public static readonly PerceptualColor Dark0 = PerceptualColor.FromRgb("#282828");
	public static readonly PerceptualColor Dark0Soft = PerceptualColor.FromRgb("#32302f");
	public static readonly PerceptualColor Dark1 = PerceptualColor.FromRgb("#3c3836");
	public static readonly PerceptualColor Dark2 = PerceptualColor.FromRgb("#504945");
	public static readonly PerceptualColor Dark3 = PerceptualColor.FromRgb("#665c54");
	public static readonly PerceptualColor Dark4 = PerceptualColor.FromRgb("#7c6f64");

	// Light colors
	public static readonly PerceptualColor Light0Hard = PerceptualColor.FromRgb("#f9f5d7");
	public static readonly PerceptualColor Light0 = PerceptualColor.FromRgb("#fbf1c7");
	public static readonly PerceptualColor Light0Soft = PerceptualColor.FromRgb("#f2e5bc");
	public static readonly PerceptualColor Light1 = PerceptualColor.FromRgb("#ebdbb2");
	public static readonly PerceptualColor Light2 = PerceptualColor.FromRgb("#d5c4a1");
	public static readonly PerceptualColor Light3 = PerceptualColor.FromRgb("#bdae93");
	public static readonly PerceptualColor Light4 = PerceptualColor.FromRgb("#a89984");

	// Bright colors
	public static readonly PerceptualColor BrightRed = PerceptualColor.FromRgb("#fb4934");
	public static readonly PerceptualColor BrightGreen = PerceptualColor.FromRgb("#b8bb26");
	public static readonly PerceptualColor BrightYellow = PerceptualColor.FromRgb("#fabd2f");
	public static readonly PerceptualColor BrightBlue = PerceptualColor.FromRgb("#83a598");
	public static readonly PerceptualColor BrightPurple = PerceptualColor.FromRgb("#d3869b");
	public static readonly PerceptualColor BrightAqua = PerceptualColor.FromRgb("#8ec07c");
	public static readonly PerceptualColor BrightOrange = PerceptualColor.FromRgb("#fe8019");

	// Neutral colors
	public static readonly PerceptualColor Gray = PerceptualColor.FromRgb("#928374");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Light1,      // Lightest
		Light2,
		Light3,
		Light4,
		Gray,
		Dark4,
		Dark3,
		Dark2,
		Dark1,
		Dark0Soft,
		Dark0,
		DarkHard,    // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [BrightBlue],
		[SemanticMeaning.Alternate] = [BrightPurple],
		[SemanticMeaning.Success] = [BrightGreen],
		[SemanticMeaning.CallToAction] = [BrightGreen],
		[SemanticMeaning.Information] = [BrightAqua],
		[SemanticMeaning.Caution] = [BrightOrange],
		[SemanticMeaning.Warning] = [BrightYellow],
		[SemanticMeaning.Error] = [BrightRed],
		[SemanticMeaning.Failure] = [BrightRed],
		[SemanticMeaning.Debug] = [BrightPurple]
	};

	/// <summary>
	/// Gruvbox Dark is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
