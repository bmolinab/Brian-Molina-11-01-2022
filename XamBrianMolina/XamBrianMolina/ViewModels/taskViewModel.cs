using System;
using System.Collections.Generic;
using System.Text;
using XamBrianMolina.Models;

namespace XamBrianMolina.ViewModels
{
    public class taskViewModel:BaseViewModel
    {
        private string _Id;
        public string Id
        {
            get { return _Id; } 
            set { _Id = value; OnPropertyChanged(); }
        }
        private string _Name;
        public string Name 
        { 
            get { return _Name;} 
            set { _Name = value; OnPropertyChanged(); } 
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged(); }
        }

        private bool _IsCompleted;
        public bool IsCompleted { 
            get { return _IsCompleted; }
            set { _IsCompleted = value; OnPropertyChanged(); }
            }

        public taskViewModel()
        {

        }

        public taskViewModel(taskModel _taskModel )
        {
            _Id = _taskModel.Id;
            _Name = _taskModel.Name;    
            _Description = _taskModel.Description;
            _IsCompleted= _taskModel.IsCompleted;
        }

        public taskModel GetTaskModel()
        {
            return new taskModel()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                IsCompleted = this.IsCompleted,
            };

        }
    }
}
