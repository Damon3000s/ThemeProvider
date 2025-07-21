// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProviderDemo;
using System.Numerics;
using Hexa.NET.ImGui;
using ktsu.ImGuiApp;
using ktsu.ThemeProvider;

internal static class Program
{
	private static ThemeDefinition theme = null!;
	private static SemanticPaletteEngine paletteEngine = null!;

	// UI State
	private static int selectedColorRole;
	private static Vector3 selectedColorVec = Vector3.Zero;
	private static Vector3 backgroundColorVec = new(0.1f, 0.1f, 0.1f);
	private static bool isLargeText;

	// Form state
	private static float sliderValue = 0.5f;
	private static bool checkboxValue;
	private static string textValue = "Text Input";

	private static void Main()
	{
		ImGuiApp.Start(new()
		{
			Title = "ThemeProvider Demo - Catppuccin Mocha Theme Explorer",
			OnRender = OnRender,
			OnStart = OnStart,
			SaveIniSettings = false,
		});
	}

	private static void OnStart()
	{
		theme = CatppuccinMocha.CreateTheme();
		paletteEngine = new SemanticPaletteEngine(theme);

		// Initialize with theme's text color
		if (theme.TryGetColor(ColorRole.Text, out ColorProperties textColor))
		{
			selectedColorVec = new Vector3(textColor.RgbValue.R, textColor.RgbValue.G, textColor.RgbValue.B);
		}
	}

	private static void OnRender(float deltaTime)
	{
		ApplyCatppuccinTheme();

		if (ImGui.BeginTabBar("DemoTabs"))
		{
			if (ImGui.BeginTabItem("Theme Overview"))
			{
				RenderThemeOverview();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("Color Roles"))
			{
				RenderColorRoles();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("Accessibility"))
			{
				RenderAccessibilityDemo();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("Semantic Engine"))
			{
				RenderSemanticEngine();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("UI Preview"))
			{
				RenderUIPreview();
				ImGui.EndTabItem();
			}

			ImGui.EndTabBar();
		}
	}

