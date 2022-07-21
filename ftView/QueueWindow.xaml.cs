using System.Windows;

namespace foxTasker
{
    public partial class QueueWindow : Window
    {
        private QueueManager queueManager { get; } = new QueueManager();
        public QueueWindow()
        {
            DataContext = queueManager;
            InitializeComponent();
        }
    }
}
