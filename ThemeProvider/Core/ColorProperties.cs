// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Core;
using System.Numerics;

/// <summary>
/// Represents the perceptual and semantic properties of a color.
/// This enables vector-space operations for semantic color mapping.
/// </summary>
public readonly record struct ColorProperties
{
	/// <summary>The color in Oklab perceptual space</summary>
	public OklabColor OklabValue { get; init; }

	/// <summary>The RGB representation of the color</summary>
	public RgbColor RgbValue { get; init; }

	/// <summary>The semantic specification this color represents</summary>
	public SemanticColorSpec SemanticSpec { get; init; }

	/// <summary>Semantic weight indicating importance (0.0 = low, 1.0 = high)</summary>
	public float Weight { get; init; } = 0.5f;

	/// <summary>Temperature bias (-1.0 = cool, 1.0 = warm)</summary>
	public float Temperature { get; init; } = 0.0f;

	/// <summary>Energy level (0.0 = calm, 1.0 = energetic)</summary>
	public float Energy { get; init; } = 0.5f;

	/// <summary>Formality level (0.0 = casual, 1.0 = formal)</summary>
	public float Formality { get; init; } = 0.5f;

	/// <summary>Accessibility priority (0.0 = decorative, 1.0 = critical)</summary>
	public float AccessibilityPriority { get; init; } = 0.5f;

	/// <summary>
	/// Initializes a new instance of the <see cref="ColorProperties"/> struct with default values.
	/// </summary>
	public ColorProperties()
	{
	}

	/// <summary>
	/// Creates color properties from an RGB color with semantic specification.
	/// </summary>
	public static ColorProperties FromRgb(RgbColor rgb, SemanticColorSpec semanticSpec, float weight = 0.5f,
		float temperature = 0.0f, float energy = 0.5f, float formality = 0.5f, float accessibilityPriority = 0.5f)
	{
		OklabColor oklab = ColorMath.RgbToOklab(rgb);
		return new ColorProperties
		{
			OklabValue = oklab,
			RgbValue = rgb,
			SemanticSpec = semanticSpec,
			Weight = weight,
			Temperature = temperature,
			Energy = energy,
			Formality = formality,
			AccessibilityPriority = accessibilityPriority
		};
	}

	/// <summary>
	/// Converts the semantic properties to a vector for mathematical operations.
	/// Combines perceptual color (Oklab) with semantic dimensions.
	/// </summary>
	public Vector4 ToSemanticVector() =>
		// Primary semantic dimensions: Temperature, Energy, Formality, AccessibilityPriority
		new(Temperature, Energy, Formality, AccessibilityPriority);

	/// <summary>
	/// Converts the perceptual properties to a vector for color space operations.
	/// </summary>
	public Vector3 ToPerceptualVector() => OklabValue.ToVector3();

	/// <summary>
	/// Calculates semantic distance to another color, considering both perceptual and semantic dimensions.
	/// </summary>
	public float SemanticDistanceTo(ColorProperties other)
	{
		// Perceptual distance (weighted higher as it's most important)
		float perceptualDistance = OklabValue.DistanceTo(other.OklabValue) * 2.0f;

		// Semantic distance
		Vector4 semanticDiff = ToSemanticVector() - other.ToSemanticVector();
		float semanticDistance = semanticDiff.Length();

		// Combined distance with perceptual weighted more heavily
		return MathF.Sqrt((perceptualDistance * perceptualDistance) + (semanticDistance * semanticDistance));
	}
}