	private static void ApplyCatppuccinTheme()
	{
		ImGuiStylePtr style = ImGui.GetStyle();

		// Apply Catppuccin Mocha colors to ImGui
		Span<Vector4> colors = style.Colors;

		// Get theme colors
		ColorProperties baseColor = theme.GetColor(ColorRole.Base);
		ColorProperties textColor = theme.GetColor(ColorRole.Text);
		ColorProperties primaryColor = theme.GetColor(ColorRole.Primary);
		ColorProperties surface0Color = theme.GetColor(ColorRole.Surface0);
		ColorProperties surface1Color = theme.GetColor(ColorRole.Surface1);
		ColorProperties overlayColor = theme.GetColor(ColorRole.Overlay0);

		// Convert to ImGui format
		colors[(int)ImGuiCol.Text] = ToImVec4(textColor.RgbValue);
		colors[(int)ImGuiCol.TextDisabled] = ToImVec4(theme.GetColor(ColorRole.Subtext0).RgbValue);
		colors[(int)ImGuiCol.WindowBg] = ToImVec4(baseColor.RgbValue);
		colors[(int)ImGuiCol.ChildBg] = ToImVec4(baseColor.RgbValue);
		colors[(int)ImGuiCol.PopupBg] = ToImVec4(surface0Color.RgbValue);
		colors[(int)ImGuiCol.Border] = ToImVec4(theme.GetColor(ColorRole.Border).RgbValue);
		colors[(int)ImGuiCol.BorderShadow] = ToImVec4(theme.GetColor(ColorRole.Shadow).RgbValue);
		colors[(int)ImGuiCol.FrameBg] = ToImVec4(surface0Color.RgbValue);
		colors[(int)ImGuiCol.FrameBgHovered] = ToImVec4(surface1Color.RgbValue);
		colors[(int)ImGuiCol.FrameBgActive] = ToImVec4(theme.GetColor(ColorRole.Surface2).RgbValue);
		colors[(int)ImGuiCol.TitleBg] = ToImVec4(theme.GetColor(ColorRole.Mantle).RgbValue);
		colors[(int)ImGuiCol.TitleBgActive] = ToImVec4(surface0Color.RgbValue);
		colors[(int)ImGuiCol.TitleBgCollapsed] = ToImVec4(theme.GetColor(ColorRole.Crust).RgbValue);
		colors[(int)ImGuiCol.MenuBarBg] = ToImVec4(surface0Color.RgbValue);
		colors[(int)ImGuiCol.ScrollbarBg] = ToImVec4(baseColor.RgbValue);
		colors[(int)ImGuiCol.ScrollbarGrab] = ToImVec4(overlayColor.RgbValue);
		colors[(int)ImGuiCol.ScrollbarGrabHovered] = ToImVec4(theme.GetColor(ColorRole.Overlay1).RgbValue);
		colors[(int)ImGuiCol.ScrollbarGrabActive] = ToImVec4(theme.GetColor(ColorRole.Overlay2).RgbValue);
		colors[(int)ImGuiCol.CheckMark] = ToImVec4(primaryColor.RgbValue);
		colors[(int)ImGuiCol.SliderGrab] = ToImVec4(primaryColor.RgbValue);
		colors[(int)ImGuiCol.SliderGrabActive] = ToImVec4(theme.GetColor(ColorRole.Blue).RgbValue);
		colors[(int)ImGuiCol.Button] = ToImVec4(theme.GetColor(ColorRole.Button).RgbValue);
		colors[(int)ImGuiCol.ButtonHovered] = ToImVec4(theme.GetColor(ColorRole.ButtonHover).RgbValue);
		colors[(int)ImGuiCol.ButtonActive] = ToImVec4(theme.GetColor(ColorRole.ButtonActive).RgbValue);
		colors[(int)ImGuiCol.Header] = ToImVec4(surface0Color.RgbValue);
		colors[(int)ImGuiCol.HeaderHovered] = ToImVec4(surface1Color.RgbValue);
		colors[(int)ImGuiCol.HeaderActive] = ToImVec4(theme.GetColor(ColorRole.Surface2).RgbValue);
		colors[(int)ImGuiCol.Separator] = ToImVec4(theme.GetColor(ColorRole.Divider).RgbValue);
		colors[(int)ImGuiCol.SeparatorHovered] = ToImVec4(overlayColor.RgbValue);
		colors[(int)ImGuiCol.SeparatorActive] = ToImVec4(theme.GetColor(ColorRole.Overlay1).RgbValue);
		colors[(int)ImGuiCol.ResizeGrip] = ToImVec4(overlayColor.RgbValue);
		colors[(int)ImGuiCol.ResizeGripHovered] = ToImVec4(theme.GetColor(ColorRole.Overlay1).RgbValue);
		colors[(int)ImGuiCol.ResizeGripActive] = ToImVec4(theme.GetColor(ColorRole.Overlay2).RgbValue);
		colors[(int)ImGuiCol.Tab] = ToImVec4(surface0Color.RgbValue);
		colors[(int)ImGuiCol.TabHovered] = ToImVec4(surface1Color.RgbValue);
		colors[(int)ImGuiCol.PlotLines] = ToImVec4(theme.GetColor(ColorRole.Blue).RgbValue);
		colors[(int)ImGuiCol.PlotLinesHovered] = ToImVec4(theme.GetColor(ColorRole.Cyan).RgbValue);
		colors[(int)ImGuiCol.PlotHistogram] = ToImVec4(theme.GetColor(ColorRole.Green).RgbValue);
		colors[(int)ImGuiCol.PlotHistogramHovered] = ToImVec4(theme.GetColor(ColorRole.Yellow).RgbValue);
		colors[(int)ImGuiCol.TextSelectedBg] = ToImVec4(theme.GetColor(ColorRole.Selection).RgbValue, 0.4f);
	}

