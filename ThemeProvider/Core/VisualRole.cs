// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Core;

/// <summary>
/// Defines the role a color plays in the visual hierarchy.
/// This determines luminance based on elevation (dark themes: high lum = high elevation, light themes: low lum = high elevation).
/// </summary>
public enum VisualRole
{
	/// <summary>
	/// Primary background, lowest elevation in the visual hierarchy.
	/// </summary>
	Background,

	/// <summary>
	/// UI element backgrounds that render over backgrounds, medium elevation.
	/// </summary>
	Surface,

	/// <summary>
	/// Interactive elements and accents that render over surfaces, higher elevation.
	/// </summary>
	Widget,

	/// <summary>
	/// Text content that must render over all other elements, highest elevation.
	/// </summary>
	Text
}
