using Caliburn.Micro;
using System.ComponentModel;
using Tourenbuch.Model;
using TourenbuchDatentypen;

namespace Tourenbuch
{
    internal class NewBookViewModel : Screen
    {
        public string SaveButtonContent { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private TourBook currentBook;
        public TourBook CurrentBook
        {
            get { return currentBook; }
            set
            {
                if (currentBook != value)
                {
                    currentBook = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public void createNewBook()
        {
            TourBook createdBook = DataManager.getInstance().saveBook(CurrentBook);
            if (createdBook != null)
            {
                CurrentBook = createdBook;
                SaveButtonContent = "Speichern erfolgreich durchgeführt";
                NotifyOfPropertyChange(() => SaveButtonContent);

            }
        }

        public NewBookViewModel(TourBook currentBook)
        {
            CurrentBook = currentBook;
            SaveButtonContent = "Speichern";
            CanSave = true;
        }

        public NewBookViewModel() : this(new TourBook())
        { }

        private bool _canSave;
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
    }
}