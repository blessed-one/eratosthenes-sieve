namespace ESContract
{
    public class Field
    {
        private (int X, int Y) _size;

        public int CellsCount { get; init; }
        public Cell[,] CellField { get; set; }
        public Field(int cellsCount)
        {
            CellsCount = cellsCount;
            _size = CalculateSize();
            CellField = new Cell[_size.X, _size.Y]; ;
        }

        /// <summary>
        /// Вычисляет размер поля, наиболее близкий к квадрату по соотношению сторон. X >= Y
        /// </summary>
        /// <returns>
        /// Пара значений X и Y
        /// </returns>
        private (int X, int Y) CalculateSize()
        {
            int x = CellsCount, y = 1;

            for (int i = 1; i < (int)Math.Pow(CellsCount, 0.5d) + 1; i++)
            {
                if (CellsCount % i == 0)
                {
                    x = CellsCount % i;
                    y = i;
                }
            }

            return (x, y);
        }

        /// <summary>
        /// Возвращает клетку с номером number
        /// </summary>
        /// <param name="number">
        /// Номер искомой клетки
        /// </param>
        /// <returns>
        /// Искомая клетка
        /// </returns>
        public Cell GetCell(int number)
        {
            int n = number - 1;
            int x, y;

            x = n / _size.X;
            y = n % _size.X;

            return CellField[x, y];
        }
    }
}