	private static void RenderThemeOverview()
	{
		ImGui.TextUnformatted($"Theme: {theme.Name}");
		ImGui.TextUnformatted($"Author: {theme.Author}");
		ImGui.TextUnformatted($"Description: {theme.Description}");
		ImGui.TextUnformatted($"Type: {(theme.IsDarkTheme ? "Dark" : "Light")} Theme");
		ImGui.Separator();

		ImGui.TextUnformatted("Color Palette:");

		// Organize colors by category
		Dictionary<string, List<(ColorRole role, ColorProperties color)>> categories = new()
		{
			["Base Colors"] = [],
			["Surface Colors"] = [],
			["Text Colors"] = [],
			["Accent Colors"] = [],
			["Semantic Colors"] = [],
			["Interactive Colors"] = [],
			["Specific Hues"] = []
		};

		foreach ((ColorRole role, ColorProperties color) in theme.AllColors)
		{
			string category = role switch
			{
				ColorRole.Base or ColorRole.Mantle or ColorRole.Crust => "Base Colors",
				ColorRole.Surface0 or ColorRole.Surface1 or ColorRole.Surface2 or
				ColorRole.Overlay0 or ColorRole.Overlay1 or ColorRole.Overlay2 => "Surface Colors",
				ColorRole.Text or ColorRole.Subtext0 or ColorRole.Subtext1 => "Text Colors",
				ColorRole.Primary or ColorRole.Secondary => "Accent Colors",
				ColorRole.Success or ColorRole.Warning or ColorRole.Error or ColorRole.Info => "Semantic Colors",
				ColorRole.Button or ColorRole.ButtonHover or ColorRole.ButtonActive or ColorRole.ButtonDisabled or
				ColorRole.Link or ColorRole.LinkHover or ColorRole.LinkActive or ColorRole.LinkVisited => "Interactive Colors",
				ColorRole.Red or ColorRole.Green or ColorRole.Blue or ColorRole.Yellow or ColorRole.Orange or
				ColorRole.Purple or ColorRole.Pink or ColorRole.Cyan or ColorRole.Teal or ColorRole.Lime or
				ColorRole.Amber or ColorRole.Indigo or ColorRole.Violet or ColorRole.Rose or ColorRole.Sky or
				ColorRole.Emerald => "Specific Hues",
				_ => "Other"
			};

			if (categories.TryGetValue(category, out List<(ColorRole role, ColorProperties color)>? list))
			{
				list.Add((role, color));
			}
		}

		float colorSize = 40f;
		foreach ((string categoryName, List<(ColorRole role, ColorProperties color)> colors) in categories.Where(kvp => kvp.Value.Count > 0))
		{
			if (ImGui.CollapsingHeader(categoryName))
			{
				int columns = Math.Max(1, (int)(ImGui.GetContentRegionAvail().X / (colorSize + 10f)));
				if (ImGui.BeginTable($"{categoryName}Table", columns, ImGuiTableFlags.None))
				{
					int col = 0;
					foreach ((ColorRole role, ColorProperties color) in colors)
					{
						if (col % columns == 0)
						{
							ImGui.TableNextRow();
						}

						ImGui.TableSetColumnIndex(col % columns);

						// Color swatch
						ImDrawListPtr drawList = ImGui.GetWindowDrawList();
						Vector2 pos = ImGui.GetCursorScreenPos();
						Vector4 rgbColor = ToImVec4(color.RgbValue);
						drawList.AddRectFilled(pos, new Vector2(pos.X + colorSize, pos.Y + colorSize),
											   ImGui.ColorConvertFloat4ToU32(rgbColor));
						drawList.AddRect(pos, new Vector2(pos.X + colorSize, pos.Y + colorSize),
										ImGui.ColorConvertFloat4ToU32(new Vector4(1, 1, 1, 0.3f)));

						ImGui.Dummy(new Vector2(colorSize, colorSize));

						// Tooltip with details
						if (ImGui.IsItemHovered())
						{
							ImGui.BeginTooltip();
							ImGui.TextUnformatted($"{role}");
							ImGui.TextUnformatted($"Hex: {color.RgbValue.ToHex()}");
							ImGui.TextUnformatted($"RGB: {color.RgbValue.R:F3}, {color.RgbValue.G:F3}, {color.RgbValue.B:F3}");
							ImGui.TextUnformatted($"Temperature: {color.Temperature:F2}");
							ImGui.TextUnformatted($"Energy: {color.Energy:F2}");
							ImGui.TextUnformatted($"Weight: {color.Weight:F2}");
							ImGui.EndTooltip();
						}

						ImGui.TextUnformatted(role.ToString());
						col++;
					}

					ImGui.EndTable();
				}
			}
		}
	}

