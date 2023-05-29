using System;

namespace SettlersSharp.UI 
{
    public class Option
    {
        public string Text;
        public Action<Game> OnSelect;

        public Option(string text, Action<Game> onSelect)
        {
            Text = text;
            OnSelect = onSelect;
        }

        public static Option Back()
        {
            return new Option(
                text: "Back",
                onSelect: Navigation.Back() 
            );
        }
    }
}