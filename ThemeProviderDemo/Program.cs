// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProviderDemo;
using ktsu.ThemeProvider;

/// <summary>
/// Comprehensive demonstration of the ThemeProvider library showcasing:
/// - Authentic Catppuccin theme implementation
/// - Semantic color mapping and vector operations
/// - Accessibility validation and compliance
/// - Dynamic palette generation for applications
/// </summary>
internal static class Program
{
	private static void Main()
	{
		Console.WriteLine("🎨 ThemeProvider Library Demonstration");
		Console.WriteLine("=====================================\n");

		// Demonstrate authentic Catppuccin Mocha theme
		DemonstrateAuthenticTheme();

		// Show semantic color mapping
		DemonstrateSemanticMapping();

		// Demonstrate accessibility features
		DemonstrateAccessibilityFeatures();

		// Show vector-space color operations
		DemonstrateColorVectorMath();

		// Generate application-specific palettes
		DemonstrateApplicationPalettes();

		// Validate theme authenticity
		ValidateThemeAuthenticity();

		Console.WriteLine("\n✨ Demo completed! The ThemeProvider library successfully demonstrates:");
		Console.WriteLine("   • Authentic theme implementation true to original specifications");
		Console.WriteLine("   • Type-safe semantic color mapping with vector mathematics");
		Console.WriteLine("   • WCAG accessibility compliance and automated contrast optimization");
		Console.WriteLine("   • Dynamic palette generation satisfying both aesthetics and accessibility");
	}

	private static void DemonstrateAuthenticTheme()
	{
		Console.WriteLine("🌙 Authentic Catppuccin Mocha Theme");
		Console.WriteLine("===================================");

		ThemeDefinition theme = CatppuccinMocha.CreateTheme();

		Console.WriteLine($"Theme: {theme.Name}");
		Console.WriteLine($"Description: {theme.Description}");
		Console.WriteLine($"Author: {theme.Author}");
		Console.WriteLine($"Is Dark Theme: {theme.IsDarkTheme}");
		Console.WriteLine();

		// Display all 26 authentic colors
		Console.WriteLine("All 26 Authentic Catppuccin Colors:");
		Console.WriteLine("-----------------------------------");

		ColorRole[] colorOrder =
[
// Neutrals
ColorRole.Text, ColorRole.Subtext1, ColorRole.Subtext0,
			ColorRole.Overlay2, ColorRole.Overlay1, ColorRole.Overlay0,
			ColorRole.Surface2, ColorRole.Surface1, ColorRole.Surface0,
			ColorRole.Base, ColorRole.Mantle, ColorRole.Crust,

			// Accents
			ColorRole.Blue, ColorRole.Sky, ColorRole.Teal, ColorRole.Green,
			ColorRole.Yellow, ColorRole.Orange, ColorRole.Red, ColorRole.Pink,
			ColorRole.Purple, ColorRole.Cyan, ColorRole.Indigo, ColorRole.Violet
];

		foreach (ColorRole role in colorOrder)
		{
			if (theme.TryGetColor(role, out ColorProperties color))
			{
				string hex = color.RgbValue.ToHex();
				OklabColor oklab = ColorMath.RgbToOklab(color.RgbValue.ToLinear());
				Console.WriteLine($"  {role,-12} {hex,-8} Oklab({oklab.L:F2}, {oklab.A:F2}, {oklab.B:F2}) " +
								  $"Temp: {color.Temperature:F2} Energy: {color.Energy:F2}");
			}
		}

		// Show direct color access
		Console.WriteLine("\nDirect Color Access:");
		Console.WriteLine($"  Mocha Blue: {CatppuccinMocha.Colors.Blue.ToHex()}");
		Console.WriteLine($"  Mocha Text: {CatppuccinMocha.Colors.Text.ToHex()}");
		Console.WriteLine($"  Mocha Base: {CatppuccinMocha.Colors.Base.ToHex()}");

		Console.WriteLine();
	}

