using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourenbuchDatentypen;

namespace Tourenbuch.Model
{
    class DataManager
    {
        private static DataManager myInstance = null;
        public static DataManager getInstance(string username, string pw)
        {
            myInstance = new DataManager(username, pw);
            return myInstance;
        }

        public static DataManager getInstance()
        {
            return myInstance;
        }

        private WCFDataBaseManager dBM;
        private DataManager(string username, string pw)
        {
            dBM = WCFDataBaseManager.getInstance(username, pw);
        }

        public TourBook saveBook(TourBook bookName)
        {
            return dBM.SaveBook(bookName);
        }

        public ObservableCollection<TourBook> getAllBooks()
        {
            return dBM.getAllBooks();
        }

        internal ObservableCollection<Tour> getToursInBook(TourBook selectedTourBook)
        {
            return dBM.getToursInBook(selectedTourBook);
        }

        internal Tour saveTour(Tour createTour)
        {
            return dBM.saveTour(createTour);
        }

        internal ObservableCollection<Mountain> getAllMountains()
        {
            return dBM.getAllMountains();
        }

        internal void deleteBook(TourBook book)
        {
            dBM.deleteBook(book);
        }

        internal ObservableCollection<Tour> getAllTours()
        {
            return dBM.getAllTours();
        }

        internal void deleteTour(Tour tour)
        {
            dBM.deleteTour(tour);
        }

        internal ObservableCollection<Category> getAllCategories()
        {
            return dBM.getAllCategories();
        }

        internal ObservableCollection<Tour> getAllToursFiltered(string searchText, bool? summitReached, int category)
        {
            return dBM.getAllToursFiltered(searchText, summitReached, category);
        }

        internal ObservableCollection<User> getAllUsers()
        {
            return dBM.getAllUsers();
        }

        internal bool tryConnection()
        {
            return dBM.tryConnection();
        }
    }
}
