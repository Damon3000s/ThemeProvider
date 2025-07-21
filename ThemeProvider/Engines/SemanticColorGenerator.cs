// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Engines;
using System.Collections.Immutable;
using ktsu.ThemeProvider.Core;
using ktsu.ThemeProvider.Themes;

/// <summary>
/// Generates semantic color palettes using the systematic hue/luminance/saturation approach.
/// This implements the three-dimensional color semantic system where:
/// - Hue is determined by semantic meaning
/// - Luminance is determined by elevation hierarchy
/// - Saturation is determined by importance level
/// </summary>
/// <param name="isDarkTheme">Whether generating for a dark theme</param>
/// <param name="customSemanticHues">Custom hue mapping, or null to use defaults</param>
public sealed class SemanticColorGenerator(bool isDarkTheme, IReadOnlyDictionary<SemanticMeaning, float>? customSemanticHues = null)
{
	/// <summary>Default hue assignments for semantic meanings (in degrees 0-360)</summary>
	public static readonly IReadOnlyDictionary<SemanticMeaning, float> DefaultSemanticHues =
		new Dictionary<SemanticMeaning, float>
		{
			[SemanticMeaning.Normal] = 220f,        // Blue-ish neutral
			[SemanticMeaning.Emphasis] = 45f,       // Orange for attention
			[SemanticMeaning.Success] = 120f,       // Green
			[SemanticMeaning.CallToAction] = 240f,  // Purple for primary actions
			[SemanticMeaning.Information] = 200f,   // Light blue
			[SemanticMeaning.Caution] = 35f,        // Orange-yellow
			[SemanticMeaning.Warning] = 60f,        // Yellow
			[SemanticMeaning.Error] = 0f,           // Red
			[SemanticMeaning.Failure] = 15f,        // Red-orange
			[SemanticMeaning.Debug] = 280f          // Purple for dev content
		};

	private readonly bool isDarkTheme = isDarkTheme;
	private readonly IReadOnlyDictionary<SemanticMeaning, float> semanticHues = customSemanticHues ?? DefaultSemanticHues;

	/// <summary>
	/// Generates a color based on semantic specification using the systematic approach.
	/// </summary>
	public RgbColor GenerateColor(SemanticColorSpec spec)
	{
		float hue = GetHueForMeaning(spec.Meaning);
		float lightness = GetLightnessForRole(spec.Role, spec.IsPrimary);
		float chroma = GetChromaForImportance(spec.Importance);

		// Create in Oklab space for perceptual uniformity
		OklabColor oklab = OklabColor.FromPolar(lightness, chroma, hue);

		// Convert to RGB and clamp to valid range
		RgbColor rgb = ColorMath.OklabToRgb(oklab).ToSRgb();
		return new RgbColor(
			Math.Clamp(rgb.R, 0f, 1f),
			Math.Clamp(rgb.G, 0f, 1f),
			Math.Clamp(rgb.B, 0f, 1f)
		);
	}

	/// <summary>
	/// Generates a complete theme using the semantic approach with specified color requirements.
	/// </summary>
	public ThemeDefinition GenerateTheme(string name, string description, string author,
		IEnumerable<SemanticColorSpec> requiredSpecs)
	{
		ArgumentNullException.ThrowIfNull(requiredSpecs);

		ThemeBuilder builder = ThemeDefinition.CreateBuilder(name)
			.WithDescription(description)
			.WithAuthor(author)
			.SetDarkTheme(isDarkTheme);

		// Add semantic hue mappings
		foreach ((SemanticMeaning meaning, float hue) in semanticHues)
		{
			builder.WithSemanticHue(meaning, hue);
		}

		// Generate colors for each required specification
		foreach (SemanticColorSpec spec in requiredSpecs)
		{
			RgbColor color = GenerateColor(spec);
			ColorProperties properties = ColorProperties.FromRgb(
				color,
				spec,
				weight: GetWeightForImportance(spec.Importance),
				temperature: GetTemperatureForMeaning(spec.Meaning),
				energy: GetEnergyForMeaning(spec.Meaning),
				formality: GetFormalityForRole(spec.Role),
				accessibilityPriority: GetAccessibilityForRole(spec.Role)
			);

			builder.WithColor(spec, properties);
		}

		return builder.Build();
	}