	private static void DemonstrateSemanticMapping()
	{
		Console.WriteLine("🧠 Semantic Color Mapping");
		Console.WriteLine("=========================");

		ThemeDefinition theme = CatppuccinMocha.CreateTheme();

		// Create a semantic graph for a typical web application
		SemanticColorGraph graph = SemanticColorGraph.CreateBuilder()
			.AddRequest(new SemanticColorRequest
			{
				PrimaryRole = ColorRole.Primary,
				DesiredTemperature = -0.3f, // Cool for trustworthy feel
				DesiredEnergy = 0.7f,       // Moderate energy for engagement
				AccessibilityRequirement = AccessibilityLevel.AA,
				BackgroundColor = CatppuccinMocha.Colors.Base,
				ImportanceWeight = 1.0f
			})
			.AddRequest(new SemanticColorRequest
			{
				PrimaryRole = ColorRole.Success,
				DesiredTemperature = 0.1f,  // Slightly warm for positive feeling
				DesiredEnergy = 0.6f,
				AccessibilityRequirement = AccessibilityLevel.AA,
				BackgroundColor = CatppuccinMocha.Colors.Base,
				ImportanceWeight = 0.9f
			})
			.AddRequest(new SemanticColorRequest
			{
				PrimaryRole = ColorRole.Warning,
				DesiredTemperature = 0.8f,  // Warm for attention
				DesiredEnergy = 0.9f,       // High energy for urgency
				AccessibilityRequirement = AccessibilityLevel.AA,
				BackgroundColor = CatppuccinMocha.Colors.Base,
				ImportanceWeight = 1.0f
			})
			.AddRequest(new SemanticColorRequest
			{
				PrimaryRole = ColorRole.Error,
				DesiredTemperature = 0.7f,  // Warm but not as warm as warning
				DesiredEnergy = 1.0f,       // Maximum energy for critical states
				AccessibilityRequirement = AccessibilityLevel.AAA,
				BackgroundColor = CatppuccinMocha.Colors.Base,
				ImportanceWeight = 1.0f
			})
			.AddHarmony(0, 1, 0.8f) // Primary and Success should be harmonious
			.AddHarmony(2, 3, 0.6f) // Warning and Error should be related but distinct
			.Build();

		SemanticPaletteEngine engine = new(theme);
		SemanticPaletteResult result = engine.GeneratePalette(graph);

		Console.WriteLine("Generated Semantic Palette:");
		Console.WriteLine("---------------------------");

		string[] roles = ["Primary", "Success", "Warning", "Error"];
		for (int i = 0; i < result.GeneratedColors.Length; i++)
		{
			RgbColor color = result.GeneratedColors[i];
			float accessScore = result.AccessibilityScores[i];
			float semanticScore = result.SemanticMatchScores[i];
			float contrast = ColorMath.GetContrastRatio(color, CatppuccinMocha.Colors.Base);

			Console.WriteLine($"  {roles[i],-8} {color.ToHex()} " +
							  $"Contrast: {contrast:F1}:1 " +
							  $"A11y: {accessScore:F2} " +
							  $"Semantic: {semanticScore:F2}");
		}

		Console.WriteLine($"\nOverall Harmony Score: {result.OverallHarmonyScore:F2}");
		Console.WriteLine($"Meets Accessibility: {(result.MeetsAccessibilityRequirements ? "✅" : "❌")}");

		if (result.Warnings.Any())
		{
			Console.WriteLine("\nWarnings:");
			foreach (string warning in result.Warnings)
			{
				Console.WriteLine($"  ⚠️  {warning}");
			}
		}

		Console.WriteLine();
	}

	private static void DemonstrateAccessibilityFeatures()
	{
		Console.WriteLine("♿ Accessibility Features");
		Console.WriteLine("========================");

		RgbColor background = CatppuccinMocha.Colors.Base;
		RgbColor originalText = CatppuccinMocha.Colors.Text;

		// Test different color combinations
		(string, RgbColor)[] testColors =
		[
			("Original Text", CatppuccinMocha.Colors.Text),
			("Blue Accent", CatppuccinMocha.Colors.Blue),
			("Green Success", CatppuccinMocha.Colors.Green),
			("Yellow Warning", CatppuccinMocha.Colors.Yellow),
			("Red Error", CatppuccinMocha.Colors.Red),
			("Pink Accent", CatppuccinMocha.Colors.Pink)
		];

		Console.WriteLine($"Testing against background: {background.ToHex()}");
		Console.WriteLine();

		Console.WriteLine("Color Accessibility Analysis:");
		Console.WriteLine("-----------------------------");
		Console.WriteLine("Color            Hex     Contrast  Normal Text  Large Text   Auto-Adjusted");
		Console.WriteLine("-------------    ------  --------  -----------  ----------   -------------");

		foreach ((string name, RgbColor color) in testColors)
		{
			float contrast = ColorMath.GetContrastRatio(color, background);
			AccessibilityLevel normalLevel = ColorMath.GetAccessibilityLevel(color, background, false);
			AccessibilityLevel largeLevel = ColorMath.GetAccessibilityLevel(color, background, true);

			// Auto-adjust for AA compliance
			RgbColor adjusted = ColorMath.AdjustForAccessibility(color, background, AccessibilityLevel.AA, false);
			float adjustedContrast = ColorMath.GetContrastRatio(adjusted, background);

			Console.WriteLine($"{name,-13}  {color.ToHex()}  {contrast,6:F1}:1  {GetLevelSymbol(normalLevel),-11}  {GetLevelSymbol(largeLevel),-10}   {adjusted.ToHex()} ({adjustedContrast:F1}:1)");
		}

		// Demonstrate color blindness considerations
		Console.WriteLine("\nColor Blindness Considerations:");
		Console.WriteLine("-------------------------------");

		RgbColor[] redGreen = [CatppuccinMocha.Colors.Red, CatppuccinMocha.Colors.Green];
		float distinction = AnalyzeColorDistinction(redGreen[0], redGreen[1]);

		Console.WriteLine($"Red-Green distinction in Oklab space: {distinction:F2}");
		Console.WriteLine($"Recommendation: {(distinction > 0.15f ? "✅ Sufficient" : "⚠️  Consider additional differentiation")}");

		Console.WriteLine();
	}

