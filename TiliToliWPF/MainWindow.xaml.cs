using System.Diagnostics.Metrics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TiliToliWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Elemek létrehozása
        Grid DynamicGrid = new Grid();
        Button emptybutton = new(); 
        private Random rnd = new Random();
        private int lepesSzamlalo = 0;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Keveres()
        {
            List<Button> gombok = DynamicGrid.Children.OfType<Button>().ToList();

            for (int i = gombok.Count - 1; i > 0; i--)
            {
                int swapIndex = rnd.Next(i + 1);
                Button temp = gombok[i];
                gombok[i] = gombok[swapIndex];
                gombok[swapIndex] = temp;
            }

            foreach (var btn in gombok)
            {
                Grid.SetColumn(btn, gombok.IndexOf(btn) % CBKivalasztottitem());
                Grid.SetRow(btn, gombok.IndexOf(btn) / CBKivalasztottitem());
            }
        }
        public int CBKivalasztottitem() 
        {
            if (cbLehetosegek.SelectedItem == cbitem3x3)
            {
                return 3;
            }
            if (cbLehetosegek.SelectedItem == cbitem4x4)
            {
                return 4;
            }
            if (cbLehetosegek.SelectedItem == cbitem5x5)
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }

        public void GridLetrehozasa() 
        {

            if (Gridalap.Children.Contains(DynamicGrid))
            {
                Gridalap.Children.Remove(DynamicGrid);
                DynamicGrid.Children.Remove(emptybutton);
                DynamicGrid.Children.Clear();
                DynamicGrid.ColumnDefinitions.Clear();
                DynamicGrid.RowDefinitions.Clear();
            }
            DynamicGrid.Width = 400;
            DynamicGrid.Background = new SolidColorBrush(Colors.Beige);
            Grid.SetRow(DynamicGrid, 1);
            Gridalap.Children.Add(DynamicGrid);
            for (int i = 0; i < CBKivalasztottitem(); i++)
            {
                ColumnDefinition Oszlopok = new();
                DynamicGrid.ColumnDefinitions.Add(Oszlopok);
                RowDefinition Sorok = new();
                DynamicGrid.RowDefinitions.Add(Sorok);
            }
        }
        public void KiRajzol() 
        {
            int gridMerete = CBKivalasztottitem();
            int btnContent = 1;

            for (int i = 0; i < gridMerete; i++)
            {
                for (int j = 0; j < gridMerete; j++)
                {
                    if (i == gridMerete - 1 && j == gridMerete - 1)
                    {
                        emptybutton.Content = "";
                        emptybutton.Click += Button_Click;
                        Grid.SetColumn(emptybutton, j);
                        Grid.SetRow(emptybutton, i);
                        DynamicGrid.Children.Add(emptybutton);
                        continue;
                    }
                    Button btnelemek = new Button
                    {
                        Content = (btnContent++).ToString()
                    };
                    btnelemek.Click += Button_Click;

                    Grid.SetColumn(btnelemek, j);
                    Grid.SetRow(btnelemek, i);
                    DynamicGrid.Children.Add(btnelemek);
                }
            } 
        }
        private bool JatekEllenorzes()
        {
            return true;
        }
        public void btnJatekKevero_Click(object sender, RoutedEventArgs e)
        {
            lepesSzamlalo = 0;
            GridLetrehozasa();
            KiRajzol();
            Keveres();
        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int clickedRow = Grid.GetRow(clickedButton);
            int clickedColumn = Grid.GetColumn(clickedButton);

            int emptyRow = Grid.GetRow(emptybutton);
            int emptyColumn = Grid.GetColumn(emptybutton);

            if ((Math.Abs(clickedRow - emptyRow) == 1 && clickedColumn == emptyColumn) ||
                (Math.Abs(clickedColumn - emptyColumn) == 1 && clickedRow == emptyRow))
            {
                Grid.SetRow(clickedButton, emptyRow);
                Grid.SetColumn(clickedButton, emptyColumn);
                Grid.SetRow(emptybutton, clickedRow);
                Grid.SetColumn(emptybutton, clickedColumn);
                lepesSzamlalo++;
                lblLepesSzamlalo.Content = $"Lépések száma: {lepesSzamlalo}";
            }

            /*
            if (JatekEllenorzes() == true)
            {
                MessageBox.Show($"Gratulálunk kiraktad a Tili-Toli játékot!");
            }
            */
        }
    }
}