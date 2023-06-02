using System;
using SadConsole;
using SadRogue.Primitives;
using Console = SadConsole.Console;
using SettlersSharp.UI;
using SettlersSharp.Engine;
using SadConsole.UI.Controls;

namespace SettlersSharp
{
    class Program
    {
        private static void Main(string[] args)
        {
            SadConsole.UI.Themes.Library.Default.SetControlTheme(typeof(ColoredButton), new ColoredButtonTheme());

            var SCREEN_WIDTH = 80;
            var SCREEN_HEIGHT = 25;

            SadConsole.Settings.WindowTitle = "Settlers of Valgard";
            SadConsole.Settings.UseDefaultExtendedFont = true;

            SadConsole.Game.Create(SCREEN_WIDTH, SCREEN_HEIGHT);
            SadConsole.Game.Instance.OnStart = Init;
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private static void Init()
        {
            // This code uses the default console created for you at start

            var game = new Game();
            var console = new MenuConsole(100, 40, game);
            game.UI = console;

            game.Settlers = new System.Collections.Generic.List<Settlers.Settler>();
            for(var i = 0; i < 25; i++)
            {
                var settler = new Settlers.Settler();
                settler.Name = $"Settlers {i}";
                game.Settlers.Add(settler);
            }

            console.Menu = MainMenus.MainMenu();
            console.Menu.NumberedOptions.Add(
                new Option("Settlers", 
                    Navigation.AddMenu(
                        GameObject.MultipageObjectMenu("Settlers", $"choose a {game.Settlers[0].Display(Color.LightGray)}", (game) => game.Settlers, 
                            (settler, game) => {
                                return new Option(settler.GetName(), Navigation.Back());
                            }
                        )
                    )
                )
            );


            console.DrawMenu();

            SadConsole.Game.Instance.Screen = console;
        }
    }
}