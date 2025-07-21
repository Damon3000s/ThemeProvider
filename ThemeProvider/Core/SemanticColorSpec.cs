// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Core;

/// <summary>
/// Represents a complete semantic color specification using the systematic approach.
/// This defines colors by their semantic meaning, visual role, and importance rather than specific hues.
/// </summary>
public readonly record struct SemanticColorSpec
{
	/// <summary>The semantic meaning determining hue</summary>
	public SemanticMeaning Meaning { get; init; }

	/// <summary>The visual role determining luminance/elevation</summary>
	public VisualRole Role { get; init; }

	/// <summary>The importance level determining saturation</summary>
	public ImportanceLevel Importance { get; init; }

	/// <summary>
	/// Creates a new semantic color specification.
	/// </summary>
	/// <param name="meaning">The semantic meaning determining hue</param>
	/// <param name="role">The visual role determining luminance/elevation</param>
	/// <param name="importance">The importance level determining saturation</param>
	public SemanticColorSpec(SemanticMeaning meaning, VisualRole role, ImportanceLevel importance = ImportanceLevel.Medium)
	{
		Meaning = meaning;
		Role = role;
		Importance = importance;
	}

	/// <summary>
	/// Converts to a readable string representation.
	/// </summary>
	public override string ToString() => $"{Role} {Meaning} ({Importance})";
}
