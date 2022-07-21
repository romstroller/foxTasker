using System.Windows.Input; // ICommand
using System.Collections.ObjectModel; // Observable collection
using static System.Console; // WriteLine
using System.Reflection;

namespace foxTasker
{
    public class QueueManager
    {
        public QueueManager()
        {
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
        public ICommand insertNodeCmd
        {
            get
            {
                return _insertNodeCmd ??
                (_insertNodeCmd = new CommandHandler(
                    () => insertNode(), () => editCollection_allow));
            }
        }
        BaseNode targetNode { get; set; } = new BaseNode();
        public void insertNode(BaseNode? node = null, int? _index = null)
        {
            int index = (_index == null) ? activeNode.members.Count : _index.Value;
            activeNode.members.Insert(index, node ?? targetNode);
            updateActiveCollection();
        }
        private ICommand? _addActionCmd { get; set; } = null;
        public ICommand addActionCmd
        {
            get
            {
                return _addActionCmd ??
                (_addActionCmd = new CommandHandler(
                    () => addAction(), () => editCollection_allow));
            }
        }

        private ActionManager? actionMgr { get; set; } = null;
        private ActionWindow? actionWin { get; set; } = null;
        public void addAction()
        {
            ActionNode nodeDraft = new ActionNode();
            actionMgr = actionMgr ?? new ActionManager();
            actionMgr.manage(nodeDraft);
            actionWin = new ActionWindow(actionMgr); // pass as datacontext
            if (actionWin.ShowDialog() == true) insertNode(nodeDraft);
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
            NodeCollection exampleQueue = new NodeCollection
            {
                new GoTo( "http://www.wikipedia.org" ),
                new Click( )
            };

            foreach (BaseNode node in exampleQueue)
            {
                targetNode = node;
                insertNode();
            }
        }
        private ICommand? _runQueue_mockCmd { get; set; } = null;
        public ICommand runQueue_mockCmd
        {
            get
            {
                return _runQueue_mockCmd ??
                (_runQueue_mockCmd = new CommandHandler(
                    () => runQueue_mock(), () => true));
            }
        }
        public void runQueue_mock()
        {
            foreach (BaseNode node in activeCollection)
            {
                WriteLine($"================================");
                WriteLine($"NODE type: {node.type}\n");
                WriteLine($"node.label: {node.label}");
            }
        }

        // public bool startDriverClick_enabled { get { return true; } }
        // private ICommand? _startDriverClickHdler;
        // public ICommand startDriverClick
        // {
        //     get
        //     {
        //         return _startDriverClickHdler ??
        //         (_startDriverClickHdler = new CommandHandler(
        //             () => startDriver(), () => startDriverClick_enabled));
        //     }
        // }
        // private void startDriver() { agent = new DriverAgent(); }
        // private DriverAgent? agent { get; set; }

    }
}