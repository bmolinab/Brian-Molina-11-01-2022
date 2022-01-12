using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamBrianMolina.Data;

namespace XamBrianMolina
{
    public partial class App : Application
    {
        private static DBContext context;

        public static DBContext Context
        {
            get
            {
                if (context == null)
                {
                    var dbPath = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "BertoniTask.db3");

                    context = new DBContext(dbPath);
                }

                return context;
            }

        }


        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Views.TaskListView());

          //  MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
