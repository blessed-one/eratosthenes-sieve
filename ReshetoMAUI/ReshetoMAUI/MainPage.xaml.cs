using ESContract;
using System.Reflection;


namespace ReshetoMAUI;


public partial class MainPage : ContentPage

{
    private Grid _grid;
    private Entry[,] _matrix;
    private int _n;
    private object _field;
    private bool _isDllLoaded = false;
    private Assembly _assembly;
    private Type[] _types;
    private Type[] _interfaces;
    private Dictionary<Type, Type> _typesDictionary = new();
    private string _realizationPath;
    private string _contractPath = @"C:\Users\1\inf-hw\eratosthenes-sieve\ReshetoMAUI\ReshetoMAUI\bin\Debug\net8.0-windows10.0.19041.0\win10-x64\ESContract.dll";
    private string _logPath = @"C:\Users\1\inf-hw\eratosthenes-sieve\ReshetoMAUI\ReshetoMAUI\log.txt";

    public MainPage()
    {
        InitializeComponent();
    }

    public void OnGridClicked(object sender, EventArgs e)
    {
        string text = NEntry.Text;
        if (int.TryParse(text, out int n))
        {
            _n = n;
            if (n > 1024)
            {
                RemainderLabel.Text = "Слишком большое n,\nвизуализации не будет";
                Layout.Children.Remove(_grid);
            }
            else
            {
                InitializeGrid(n);
                RemainderLabel.Text = "";
            }
            ResultLabel.Text = $"Простые числа до {_n}:";
            StartButton.IsEnabled = true;
        }
        else
        {
            RemainderLabel.Text = "Введите НАТУРАЛЬНОЕ ЧИСЛО";
        }
    }
    public void InitializeGrid(int n)
    {
        if (_grid != null)
        {
            Layout.Children.Remove(_grid);
        }

        int matrixSize = 0;
        while (matrixSize * matrixSize < n)
        {
            matrixSize++;
        }

        int gridSize = 700;
        _matrix = new Entry[matrixSize, matrixSize];

        _grid = new Grid
        {
            ColumnSpacing = 5,
            RowSpacing = 5,
            WidthRequest = gridSize,
            HeightRequest = gridSize,
        };

        for (int i = 0; i < matrixSize; i++)
        {
            _grid.RowDefinitions.Add(new RowDefinition());
            _grid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        int counter = 1;
        for (int i = 0; i < matrixSize; i++)
        {
            for (int j = 0; j < matrixSize; j++)
            {
                var ent = new Entry
                {
                    Placeholder = $"{counter}",
                    IsReadOnly = true,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = 30 * (5 / (float)matrixSize)
                };
                _grid.Add(ent, j, i);
                _matrix[i, j] = ent;
                counter++;
            }
        }

        Layout.Add(_grid);
        Layout.SetLayoutBounds(_grid, new Rect(20, 20, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
    }

    public async void OnLoadClicked(object sender, EventArgs e)
    {
        await PickDll();
    }
    private async Task PickDll()
    {
        var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, new[] { ".dll" } },
                });

        try
        {
            var result = await FilePicker.PickAsync(new PickOptions()
            {
                FileTypes = customFileType,
                PickerTitle = "Выберите DLL файл"
            });

            if (result != null)
            {
                _realizationPath = result.FullPath;
                await LoadDll();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", "Ошибка при загрузке сборки: " + ex.Message, "OK");
        }
    }
    private async Task LoadDll()
    {
        _assembly = Assembly.LoadFrom(_realizationPath);
        await CheckContractRealization();
    }
    private async Task CheckContractRealization()
    {
        bool isAllRight = true;

        _types = _assembly.GetTypes();
        _interfaces = Assembly.LoadFrom(_contractPath).GetTypes().Where(type => type.IsInterface).ToArray();

        /*foreach (var Interface in  _interfaces)
        {
            if (Interface.IsGenericType)
            {
                Logg(Interface.GetGenericTypeDefinition().ToString());
            }
            else
            {
                Logg(Interface.ToString());
            }
        }*/
        foreach (var interfaceType in _interfaces)
        {
            bool isImplemented = false;
            foreach (var type in _types)
            {
                foreach (var face in type.GetInterfaces())
                {
                    if (face.IsGenericType && face.GetGenericTypeDefinition() == interfaceType)
                    {
                        isImplemented = true;
                        _typesDictionary.Add(face.GetGenericTypeDefinition(), type);

                        break;
                    }
                    else if(face == interfaceType)
                    {
                        isImplemented = true;
                        _typesDictionary.Add(face, type);

                        break;
                    }
                }
                if (isImplemented) break;
            }
            
            if(!isImplemented)
            {
                isAllRight = false;
                Logg(interfaceType.ToString() +  "<------");
                break;
            }
        }

        if (isAllRight)
        {
            _isDllLoaded = true;
            LoadButton.IsEnabled = false;
            GridButton.IsEnabled = true;
        }
        else
        {
            await DisplayAlert("Ошибка", "Реализация не соответсвует контракту", "OK");
        }
    }

    public void OnStartClicked(object sender, EventArgs e)
    {
        var managerType = _typesDictionary[typeof(ISieveManager<,>)];

        MethodInfo[] methods = managerType.GetMethods();

        // Создание экземпляра основного класса
        var sieveManager = Activator.CreateInstance(managerType, new object[1] {_n});
        var linkMatrices = methods.First(method => method.Name == "LinkMatrices");
        var findPrimes = methods.First(method => method.Name == "FindPrimes");

        // Связывание Entry (ui) и соответсвующей ей клетки (realization)
        for (int i = 0; i < _matrix.GetLength(0); i++)
        {
            for (int j = 0; j < _matrix.GetLength(0); j++)
            {
                Action<ESContract.State> action = _matrix[i, j].ChangeColourByState;
                linkMatrices.Invoke(sieveManager, new object[3] { i, j, action });
            }
        }

        // Поиск простых чисел
        int[] primes = (int[])findPrimes.Invoke(sieveManager, parameters: null)!;

        ResultLabel.Text += string.Join(", ", primes);

        StartButton.IsEnabled = false;
    }

    public void Logg(string message)
    {
        File.AppendAllText(_logPath, $"{DateTime.Now}: {message}\n");
    }
}
