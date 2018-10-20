using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourenbuch.Model;
using TourenbuchDatentypen;

namespace Tourenbuch
{
    internal class SearchTourViewModel : Screen, INotifyPropertyChanged
    {
        public string SaveButtonContent { get; set; }

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

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                if(searchText != value)
                {
                    searchText = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private bool? summitReached = null;
        public bool? SummitReached
        {
            get { return summitReached; }
            set
            {
                if(summitReached != value)
                {
                    summitReached = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private int category;
        public int Category
        {
            get { return category; }
            set
            {
                if(category != value)
                {
                    category = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public ObservableCollection<Category> PossibleCategories { get; set; }

        public SearchTourViewModel ()
        {
            Tours = DataManager.getInstance().getAllTours();
            PossibleCategories = DataManager.getInstance().getAllCategories();
            PossibleCategories.Add(new Category() { Id = 0, CategoryName = "Alle" });
        }

        public void SearchTours()
        {
            Tours = DataManager.getInstance().getAllToursFiltered(SearchText, SummitReached, Category);
        }
    }
}