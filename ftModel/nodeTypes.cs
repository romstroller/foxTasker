using System.Collections.ObjectModel; // for Collection

namespace foxTasker
{
    public class Click : ActionNode
    {
        public override string actionType { get; } = "Click";
        public Click() { }
    }
    public class GoTo : ActionNode
    {
        public override string actionType { get; } = "GoTo";
        public GoTo() { }
    }
    public class ActionNode : BaseNode
    {
        public override string nodeType { get; } = "action";
        public virtual string actionType { get; } = "actionbase";
        public ActionNode() { }
    }
    public class GroupNode : BaseNode
    {
        public override string nodeType { get; } = "group";
        public GroupNode() { }
    }
    public class NodeCollection : Collection<BaseNode> { }
    public class BaseNode
    {
        public virtual string nodeType { get; } = "base";
        public virtual string nodeLabel { get; } = "default label";
        private NodeCollection _members { get; set; }
            = new NodeCollection();
        public NodeCollection members
        {
            get { return _members; }
            set { _members = value; }
        }
        public BaseNode() { }
        public void addSubNodes(NodeCollection nodes)
        {
            foreach (BaseNode node in nodes) members.Add(node);
        }
    }
}