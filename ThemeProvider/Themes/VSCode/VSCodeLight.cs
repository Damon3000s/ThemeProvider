// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.VSCode;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the VSCode Light+ color palette with official hex values.
/// Based on the default VSCode Light+ theme.
/// </summary>
public class VSCodeLight : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#ffffff");
	public static readonly PerceptualColor SidebarBackground = PerceptualColor.FromRgb("#f3f3f3");
	public static readonly PerceptualColor Selection = PerceptualColor.FromRgb("#add6ff");
	public static readonly PerceptualColor LineHighlight = PerceptualColor.FromRgb("#f0f0f0");
	public static readonly PerceptualColor Foreground = PerceptualColor.FromRgb("#000000");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#008000");
	public static readonly PerceptualColor StringColor = PerceptualColor.FromRgb("#a31515");
	public static readonly PerceptualColor Number = PerceptualColor.FromRgb("#098658");
	public static readonly PerceptualColor Keyword = PerceptualColor.FromRgb("#0000ff");
	public static readonly PerceptualColor Function = PerceptualColor.FromRgb("#795e26");
	public static readonly PerceptualColor Variable = PerceptualColor.FromRgb("#001080");
	public static readonly PerceptualColor Type = PerceptualColor.FromRgb("#267f99");
	public static readonly PerceptualColor Error = PerceptualColor.FromRgb("#cd3131");
	public static readonly PerceptualColor Warning = PerceptualColor.FromRgb("#bf8803");
	public static readonly PerceptualColor Info = PerceptualColor.FromRgb("#316bcd");
	public static readonly PerceptualColor Purple = PerceptualColor.FromRgb("#af00db");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#e07041");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Foreground,         // Darkest (for text in light theme)
		Background,         // Lightest (for backgrounds in light theme)
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
	/// VSCode Light+ is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
