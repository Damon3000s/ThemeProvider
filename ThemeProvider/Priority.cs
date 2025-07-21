// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;

/// <summary>
/// Defines the priority level for color assignment requests.
/// Higher priority requests are processed first when assigning colors to semantic meanings.
/// </summary>
public enum Priority
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	VeryLow,
	Low,
	MediumLow,
	Medium,
	MediumHigh,
	High,
	VeryHigh,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
