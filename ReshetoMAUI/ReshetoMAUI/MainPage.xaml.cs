using Microsoft.Maui.Platform;
using System.Xml.Linq;

namespace ReshetoMAUI
{
    public partial class MainPage : ContentPage
    {
        private Grid _grid;
        private Entry[,] _matrix;
        public MainPage()
        {
            InitializeComponent();
        }
        public void InitializeGrid(int n, int m)
        {
            if (_grid != null)
            {
                Layout.Children.Remove(_grid);
            }
            float gridCoef = (float)m / n;
            int gridSize = 500;
            _matrix = new Entry[n, m];

            _grid = new Grid
            { 
                ColumnSpacing = 5,
                RowSpacing = 5,
                WidthRequest = gridSize,
                HeightRequest = gridSize * gridCoef,
            };

/*            for (int i = 0; i < n; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }*/

            int counter = 1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    var ent = new Entry
                    {
                        Placeholder = $"{counter}",
                        IsReadOnly = true,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                    };
                    _grid.Add(ent, j, i);
                    _matrix[i, j] = ent;
                    counter++;
                }

            }

            Layout.Children.Add(_grid);
        } 

        public void OnButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            string text = NEntry.Text;
            if (int.TryParse(text, out int n))
            {
                InitializeGrid(n, n);
                Remainder.Text = "";
            }
            else
            {
                Remainder.Text = "Введите НАТУРАЛЬНОЕ ЧИСЛО";
            }
        }
    }

}
