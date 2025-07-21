// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;
using System.Collections.Immutable;
using System.Numerics;

/// <summary>
/// Defines the semantic role of a color within a theme.
/// This enables applications to request colors by meaning rather than specific hue.
/// </summary>
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public enum ColorRole
{
	// Base colors - fundamental background and surface colors
	Base,           // Primary background
	Mantle,         // Secondary background (darker than base)
	Crust,          // Tertiary background (darkest background)

	// Surface colors - UI element backgrounds with hierarchy
	Surface0,       // Lowest elevation surface
	Surface1,       // Medium elevation surface
	Surface2,       // Highest elevation surface

	// Overlay colors - transparent/translucent elements
	Overlay0,       // Subtle overlay
	Overlay1,       // Medium overlay
	Overlay2,       // Strong overlay

	// Text colors - typography with hierarchy
	Text,           // Primary text
	Subtext0,       // Secondary text
	Subtext1,       // Tertiary text

	// Accent colors - semantic and interactive elements
	Primary,        // Primary brand/action color
	Secondary,      // Secondary brand color
	Success,        // Success states (green)
	Warning,        // Warning states (yellow)
	Error,          // Error states (red)
	Info,           // Informational states (blue)

	// Specific semantic colors
	Link,           // Hyperlinks and navigation
	LinkHover,      // Hovered links
	LinkActive,     // Active/pressed links
	LinkVisited,    // Visited links

	// Interactive element colors
	Button,         // Default button background
	ButtonHover,    // Hovered button background
	ButtonActive,   // Pressed button background
	ButtonDisabled, // Disabled button background

	// Form element colors
	Input,          // Input field background
	InputBorder,    // Input field border
	InputFocus,     // Focused input border
	InputError,     // Error state input border

	// Status colors
	Border,         // Default border color
	Divider,        // Divider/separator color
	Selection,      // Text selection background

	// Extended semantic colors for comprehensive coverage
	Highlight,      // Highlighted content
	Shadow,         // Drop shadows and depth
	Focus,          // Focus indicators
	Disabled,       // Disabled element color

	// Color-specific roles for when semantic mapping needs specific hues
	Red, Green, Blue, Yellow, Orange, Purple, Pink, Cyan,
	Teal, Lime, Amber, Indigo, Violet, Rose, Sky, Emerald
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

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

	/// <summary>Semantic weight indicating importance (0.0 = low, 1.0 = high)</summary>
	public float Weight { get; init; } = 0.5f;

	/// <summary>Temperature bias (-1.0 = cool, 1.0 = warm)</summary>
	public float Temperature { get; init; } = 0.0f;

	/// <summary>Energy level (0.0 = calm, 1.0 = energetic)</summary>
	public float Energy { get; init; } = 0.5f;

	/// <summary>
	/// Initializes a new instance of the <see cref="ColorProperties"/> struct with default values.
	/// </summary>
	public ColorProperties()
	{
	}

	/// <summary>Formality level (0.0 = casual, 1.0 = formal)</summary>
	public float Formality { get; init; } = 0.5f;

	/// <summary>Accessibility priority (0.0 = decorative, 1.0 = critical)</summary>
	public float AccessibilityPriority { get; init; } = 0.5f;

	/// <summary>
	/// Creates color properties from an RGB color with semantic attributes.
	/// </summary>
	public static ColorProperties FromRgb(RgbColor rgb, float weight = 0.5f, float temperature = 0.0f,
		float energy = 0.5f, float formality = 0.5f, float accessibilityPriority = 0.5f)
	{
		OklabColor oklab = ColorMath.RgbToOklab(rgb.ToLinear());
		return new ColorProperties
		{
			OklabValue = oklab,
			RgbValue = rgb,
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

/// <summary>
/// Defines a complete theme with semantic color roles and authentic specifications.
/// </summary>
public sealed record ThemeDefinition
{
	/// <summary>The name of the theme (e.g., "Catppuccin Mocha")</summary>
	public required string Name { get; init; }

	/// <summary>A description of the theme's characteristics</summary>
	public required string Description { get; init; }

	/// <summary>The theme author or source</summary>
	public required string Author { get; init; }

	/// <summary>Whether this is a dark or light theme</summary>
	public required bool IsDarkTheme { get; init; }

	/// <summary>Mapping of semantic roles to color properties</summary>
	public required ImmutableDictionary<ColorRole, ColorProperties> Colors { get; init; }

	/// <summary>Additional metadata about the theme</summary>
	public ImmutableDictionary<string, object> Metadata { get; init; } = ImmutableDictionary<string, object>.Empty;

	/// <summary>
	/// Gets a color by its semantic role.
	/// </summary>
	public ColorProperties GetColor(ColorRole role)
	{
		if (Colors.TryGetValue(role, out ColorProperties color))
		{
			return color;
		}

		throw new ArgumentException($"Color role '{role}' not defined in theme '{Name}'", nameof(role));
	}

	/// <summary>
	/// Tries to get a color by its semantic role.
	/// </summary>
	public bool TryGetColor(ColorRole role, out ColorProperties color) => Colors.TryGetValue(role, out color);

	/// <summary>
	/// Gets all colors defined in this theme.
	/// </summary>
	public IEnumerable<KeyValuePair<ColorRole, ColorProperties>> AllColors => Colors;

	/// <summary>
	/// Gets the vector dimensionality of this theme (number of semantic dimensions).
	/// </summary>
	public static int VectorDimensionality => 4; // Weight, Temperature, Energy, Formality

	/// <summary>
	/// Gets all colors that match the specified criteria.
	/// </summary>
	public IEnumerable<(ColorRole Role, ColorProperties Color)> GetColorsByPredicate(Func<ColorRole, ColorProperties, bool> predicate)
	{
		return Colors.Where(kvp => predicate(kvp.Key, kvp.Value))
					 .Select(kvp => (kvp.Key, kvp.Value));
	}

	/// <summary>
	/// Finds the closest semantic color to the specified target properties.
	/// </summary>
	public (ColorRole Role, ColorProperties Color) FindClosestColor(ColorProperties target)
	{
		KeyValuePair<ColorRole, ColorProperties> closest = Colors.MinBy(kvp => kvp.Value.SemanticDistanceTo(target));
		return (closest.Key, closest.Value);
	}

	/// <summary>
	/// Creates a builder for constructing theme definitions.
	/// </summary>
	public static ThemeBuilder CreateBuilder(string name) => new(name);
}

/// <summary>
/// Builder for creating theme definitions with fluent API.
/// </summary>
public sealed class ThemeBuilder
{
	private readonly string name;
	private string description = string.Empty;
	private string author = string.Empty;
	private bool isDarkTheme = true;
	private readonly Dictionary<ColorRole, ColorProperties> colors = [];
	private readonly Dictionary<string, object> metadata = [];

	internal ThemeBuilder(string name) => this.name = name;

	/// <summary>Sets the theme description.</summary>
	public ThemeBuilder WithDescription(string description)
	{
		this.description = description;
		return this;
	}

	/// <summary>Sets the theme author.</summary>
	public ThemeBuilder WithAuthor(string author)
	{
		this.author = author;
		return this;
	}

	/// <summary>Sets whether this is a dark theme.</summary>
	public ThemeBuilder SetDarkTheme(bool isDark)
	{
		isDarkTheme = isDark;
		return this;
	}

	/// <summary>Adds a color with the specified role and properties.</summary>
	public ThemeBuilder WithColor(ColorRole role, ColorProperties color)
	{
		colors[role] = color;
		return this;
	}

	/// <summary>Adds a color with the specified role and RGB value.</summary>
	public ThemeBuilder WithColor(ColorRole role, RgbColor rgb, float weight = 0.5f,
		float temperature = 0.0f, float energy = 0.5f, float formality = 0.5f, float accessibilityPriority = 0.5f) => WithColor(role, ColorProperties.FromRgb(rgb, weight, temperature, energy, formality, accessibilityPriority));

	/// <summary>Adds a color with the specified role and hex value.</summary>
	public ThemeBuilder WithColor(ColorRole role, string hex, float weight = 0.5f,
		float temperature = 0.0f, float energy = 0.5f, float formality = 0.5f, float accessibilityPriority = 0.5f) => WithColor(role, RgbColor.FromHex(hex), weight, temperature, energy, formality, accessibilityPriority);

	/// <summary>Adds metadata to the theme.</summary>
	public ThemeBuilder WithMetadata(string key, object value)
	{
		metadata[key] = value;
		return this;
	}

	/// <summary>Builds the theme definition.</summary>
	public ThemeDefinition Build()
	{
		if (string.IsNullOrEmpty(description))
		{
			throw new InvalidOperationException("Theme description is required");
		}

		if (string.IsNullOrEmpty(author))
		{
			throw new InvalidOperationException("Theme author is required");
		}

		if (colors.Count == 0)
		{
			throw new InvalidOperationException("Theme must define at least one color");
		}

		return new ThemeDefinition
		{
			Name = name,
			Description = description,
			Author = author,
			IsDarkTheme = isDarkTheme,
			Colors = colors.ToImmutableDictionary(),
			Metadata = metadata.ToImmutableDictionary()
		};
	}
}
