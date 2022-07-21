using System.Linq;
using static System.Console;

namespace foxTasker
{
    public class Click : ActionNode
    {
        public override string subtype { get; } = "Click";
        public override NodeProperty[] propertyArray { get; }
            = new NodeProperty[]
            {
                new LocatorType(),
                new LocatorPath()
            };
        public Click() { }
    }
    public class GoTo : ActionNode
    {
        public override string subtype { get; } = "GoTo";
        public override NodeProperty[] propertyArray { get; }
            = new NodeProperty[]
            {
                new URL()
            };
        public GoTo() { }
        public GoTo(string url)
        {
            // quick-add URL to first null in array, 
            // expand to fit inputarray to objectarray
            propertyArray.Where(_prop =>
                (_prop.name == "URL" && _prop.value == null)
                ).First().value = url;
        }
    }
    public class ActionNode : BaseNode
    {
        public override string subtype { get; } = "base";
        public override string type { get; } = "ActionNode";
        public virtual NodeProperty[] propertyArray { get; }
            = new NodeProperty[] { };
        public ActionNode() { }
    }
}