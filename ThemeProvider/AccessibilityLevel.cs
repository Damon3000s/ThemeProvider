// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;

/// <summary>
/// Represents accessibility compliance levels according to WCAG standards.
/// </summary>
public enum AccessibilityLevel
{
	/// <summary>Does not meet minimum accessibility standards</summary>
	Fail,
	/// <summary>Meets WCAG AA standards (minimum compliance)</summary>
	AA,
	/// <summary>Meets WCAG AAA standards (enhanced compliance)</summary>
	AAA
}