	/// <summary>
	/// Creates a comprehensive UI theme with all common semantic specifications.
	/// </summary>
	public static ThemeDefinition CreateComprehensiveTheme(string name, bool isDarkTheme,
		IReadOnlyDictionary<SemanticMeaning, float>? customHues = null)
	{
		SemanticColorGenerator generator = new(isDarkTheme, customHues);

		// Define comprehensive set of UI color specifications
		List<SemanticColorSpec> specs = [
			// Backgrounds
			new(SemanticMeaning.Normal, VisualRole.Background, ImportanceLevel.Low, true),

			// Surfaces
			new(SemanticMeaning.Normal, VisualRole.Surface, ImportanceLevel.Low, true),
			new(SemanticMeaning.Normal, VisualRole.Surface, ImportanceLevel.Medium, true),
			new(SemanticMeaning.Normal, VisualRole.Surface, ImportanceLevel.High, true),
			new(SemanticMeaning.Emphasis, VisualRole.Surface, ImportanceLevel.Medium, true),

			// Text hierarchy
			new(SemanticMeaning.Normal, VisualRole.Text, ImportanceLevel.Critical, true),  // Primary text
			new(SemanticMeaning.Normal, VisualRole.Text, ImportanceLevel.Medium, false),   // Secondary text

			// Semantic text colors
			new(SemanticMeaning.Success, VisualRole.Text, ImportanceLevel.High, true),
			new(SemanticMeaning.Warning, VisualRole.Text, ImportanceLevel.High, true),
			new(SemanticMeaning.Error, VisualRole.Text, ImportanceLevel.Critical, true),
			new(SemanticMeaning.CallToAction, VisualRole.Text, ImportanceLevel.High, true),
			new(SemanticMeaning.Information, VisualRole.Text, ImportanceLevel.Medium, true),

			// Widget/accent colors
			new(SemanticMeaning.CallToAction, VisualRole.Widget, ImportanceLevel.Critical, true), // Primary button
			new(SemanticMeaning.Normal, VisualRole.Widget, ImportanceLevel.Medium, false),        // Secondary button
			new(SemanticMeaning.Success, VisualRole.Widget, ImportanceLevel.High, true),
			new(SemanticMeaning.Warning, VisualRole.Widget, ImportanceLevel.High, true),
			new(SemanticMeaning.Error, VisualRole.Widget, ImportanceLevel.Critical, true),
			new(SemanticMeaning.Information, VisualRole.Widget, ImportanceLevel.Medium, true),

			// Debug/development colors
			new(SemanticMeaning.Debug, VisualRole.Text, ImportanceLevel.Low, true),
			new(SemanticMeaning.Debug, VisualRole.Widget, ImportanceLevel.Low, true)
		];

		return generator.GenerateTheme(
			name,
			$"Systematically generated {(isDarkTheme ? "dark" : "light")} theme using semantic color principles",
			"SemanticColorGenerator",
			specs
		);
	}

	private float GetHueForMeaning(SemanticMeaning meaning) =>
		semanticHues.GetValueOrDefault(meaning, 220f); // Default to neutral blue

	private float GetLightnessForRole(VisualRole role, bool isPrimary)
	{
		// Dark themes: high luminance = high elevation
		// Light themes: low luminance = high elevation
		float baseMultiplier = isDarkTheme ? 1.0f : -1.0f;
		float primaryAdjustment = isPrimary ? 0.1f : -0.05f;

		return role switch
		{
			VisualRole.Background => isDarkTheme ? 0.15f : 0.95f,
			VisualRole.Surface => isDarkTheme
				? 0.25f + (primaryAdjustment * baseMultiplier)
				: 0.85f + (primaryAdjustment * baseMultiplier),
			VisualRole.Widget => isDarkTheme
				? 0.45f + (primaryAdjustment * baseMultiplier)
				: 0.65f + (primaryAdjustment * baseMultiplier),
			VisualRole.Text => isDarkTheme
				? 0.85f + (primaryAdjustment * baseMultiplier)
				: 0.15f + (primaryAdjustment * baseMultiplier),
			_ => isDarkTheme ? 0.5f : 0.5f
		};
	}

	private static float GetChromaForImportance(ImportanceLevel importance)
	{
		// Higher importance = higher saturation
		return importance switch
		{
			ImportanceLevel.Low => 0.02f,      // Very desaturated
			ImportanceLevel.Medium => 0.06f,   // Moderately saturated
			ImportanceLevel.High => 0.12f,     // Highly saturated
			ImportanceLevel.Critical => 0.18f, // Maximum saturation
			_ => 0.06f
		};
	}

	private static float GetWeightForImportance(ImportanceLevel importance)
	{
		return importance switch
		{
			ImportanceLevel.Low => 0.2f,
			ImportanceLevel.Medium => 0.5f,
			ImportanceLevel.High => 0.8f,
			ImportanceLevel.Critical => 1.0f,
			_ => 0.5f
		};
	}

	private static float GetTemperatureForMeaning(SemanticMeaning meaning)
	{
		return meaning switch
		{
			SemanticMeaning.Error => 0.8f,        // Warm (red)
			SemanticMeaning.Warning => 0.6f,      // Warm (yellow)
			SemanticMeaning.Success => -0.2f,     // Cool (green)
			SemanticMeaning.Information => -0.5f, // Cool (blue)
			SemanticMeaning.CallToAction => 0.1f, // Slightly warm
			_ => 0.0f                             // Neutral
		};
	}

	private static float GetEnergyForMeaning(SemanticMeaning meaning)
	{
		return meaning switch
		{
			SemanticMeaning.CallToAction => 0.9f,
			SemanticMeaning.Error => 0.8f,
			SemanticMeaning.Warning => 0.7f,
			SemanticMeaning.Emphasis => 0.8f,
			SemanticMeaning.Success => 0.6f,
			SemanticMeaning.Debug => 0.3f,
			_ => 0.5f
		};
	}

	private static float GetFormalityForRole(VisualRole role)
	{
		return role switch
		{
			VisualRole.Text => 0.7f,       // Text should be fairly formal
			VisualRole.Widget => 0.4f,     // Widgets can be more casual
			VisualRole.Surface => 0.8f,    // Surfaces are formal/structural
			VisualRole.Background => 0.9f, // Backgrounds are very formal
			_ => 0.5f
		};
	}

	private static float GetAccessibilityForRole(VisualRole role)
	{
		return role switch
		{
			VisualRole.Text => 1.0f,       // Text is critical for accessibility
			VisualRole.Widget => 0.8f,     // Widgets are important
			VisualRole.Surface => 0.6f,    // Surfaces are moderately important
			VisualRole.Background => 0.3f, // Backgrounds are least critical
			_ => 0.5f
		};
	}
}
