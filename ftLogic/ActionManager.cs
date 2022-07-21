using System;
using System.Linq;
using System.Windows.Input;
using static System.Console;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace foxTasker
{
    public class ActionManager : INotifyPropertyChanged
    {
        public ActionManager()
        {
            initializeState();
            WriteLine("new action manager instantiated");
        }
        internal ActionNode actionDraft { get; set; } = new ActionNode();
        public string actionLabel
        {
            get { return actionDraft.label; }
            set
            {
                if (actionDraft.label == value) return;
                actionDraft.label = value;
                OnPropertyChanged(nameof(actionLabel));
            }
        }
        public void fromSelected(ActionNode inputNode)
        {
            actionDraft = inputNode;
            updateInputProperties();
        }
        Dictionary<string, Type> typeDict { get; } = new Dictionary<string, Type>();
        public List<string> actTypeList { get; } = new List<string>();
        public void initializeState()
        {
            // get name property from auto-instances of all ActionItem subclasses
            List<Type> actItemSubtypes = new List<Type>();
            foreach (var assem in AppDomain.CurrentDomain.GetAssemblies())
            {
                actItemSubtypes.AddRange(assem.GetTypes().Where(
                    myType => myType.IsSubclassOf(typeof(ActionNode))));
            }
            foreach (var type in actItemSubtypes)
            {
                object? myobject = Activator.CreateInstance(type);
                if (myobject != null)
                {
                    ActionNode? newAction = myobject as ActionNode;
                    if (newAction != null)
                    {
                        typeDict.Add(newAction.subtype, type); // for obj drafter
                        actTypeList.Add(newAction.subtype); // for dropdown selector
                    }
                }
            }
        }
        public string selectedActType { get; set; } = "";
        public bool draftAction_enabled { get { return (selectedActType.Length > 0); } }
        private ICommand? _draftAction_cmd;
        public ICommand draftActionClick
        {
            get
            {
                return _draftAction_cmd ?? // reconfig if editing existing action
                (_draftAction_cmd = new CommandHandler(
                    () => draftAction(), () => draftAction_enabled));
            }
        }
        private void draftAction()
        {
            WriteLine("draftAction fired");
            object? newAct = Activator.CreateInstance(typeDict[selectedActType]);
            actionDraft = newAct as ActionNode ?? actionDraft;
            updateInputProperties();
        }
        public ObservableCollection<NodeProperty> inputProperties { get; set; }
            = new ObservableCollection<NodeProperty>() { };
        private string _draftActName = "Choose an action (type-search available)...";
        public string draftActName
        {
            get { return _draftActName; }
            set
            {
                if (_draftActName == value) return;
                _draftActName = value;
                OnPropertyChanged(nameof(draftActName));
            }
        }
        internal void clearProperties()
        {
            foreach (NodeProperty prop in inputProperties.ToList())
                inputProperties.Remove(prop);
        }
        private void updateInputProperties()
        {
            WriteLine("Updating form property source");

            if (inputProperties != null && actionDraft != null)
            {
                // re-populate inputProperties, update label
                clearProperties();
                foreach (NodeProperty prop in actionDraft.propertyArray)
                    inputProperties.Add(prop);

                draftActName = actionDraft.subtype;
                actionLabel = actionDraft.label;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}