using Caliburn.Micro;
using System;
using TourenbuchDatentypen;

namespace Tourenbuch
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell
    {
        private bool userIsLoggedIn = false;
        public bool UserIsLoggedIn
        {
            get { return userIsLoggedIn; }
            set
            {
                if (userIsLoggedIn != value)
                {
                    userIsLoggedIn = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public ShellViewModel()
        {
            ShowLogInPage();
        }

        public void ShowLogInPage()
        {
            LogInPageViewModel lpvm = new LogInPageViewModel(this);
            ActivateItem(lpvm);
        }

        public void ShowExceptionPage(Exception e)
        {
            ExceptionViewModel exvm = new ExceptionViewModel(this);
            exvm.ExceptionText = e.Message;
            if (e.InnerException != null)
                exvm.InnerExceptionText = e.InnerException.Message;
            ActivateItem(exvm);
        }

        public void ShowStartPage()
        {
            StartPageViewModel spvm = new StartPageViewModel();
            ActivateItem(spvm);
        }

        public void CreatNewBook()
        {
            ActivateItem(new NewBookViewModel());
        }

        public void OpenNewTour()
        {
            ActivateItem(new NewTourViewModel());
        }

        public void SearchTour()
        {
            ActivateItem(new SearchTourViewModel());
        }

        public void EditTour(Tour t)
        {
            ActivateItem(new NewTourViewModel(t));
        }

        public void EditBook(TourBook book)
        {
            ActivateItem(new NewBookViewModel(book));
        }

        public void OpenNewMountain()
        {
            ActivateItem(new NewMountainViewModel());
        }

        public void OpenNewCategory()
        {
            ActivateItem(new NewCategoryViewModel());
        }
    }
}
// Caliburn.Micro.PropertyChangedBase,