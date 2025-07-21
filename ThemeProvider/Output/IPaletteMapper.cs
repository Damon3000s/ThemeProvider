// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Output;
using System.Collections.Immutable;
using ktsu.ThemeProvider.Themes;

/// <summary>
/// Interface for mapping semantic themes to UI framework-specific color palettes.
/// Implementations can convert semantic color specifications to concrete framework colors.
/// </summary>
/// <typeparam name="TColorKey">The type used to identify colors in the target framework (e.g., enum, string)</typeparam>
/// <typeparam name="TColorValue">The type used to represent color values in the target framework</typeparam>
public interface IPaletteMapper<TColorKey, TColorValue>
	where TColorKey : notnull
{
	/// <summary>
	/// Gets the name of the target framework this mapper supports.
	/// </summary>
	public string FrameworkName { get; }

	/// <summary>
	/// Maps a semantic theme to a complete color palette for the target framework.
	/// </summary>
	/// <param name="theme">The semantic theme to map from</param>
	/// <returns>A dictionary mapping framework color keys to color values</returns>
	public ImmutableDictionary<TColorKey, TColorValue> MapTheme(ThemeDefinition theme);

	/// <summary>
	/// Gets the default/fallback color for a specific framework color key when
	/// the semantic theme doesn't provide an appropriate mapping.
	/// </summary>
	/// <param name="colorKey">The framework color key</param>
	/// <returns>The default color value for this key</returns>
	public TColorValue GetDefaultColor(TColorKey colorKey);

	/// <summary>
	/// Gets metadata about the mapping, such as which semantic specs were used
	/// for each framework color.
	/// </summary>
	/// <param name="theme">The theme that was mapped</param>
	/// <returns>Metadata about the mapping process</returns>
	public ImmutableDictionary<string, object> GetMappingMetadata(ThemeDefinition theme);
}