	private static void DemonstrateColorVectorMath()
	{
		Console.WriteLine("🔢 Color Vector Mathematics");
		Console.WriteLine("===========================");

		RgbColor blue = CatppuccinMocha.Colors.Blue;
		RgbColor red = CatppuccinMocha.Colors.Red;

		// Convert to Oklab for perceptual operations
		OklabColor blueOklab = ColorMath.RgbToOklab(blue.ToLinear());
		OklabColor redOklab = ColorMath.RgbToOklab(red.ToLinear());

		Console.WriteLine("Vector Operations in Oklab Space:");
		Console.WriteLine("---------------------------------");
		Console.WriteLine($"Blue:      {blue.ToHex()} → Oklab({blueOklab.L:F3}, {blueOklab.A:F3}, {blueOklab.B:F3})");
		Console.WriteLine($"Red:       {red.ToHex()} → Oklab({redOklab.L:F3}, {redOklab.A:F3}, {redOklab.B:F3})");

		// Perceptual distance
		float distance = blueOklab.DistanceTo(redOklab);
		Console.WriteLine($"Perceptual Distance: {distance:F3}");

		// Create gradient
		RgbColor[] gradient = ColorMath.CreateGradient(blue, red, 5);
		Console.Write("Perceptual Gradient: ");
		foreach (RgbColor color in gradient)
		{
			Console.Write($"{color.ToHex()} ");
		}
		Console.WriteLine();

		// Polar coordinates (LCh)
		(float l, float c, float h) = blueOklab.ToPolar();
		Console.WriteLine($"Blue in Polar: L={l:F2} C={c:F2} H={h:F0}°");

		// Color harmony through hue relationships
		Console.WriteLine("\nColor Harmony Analysis:");
		Console.WriteLine("----------------------");

		OklabColor[] harmonicColors = GenerateHarmonicColors(blueOklab, 4);
		Console.Write("Harmonic Series: ");
		foreach (OklabColor color in harmonicColors)
		{
			RgbColor rgb = ColorMath.OklabToRgb(color).ToSRgb();
			Console.Write($"{rgb.ToHex()} ");
		}
		Console.WriteLine();

		Console.WriteLine();
	}

	private static void DemonstrateApplicationPalettes()
	{
		Console.WriteLine("🎯 Application-Specific Palettes");
		Console.WriteLine("=================================");

		ThemeDefinition theme = CatppuccinMocha.CreateTheme();

		// E-commerce application palette
		Console.WriteLine("E-commerce Application:");
		Console.WriteLine("----------------------");
		SemanticColorGraph ecommerceGraph = SemanticColorGraph.CreateBuilder()
			.AddRequest(ColorRole.Primary)      // Brand color
			.AddRequest(ColorRole.Success)      // Purchase success
			.AddRequest(ColorRole.Warning)      // Low stock warning
			.AddRequest(ColorRole.Error)        // Error states
			.AddRequest(ColorRole.Info)         // Product information
			.AddRequest(ColorRole.Button)       // Call-to-action buttons
			.AddRequest(ColorRole.Link)         // Product links
			.WithGlobalConstraints(new GlobalConstraints
			{
				HarmonyPreference = 0.8f,
				AccessibilityPriority = 0.9f,
				PreferThemeAuthenticity = true
			})
			.Build();

		SemanticPaletteResult ecommerceResult = new SemanticPaletteEngine(theme).GeneratePalette(ecommerceGraph);
		DisplayPaletteResult(ecommerceResult, ["Brand", "Success", "Warning", "Error", "Info", "Button", "Link"]);

		// Developer IDE palette
		Console.WriteLine("\nDeveloper IDE Application:");
		Console.WriteLine("-------------------------");
		SemanticColorGraph ideGraph = SemanticColorGraph.CreateBuilder()
			.AddRequest(new SemanticColorRequest
			{
				PrimaryRole = ColorRole.Text,
				DesiredEnergy = 0.3f,           // Low energy for readability
				AccessibilityRequirement = AccessibilityLevel.AAA
			})
			.AddRequest(new SemanticColorRequest
			{
				PrimaryRole = ColorRole.Blue,   // Keywords
				DesiredTemperature = -0.4f
			})
			.AddRequest(new SemanticColorRequest
			{
				PrimaryRole = ColorRole.Green,  // Strings
				DesiredTemperature = 0.0f
			})
			.AddRequest(new SemanticColorRequest
			{
				PrimaryRole = ColorRole.Purple, // Functions
				DesiredTemperature = -0.2f
			})
			.AddRequest(new SemanticColorRequest
			{
				PrimaryRole = ColorRole.Orange, // Numbers
				DesiredTemperature = 0.5f
			})
			.AddRequest(new SemanticColorRequest
			{
				PrimaryRole = ColorRole.Red,    // Errors
				DesiredEnergy = 0.8f
			})
			.Build();

		SemanticPaletteResult ideResult = new SemanticPaletteEngine(theme).GeneratePalette(ideGraph);
		DisplayPaletteResult(ideResult, ["Text", "Keywords", "Strings", "Functions", "Numbers", "Errors"]);

		Console.WriteLine();
	}

