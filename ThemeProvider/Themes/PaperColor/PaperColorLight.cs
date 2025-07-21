// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.PaperColor;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the PaperColor Light color palette with official hex values.
/// A clean, minimal light theme inspired by Google's Material Design.
/// Based on the PaperColor theme by NLKNguyen.
/// </summary>
public class PaperColorLight : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Light background colors
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#eeeeee");
	public static readonly PerceptualColor BgAlt = PerceptualColor.FromRgb("#ffffff");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#eeeeee");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#e4e4e4");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#d0d0d0");
	public static readonly PerceptualColor Selection = PerceptualColor.FromRgb("#e4e4e4");
	public static readonly PerceptualColor LineNumbers = PerceptualColor.FromRgb("#878787");

	// Dark foreground colors
	public static readonly PerceptualColor Fg0 = PerceptualColor.FromRgb("#444444");
	public static readonly PerceptualColor Fg1 = PerceptualColor.FromRgb("#878787");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#8e908c");

	// Material Design inspired colors
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#af0000");
	public static readonly PerceptualColor Pink = PerceptualColor.FromRgb("#d70087");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#d75f00");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#d78700");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#008700");
	public static readonly PerceptualColor Teal = PerceptualColor.FromRgb("#00af87");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#0087af");
	public static readonly PerceptualColor Purple = PerceptualColor.FromRgb("#8700af");
	public static readonly PerceptualColor Brown = PerceptualColor.FromRgb("#5f8700");
	public static readonly PerceptualColor Gray = PerceptualColor.FromRgb("#5f5f5f");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg0,         // Darkest (for text in light theme)
		BgAlt,       // Lightest (for backgrounds)
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
	/// PaperColor Light is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
