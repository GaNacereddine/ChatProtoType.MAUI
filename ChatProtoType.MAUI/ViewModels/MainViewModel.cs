using ChatProtoType.MAUI.Models;
using ChatProtoType.MAUI.Services;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatProtoType.MAUI.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }

    public class MainViewModel : ViewModelBase
    {
        public ICommand GoToSpace => new Command(GoToSpaceView);
        public ICommand GoToChat => new Command(GoToChatView);

        public MainViewModel()
        {
        }

        private void GoToSpaceView()
        {
            NavigationService.Instance.NavigateToAsync<SpaceViewModel>();
        }

        private void GoToChatView()
        {
            NavigationService.Instance.NavigateToAsync<ChatViewModel>();
        }

    }

    public class SpaceViewModel : ViewModelBase
    {

    
    }

    public class ChatViewModel : ViewModelBase
    {
        public ObservableCollection<ChatMessage> Messages { get; } = new();

        [Reactive]
        public bool IsRefreshing { get; set; }
 
        public ICommand RefreshCommand => new Command(AddMessages);

        Random ran = new Random();

        private void AddMessages(object obj)
        {
            IsRefreshing = true;
            Messages.AddOrInsertRange(MessageService.Instance.GetMessages(MessageService.Instance.GetUsers()[ran.Next(0, 10)]), 0);
            IsRefreshing = false;
        }

        public ChatViewModel()
        {
            Messages.AddRange(MessageService.Instance.GetMessages(MessageService.Instance.user10));
        }

    }

}
