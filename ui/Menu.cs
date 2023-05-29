using System.Collections.Generic;
using System;


namespace SettlersSharp.UI 
{
    public class Menu
    {
        public string Title;
        public string Text;
        public List<Option> NumberedOptions;
        public Dictionary<char, Option> SpecialOptions;

        public Menu(string title, string text = "", List<Option> numberedOptions = null, Dictionary<char, Option> specialOptions = null)
        {
            Title = title;
            Text = text;
            NumberedOptions = numberedOptions ?? new List<Option>();
            SpecialOptions = specialOptions ?? new Dictionary<char, Option>();
        }

        public static Menu MultiPageMenu(string title, int numEntries, Func<int, Option> optionGenerator, string text = "", int pageNum = 0, int entriesPerPage = 9, bool canGoBack = true)
        {
            var maxPages = (numEntries + entriesPerPage - 1) / entriesPerPage;
            var options = new List<Option>();

            for(var i = entriesPerPage * pageNum; i < numEntries && i < entriesPerPage * (pageNum + 1); i++)
            {
                options.Add(optionGenerator(i));
            }

            var specialOptions = new Dictionary<char, Option>();

            if(pageNum > 0)
            {
                specialOptions.Add('<', new Option("Previous Page", Navigation.SwapMenu((game) => {
                    return MultiPageMenu(title, numEntries, optionGenerator, text, pageNum - 1, entriesPerPage, canGoBack);
                })));
            }

            if(pageNum < maxPages - 1)
            {
                specialOptions.Add('>', new Option("Next Page", Navigation.SwapMenu((game) => {
                    return MultiPageMenu(title, numEntries, optionGenerator, text, pageNum + 1, entriesPerPage, canGoBack);
                })));
            }

            if(canGoBack)
            {
                specialOptions.Add('.', Option.Back());
            }

            return new Menu(
                title: title,
                text: $"{text} (page {pageNum}/{maxPages})",
                numberedOptions: options,
                specialOptions: specialOptions
            );
        }
    }
}