	private static void RenderColorRoles()
	{
		ImGui.TextUnformatted("Explore individual color roles and their properties:");
		ImGui.Separator();

		// Color role selector
		string[] roleNames = Enum.GetNames<ColorRole>();
		if (ImGui.Combo("Color Role", ref selectedColorRole, roleNames, roleNames.Length))
		{
			ColorRole role = (ColorRole)selectedColorRole;
			if (theme.TryGetColor(role, out ColorProperties color))
			{
				selectedColorVec = new Vector3(color.RgbValue.R, color.RgbValue.G, color.RgbValue.B);
			}
		}

		ColorRole selectedRole = (ColorRole)selectedColorRole;
		if (theme.TryGetColor(selectedRole, out ColorProperties selectedColor))
		{
			ImGui.Separator();

			// Large color preview
			float previewSize = 150f;
			ImDrawListPtr drawList = ImGui.GetWindowDrawList();
			Vector2 pos = ImGui.GetCursorScreenPos();
			Vector4 rgbColor = ToImVec4(selectedColor.RgbValue);
			drawList.AddRectFilled(pos, new Vector2(pos.X + previewSize, pos.Y + previewSize),
								   ImGui.ColorConvertFloat4ToU32(rgbColor));
			drawList.AddRect(pos, new Vector2(pos.X + previewSize, pos.Y + previewSize),
							ImGui.ColorConvertFloat4ToU32(new Vector4(1, 1, 1, 0.5f)));
			ImGui.Dummy(new Vector2(previewSize, previewSize));

			ImGui.SameLine();
			ImGui.BeginGroup();

			// Color information
			ImGui.TextUnformatted($"Role: {selectedRole}");
			ImGui.TextUnformatted($"Hex: {selectedColor.RgbValue.ToHex()}");
			ImGui.TextUnformatted($"RGB: ({selectedColor.RgbValue.R:F3}, {selectedColor.RgbValue.G:F3}, {selectedColor.RgbValue.B:F3})");

			// Oklab information
			OklabColor oklab = selectedColor.OklabValue;
			ImGui.TextUnformatted($"Oklab: L={oklab.L:F3}, a={oklab.A:F3}, b={oklab.B:F3}");

			(float l, float c, float h) = oklab.ToPolar();
			ImGui.TextUnformatted($"LCH: L={l:F3}, C={c:F3}, H={h:F1}°");

			ImGui.Separator();

			// Semantic properties
			ImGui.TextUnformatted("Semantic Properties:");
			ImGui.TextUnformatted($"Weight: {selectedColor.Weight:F2}");
			ImGui.TextUnformatted($"Temperature: {selectedColor.Temperature:F2} ({(selectedColor.Temperature > 0 ? "Warm" : "Cool")})");
			ImGui.TextUnformatted($"Energy: {selectedColor.Energy:F2}");
			ImGui.TextUnformatted($"Formality: {selectedColor.Formality:F2}");
			ImGui.TextUnformatted($"Accessibility Priority: {selectedColor.AccessibilityPriority:F2}");

			ImGui.EndGroup();
		}
	}

