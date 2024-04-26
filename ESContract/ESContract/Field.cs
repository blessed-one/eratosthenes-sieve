using System;

namespace ESContract
{
    public class Field
    {
        private int _size;

        public int CellsCount { get; init; }
        public Cell[,] CellField { get; set; }
        public Field(int cellsCount)
        {
            CellsCount = cellsCount;
            _size = CalculateSize();
            CellField = new Cell[_size, _size]; 
            for(int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    CellField[i, j] = new Cell();
                }
            }
        }

        /// <summary>
        /// Вычисляет оптимальный размер квадратного поля (X на X)
        /// </summary>
        /// <returns>
        /// Размер поля X
        /// </returns>
        private int CalculateSize()
        {
            int i = 0;
            while (i * i < CellsCount)
            {
                i++;
            }

            return i;
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

            x = n / _size;
            y = n % _size;

            Console.Write(" ");
            return CellField[x, y];
        }
    }
}

