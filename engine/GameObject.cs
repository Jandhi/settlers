using SadRogue.Primitives;
using SettlersSharp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using SettlersSharp.Utils;

namespace SettlersSharp.Engine
{
    public abstract class GameObject
    {
        public abstract string GetName();
        public abstract Color GetColor();

        public string Display(Color Default = default) {
            var myColor = ColorUtils.ToForegroundTag(GetColor());
            var defaultColor = ColorUtils.ToForegroundTag(Default);
            return $"{myColor}{GetName()}{defaultColor}";
        }

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