// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Monokai;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the classic Monokai color palette with exact hex values.
/// Based on the original Monokai theme by Wimer Hazenberg.
/// </summary>
public class Monokai : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#272822");
	public static readonly PerceptualColor CurrentLine = PerceptualColor.FromRgb("#49483e");
	public static readonly PerceptualColor Selection = PerceptualColor.FromRgb("#49483e");
	public static readonly PerceptualColor Foreground = PerceptualColor.FromRgb("#f8f8f2");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#75715e");
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#f92672");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#fd971f");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#f4bf75");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#a6e22e");
	public static readonly PerceptualColor Aqua = PerceptualColor.FromRgb("#a1efe4");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#66d9ef");
	public static readonly PerceptualColor Purple = PerceptualColor.FromRgb("#ae81ff");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Foreground,    // Lightest
		Comment,
		Selection,
		CurrentLine,
		Background,    // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Purple],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Green],
		[SemanticMeaning.Information] = [Aqua],
		[SemanticMeaning.Caution] = [Orange],
		[SemanticMeaning.Warning] = [Yellow],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Purple]
	};

	/// <summary>
	/// Monokai is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
