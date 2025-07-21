// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.VSCode;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the VSCode Dark+ color palette with official hex values.
/// Based on the default VSCode Dark+ theme.
/// </summary>
public class VSCodeDark : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#1e1e1e");
	public static readonly PerceptualColor SidebarBackground = PerceptualColor.FromRgb("#252526");
	public static readonly PerceptualColor Selection = PerceptualColor.FromRgb("#264f78");
	public static readonly PerceptualColor LineHighlight = PerceptualColor.FromRgb("#2a2d2e");
	public static readonly PerceptualColor Foreground = PerceptualColor.FromRgb("#d4d4d4");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#6a9955");
	public static readonly PerceptualColor StringColor = PerceptualColor.FromRgb("#ce9178");
	public static readonly PerceptualColor Number = PerceptualColor.FromRgb("#b5cea8");
	public static readonly PerceptualColor Keyword = PerceptualColor.FromRgb("#569cd6");
	public static readonly PerceptualColor Function = PerceptualColor.FromRgb("#dcdcaa");
	public static readonly PerceptualColor Variable = PerceptualColor.FromRgb("#9cdcfe");
	public static readonly PerceptualColor Type = PerceptualColor.FromRgb("#4ec9b0");
	public static readonly PerceptualColor Error = PerceptualColor.FromRgb("#f44747");
	public static readonly PerceptualColor Warning = PerceptualColor.FromRgb("#ffcc02");
	public static readonly PerceptualColor Info = PerceptualColor.FromRgb("#75beff");
	public static readonly PerceptualColor Purple = PerceptualColor.FromRgb("#c586c0");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#d7ba7d");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Foreground,      // Lightest
		Comment,
		Selection,
		LineHighlight,
		SidebarBackground,
		Background,      // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Keyword],
		[SemanticMeaning.Alternate] = [Purple],
		[SemanticMeaning.Success] = [Number],
		[SemanticMeaning.CallToAction] = [Number],
		[SemanticMeaning.Information] = [Info],
		[SemanticMeaning.Caution] = [StringColor],
		[SemanticMeaning.Warning] = [Warning],
		[SemanticMeaning.Error] = [Error],
		[SemanticMeaning.Failure] = [Error],
		[SemanticMeaning.Debug] = [Purple]
	};

	/// <summary>
	/// VSCode Dark+ is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