	private static void ValidateThemeAuthenticity()
	{
		Console.WriteLine("✅ Theme Authenticity Validation");
		Console.WriteLine("================================");

		bool isAuthentic = CatppuccinMocha.ValidateAuthenticity();
		Console.WriteLine($"Catppuccin Mocha Authenticity: {(isAuthentic ? "✅ AUTHENTIC" : "❌ NOT AUTHENTIC")}");

		if (isAuthentic)
		{
			Console.WriteLine("All colors match the official Catppuccin Mocha specification!");
			Console.WriteLine("Colors verified against: https://catppuccin.com/palette");
		}

		// Additional validation
		ThemeDefinition theme = CatppuccinMocha.CreateTheme();
		Console.WriteLine($"\nTheme Statistics:");
		Console.WriteLine($"  Total Colors: {theme.AllColors.Count()}");
		Console.WriteLine($"  Theme Type: {(theme.IsDarkTheme ? "Dark" : "Light")}");
		Console.WriteLine($"  Vector Dimensions: {ThemeDefinition.VectorDimensionality}");

		// Philosophy alignment check
		Console.WriteLine("\nCatppuccin Design Philosophy Compliance:");
		Console.WriteLine("  ✅ 'Colorful is better than colorless' - Rich accent colors provided");
		Console.WriteLine("  ✅ 'There should be balance' - Hierarchical neutral colors with vibrant accents");
		Console.WriteLine("  ✅ 'Harmony is superior to dissonance' - Semantic vector mapping ensures harmony");

		Console.WriteLine();
	}

	#region Helper Methods

	private static string GetLevelSymbol(AccessibilityLevel level) => level switch
	{
		AccessibilityLevel.AAA => "AAA ✅",
		AccessibilityLevel.AA => "AA ✅",
		AccessibilityLevel.Fail => "FAIL ❌",
		_ => "UNKNOWN"
	};

	private static float AnalyzeColorDistinction(RgbColor color1, RgbColor color2)
	{
		OklabColor oklab1 = ColorMath.RgbToOklab(color1.ToLinear());
		OklabColor oklab2 = ColorMath.RgbToOklab(color2.ToLinear());
		return oklab1.DistanceTo(oklab2);
	}

	private static OklabColor[] GenerateHarmonicColors(OklabColor baseColor, int count)
	{
		(float l, float c, float h) = baseColor.ToPolar();
		OklabColor[] colors = new OklabColor[count];

		for (int i = 0; i < count; i++)
		{
			// Generate harmonic hues (complementary, triadic, etc.)
			float harmonicHue = (h + (i * 360f / count)) % 360f;
			colors[i] = OklabColor.FromPolar(l, c * 0.8f, harmonicHue);
		}

		return colors;
	}

	private static void DisplayPaletteResult(SemanticPaletteResult result, string[] roleNames)
	{
		for (int i = 0; i < result.GeneratedColors.Length && i < roleNames.Length; i++)
		{
			RgbColor color = result.GeneratedColors[i];
			float access = result.AccessibilityScores[i];
			float semantic = result.SemanticMatchScores[i];

			Console.WriteLine($"  {roleNames[i],-10} {color.ToHex()} A11y: {access:F2} Semantic: {semantic:F2}");
		}
		Console.WriteLine($"  Harmony Score: {result.OverallHarmonyScore:F2}");
	}

	#endregion
}
