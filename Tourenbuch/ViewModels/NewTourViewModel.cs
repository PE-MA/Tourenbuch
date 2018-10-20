using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Tourenbuch.Model;
using TourenbuchDatentypen;

namespace Tourenbuch
{
    internal class NewTourViewModel : Screen, INotifyPropertyChanged
    {
        public string SaveButtonContent { get; set; }

        public Dictionary<string, object> PossibleMates { get; set; }

        private Dictionary<string, object> selectedMates;
        public Dictionary<string, object> SelectedMates
        {
            get { return selectedMates; }
            set
            {
                if (selectedMates != value)
                {
                    selectedMates = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public Dictionary<string, object> TourBooksAsMCItems { get; set; }
        
        private Dictionary<string, object> selectedTourBooks;
        public Dictionary<string, object> SelectedTourBooks
        {
            get { return selectedTourBooks; }
            set
            {
                if (selectedTourBooks != value)
                {
                    selectedTourBooks = value;
                    NotifyOfPropertyChange();
                }
            }
        }
        public ObservableCollection<Mountain> PredictedMountains { get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        private Tour currentTour;
        public Tour CurrentTour
        {
            get { return currentTour; }
            set
            {
                if (currentTour != value)
                {
                    currentTour = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public NewTourViewModel(Tour currentTour)
        {
            CurrentTour = currentTour;
            //if (PredictedMountains == null)
                //PredictedMountains = DataManager.getInstance().getAllMountains();
            if (Categories == null)
                Categories = DataManager.getInstance().getAllCategories();

            PossibleMates = new Dictionary<string, object>();
            ObservableCollection<User> AllUsers = DataManager.getInstance().getAllUsers();
            foreach (User user in AllUsers)
            {
                PossibleMates.Add(user.Forename + " " + user.Surname, user);
            }

            SelectedMates = new Dictionary<string, object>();
            foreach (int id in currentTour.AssignedUserIDs)
            {
                KeyValuePair<string, object> k = PossibleMates.FirstOrDefault(t => ((User)t.Value).id == id);
                SelectedMates.Add(k.Key, k.Value);
            }

            TourBooksAsMCItems = new Dictionary<string, object>();
            ObservableCollection<TourBook> TourBooks = DataManager.getInstance().getAllBooks();
            foreach (TourBook tb in TourBooks)
            {
                TourBooksAsMCItems.Add(tb.BookName, tb);
            }

            SelectedTourBooks = new Dictionary<string, object>();
            foreach (int id in currentTour.AssignedTourBookIDs)
            {
                KeyValuePair<string, object> k = TourBooksAsMCItems.FirstOrDefault(t => ((TourBook)t.Value).id == id);
                SelectedTourBooks.Add(k.Key, k.Value);
            }

            SaveButtonContent = "Speichern";

            NotifyOfPropertyChange(() => PredictedMountains);
            NotifyOfPropertyChange(() => TourBooksAsMCItems);
            NotifyOfPropertyChange(() => SelectedTourBooks);
            NotifyOfPropertyChange(() => PossibleMates);
            NotifyOfPropertyChange(() => SelectedMates);
        }

        public NewTourViewModel() : this(new Tour())
        { }

        private bool _canSave = true;
        public bool CanSave
        {
            get
            { return _canSave; }
            set
            {
                if (_canSave != value)
                {
                    _canSave = value;
                    NotifyOfPropertyChange(() => CanSave);
                }
            }
        }

        public void saveTour()
        {
            // Ermittelt die ausgewählten Tourenbücher und fügt Sie dem Tourenobjekt hinzu
            CurrentTour.AssignedTourBookIDs = new List<int>();
            foreach (KeyValuePair<string, object> tb in SelectedTourBooks)
            {
                TourBook tourBookToSave = (TourBook)tb.Value;
                if (tourBookToSave.id != null)
                    CurrentTour.AssignedTourBookIDs.Add(tourBookToSave.id.GetValueOrDefault());
            }

            // Ermittelt die ausgewählten Kameraden und fügt Sie dem Tourenobjekt hinzu
            CurrentTour.AssignedUserIDs = new List<int>();
            foreach (KeyValuePair<string, object> mate in SelectedMates)
            {
                User userToSave = (User)mate.Value;
                CurrentTour.AssignedUserIDs.Add(userToSave.id);
            }

            Tour createdTour = DataManager.getInstance().saveTour(CurrentTour);
            if (createdTour != null)
            {
                CurrentTour = createdTour;
                SaveButtonContent = "Speichern erfolgreich durchgeführt";
                NotifyOfPropertyChange(() => SaveButtonContent);
            }
        }
    }
}