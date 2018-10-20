using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel.Description;
using TourenbuchDatentypen;

namespace Tourenbuch.Model
{
    internal class WCFDataBaseManager
    {
        ServiceReference1.TourenbuchServiceClient TBSC;

        private static WCFDataBaseManager myInstance = null;
        public static WCFDataBaseManager getInstance(string username, string pw)
        {
            myInstance = new WCFDataBaseManager(username, pw);
            return myInstance;
        }

        private WCFDataBaseManager(string username, string pw)
        {
            ClientCredentials clientCredentials = new ClientCredentials();
            clientCredentials.UserName.UserName = username;
            clientCredentials.UserName.Password = pw;

            if(TBSC == null)
            {
                TBSC = new ServiceReference1.TourenbuchServiceClient();
            }

            TBSC.ChannelFactory.Endpoint.EndpointBehaviors.RemoveAt(1);
            TBSC.ChannelFactory.Endpoint.EndpointBehaviors.Add(clientCredentials);
        }

        public void deleteBook(TourBook book)
        {
            TBSC.DeleteBook(book);
        }

        public void deleteTour(Tour tour)
        {
            TBSC.DeleteTour(tour);
        }

        public ObservableCollection<TourBook> getAllBooks()
        {
            return TBSC.GetAllBooks();
        }

        public ObservableCollection<Mountain> getAllMountains()
        {
            return TBSC.GetAllMountains();
        }

        public ObservableCollection<Tour> getAllTours()
        {
            return TBSC.GetAllTours();
        }

        public ObservableCollection<Tour> getToursInBook(TourBook selectedTourBook)
        {
            return TBSC.GetToursInBook(selectedTourBook);
        }

        public TourBook SaveBook(TourBook bookName)
        {
            return TBSC.SaveBook(bookName);
        }

        public Tour saveTour(Tour createTour)
        {
            return TBSC.SaveTour(createTour);
        }

        public ObservableCollection<Category> getAllCategories()
        {
            return TBSC.GetAllCategories();
        }

        public ObservableCollection<Tour> getAllToursFiltered(string searchText, bool? summitReached, int category)
        {
            return TBSC.GetAllToursFiltered(searchText, summitReached, category);
        }

        public ObservableCollection<User> getAllUsers()
        {
            return TBSC.GetAllUsers();
        }

        public bool tryConnection()
        {
            return TBSC.TryConnection();
        }
    }
}