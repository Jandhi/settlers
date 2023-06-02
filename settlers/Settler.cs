using SadRogue.Primitives;
using SettlersSharp.Engine;

namespace SettlersSharp.Settlers 
{
    public class Settler : GameObject
    {
        public string Name;

        public override Color GetColor()
        {
            return Color.White;
        }

        public override string GetName()
        {
            return Name;
        }
    }
}