using System.Collections.ObjectModel; // for Collection

namespace foxTasker
{
    public class GroupNode : BaseNode
    {
        public override string type { get; } = "group";
        public GroupNode() { }
    }
    public class NodeCollection : Collection<BaseNode> { }
    public class BaseNode
    {
        public virtual string label { get; set; } = "default label";
        public virtual string type { get; } = "base";
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