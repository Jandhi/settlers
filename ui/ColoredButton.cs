using SadConsole.UI.Controls;
using SadConsole.UI;
using SadConsole.UI.Themes;
using SadRogue.Primitives;
using System;

namespace SadConsole.UI.Controls
{
    class ColoredButton : Button
    {
        public ColoredString AlternativeText { get; set; }

        public ColoredButton(int width, int height = 1) : base(width, height)
        {
        }
    }

    class ColoredButtonTheme : ButtonTheme
    {
        public override void UpdateAndDraw(ControlBase control, TimeSpan time)
        {
            if (control is not ColoredButton button) return;
            if (!button.IsDirty) return;

            // Button is being used normally
            if (button.AlternativeText == null)
            {
                base.UpdateAndDraw(control, time);
                return;
            }

            // Button is using the new AlternativeText
            RefreshTheme(control.FindThemeColors(), control);
            ColoredGlyph appearance = ControlThemeState.GetStateAppearance(control.State);
            ColoredGlyph endGlyphAppearance = EndsThemeState.GetStateAppearance(control.State);

            int middle = (button.Height != 1 ? button.Height / 2 : 0);

            // Redraw the control
            button.Surface.Fill(
                appearance.Foreground,
                appearance.Background,
                appearance.Glyph, null);

            // Button same size as text, straight print
            if (button.Width == button.AlternativeText.Length)
                button.Surface.Print(0, middle, button.AlternativeText);

            // Button bigger than text, do alignment
            else if (button.Width > button.AlternativeText.Length)
            {
                if (button.TextAlignment == HorizontalAlignment.Left)
                    button.Surface.Print(0, middle, button.AlternativeText);

                else if (button.TextAlignment == HorizontalAlignment.Right)
                    button.Surface.Print(button.Width - button.AlternativeText.Length, middle, button.AlternativeText);

                else if (button.TextAlignment == HorizontalAlignment.Center)
                    button.Surface.Print(button.Width / 2 - button.AlternativeText.Length / 2, middle, button.AlternativeText);
            }

            // Text is bigger than the text, fill
            else
            {
                if (button.TextAlignment == HorizontalAlignment.Left)
                    button.Surface.Print(0, middle, button.AlternativeText);

                else if (button.TextAlignment == HorizontalAlignment.Right)
                    button.Surface.Print(0, middle, button.AlternativeText.SubString(button.AlternativeText.Length - button.Width));

                else if (button.TextAlignment == HorizontalAlignment.Center)
                    button.Surface.Print(0, middle, button.AlternativeText.SubString(button.AlternativeText.Length / 2 - button.Width / 2, button.Width));
            }

            button.IsDirty = false;
        }
    }
}