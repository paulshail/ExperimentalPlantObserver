using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MessageHandler
{
    public class NotificationHandler
    {
            #region Properties

            public ObservableCollection<Message> Messages { get; set; }

            #endregion Properties

            public RelayCommand<Message?> DeleteItemCommand { get; }

            #region Constructors

            public NotificationHandler()
            {
                Messages = new ObservableCollection<Message>();
                Messages.CollectionChanged += MessagesOnCollectionChanged;

                DeleteItemCommand = new RelayCommand<Message?>(param =>
                {
                    if (param is null)
                        return;
                    Messages.Remove(param);
                });
            }

            public NotificationHandler(IEnumerable<Message>? messages)
            {
                Messages = messages == null ? new ObservableCollection<Message>() : new ObservableCollection<Message>(messages);
                Messages.CollectionChanged += MessagesOnCollectionChanged;

                DeleteItemCommand = new RelayCommand<Message?>(param =>
                {
                    if (param is null)
                        return;
                    Messages.Remove(param);
                });
            }

            #endregion Constructors

            public void Add(Message? message)
            {
                if (message is null)
                    return;
                Messages.Add(message);
            }

            private void MessagesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.NewItems == null) return;
                foreach (object item in e.NewItems)
                {
                    if (item is not Message message) continue;

                    DispatcherTimer timer = new()
                    {
                        Interval = new TimeSpan(0, 0, 8)
                    };
                    void OnTimerOnTick(object? o, EventArgs args)
                    {
                        DeleteItemCommand.Execute(message);
                        timer.Tick -= OnTimerOnTick;
                        timer.Stop();
                    }
                    timer.Tick += OnTimerOnTick;
                    timer.Start();
                }
            }
        }
}
