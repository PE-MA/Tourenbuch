using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Tourenbuch.Controls
{
    /// <summary>
    /// Interaktionslogik für Multiselect.xaml
    /// </summary>
    public partial class Multiselect : UserControl
    {
        private ObservableCollection<Node> _nodeList;

        public Multiselect()
        {
            InitializeComponent();
            _nodeList = new ObservableCollection<Node>();
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(Dictionary<string, object>), typeof(Multiselect), new FrameworkPropertyMetadata(null,
       new PropertyChangedCallback(Multiselect.OnItemsSourceChanged)));

        public static readonly DependencyProperty SelectedItemsProperty =
         DependencyProperty.Register("SelectedItems", typeof(Dictionary<string, object>), typeof(Multiselect), new FrameworkPropertyMetadata(null,
     new PropertyChangedCallback(Multiselect.OnSelectedItemsChanged)));

        public static readonly DependencyProperty TextProperty =
           DependencyProperty.Register("Text", typeof(string), typeof(Multiselect), new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty DefaultTextProperty =
            DependencyProperty.Register("DefaultText", typeof(string), typeof(Multiselect), new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty AllPossibleProperty =
            DependencyProperty.Register("AllPossible", typeof(bool), typeof(Multiselect), new UIPropertyMetadata(true));

        public Dictionary<string, object> ItemsSource
        {
            get { return (Dictionary<string, object>)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Multiselect control = (Multiselect)d;
            control.DisplayInControl();
        }

        public Dictionary<string, object> SelectedItems
        {
            get { return (Dictionary<string, object>)GetValue(SelectedItemsProperty); }
            set
            {
                SetValue(SelectedItemsProperty, value);
            }
        }

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Multiselect control = (Multiselect)d;
            control.SelectNodes();
            control.SetText();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string DefaultText
        {
            get { return (string)GetValue(DefaultTextProperty); }
            set { SetValue(DefaultTextProperty, value); }
        }

        public bool AllPossible
        {
            get { return (bool)GetValue(AllPossibleProperty); }
            set { SetValue(DefaultTextProperty, value); }
        }

        private void DisplayInControl()
        {
            _nodeList.Clear();
            if (AllPossible && this.ItemsSource.Count > 0)
                _nodeList.Add(new Node("All"));
            foreach (KeyValuePair<string, object> keyValue in this.ItemsSource)
            {
                Node node = new Node(keyValue.Key);
                _nodeList.Add(node);
            }
            MultiSelectCombo.ItemsSource = _nodeList;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox clickedBox = (CheckBox)sender;
            if (((string)clickedBox.Content).Equals("All"))
            {
                foreach (Node node in _nodeList)
                {
                    node.IsSelected = true;
                }
            }
            else
            {
                int _selectedCount = 0;
                foreach (Node s in _nodeList)
                {
                    if (s.IsSelected && s.Title != "All")
                        _selectedCount++;
                }
                if(AllPossible)
                {
                    if (_selectedCount == _nodeList.Count - 1)
                        _nodeList.FirstOrDefault(i => i.Title == "All").IsSelected = true;
                    else
                        _nodeList.FirstOrDefault(i => i.Title == "All").IsSelected = false;
                }
            }
            SetSelectedItems();
            SetText();
        }

        private void SetSelectedItems()
        {
            if (SelectedItems == null)
                SelectedItems = new Dictionary<string, object>();
            SelectedItems.Clear();
            foreach (Node node in _nodeList)
            {
                if (node.IsSelected && node.Title != "All")
                {
                    if (this.ItemsSource.Count > 0)
                        SelectedItems.Add(node.Title, this.ItemsSource[node.Title]);
                }
            }
        }

        private void SetText()
        {
            if (this.SelectedItems != null)
            {
                StringBuilder displayText = new StringBuilder();
                foreach (Node s in _nodeList)
                {
                    if (s.IsSelected == true && s.Title == "All")
                    {
                        displayText = new StringBuilder();
                        displayText.Append("All");
                        break;
                    }
                    else if (s.IsSelected == true && s.Title != "All")
                    {
                        displayText.Append(s.Title);
                        displayText.Append(',');
                    }
                }
                this.Text = displayText.ToString().TrimEnd(new char[] { ',' });
            }
            // set DefaultText if nothing else selected
            if (string.IsNullOrEmpty(this.Text))
            {
                this.Text = this.DefaultText;
            }
        }

        private void SelectNodes()
        {
            foreach (KeyValuePair<string, object> keyValue in SelectedItems)
            {
                Node node = _nodeList.FirstOrDefault(i => i.Title == keyValue.Key);
                if (node != null)
                    node.IsSelected = true;
            }
        }
    }
}