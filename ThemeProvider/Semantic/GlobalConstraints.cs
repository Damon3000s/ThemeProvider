// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Semantic;

/// <summary>
/// Global constraints that apply to an entire color palette.
/// </summary>
public readonly record struct GlobalConstraints
{
	/// <summary>Overall color harmony preference (0.0 = diverse, 1.0 = harmonious)</summary>
	public float HarmonyPreference { get; init; } = 0.7f;

	/// <summary>Overall accessibility priority (0.0 = aesthetics first, 1.0 = accessibility first)</summary>
	public float AccessibilityPriority { get; init; } = 0.8f;

	/// <summary>Color distribution preference (0.0 = clustered, 1.0 = distributed)</summary>
	public float DistributionPreference { get; init; } = 0.6f;

	/// <summary>Whether to prefer theme authenticity over exact semantic matching</summary>
	public bool PreferThemeAuthenticity { get; init; } = true;

	/// <summary>
	/// Initializes a new instance of the <see cref="GlobalConstraints"/> record struct
	/// with default values for all properties.
	/// </summary>
	public GlobalConstraints()
	{
	}
}
