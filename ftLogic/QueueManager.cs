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
        private BaseNode nodeDraft { get; set; } = new BaseNode();
        private ICommand? _insertNodeCmd { get; set; } = null;
        public ICommand insertNodeCmd
        {
            get
            {
                return _insertNodeCmd ??
                (_insertNodeCmd = new CommandHandler(
                    () => insertNodeDraft(), () => editCollection_allow));
            }
        }
        public void insertNodeDraft( int? index = null )
        {
            int _index = ( index == null ) ? activeNode.members.Count : index.Value;
            activeNode.members.Insert(_index, nodeDraft);
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
        public void addAction()
        {
            // new ActionManager( );
            // new ActionEditor( ActionManager ); // needs for datacontext
            // if ( show dlg == true )
            // {
                // nodeDraft = actMgr.draft;
                // insertNodeDraft();
            // }
            
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
                nodeDraft = node;
                insertNodeDraft();
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
                WriteLine( $"================================" );
                WriteLine( $"NODE type: {node.type }\n" );
                WriteLine( $"node.label: {node.label}" );
            }
        }

    }
}