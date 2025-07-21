// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Semantic;
using System.Collections.Immutable;
using ktsu.ThemeProvider.Core;

/// <summary>
/// Defines constraints that can be applied to color generation.
/// </summary>
public readonly record struct ColorConstraints
{
	/// <summary>Minimum lightness (0.0 = black, 1.0 = white)</summary>
	public float? MinLightness { get; init; }

	/// <summary>Maximum lightness (0.0 = black, 1.0 = white)</summary>
	public float? MaxLightness { get; init; }

	/// <summary>Minimum chroma/saturation</summary>
	public float? MinChroma { get; init; }

	/// <summary>Maximum chroma/saturation</summary>
	public float? MaxChroma { get; init; }

	/// <summary>Preferred hue range (in degrees, 0-360)</summary>
	public (float Min, float Max)? HueRange { get; init; }

	/// <summary>Colors to avoid (maintain distance from)</summary>
	public ImmutableArray<RgbColor> AvoidColors { get; init; } = [];

	/// <summary>Minimum distance to maintain from avoid colors</summary>
	public float AvoidanceDistance { get; init; } = 0.1f;

	/// <summary>
	/// Initializes a new instance of the <see cref="ColorConstraints"/> record struct
	/// with default values for all properties.
	/// </summary>
	public ColorConstraints()
	{
	}
}
