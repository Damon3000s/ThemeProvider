// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Catppuccin;

using System.Collections.ObjectModel;
using ktsu.ThemeProvider.Core;

/// <summary>
/// Provides the official Catppuccin Mocha color palette with exact hex values and properties.
/// Based on the official specification: https://catppuccin.com/palette
/// </summary>
public class Mocha : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Base = PerceptualColor.FromRgb("#1e1e2e");
	public static readonly PerceptualColor Mantle = PerceptualColor.FromRgb("#181825");
	public static readonly PerceptualColor Crust = PerceptualColor.FromRgb("#11111b");
	public static readonly PerceptualColor Surface0 = PerceptualColor.FromRgb("#313244");
	public static readonly PerceptualColor Surface1 = PerceptualColor.FromRgb("#45475a");
	public static readonly PerceptualColor Surface2 = PerceptualColor.FromRgb("#585b70");
	public static readonly PerceptualColor Overlay0 = PerceptualColor.FromRgb("#6c7086");
	public static readonly PerceptualColor Overlay1 = PerceptualColor.FromRgb("#7f849c");
	public static readonly PerceptualColor Overlay2 = PerceptualColor.FromRgb("#9399b2");
	public static readonly PerceptualColor Text = PerceptualColor.FromRgb("#cdd6f4");
	public static readonly PerceptualColor Subtext1 = PerceptualColor.FromRgb("#bac2de");
	public static readonly PerceptualColor Subtext0 = PerceptualColor.FromRgb("#a6adc8");
	public static readonly PerceptualColor Lavender = PerceptualColor.FromRgb("#b4befe");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#89b4fa");
	public static readonly PerceptualColor Sapphire = PerceptualColor.FromRgb("#74c7ec");
	public static readonly PerceptualColor Sky = PerceptualColor.FromRgb("#89dceb");
	public static readonly PerceptualColor Teal = PerceptualColor.FromRgb("#94e2d5");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#a6e3a1");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#f9e2af");
	public static readonly PerceptualColor Peach = PerceptualColor.FromRgb("#fab387");
	public static readonly PerceptualColor Maroon = PerceptualColor.FromRgb("#eba0ac");
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#f38ba8");
	public static readonly PerceptualColor Mauve = PerceptualColor.FromRgb("#cba6f7");
	public static readonly PerceptualColor Pink = PerceptualColor.FromRgb("#f5c2e7");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Text,
		Subtext1,
		Subtext0,
		Overlay2,
		Overlay1,
		Overlay0,
		Surface2,
		Surface1,
		Surface0,
		Base,
		Mantle,
		Crust,
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Pink],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Green],
		[SemanticMeaning.Information] = [Sapphire],
		[SemanticMeaning.Caution] = [Maroon],
		[SemanticMeaning.Warning] = [Peach],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Mauve]
	};
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
