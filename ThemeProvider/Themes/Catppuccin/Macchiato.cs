// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Catppuccin;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the official Catppuccin Macchiato color palette with exact hex values and properties.
/// Based on the official specification: https://catppuccin.com/palette
/// </summary>
public class Macchiato : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Base = PerceptualColor.FromRgb("#24273a");
	public static readonly PerceptualColor Mantle = PerceptualColor.FromRgb("#1e2030");
	public static readonly PerceptualColor Crust = PerceptualColor.FromRgb("#181926");
	public static readonly PerceptualColor Surface0 = PerceptualColor.FromRgb("#363a4f");
	public static readonly PerceptualColor Surface1 = PerceptualColor.FromRgb("#494d64");
	public static readonly PerceptualColor Surface2 = PerceptualColor.FromRgb("#5b6078");
	public static readonly PerceptualColor Overlay0 = PerceptualColor.FromRgb("#6e738d");
	public static readonly PerceptualColor Overlay1 = PerceptualColor.FromRgb("#8087a2");
	public static readonly PerceptualColor Overlay2 = PerceptualColor.FromRgb("#939ab7");
	public static readonly PerceptualColor Text = PerceptualColor.FromRgb("#cad3f5");
	public static readonly PerceptualColor Subtext1 = PerceptualColor.FromRgb("#b8c0e0");
	public static readonly PerceptualColor Subtext0 = PerceptualColor.FromRgb("#a5adcb");
	public static readonly PerceptualColor Lavender = PerceptualColor.FromRgb("#b7bdf8");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#8aadf4");
	public static readonly PerceptualColor Sapphire = PerceptualColor.FromRgb("#7dc4e4");
	public static readonly PerceptualColor Sky = PerceptualColor.FromRgb("#91d7e3");
	public static readonly PerceptualColor Teal = PerceptualColor.FromRgb("#8bd5ca");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#a6da95");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#eed49f");
	public static readonly PerceptualColor Peach = PerceptualColor.FromRgb("#f5a97f");
	public static readonly PerceptualColor Maroon = PerceptualColor.FromRgb("#ee99a0");
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#ed8796");
	public static readonly PerceptualColor Mauve = PerceptualColor.FromRgb("#c6a0f6");
	public static readonly PerceptualColor Pink = PerceptualColor.FromRgb("#f5bde6");

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
	/// Catppuccin Macchiato is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
