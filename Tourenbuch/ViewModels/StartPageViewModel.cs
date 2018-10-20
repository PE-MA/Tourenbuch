using Caliburn.Micro;
using System.Collections.ObjectModel;
using System.Windows;
using Tourenbuch.Model;
using TourenbuchDatentypen;

namespace Tourenbuch
{
    internal class StartPageViewModel : Screen
    {
        DataManager dataManager;

        private ObservableCollection<TourBook> tourBooks;
        public ObservableCollection<TourBook> TourBooks
        {
            get { return tourBooks; }
            set
            {
                if (tourBooks != value)
                {
                    tourBooks = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private ObservableCollection<Tour> containedTours;
        public ObservableCollection<Tour> ContainedTours
        {
            get { return containedTours; }
            set
            {
                if (containedTours != value)
                {
                    containedTours = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private ObservableCollection<Tour> tours;
        public ObservableCollection<Tour> Tours
        {
            get { return tours; }
            set
            {
                if (tours != value)
                {
                    tours = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public StartPageViewModel()
        {
            if(dataManager == null)
            {
                dataManager = DataManager.getInstance();
            }
            TourBooks = dataManager.getAllBooks();
            Tours = dataManager.getAllTours();
        }

        private TourBook selectedTourBook;
        public TourBook SelectedTourBook
        {
            get { return selectedTourBook; }
            set
            {
                if (selectedTourBook != value)
                {
                    selectedTourBook = value;
                    NotifyOfPropertyChange();
                    ContainedTours = dataManager.getToursInBook(selectedTourBook);
                }
            }
        }

        private Tour selectedTour;
        public Tour SelectedTour
        {
            get { return selectedTour; }
            set
            {
                if (selectedTour != value)
                {
                    selectedTour = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public void DeleteBook(object o)
        {
            TourBook book = (TourBook)o;
            dataManager.deleteBook(book);
            TourBooks = dataManager.getAllBooks();
        }

        public void EditBook(object o)
        {
            TourBook book = (TourBook)o;
            ((ShellViewModel)this.Parent).EditBook(book);
        }

        public void DeleteTour(object o)
        {
            Tour tour = (Tour)o;
            dataManager.deleteTour(tour);
            Tours = dataManager.getAllTours();
        }

        public void EditTour(object o)
        {
            Tour tour = (Tour)o;
            ((ShellViewModel)this.Parent).EditTour(tour);
        }
    }
}