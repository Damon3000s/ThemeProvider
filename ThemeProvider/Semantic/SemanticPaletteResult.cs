// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Semantic;
using System.Collections.Immutable;
using ktsu.ThemeProvider.Core;

/// <summary>
/// Result of semantic palette generation, containing the generated colors
/// and metadata about how well the requirements were satisfied.
/// </summary>
public sealed record SemanticPaletteResult
{
	/// <summary>The generated colors, indexed to match the input requests</summary>
	public required ImmutableArray<RgbColor> GeneratedColors { get; init; }

	/// <summary>How well each color satisfies its accessibility requirements (0.0 = fail, 1.0 = perfect)</summary>
	public required ImmutableArray<float> AccessibilityScores { get; init; }

	/// <summary>How well each color matches its semantic requirements (0.0 = poor, 1.0 = perfect)</summary>
	public required ImmutableArray<float> SemanticMatchScores { get; init; }

	/// <summary>Overall harmony score for the entire palette</summary>
	public required float OverallHarmonyScore { get; init; }

	/// <summary>Whether all critical accessibility requirements were met</summary>
	public bool MeetsAccessibilityRequirements => AccessibilityScores.All(score => score >= 0.8f);

	/// <summary>Warnings or notes about the generation process</summary>
	public ImmutableArray<string> Warnings { get; init; } = [];

	/// <summary>Additional metadata about the generation process</summary>
	public ImmutableDictionary<string, object> Metadata { get; init; } = ImmutableDictionary<string, object>.Empty;
}
