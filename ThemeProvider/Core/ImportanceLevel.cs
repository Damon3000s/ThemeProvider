// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Core;

/// <summary>
/// Defines the importance level for saturation assignment.
/// Higher importance receives higher saturation.
/// </summary>
public enum ImportanceLevel
{
	/// <summary>
	/// 0-25% importance, low saturation for less prominent elements.
	/// </summary>
	Low = 0,

	/// <summary>
	/// 25-50% importance, medium saturation for standard elements.
	/// </summary>
	Medium = 1,

	/// <summary>
	/// 50-75% importance, high saturation for important elements.
	/// </summary>
	High = 2,

	/// <summary>
	/// 75-100% importance, highest saturation for critical elements.
	/// </summary>
	Critical = 3
}
