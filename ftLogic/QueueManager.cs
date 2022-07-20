using System.Windows.Input; // ICommand
using System.Collections.ObjectModel; // Observable collection
using static System.Console; // WriteLine

namespace foxTasker
{
    public class QueueManager
    {
        public QueueManager(BaseNode? startupNode = null)
        {
            activeNode = startupNode ?? new BaseNode();
            WriteLine("QueueManager instantiated");
        }
        private BaseNode _activeNode { get; set; } = new BaseNode();
        public BaseNode activeNode
        {
            get { return _activeNode; }
            set
            {
                WriteLine("implement safe-to-change-node checks, eg. collection changes saved");
                _activeNode = value;
                updateActiveCollection();
            }
        }
        public ObservableCollection<BaseNode> activeCollection { get; }
            = new ObservableCollection<BaseNode>();
        public void updateActiveCollection()
        {
            activeCollection.Clear();
            foreach (BaseNode node in activeNode.members) activeCollection.Add(node);
        }
        private bool editCollection_allow { get; set; } = true;
        private ICommand? _insertNodeCmd { get; set; } = null;
        private BaseNode draftNode { get; set; } = new BaseNode();
        public ICommand insertNodeCmd
        {
            get
            {
                return _insertNodeCmd ??
                (_insertNodeCmd = new CommandHandler(
                    () => insertNode(), () => editCollection_allow));
            }
        }
        public void insertNode()
        {
            activeNode.members.Insert(activeNode.members.Count, draftNode);
            updateActiveCollection();
        }
        private ICommand? _addExampleItems_cmd { get; set; } = null;
        public ICommand addExampleItems_cmd
        {
            get
            {
                return _addExampleItems_cmd ??
                (_addExampleItems_cmd = new CommandHandler(
                    () => addExampleItems(), () => true));
            }
        }
        public void addExampleItems()
        {
            NodeCollection exampleNodes = new NodeCollection { new GoTo(), new Click() };
            foreach (BaseNode node in exampleNodes)
            {
                draftNode = node;
                insertNode();
            }
        }
    }
}