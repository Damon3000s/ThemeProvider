// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Core;

/// <summary>
/// Defines semantic color meanings based on purpose rather than specific hue.
/// This determines hue assignment in the semantic color system.
/// </summary>
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public enum SemanticMeaning
{
	// Core semantic meanings for hue assignment
	Normal,      // Default/neutral content
	Emphasis,    // Emphasized content requiring attention
	Success,     // Successful operations, confirmations
	CallToAction, // Primary actions, buttons user should take
	Information, // Informational content, help text
	Caution,     // Cautionary content, warnings that need attention
	Warning,     // Warning states, potentially problematic
	Error,       // Error states, failed operations
	Failure,     // Failed operations (distinct from error)
	Debug        // Debug/development information
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
