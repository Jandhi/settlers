using SadRogue.Primitives;
using SettlersSharp.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SettlersSharp.Engine
{
    public interface GameObject
    {
        public string GetName();
        public Color GetColor();

        public static Func<Game, Menu> ObjectMenu<T>(string title, string text, Func<Game, List<T>> contentGetter, Func<T, Game, Option> optionGenerator, bool canGoBack = true) where T : GameObject {
            return (game) => new Menu(
                title: title,
                text: text,
                numberedOptions: contentGetter(game).Select(item => optionGenerator(item, game)).ToList(),
                specialOptions: canGoBack ? new Dictionary<char, Option>{
                    {'.', Option.Back()}
                } : null
            );
        }

        public static Func<Game, Menu> MultipageObjectMenu<T>(string title, string text, Func<Game, List<T>> contentGetter, Func<T, Game, Option> optionGenerator,
            int pageNum = 0, int entriesPerPage = 9, bool canGoBack = true
        ) where T : GameObject {            
            return (game) => {
                var content = contentGetter(game);
                return Menu.MultiPageMenu(
                    title, 
                    content.Count, 
                    (index) => {
                        return optionGenerator(content[index], game);
                    },
                    text,
                    pageNum,
                    entriesPerPage,
                    canGoBack
                );
            };
        }
    }
}