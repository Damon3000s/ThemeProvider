// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Catppuccin;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the official Catppuccin Frappe color palette with exact hex values and properties.
/// Based on the official specification: https://catppuccin.com/palette
/// </summary>
public class Frappe : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Base = PerceptualColor.FromRgb("#303446");
	public static readonly PerceptualColor Mantle = PerceptualColor.FromRgb("#292c3c");
	public static readonly PerceptualColor Crust = PerceptualColor.FromRgb("#232634");
	public static readonly PerceptualColor Surface0 = PerceptualColor.FromRgb("#414559");
	public static readonly PerceptualColor Surface1 = PerceptualColor.FromRgb("#51576d");
	public static readonly PerceptualColor Surface2 = PerceptualColor.FromRgb("#626880");
	public static readonly PerceptualColor Overlay0 = PerceptualColor.FromRgb("#737994");
	public static readonly PerceptualColor Overlay1 = PerceptualColor.FromRgb("#838ba7");
	public static readonly PerceptualColor Overlay2 = PerceptualColor.FromRgb("#949cbb");
	public static readonly PerceptualColor Text = PerceptualColor.FromRgb("#c6d0f5");
	public static readonly PerceptualColor Subtext1 = PerceptualColor.FromRgb("#b5bfe2");
	public static readonly PerceptualColor Subtext0 = PerceptualColor.FromRgb("#a5adce");
	public static readonly PerceptualColor Lavender = PerceptualColor.FromRgb("#babbf1");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#8caaee");
	public static readonly PerceptualColor Sapphire = PerceptualColor.FromRgb("#85c1dc");
	public static readonly PerceptualColor Sky = PerceptualColor.FromRgb("#99d1db");
	public static readonly PerceptualColor Teal = PerceptualColor.FromRgb("#81c8be");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#a6d189");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#e5c890");
	public static readonly PerceptualColor Peach = PerceptualColor.FromRgb("#ef9f76");
	public static readonly PerceptualColor Maroon = PerceptualColor.FromRgb("#ea999c");
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#e78284");
	public static readonly PerceptualColor Mauve = PerceptualColor.FromRgb("#ca9ee6");
	public static readonly PerceptualColor Pink = PerceptualColor.FromRgb("#f4b8e4");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Text,
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

	/// <summary>
	/// Catppuccin Frappe is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
