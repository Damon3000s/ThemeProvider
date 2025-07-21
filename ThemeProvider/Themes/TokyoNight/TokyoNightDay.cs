// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.TokyoNight;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Tokyo Night Day color palette with official hex values.
/// The light variant of Tokyo Night with bright backgrounds and dark text.
/// Based on the Tokyo Night theme by enkia.
/// </summary>
public class TokyoNightDay : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Light background colors
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#e1e2e7");
	public static readonly PerceptualColor BgAlt = PerceptualColor.FromRgb("#e9e9ed");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#e1e2e7");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#e9e9ec");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#c4c8da");
	public static readonly PerceptualColor Sel0 = PerceptualColor.FromRgb("#b7c5d3");
	public static readonly PerceptualColor Sel1 = PerceptualColor.FromRgb("#a8b4c5");

	// Dark foreground colors for light theme
	public static readonly PerceptualColor Fg0 = PerceptualColor.FromRgb("#3760bf");
	public static readonly PerceptualColor Fg1 = PerceptualColor.FromRgb("#4c505e");
	public static readonly PerceptualColor Fg2 = PerceptualColor.FromRgb("#5a5f6d");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#9699a3");

	// Tokyo Night Day color palette
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#2e7de9");
	public static readonly PerceptualColor Purple = PerceptualColor.FromRgb("#9854f1");
	public static readonly PerceptualColor Cyan = PerceptualColor.FromRgb("#007197");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#587539");
	public static readonly PerceptualColor Teal = PerceptualColor.FromRgb("#33635c");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#8c6c3e");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#b15c00");
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#f52a65");
	public static readonly PerceptualColor Magenta = PerceptualColor.FromRgb("#9854f1");

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
		[SemanticMeaning.CallToAction] = [Cyan],
		[SemanticMeaning.Information] = [Blue],
		[SemanticMeaning.Caution] = [Yellow],
		[SemanticMeaning.Warning] = [Orange],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Magenta]
	};

	/// <summary>
	/// Tokyo Night Day is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
