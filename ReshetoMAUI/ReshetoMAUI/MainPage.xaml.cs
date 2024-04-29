using ESContract;
using Microsoft.VisualBasic.FileIO;
using System.Reflection;


namespace ReshetoMAUI;


public partial class MainPage : ContentPage

{
    private Grid _grid;
    private Entry[,] _matrix;
    private bool dllLoaded = false;
    private bool gridCreated = false;
    private int _n;
    private object _field;
    private Type[] _types;
    private Assembly _assembly;
    private string _realizationPath;
    private Type[] _commonIterfaces = { typeof(ICell), typeof(IFilter) };
    private Type[] _genericInterfaces = { typeof(IField<>), typeof(ISieveManager<,>) };
    private Type[] _interfaces = { typeof(ICell), typeof(IFilter), typeof(IField<>), typeof(ISieveManager<,>) };
    private string _contractPath = @"C:\Users\1\inf-hw\eratosthenes-sieve\ReshetoMAUI\ReshetoMAUI\bin\Debug\net8.0-windows10.0.19041.0\win10-x64\ESContract.dll";
    string _logPath = @"C:\Users\1\inf-hw\eratosthenes-sieve\ReshetoMAUI\ReshetoMAUI\log.txt";

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
            ResultLabel.Text = "Простые числа до n:";
            gridCreated = true;
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

        foreach (var interfaceType in _interfaces)
        {
            if (interfaceType.IsGenericType)
            {
                isAllRight = _types.Any(type => type.GetInterfaces().Any(face => face.IsGenericType && face.GetGenericTypeDefinition() == interfaceType));
            }

            else
            {
                isAllRight = _types.Any(type => type.GetInterfaces().Any(face => face == interfaceType));
            }
        }

        if (isAllRight)
        {
            dllLoaded = true;
            LoadButton.IsEnabled = false;
            GridButton.IsEnabled = true;
        }
    }

    public void OnStartClicked(object sender, EventArgs e)
    {
        Type sieveManagerType = _types.First(type => type.IsGenericType 
                                          && type.GetGenericTypeDefinition() == typeof(ISieveManager<,>));
        var sieveManager = Activator.CreateInstance(sieveManagerType);
        MethodInfo[] methods = typeof(ISieveManager<,>).GetMethods();

        var linkMatrices = methods.First(method => method.Name == "LinkMatrices");

        foreach (MethodInfo method in methods)
        {
            // Выводим информацию о методе
            File.WriteAllText(_logPath, $"Метод: {method.Name}\n");
            File.WriteAllText(_logPath, $"Возвращаемый тип: {method.ReturnType}\n");

            // Получаем параметры метода
            ParameterInfo[] parameters = method.GetParameters();
            File.WriteAllText(_logPath, "Параметры:\n");

            // Выводим информацию о параметрах метода
            foreach (ParameterInfo parameter in parameters)
            {
                File.WriteAllText(_logPath, $"Тип: {parameter.ParameterType}, Имя: {parameter.Name}\n");
            }

            File.WriteAllText(_logPath, "\n");
        }


        StartButton.IsEnabled = false;
    }
}
