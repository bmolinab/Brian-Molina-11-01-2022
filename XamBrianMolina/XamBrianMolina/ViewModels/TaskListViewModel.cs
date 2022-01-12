using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamBrianMolina.Models;

namespace XamBrianMolina.ViewModels
{
  

    public class TaskListViewModel : BaseViewModel
{
    private ObservableCollection<taskViewModel> _mytasks;

        public ObservableCollection<taskViewModel> Mytasks
        {
            get { return _mytasks; }
            set { _mytasks = value; OnPropertyChanged(); }
        }

        private taskViewModel _selectedTask;

        public taskViewModel SelectedTask
        {
            get { return _selectedTask; }
            set { _selectedTask = value; OnPropertyChanged(); }
        }

        public ICommand SearchByNameCommand { private set; get; }
        public ICommand GoToDetailsCommand { private set; get; }
        public ICommand AddNewTaskCommand { private set; get; }

        public INavigation Navigation { get; set; }

        public TaskListViewModel(INavigation navigation)
        {
            Navigation = navigation;

            SearchByNameCommand = new Command<string>(async (name) => await LoadData(name));
            GoToDetailsCommand = new Command<Type>(async (pageType) => await GoToDetails(pageType));
            AddNewTaskCommand = new Command<Type>(async (pageType) => await AddNewTask(pageType));
        }

        async Task LoadData(string name)
        {
            Mytasks = new ObservableCollection<taskViewModel>();
            var mytasks = string.IsNullOrWhiteSpace(name)
                ? await App.Context.GetItemsAsync<taskModel>()
                : await App.Context.FilterItemsAsync<taskModel>("TaskTable", $"name LIKE '%{name}%'");

            foreach (var item in mytasks)
                Mytasks.Add(new taskViewModel(item));
        }

        async Task GoToDetails(Type pageType)
        {
            if (SelectedTask != null)
            {
                var page = (Page)Activator.CreateInstance(pageType);
                page.BindingContext = new TaskDetailsViewModel(SelectedTask, Navigation);
                await Navigation.PushAsync(page);

                SelectedTask = null;
            }
        }

        async Task AddNewTask(Type pageType)
        {
            SelectedTask = null;

            var page = (Page)Activator.CreateInstance(pageType);
            page.BindingContext = new TaskDetailsViewModel(new taskViewModel(), Navigation);
            await Navigation.PushAsync(page);
        }
    }
}
