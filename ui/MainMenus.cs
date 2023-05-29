using System.Collections.Generic;

namespace SettlersSharp.UI
{
    public static class MainMenus
    {
        public static Menu MainMenu()
        {
            return new Menu(
                title: "Settlers of Valgard",
                text: "Welcome to Settlers of Valgard",    
                numberedOptions: new List<Option>{
                    new Option("test", Navigation.AddMenu((game) => TestMenu()))
                },
                specialOptions: new Dictionary<char, Option>{

                }
            );
        }

        public static Menu TestMenu()
        {
            return new Menu(
                title: "Test Menu",
                text: "this is a test",
                numberedOptions: new List<Option>{
                    Option.Back(),
                }
            );
        }
    }
}