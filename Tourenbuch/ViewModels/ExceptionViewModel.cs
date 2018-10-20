using Caliburn.Micro;

namespace Tourenbuch
{
    class ExceptionViewModel : Screen
    {
        private string exceptionText;
        public string ExceptionText
        {
            get { return exceptionText; }
            set
            {
                if (exceptionText != value)
                {
                    exceptionText = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private string innerExceptionText;
        public string InnerExceptionText
        {
            get { return innerExceptionText; }
            set
            {
                if (innerExceptionText != value)
                {
                    innerExceptionText = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private bool exceptionVisible = true;
        public bool ExceptionVisible
        {
            get { return exceptionVisible; }
            set
            {
                if (exceptionVisible != value)
                {
                    exceptionVisible = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private ShellViewModel shellViewModel;
        public ExceptionViewModel(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
        }

        public void retry()
        {
            shellViewModel.ShowLogInPage();
        }
    }
}
