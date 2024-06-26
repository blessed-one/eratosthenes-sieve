﻿using ESContract;
using Microsoft.UI.Xaml.Media.Animation;
using System.Diagnostics;
using System.Reflection;


namespace ReshetoMAUI;


public partial class MainPage : ContentPage

{
    private Grid _grid;
    private Entry[,] _matrix;
    private int _n;
    private bool isGridCreated = false;
    private Assembly _assembly;
    private Type[] _types;
    private Type[] _interfaces;
    private Dictionary<Type, Type> _typesDictionary = new();
    private string _realizationPath;
    private string _contractPath = @"C:\Users\1\inf-hw\eratosthenes-sieve\ReshetoMAUI\ReshetoMAUI\bin\Debug\net8.0-windows10.0.19041.0\win10-x64\ESContract.dll";
    private int _themeIndex = 0;
    private string[] _themes = {
        nameof(ReshetoMAUI.Resources.Themes.Default),
        nameof(ReshetoMAUI.Resources.Themes.Nature),
        nameof(ReshetoMAUI.Resources.Themes.Fire)
    };

    public MainPage()
    {
        InitializeComponent();
    }

    public void OnGridClicked(object sender, EventArgs e)
    {
        string text = NEntry.Text;
        if (int.TryParse(text, out int n) && n != 0)
        {
            _n = n;
            if (n > 1024)
            {
                RemainderLabel.Text = "Слишком большое n,\nвизуализации не будет";
                isGridCreated = false;
                Layout.Children.Remove(_grid);
            }
            else
            {
                InitializeGrid(n);
                isGridCreated = true;
                RemainderLabel.Text = "";
            }
            ResultLabel.Text = $"Простые числа до {_n}: ";
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


        int counter = 1;
        for (int i = 0; i < matrixSize; i++)
        {
            for (int j = 0; j < matrixSize; j++)
            {
                var ent = new Entry
                {
                    Placeholder = $"{counter}",
                    FontSize = 30 * (5 / (float)matrixSize),
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

        bool isAllright = _interfaces.All(face =>
        {
            bool isImplemented = false;

            foreach (var type in _types)
            {
                if (type.GetInterfaces().Contains(face))
                {
                    _typesDictionary[face] = type;
                    isImplemented = true;
                    break;
                }
            }

            return isImplemented;
        });

        if (isAllRight)
        {
            LoadButton.IsEnabled = false;
            GridButton.IsEnabled = true;
        }
        else
        {
            await DisplayAlert("Ошибка", "Реализация не соответсвует контракту", "OK");
        }
    }

    public async void OnStartClicked(object sender, EventArgs e)
    {
        var managerType = _typesDictionary[typeof(ISieveManager)];

        MethodInfo[] methods = managerType.GetMethods();

        // Создание экземпляра основного класса
        var sieveManager = Activator.CreateInstance(managerType);
        var findPrimes = methods.First(method => method.Name == "FindPrimes");
        var getSteps = methods.First(method => method.Name == "GetSteps");

        // Поиск простых чисел
        int[] primes = await Task<int[]>.Run(() => (int[])findPrimes.Invoke(sieveManager, parameters: new object[1] { _n })!);
        //int[] primes = (int[])findPrimes.Invoke(sieveManager, parameters: new object[1] { _n })!;

        // Визуализация поиска
        if (isGridCreated)
        {
            (int Number, ESContract.State State)[] steps = ((int, ESContract.State)[])getSteps.Invoke(sieveManager, parameters: null)!;

            int matrixSize = _matrix.GetLength(0);
            foreach (var step in steps)
            {
                int n = step.Number - 1;

                int y = n % matrixSize;
                int x = n / matrixSize;

                Color colour = step.State switch
                {
                    State.Good => new Color(0, 255, 0),
                    State.Bad => new Color(255, 0, 0),
                    _ => new Color(100, 100, 100),
                };

                await Task.Run(() =>
                {
                    Dispatcher.DispatchAsync(() => _matrix[x, y].BackgroundColor = colour);
                });
            }
        }

        // Вывод ответа
        ResultLabel.Text += string.Join(", ", primes);

        StartButton.IsEnabled = false;
    }

    public async void OnThemeClicked(object sender, EventArgs e)
    {
        _themeIndex++;
        await Dispatcher.DispatchAsync(async () =>
        {
            await ThemeManager.SetTheme(_themes[_themeIndex % _themes.Length]);
        });
    }
}
