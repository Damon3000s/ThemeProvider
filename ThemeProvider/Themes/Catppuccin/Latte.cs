// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Catppuccin;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the official Catppuccin Latte color palette with exact hex values and properties.
/// Based on the official specification: https://catppuccin.com/palette
/// </summary>
public class Latte : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Base = PerceptualColor.FromRgb("#eff1f5");
	public static readonly PerceptualColor Mantle = PerceptualColor.FromRgb("#e6e9ef");
	public static readonly PerceptualColor Crust = PerceptualColor.FromRgb("#dce0e8");
	public static readonly PerceptualColor Surface0 = PerceptualColor.FromRgb("#ccd0da");
	public static readonly PerceptualColor Surface1 = PerceptualColor.FromRgb("#bcc0cc");
	public static readonly PerceptualColor Surface2 = PerceptualColor.FromRgb("#acb0be");
	public static readonly PerceptualColor Overlay0 = PerceptualColor.FromRgb("#9ca0b0");
	public static readonly PerceptualColor Overlay1 = PerceptualColor.FromRgb("#8c8fa1");
	public static readonly PerceptualColor Overlay2 = PerceptualColor.FromRgb("#7c7f93");
	public static readonly PerceptualColor Text = PerceptualColor.FromRgb("#4c4f69");
	public static readonly PerceptualColor Subtext1 = PerceptualColor.FromRgb("#5c5f77");
	public static readonly PerceptualColor Subtext0 = PerceptualColor.FromRgb("#6c6f85");
	public static readonly PerceptualColor Lavender = PerceptualColor.FromRgb("#7287fd");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#1e66f5");
	public static readonly PerceptualColor Sapphire = PerceptualColor.FromRgb("#209fb5");
	public static readonly PerceptualColor Sky = PerceptualColor.FromRgb("#04a5e5");
	public static readonly PerceptualColor Teal = PerceptualColor.FromRgb("#179299");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#40a02b");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#df8e1d");
	public static readonly PerceptualColor Peach = PerceptualColor.FromRgb("#fe640b");
	public static readonly PerceptualColor Maroon = PerceptualColor.FromRgb("#e64553");
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#d20f39");
	public static readonly PerceptualColor Mauve = PerceptualColor.FromRgb("#8839ef");
	public static readonly PerceptualColor Pink = PerceptualColor.FromRgb("#ea76cb");

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

	/// <summary>
	/// Catppuccin Latte is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
