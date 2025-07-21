// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;

using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// Defines the contract for a semantic theme, which maps semantic meanings to perceptual color collections.
/// </summary>
public interface ISemanticTheme
{
	/// <summary>
	/// Gets the mapping of <see cref="SemanticMeaning"/> to collections of <see cref="PerceptualColor"/>.
	/// This enables semantic color assignment for UI elements based on their intended meaning.
	/// </summary>
	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping { get; }

	/// <summary>
	/// Gets a value indicating whether this is a dark theme.
	/// Used to determine color range sorting and interpolation direction.
	/// </summary>
	public bool IsDarkTheme { get; }
}
