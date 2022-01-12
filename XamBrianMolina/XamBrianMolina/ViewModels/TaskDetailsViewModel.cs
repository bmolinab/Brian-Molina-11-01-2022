using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamBrianMolina.Models;

namespace XamBrianMolina.ViewModels
{
    public class TaskDetailsViewModel : BaseViewModel
    {
        private taskViewModel _taskVM;

        public taskViewModel TaskVM
        {
            get { return _taskVM; }
            set { _taskVM = value; OnPropertyChanged(); }
        }

        public ICommand SaveTaskCommand { private set; get; }
        public ICommand DeleteTaskCommand { private set; get; }

        public INavigation Navigation { get; set; }


        public TaskDetailsViewModel(taskViewModel mytask, INavigation navigation)
        {
            TaskVM = mytask;
            Navigation = navigation;

            SaveTaskCommand = new Command(async () => await SaveTask());
            DeleteTaskCommand = new Command(async () => await DeleteTask());
        }

        async Task SaveTask()
        {
            var isInsert = false;

            if (string.IsNullOrWhiteSpace(TaskVM.Id))
            {
                TaskVM.Id = Guid.NewGuid().ToString();
                isInsert = true;
            }

            var myTask = TaskVM.GetTaskModel();
            var success = await App.Context.SaveItemAsync<taskModel>(myTask, isInsert);
            await UserDialogs.Instance.AlertAsync((success > 0) ? "Completo!" : "Error!", "Guardando...", "OK");
              await Navigation.PopAsync ();

        }
       
        async Task DeleteTask()
        {
            var confirm = await UserDialogs.Instance.ConfirmAsync("Esta seguro", "Borrar?", "Si", "No");

            if (confirm)
            {
                var myTask = TaskVM.GetTaskModel();
                var success = await App.Context.DeleteItemAsync<taskModel>(myTask);
                await UserDialogs.Instance.AlertAsync((success > 0) ? "Completo!" : "Error!", "Borrando...", "OK");
                await Navigation.PopAsync();

            }
        }
    }
}