	private static void RenderAccessibilityDemo()
	{
		ImGui.TextUnformatted("Test color combinations for WCAG accessibility compliance:");
		ImGui.Separator();

		// Color pickers
		ImGui.TextUnformatted("Foreground Color:");
		ImGui.ColorEdit3("##Foreground", ref selectedColorVec);

		ImGui.TextUnformatted("Background Color:");
		ImGui.ColorEdit3("##Background", ref backgroundColorVec);

		ImGui.Checkbox("Large Text (18pt+ or 14pt+ bold)", ref isLargeText);

		// Convert to RgbColor
		RgbColor foregroundColor = new(selectedColorVec.X, selectedColorVec.Y, selectedColorVec.Z);
		RgbColor backgroundColor = new(backgroundColorVec.X, backgroundColorVec.Y, backgroundColorVec.Z);

		// Calculate accessibility metrics
		float contrastRatio = ColorMath.GetContrastRatio(foregroundColor, backgroundColor);
		AccessibilityLevel accessibilityLevel = ColorMath.GetAccessibilityLevel(foregroundColor, backgroundColor, isLargeText);

		ImGui.Separator();

		// Results
		ImGui.TextUnformatted($"Contrast Ratio: {contrastRatio:F2}:1");

		// Color-coded accessibility level
		Vector4 levelColor = accessibilityLevel switch
		{
			AccessibilityLevel.AAA => new Vector4(0, 1, 0, 1), // Green
			AccessibilityLevel.AA => new Vector4(1, 1, 0, 1),  // Yellow
			_ => new Vector4(1, 0, 0, 1) // Red
		};

		ImGui.TextColored(levelColor, $"Accessibility Level: {accessibilityLevel}");

		// Requirements
		float aaRequired = isLargeText ? 3.0f : 4.5f;
		float aaaRequired = isLargeText ? 4.5f : 7.0f;

		ImGui.TextUnformatted($"Required for AA: {aaRequired:F1}:1 {(contrastRatio >= aaRequired ? "✓" : "✗")}");
		ImGui.TextUnformatted($"Required for AAA: {aaaRequired:F1}:1 {(contrastRatio >= aaaRequired ? "✓" : "✗")}");

		// Preview text
		ImGui.Separator();
		ImGui.TextUnformatted("Preview:");

		ImDrawListPtr drawList = ImGui.GetWindowDrawList();
		Vector2 pos = ImGui.GetCursorScreenPos();
		Vector2 previewSize = new(400, 100);

		// Background
		drawList.AddRectFilled(pos, new Vector2(pos.X + previewSize.X, pos.Y + previewSize.Y),
							   ImGui.ColorConvertFloat4ToU32(new Vector4(backgroundColorVec, 1)));

		// Text - using ImGui text instead of direct drawing to avoid unsafe context
		ImGui.SetCursorScreenPos(new Vector2(pos.X + 10, pos.Y + 10));
		ImGui.TextColored(new Vector4(selectedColorVec, 1), isLargeText ? "Large Text Sample (18pt+)" : "Normal Text Sample");

		ImGui.Dummy(previewSize);

		// Test with theme colors
		ImGui.Separator();
		ImGui.TextUnformatted("Theme Color Accessibility Test:");

		if (ImGui.BeginTable("AccessibilityTable", 4, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
		{
			ImGui.TableSetupColumn("Foreground");
			ImGui.TableSetupColumn("Background");
			ImGui.TableSetupColumn("Contrast");
			ImGui.TableSetupColumn("Level");
			ImGui.TableHeadersRow();

			// Test common combinations
			(ColorRole fg, ColorRole bg)[] combinations =
			[
				(ColorRole.Text, ColorRole.Base),
				(ColorRole.Subtext0, ColorRole.Base),
				(ColorRole.Primary, ColorRole.Base),
				(ColorRole.Success, ColorRole.Base),
				(ColorRole.Warning, ColorRole.Base),
				(ColorRole.Error, ColorRole.Base),
				(ColorRole.Text, ColorRole.Surface0),
				(ColorRole.Primary, ColorRole.Surface1),
			];

			foreach ((ColorRole fgRole, ColorRole bgRole) in combinations)
			{
				if (theme.TryGetColor(fgRole, out ColorProperties fgColor) && theme.TryGetColor(bgRole, out ColorProperties bgColor))
				{
					float contrast = ColorMath.GetContrastRatio(fgColor.RgbValue, bgColor.RgbValue);
					AccessibilityLevel level = ColorMath.GetAccessibilityLevel(fgColor.RgbValue, bgColor.RgbValue, false);

					ImGui.TableNextRow();
					ImGui.TableSetColumnIndex(0);
					ImGui.TextUnformatted(fgRole.ToString());

					ImGui.TableSetColumnIndex(1);
					ImGui.TextUnformatted(bgRole.ToString());

					ImGui.TableSetColumnIndex(2);
					ImGui.TextUnformatted($"{contrast:F2}:1");

					ImGui.TableSetColumnIndex(3);
					Vector4 color = level switch
					{
						AccessibilityLevel.AAA => new Vector4(0, 1, 0, 1),
						AccessibilityLevel.AA => new Vector4(1, 1, 0, 1),
						_ => new Vector4(1, 0, 0, 1)
					};
					ImGui.TextColored(color, level.ToString());
				}
			}

			ImGui.EndTable();
		}
	}

	private static void RenderSemanticEngine()
	{
		ImGui.TextUnformatted("Generate semantic color palettes using the theme engine:");
		ImGui.Separator();

		// Create a simple semantic request
		ColorProperties baseColor = theme.GetColor(ColorRole.Base);
		SemanticColorGraph graph = SemanticColorGraph.CreateBuilder()
			.AddRequest(ColorRole.Primary, AccessibilityLevel.AA, baseColor.RgbValue)
			.AddRequest(ColorRole.Secondary, AccessibilityLevel.AA, baseColor.RgbValue)
			.AddRequest(ColorRole.Success, AccessibilityLevel.AA, baseColor.RgbValue)
			.AddRequest(ColorRole.Warning, AccessibilityLevel.AA, baseColor.RgbValue)
			.AddRequest(ColorRole.Error, AccessibilityLevel.AA, baseColor.RgbValue)
			.AddHarmony(0, 1, 0.8f) // Primary and Secondary should be harmonious
			.Build();

		SemanticPaletteResult result = paletteEngine.GeneratePalette(graph);

		ImGui.TextUnformatted($"Generated {result.GeneratedColors.Length} colors");
		ImGui.TextUnformatted($"Overall Harmony Score: {result.OverallHarmonyScore:F2}");
		ImGui.TextUnformatted($"Meets Accessibility Requirements: {(result.MeetsAccessibilityRequirements ? "Yes" : "No")}");

		if (result.Warnings.Length > 0)
		{
			ImGui.Separator();
			ImGui.TextUnformatted("Warnings:");
			foreach (string warning in result.Warnings)
			{
				ImGui.TextColored(new Vector4(1, 1, 0, 1), $"• {warning}");
			}
		}

		ImGui.Separator();
		ImGui.TextUnformatted("Generated Palette:");

		string[] roleNames = ["Primary", "Secondary", "Success", "Warning", "Error"];
		float swatchSize = 60f;

		for (int i = 0; i < result.GeneratedColors.Length && i < roleNames.Length; i++)
		{
			RgbColor color = result.GeneratedColors[i];
			float accessibilityScore = result.AccessibilityScores[i];
			float semanticScore = result.SemanticMatchScores[i];

			// Color swatch
			ImDrawListPtr drawList = ImGui.GetWindowDrawList();
			Vector2 pos = ImGui.GetCursorScreenPos();
			Vector4 rgbVec = ToImVec4(color);
			drawList.AddRectFilled(pos, new Vector2(pos.X + swatchSize, pos.Y + swatchSize),
								   ImGui.ColorConvertFloat4ToU32(rgbVec));
			drawList.AddRect(pos, new Vector2(pos.X + swatchSize, pos.Y + swatchSize),
							ImGui.ColorConvertFloat4ToU32(new Vector4(1, 1, 1, 0.3f)));
			ImGui.Dummy(new Vector2(swatchSize, swatchSize));

			ImGui.SameLine();
			ImGui.BeginGroup();
			ImGui.TextUnformatted(roleNames[i]);
			ImGui.TextUnformatted($"Hex: {color.ToHex()}");
			ImGui.TextUnformatted($"Accessibility: {accessibilityScore:F2}");
			ImGui.TextUnformatted($"Semantic Match: {semanticScore:F2}");
			ImGui.EndGroup();

			if (i < result.GeneratedColors.Length - 1)
			{
				ImGui.Separator();
			}
		}
	}

	private static void RenderUIPreview()
	{
		ImGui.TextUnformatted("Live preview of theme applied to various UI elements:");
		ImGui.Separator();

		// Buttons
		ImGui.TextUnformatted("Buttons:");
		if (ImGui.Button("Primary Button"))
		{
			// Action
		}
		ImGui.SameLine();

		ImGui.PushStyleColor(ImGuiCol.Button, ToImVec4(theme.GetColor(ColorRole.Success).RgbValue));
		ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ToImVec4(theme.GetColor(ColorRole.Green).RgbValue));
		if (ImGui.Button("Success Button"))
		{
			// Action
		}
		ImGui.PopStyleColor(2);

		ImGui.SameLine();

		ImGui.PushStyleColor(ImGuiCol.Button, ToImVec4(theme.GetColor(ColorRole.Error).RgbValue));
		ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ToImVec4(theme.GetColor(ColorRole.Red).RgbValue));
		if (ImGui.Button("Error Button"))
		{
			// Action
		}
		ImGui.PopStyleColor(2);

		ImGui.Separator();

		// Form elements
		ImGui.TextUnformatted("Form Elements:");
		ImGui.SliderFloat("Slider", ref sliderValue, 0f, 1f);

		ImGui.Checkbox("Checkbox", ref checkboxValue);

		ImGui.InputText("Input", ref textValue, 100);

		ImGui.Separator();

		// Progress bars with theme colors
		ImGui.TextUnformatted("Progress Indicators:");

		ImGui.PushStyleColor(ImGuiCol.PlotHistogram, ToImVec4(theme.GetColor(ColorRole.Success).RgbValue));
		ImGui.ProgressBar(0.7f, new Vector2(0, 0), "70%");
		ImGui.PopStyleColor();

		ImGui.PushStyleColor(ImGuiCol.PlotHistogram, ToImVec4(theme.GetColor(ColorRole.Warning).RgbValue));
		ImGui.ProgressBar(0.4f, new Vector2(0, 0), "40%");
		ImGui.PopStyleColor();

		ImGui.PushStyleColor(ImGuiCol.PlotHistogram, ToImVec4(theme.GetColor(ColorRole.Error).RgbValue));
		ImGui.ProgressBar(0.2f, new Vector2(0, 0), "20%");
		ImGui.PopStyleColor();

		ImGui.Separator();

		// Color-coded text
		ImGui.TextUnformatted("Themed Text:");
		ImGui.TextColored(ToImVec4(theme.GetColor(ColorRole.Success).RgbValue), "Success message");
		ImGui.TextColored(ToImVec4(theme.GetColor(ColorRole.Warning).RgbValue), "Warning message");
		ImGui.TextColored(ToImVec4(theme.GetColor(ColorRole.Error).RgbValue), "Error message");
		ImGui.TextColored(ToImVec4(theme.GetColor(ColorRole.Info).RgbValue), "Info message");

		ImGui.Separator();

		// Tables with theme colors
		ImGui.TextUnformatted("Themed Table:");
		if (ImGui.BeginTable("PreviewTable", 3, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
		{
			ImGui.TableSetupColumn("Name");
			ImGui.TableSetupColumn("Status");
			ImGui.TableSetupColumn("Value");
			ImGui.TableHeadersRow();

			(string name, string status, string value, ColorRole color)[] statuses =
			[
				("Task 1", "Complete", "100%", ColorRole.Success),
				("Task 2", "In Progress", "60%", ColorRole.Warning),
				("Task 3", "Failed", "0%", ColorRole.Error),
				("Task 4", "Pending", "25%", ColorRole.Info),
			];

			foreach ((string name, string status, string value, ColorRole color) in statuses)
			{
				ImGui.TableNextRow();

				ImGui.TableSetColumnIndex(0);
				ImGui.TextUnformatted(name);

				ImGui.TableSetColumnIndex(1);
				ImGui.TextColored(ToImVec4(theme.GetColor(color).RgbValue), status);

				ImGui.TableSetColumnIndex(2);
				ImGui.TextUnformatted(value);
			}

			ImGui.EndTable();
		}
	}

	private static Vector4 ToImVec4(RgbColor color, float alpha = 1.0f) => new(color.R, color.G, color.B, alpha);
}
