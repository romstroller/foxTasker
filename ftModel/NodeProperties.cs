namespace foxTasker
{
    public class LocatorType : NodeProperty
    {
        public override string name { get; } = "LocatorType";
    }
    public class LocatorPath : NodeProperty
    {
        public override string name { get; } = "LocatorPath";
    }
    public class URL : NodeProperty
    {
        public override string name { get; } = "URL";
    }
    public class NodeProperty
    {
        public virtual string name { get; } = "NodeProperty";
        public string? value { get; set; } = null;
        public NodeProperty(string _value) { value = _value; }
        public NodeProperty() { }
    }
}