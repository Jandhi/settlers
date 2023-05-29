using SadRogue.Primitives;
using SettlersSharp.Engine;

namespace SettlersSharp.Settlers 
{
    public class Settler : GameObject
    {
        public string Name;

        public Color GetColor()
        {
            return Color.White;
        }

        public string GetName()
        {
            return Name;
        }
    }
}