using System;

namespace SettlersSharp.UI
{
    public static class Navigation 
    {
        public static Action<Game> AddMenu(Func<Game, Menu> createMenu)
        {
            return (game) => {
                game.UI.Menu = createMenu(game);
            };
        }

        public static Action<Game> SwapMenu(Func<Game, Menu> createMenu)
        {
            return (game) => {
                game.UI.Menus.RemoveAt(game.UI.Menus.Count - 1);
                game.UI.Menu = createMenu(game);
            };
        }

        public static Action<Game> Back()
        {
            return (game) => {
                game.UI.Menus.RemoveAt(game.UI.Menus.Count - 1);
                game.UI.DrawMenu();
            };
        }

    }
}