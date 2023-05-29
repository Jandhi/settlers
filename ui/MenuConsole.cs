using System;
using System.Collections.Generic;
using System.Linq;
using SadConsole;
using SadConsole.Input;
using SadConsole.UI.Controls;
using SadConsole.UI.Themes;
using SadRogue.Primitives;
namespace SettlersSharp.UI 
{
    public class MenuConsole : SadConsole.UI.ControlsConsole
    {
        public List<Menu> Menus = new List<Menu>();
        public Game Game;
        public Menu Menu {
            get {
                if(Menus.Count > 0) {
                    return Menus.Last();
                }

                return null;
            }

            set {
                Menus.Add(value);
                DrawMenu();
            }
        }

        public MenuConsole(int width, int height, Game game) : base(width, height)
        {
            this.UsePrintProcessor = true;
            Game = game;

            if(Menu != null)
            {
                DrawMenu();
            }
        }

        public void DrawMenu()
        {
            this.Clear();
            Controls.Clear();

            this.Print(1, 1, Menu.Title.ToUpper(), Color.White);
            
            var line = "";
            for(var i = 0; i < Menu.Title.Length; i++) line += "-";
            this.Print(1, 2, line);

            this.Print(1, 3, Menu.Text);

            var theme = new ButtonTheme();
            theme.ShowEnds = false;

            var y = 5;
            var counter = 1;
            foreach(var option in Menu.NumberedOptions)
            {
                MakeButton(y, (char) ((int) '0' + counter), option, theme);
                counter++;
                y++;
            }

            foreach(var (prefix, option) in Menu.SpecialOptions)
            {
                MakeButton(y, prefix, option, theme);
                y++;
            }
        }

        public void MakeButton(int y, char prefix, Option option, ButtonTheme theme)
        {
            var text = prefix.ToString() + ": " + option.Text;

            var button = new Button(text.Length, 1)
                {
                    Text = text,
                    Position = new Point(1, y)
                };
                button.Click += (s, a) => option.OnSelect(Game);
                button.Theme = theme;
                Controls.Add(button);
        }
    }
}