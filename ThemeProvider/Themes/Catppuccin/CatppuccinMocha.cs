// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Catppuccin;
using ktsu.ThemeProvider.Core;
using ktsu.ThemeProvider.Themes;

/// <summary>
/// Provides the authentic Catppuccin Mocha theme specification using the semantic color system.
/// Based on the official Catppuccin color palette: https://catppuccin.com/palette
///
/// Catppuccin is a community-driven pastel theme that aims to be the middle ground
/// between low and high contrast themes. Mocha is the darkest variant offering
/// a cozy feeling with color-rich accents.
/// </summary>
public static class CatppuccinMocha
{
	/// <summary>
	/// Creates the authentic Catppuccin Mocha theme definition using semantic color specifications.
	/// Maps the 26 Catppuccin colors to appropriate semantic meanings, visual roles, and importance levels.
	/// </summary>
	public static ThemeDefinition CreateTheme()
	{
		ThemeBuilder builder = ThemeDefinition.CreateBuilder("Catppuccin Mocha")
			.WithDescription("A soothing pastel theme for the high-spirited! The darkest Catppuccin flavor offering a cozy feeling with color-rich accents.")
			.WithAuthor("Catppuccin Organization")
			.SetDarkTheme(true)
			.WithMetadata("source", "https://catppuccin.com/palette")
			.WithMetadata("flavor", "Mocha")
			.WithMetadata("version", "1.0.0")
			.WithMetadata("license", "MIT");

		// Define semantic hue mappings extracted from authentic Catppuccin palette
		Dictionary<SemanticMeaning, float> semanticHues = CatppuccinMochaSemanticMapping.SemanticHues;
		foreach ((SemanticMeaning meaning, float hue) in semanticHues)
		{
			builder.WithSemanticHue(meaning, hue);
		}

		// Background hierarchy (lowest elevation in dark theme)
		SemanticColorSpec baseSpec = new(SemanticMeaning.Primary, VisualRole.Background, ImportanceLevel.Low, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor baseColor = CatppuccinMochaSemanticMapping.GetBackgroundColor(baseSpec);
		builder.WithColor(baseSpec, baseColor.HexValue,
			weight: baseColor.Properties.Weight, temperature: baseColor.Properties.Temperature, energy: baseColor.Properties.Energy,
			formality: baseColor.Properties.Formality, accessibilityPriority: baseColor.Properties.AccessibilityPriority);

		SemanticColorSpec mantleSpec = new(SemanticMeaning.Primary, VisualRole.Background, ImportanceLevel.Medium, isPrimary: false);
		CatppuccinMochaPalette.PaletteColor mantleColor = CatppuccinMochaPalette.Mantle;
		builder.WithColor(mantleSpec, mantleColor.HexValue,
			weight: mantleColor.Properties.Weight, temperature: mantleColor.Properties.Temperature, energy: mantleColor.Properties.Energy,
			formality: mantleColor.Properties.Formality, accessibilityPriority: mantleColor.Properties.AccessibilityPriority);

		SemanticColorSpec crustSpec = new(SemanticMeaning.Primary, VisualRole.Background, ImportanceLevel.Low, isPrimary: false);
		CatppuccinMochaPalette.PaletteColor crustColor = CatppuccinMochaPalette.Crust;
		builder.WithColor(crustSpec, crustColor.HexValue,
			weight: crustColor.Properties.Weight, temperature: crustColor.Properties.Temperature, energy: crustColor.Properties.Energy,
			formality: crustColor.Properties.Formality, accessibilityPriority: crustColor.Properties.AccessibilityPriority);

		// Surface hierarchy (medium elevation)
		SemanticColorSpec surface0Spec = new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.Low, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor surface0Color = CatppuccinMochaSemanticMapping.GetSurfaceColor(surface0Spec);
		builder.WithColor(surface0Spec, surface0Color.HexValue,
			weight: surface0Color.Properties.Weight, temperature: surface0Color.Properties.Temperature, energy: surface0Color.Properties.Energy,
			formality: surface0Color.Properties.Formality, accessibilityPriority: surface0Color.Properties.AccessibilityPriority);

		SemanticColorSpec surface1Spec = new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.Medium, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor surface1Color = CatppuccinMochaSemanticMapping.GetSurfaceColor(surface1Spec);
		builder.WithColor(surface1Spec, surface1Color.HexValue,
			weight: surface1Color.Properties.Weight, temperature: surface1Color.Properties.Temperature, energy: surface1Color.Properties.Energy,
			formality: surface1Color.Properties.Formality, accessibilityPriority: surface1Color.Properties.AccessibilityPriority);

		SemanticColorSpec surface2Spec = new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.High, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor surface2Color = CatppuccinMochaSemanticMapping.GetSurfaceColor(surface2Spec);
		builder.WithColor(surface2Spec, surface2Color.HexValue,
			weight: surface2Color.Properties.Weight, temperature: surface2Color.Properties.Temperature, energy: surface2Color.Properties.Energy,
			formality: surface2Color.Properties.Formality, accessibilityPriority: surface2Color.Properties.AccessibilityPriority);

		// Surface overlays (semi-transparent conceptually)
		SemanticColorSpec overlay0Spec = new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.Low, isPrimary: false);
		CatppuccinMochaPalette.PaletteColor overlay0Color = CatppuccinMochaSemanticMapping.GetSurfaceColor(overlay0Spec);
		builder.WithColor(overlay0Spec, overlay0Color.HexValue,
			weight: overlay0Color.Properties.Weight, temperature: overlay0Color.Properties.Temperature, energy: overlay0Color.Properties.Energy,
			formality: overlay0Color.Properties.Formality, accessibilityPriority: overlay0Color.Properties.AccessibilityPriority);

		SemanticColorSpec overlay1Spec = new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.Medium, isPrimary: false);
		CatppuccinMochaPalette.PaletteColor overlay1Color = CatppuccinMochaSemanticMapping.GetSurfaceColor(overlay1Spec);
		builder.WithColor(overlay1Spec, overlay1Color.HexValue,
			weight: overlay1Color.Properties.Weight, temperature: overlay1Color.Properties.Temperature, energy: overlay1Color.Properties.Energy,
			formality: overlay1Color.Properties.Formality, accessibilityPriority: overlay1Color.Properties.AccessibilityPriority);

