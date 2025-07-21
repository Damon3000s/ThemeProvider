// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes;
using System.Collections.Immutable;
using ktsu.ThemeProvider.Core;

/// <summary>
/// Builder for creating theme definitions with fluent API using semantic specifications.
/// </summary>
public sealed class ThemeBuilder
{
	private readonly string name;
	private string description = string.Empty;
	private string author = string.Empty;
	private bool isDarkTheme = true;
	private readonly Dictionary<SemanticColorSpec, ColorProperties> colors = [];
	private readonly Dictionary<SemanticMeaning, float> semanticHues = [];
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

	/// <summary>Adds a color with the specified semantic specification and properties.</summary>
	public ThemeBuilder WithColor(SemanticColorSpec spec, ColorProperties color)
	{
		colors[spec] = color;
		return this;
	}

	/// <summary>Adds a color with the specified semantic specification and RGB value.</summary>
	public ThemeBuilder WithColor(SemanticColorSpec spec, RgbColor rgb, float weight = 0.5f,
		float temperature = 0.0f, float energy = 0.5f, float formality = 0.5f, float accessibilityPriority = 0.5f) =>
		WithColor(spec, ColorProperties.FromRgb(rgb, spec, weight, temperature, energy, formality, accessibilityPriority));

	/// <summary>Adds a color with the specified semantic specification and hex value.</summary>
	public ThemeBuilder WithColor(SemanticColorSpec spec, string hex, float weight = 0.5f,
		float temperature = 0.0f, float energy = 0.5f, float formality = 0.5f, float accessibilityPriority = 0.5f) =>
		WithColor(spec, RgbColor.FromHex(hex), weight, temperature, energy, formality, accessibilityPriority);

	/// <summary>Sets the base hue for a semantic meaning (in degrees 0-360).</summary>
	public ThemeBuilder WithSemanticHue(SemanticMeaning meaning, float hue)
	{
		semanticHues[meaning] = hue % 360f;
		return this;
	}

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
			SemanticHues = semanticHues.ToImmutableDictionary(),
			Metadata = metadata.ToImmutableDictionary()
		};
	}
}
