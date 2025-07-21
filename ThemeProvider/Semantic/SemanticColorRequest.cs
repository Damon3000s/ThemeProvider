// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Semantic;
using System.Collections.Immutable;
using ktsu.ThemeProvider.Core;

/// <summary>
/// Represents a semantic request for colors, describing the desired characteristics
/// rather than specific colors. This allows the system to generate appropriate
/// colors that match the theme while meeting accessibility requirements.
/// </summary>
public readonly record struct SemanticColorRequest
{
	/// <summary>The primary semantic specification being requested</summary>
	public SemanticColorSpec PrimarySpec { get; init; }

	/// <summary>Related specs that should be considered for harmony</summary>
	public ImmutableArray<SemanticColorSpec> RelatedSpecs { get; init; } = [];

	/// <summary>Desired temperature bias (-1.0 = cool, 1.0 = warm)</summary>
	public float DesiredTemperature { get; init; } = 0.0f;

	/// <summary>Desired energy level (0.0 = calm, 1.0 = energetic)</summary>
	public float DesiredEnergy { get; init; } = 0.5f;

	/// <summary>Desired formality level (0.0 = casual, 1.0 = formal)</summary>
	public float DesiredFormality { get; init; } = 0.5f;

	/// <summary>Required accessibility level</summary>
	public AccessibilityLevel AccessibilityRequirement { get; init; } = AccessibilityLevel.AA;

	/// <summary>Background color this will be used against (for contrast calculation)</summary>
	public RgbColor? BackgroundColor { get; init; }

	/// <summary>Whether this is for large text (affects contrast requirements)</summary>
	public bool IsLargeText { get; init; } = false;

	/// <summary>Importance weight (0.0 = low priority, 1.0 = critical)</summary>
	public float ImportanceWeight { get; init; } = 0.5f;

	/// <summary>Optional constraints on the final color</summary>
	public ColorConstraints? Constraints { get; init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="SemanticColorRequest"/> record struct
	/// with default values for all properties.
	/// </summary>
	public SemanticColorRequest()
	{
	}
}
