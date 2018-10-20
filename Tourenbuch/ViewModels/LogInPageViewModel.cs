using Caliburn.Micro;
using System;
using Tourenbuch.Model;

namespace Tourenbuch
{
    internal class LogInPageViewModel : Screen
    {
        DataManager dataManager;

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private string passwort;
        public string Passwort
        {
            get { return passwort; }
            set
            {
                if (passwort != value)
                {
                    passwort = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private ShellViewModel shellViewModel;
        public LogInPageViewModel(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
        }

        public void logIn()
        {
            dataManager = DataManager.getInstance(userName, passwort);
            if (dataManager != null)
            {
                try
                {
                    dataManager.tryConnection();
                    shellViewModel.UserIsLoggedIn = true;
                    shellViewModel.ShowStartPage();
                }
                catch(Exception e)
                {
                    shellViewModel.ShowExceptionPage(e);
                }
             }
        }
    }
}