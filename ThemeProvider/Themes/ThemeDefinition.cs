// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes;
using System.Collections.Immutable;
using ktsu.ThemeProvider.Core;

/// <summary>
/// Defines a complete theme with semantic color specifications.
/// Uses the systematic semantic approach rather than specific color roles.
/// </summary>
public sealed record ThemeDefinition
{
	/// <summary>The name of the theme (e.g., "Material Design Dark")</summary>
	public required string Name { get; init; }

	/// <summary>A description of the theme's characteristics</summary>
	public required string Description { get; init; }

	/// <summary>The theme author or source</summary>
	public required string Author { get; init; }

	/// <summary>Whether this is a dark or light theme</summary>
	public required bool IsDarkTheme { get; init; } // TODO: theme types should be an enum Light, Medium, and Dark

	/// <summary>Mapping of semantic specifications to color properties</summary>
	public required ImmutableDictionary<SemanticColorSpec, ColorProperties> Colors { get; init; }

	/// <summary>Base hue mapping for semantic meanings (in degrees 0-360)</summary>
	public required ImmutableDictionary<SemanticMeaning, float> SemanticHues { get; init; }

	/// <summary>Additional metadata about the theme</summary>
	public ImmutableDictionary<string, object> Metadata { get; init; } = ImmutableDictionary<string, object>.Empty;

	/// <summary>
	/// Gets a color by its semantic specification.
	/// </summary>
	public ColorProperties GetColor(SemanticColorSpec spec)
	{
		if (Colors.TryGetValue(spec, out ColorProperties color))
		{
			return color;
		}

		throw new ArgumentException($"Semantic color spec '{spec}' not defined in theme '{Name}'", nameof(spec));
	}

	/// <summary>
	/// Tries to get a color by its semantic specification.
	/// </summary>
	public bool TryGetColor(SemanticColorSpec spec, out ColorProperties color) => Colors.TryGetValue(spec, out color);

	/// <summary>
	/// Gets all colors defined in this theme.
	/// </summary>
	public IEnumerable<KeyValuePair<SemanticColorSpec, ColorProperties>> AllColors => Colors;

	/// <summary>
	/// Gets all colors that match the specified criteria.
	/// </summary>
	public IEnumerable<(SemanticColorSpec Spec, ColorProperties Color)> GetColorsByPredicate(Func<SemanticColorSpec, ColorProperties, bool> predicate)
	{
		return Colors.Where(kvp => predicate(kvp.Key, kvp.Value))
					 .Select(kvp => (kvp.Key, kvp.Value));
	}

	/// <summary>
	/// Gets all colors for a specific semantic meaning.
	/// </summary>
	public IEnumerable<(SemanticColorSpec Spec, ColorProperties Color)> GetColorsByMeaning(SemanticMeaning meaning) =>
		GetColorsByPredicate((spec, _) => spec.Meaning == meaning);

	/// <summary>
	/// Gets all colors for a specific visual role.
	/// </summary>
	public IEnumerable<(SemanticColorSpec Spec, ColorProperties Color)> GetColorsByRole(VisualRole role) =>
		GetColorsByPredicate((spec, _) => spec.Role == role);

	/// <summary>
	/// Gets all colors for a specific importance level.
	/// </summary>
	public IEnumerable<(SemanticColorSpec Spec, ColorProperties Color)> GetColorsByImportance(ImportanceLevel importance) =>
		GetColorsByPredicate((spec, _) => spec.Importance == importance);

	/// <summary>
	/// Finds the closest semantic color to the specified target properties.
	/// </summary>
	public (SemanticColorSpec Spec, ColorProperties Color) FindClosestColor(ColorProperties target)
	{
		KeyValuePair<SemanticColorSpec, ColorProperties> closest = Colors.MinBy(kvp => kvp.Value.SemanticDistanceTo(target));
		return (closest.Key, closest.Value);
	}

	/// <summary>
	/// Creates a builder for constructing theme definitions.
	/// </summary>
	public static ThemeBuilder CreateBuilder(string name) => new(name);
}