		SemanticColorSpec overlay2Spec = new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.High, isPrimary: false);
		CatppuccinMochaPalette.PaletteColor overlay2Color = CatppuccinMochaSemanticMapping.GetSurfaceColor(overlay2Spec);
		builder.WithColor(overlay2Spec, overlay2Color.HexValue,
			weight: overlay2Color.Properties.Weight, temperature: overlay2Color.Properties.Temperature, energy: overlay2Color.Properties.Energy,
			formality: overlay2Color.Properties.Formality, accessibilityPriority: overlay2Color.Properties.AccessibilityPriority);

		// Text hierarchy (highest elevation in dark theme)
		SemanticColorSpec textSpec = new(SemanticMeaning.Primary, VisualRole.Text, ImportanceLevel.Critical, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor textColor = CatppuccinMochaSemanticMapping.GetTextColor(textSpec);
		builder.WithColor(textSpec, textColor.HexValue,
			weight: textColor.Properties.Weight, temperature: textColor.Properties.Temperature, energy: textColor.Properties.Energy,
			formality: textColor.Properties.Formality, accessibilityPriority: textColor.Properties.AccessibilityPriority);

		SemanticColorSpec subtext1Spec = new(SemanticMeaning.Primary, VisualRole.Text, ImportanceLevel.High, isPrimary: false);
		CatppuccinMochaPalette.PaletteColor subtext1Color = CatppuccinMochaSemanticMapping.GetTextColor(subtext1Spec);
		builder.WithColor(subtext1Spec, subtext1Color.HexValue,
			weight: subtext1Color.Properties.Weight, temperature: subtext1Color.Properties.Temperature, energy: subtext1Color.Properties.Energy,
			formality: subtext1Color.Properties.Formality, accessibilityPriority: subtext1Color.Properties.AccessibilityPriority);

		SemanticColorSpec subtext0Spec = new(SemanticMeaning.Primary, VisualRole.Text, ImportanceLevel.Medium, isPrimary: false);
		CatppuccinMochaPalette.PaletteColor subtext0Color = CatppuccinMochaSemanticMapping.GetTextColor(subtext0Spec);
		builder.WithColor(subtext0Spec, subtext0Color.HexValue,
			weight: subtext0Color.Properties.Weight, temperature: subtext0Color.Properties.Temperature, energy: subtext0Color.Properties.Energy,
			formality: subtext0Color.Properties.Formality, accessibilityPriority: subtext0Color.Properties.AccessibilityPriority);

		// Semantic text colors
		SemanticColorSpec successTextSpec = new(SemanticMeaning.Success, VisualRole.Text, ImportanceLevel.High, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor successTextColor = CatppuccinMochaSemanticMapping.GetTextColor(successTextSpec);
		builder.WithColor(successTextSpec, successTextColor.HexValue,
			weight: successTextColor.Properties.Weight, temperature: successTextColor.Properties.Temperature, energy: successTextColor.Properties.Energy,
			formality: successTextColor.Properties.Formality, accessibilityPriority: successTextColor.Properties.AccessibilityPriority);

		SemanticColorSpec warningTextSpec = new(SemanticMeaning.Warning, VisualRole.Text, ImportanceLevel.High, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor warningTextColor = CatppuccinMochaSemanticMapping.GetTextColor(warningTextSpec);
		builder.WithColor(warningTextSpec, warningTextColor.HexValue,
			weight: warningTextColor.Properties.Weight, temperature: warningTextColor.Properties.Temperature, energy: warningTextColor.Properties.Energy,
			formality: warningTextColor.Properties.Formality, accessibilityPriority: warningTextColor.Properties.AccessibilityPriority);

		SemanticColorSpec errorTextSpec = new(SemanticMeaning.Error, VisualRole.Text, ImportanceLevel.Critical, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor errorTextColor = CatppuccinMochaSemanticMapping.GetTextColor(errorTextSpec);
		builder.WithColor(errorTextSpec, errorTextColor.HexValue,
			weight: errorTextColor.Properties.Weight, temperature: errorTextColor.Properties.Temperature, energy: errorTextColor.Properties.Energy,
			formality: errorTextColor.Properties.Formality, accessibilityPriority: errorTextColor.Properties.AccessibilityPriority);

		SemanticColorSpec ctaTextSpec = new(SemanticMeaning.CallToAction, VisualRole.Text, ImportanceLevel.High, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor ctaTextColor = CatppuccinMochaSemanticMapping.GetTextColor(ctaTextSpec);
		builder.WithColor(ctaTextSpec, ctaTextColor.HexValue,
			weight: ctaTextColor.Properties.Weight, temperature: ctaTextColor.Properties.Temperature, energy: ctaTextColor.Properties.Energy,
			formality: ctaTextColor.Properties.Formality, accessibilityPriority: ctaTextColor.Properties.AccessibilityPriority);

		SemanticColorSpec infoTextSpec = new(SemanticMeaning.Information, VisualRole.Text, ImportanceLevel.Medium, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor infoTextColor = CatppuccinMochaSemanticMapping.GetTextColor(infoTextSpec);
		builder.WithColor(infoTextSpec, infoTextColor.HexValue,
			weight: infoTextColor.Properties.Weight, temperature: infoTextColor.Properties.Temperature, energy: infoTextColor.Properties.Energy,
			formality: infoTextColor.Properties.Formality, accessibilityPriority: infoTextColor.Properties.AccessibilityPriority);

		// Widget/accent colors (interactive elements)
		SemanticColorSpec ctaWidgetSpec = new(SemanticMeaning.CallToAction, VisualRole.Widget, ImportanceLevel.Critical, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor ctaWidgetColor = CatppuccinMochaSemanticMapping.GetWidgetColor(ctaWidgetSpec);
		builder.WithColor(ctaWidgetSpec, ctaWidgetColor.HexValue,
			weight: ctaWidgetColor.Properties.Weight, temperature: ctaWidgetColor.Properties.Temperature, energy: ctaWidgetColor.Properties.Energy,
			formality: ctaWidgetColor.Properties.Formality, accessibilityPriority: ctaWidgetColor.Properties.AccessibilityPriority);

		SemanticColorSpec normalWidgetSpec = new(SemanticMeaning.Primary, VisualRole.Widget, ImportanceLevel.Medium, isPrimary: false);
		CatppuccinMochaPalette.PaletteColor normalWidgetColor = CatppuccinMochaSemanticMapping.GetWidgetColor(normalWidgetSpec);
		builder.WithColor(normalWidgetSpec, normalWidgetColor.HexValue,
			weight: normalWidgetColor.Properties.Weight, temperature: normalWidgetColor.Properties.Temperature, energy: normalWidgetColor.Properties.Energy,
			formality: normalWidgetColor.Properties.Formality, accessibilityPriority: normalWidgetColor.Properties.AccessibilityPriority);

		SemanticColorSpec successWidgetSpec = new(SemanticMeaning.Success, VisualRole.Widget, ImportanceLevel.High, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor successWidgetColor = CatppuccinMochaSemanticMapping.GetWidgetColor(successWidgetSpec);
		builder.WithColor(successWidgetSpec, successWidgetColor.HexValue,
			weight: successWidgetColor.Properties.Weight, temperature: successWidgetColor.Properties.Temperature, energy: successWidgetColor.Properties.Energy,
			formality: successWidgetColor.Properties.Formality, accessibilityPriority: successWidgetColor.Properties.AccessibilityPriority);

		SemanticColorSpec warningWidgetSpec = new(SemanticMeaning.Warning, VisualRole.Widget, ImportanceLevel.High, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor warningWidgetColor = CatppuccinMochaSemanticMapping.GetWidgetColor(warningWidgetSpec);
		builder.WithColor(warningWidgetSpec, warningWidgetColor.HexValue,
			weight: warningWidgetColor.Properties.Weight, temperature: warningWidgetColor.Properties.Temperature, energy: warningWidgetColor.Properties.Energy,
			formality: warningWidgetColor.Properties.Formality, accessibilityPriority: warningWidgetColor.Properties.AccessibilityPriority);

		SemanticColorSpec errorWidgetSpec = new(SemanticMeaning.Error, VisualRole.Widget, ImportanceLevel.Critical, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor errorWidgetColor = CatppuccinMochaSemanticMapping.GetWidgetColor(errorWidgetSpec);
		builder.WithColor(errorWidgetSpec, errorWidgetColor.HexValue,
			weight: errorWidgetColor.Properties.Weight, temperature: errorWidgetColor.Properties.Temperature, energy: errorWidgetColor.Properties.Energy,
			formality: errorWidgetColor.Properties.Formality, accessibilityPriority: errorWidgetColor.Properties.AccessibilityPriority);

		SemanticColorSpec infoWidgetSpec = new(SemanticMeaning.Information, VisualRole.Widget, ImportanceLevel.Medium, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor infoWidgetColor = CatppuccinMochaSemanticMapping.GetWidgetColor(infoWidgetSpec);
		builder.WithColor(infoWidgetSpec, infoWidgetColor.HexValue,
			weight: infoWidgetColor.Properties.Weight, temperature: infoWidgetColor.Properties.Temperature, energy: infoWidgetColor.Properties.Energy,
			formality: infoWidgetColor.Properties.Formality, accessibilityPriority: infoWidgetColor.Properties.AccessibilityPriority);

		// Additional accent colors for variety
		SemanticColorSpec emphasisWidgetSpec = new(SemanticMeaning.Secondary, VisualRole.Widget, ImportanceLevel.High, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor emphasisWidgetColor = CatppuccinMochaSemanticMapping.GetWidgetColor(emphasisWidgetSpec);
		builder.WithColor(emphasisWidgetSpec, emphasisWidgetColor.HexValue,
			weight: emphasisWidgetColor.Properties.Weight, temperature: emphasisWidgetColor.Properties.Temperature, energy: emphasisWidgetColor.Properties.Energy,
			formality: emphasisWidgetColor.Properties.Formality, accessibilityPriority: emphasisWidgetColor.Properties.AccessibilityPriority);

		SemanticColorSpec debugWidgetSpec = new(SemanticMeaning.Debug, VisualRole.Widget, ImportanceLevel.Low, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor debugWidgetColor = CatppuccinMochaSemanticMapping.GetWidgetColor(debugWidgetSpec);
		builder.WithColor(debugWidgetSpec, debugWidgetColor.HexValue,
			weight: debugWidgetColor.Properties.Weight, temperature: debugWidgetColor.Properties.Temperature, energy: debugWidgetColor.Properties.Energy,
			formality: debugWidgetColor.Properties.Formality, accessibilityPriority: debugWidgetColor.Properties.AccessibilityPriority);

		// Additional semantic variations
		SemanticColorSpec normalLowWidgetSpec = new(SemanticMeaning.Primary, VisualRole.Widget, ImportanceLevel.Low, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor normalLowWidgetColor = CatppuccinMochaSemanticMapping.GetWidgetColor(normalLowWidgetSpec);
		builder.WithColor(normalLowWidgetSpec, normalLowWidgetColor.HexValue,
			weight: normalLowWidgetColor.Properties.Weight, temperature: normalLowWidgetColor.Properties.Temperature, energy: normalLowWidgetColor.Properties.Energy,
			formality: normalLowWidgetColor.Properties.Formality, accessibilityPriority: normalLowWidgetColor.Properties.AccessibilityPriority);

		SemanticColorSpec emphasisTextSpec = new(SemanticMeaning.Secondary, VisualRole.Text, ImportanceLevel.Medium, isPrimary: true);
		CatppuccinMochaPalette.PaletteColor emphasisTextColor = CatppuccinMochaSemanticMapping.GetTextColor(emphasisTextSpec);
		builder.WithColor(emphasisTextSpec, emphasisTextColor.HexValue,
			weight: emphasisTextColor.Properties.Weight, temperature: emphasisTextColor.Properties.Temperature, energy: emphasisTextColor.Properties.Energy,
			formality: emphasisTextColor.Properties.Formality, accessibilityPriority: emphasisTextColor.Properties.AccessibilityPriority);

		return builder.Build();
	}
}
