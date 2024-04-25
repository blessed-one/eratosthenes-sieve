﻿using ESContract;

namespace ESContract
{
    public class SieveFilter
    {
        private Field _field;

        /// <summary>
        /// Следующий фильтр в цепочке
        /// </summary>
        public SieveFilter NextFilter { get; set; }
        /// <summary>
        /// Число, кратность которому проверяет фильтр
        /// </summary>
        public int MainNumber { get; set; }
        /// <summary>
        /// Закончена ли работа фильтра
        /// </summary>
        public bool IsDone { get; set; }
        /// <summary>
        /// Очередь чисел на проверку
        /// </summary>
        public Queue<int> NumbersQueue { get; set; }


        public SieveFilter(Field field)
        {
            _field = field;
            IsDone = false;
            NumbersQueue = new Queue<int>();
        }

        /// <summary>
        /// Обработка чисел из NumbersQueue
        /// </summary>
        public void Operate()
        {
            int number;

            while (!IsDone)
            {
                if (NumbersQueue.TryDequeue(out number))
                {
                    if (number == -1)
                    {
                        IsDone = true;
                        continue;
                    }

                    if (MainNumber == 0)
                    {
                        _field.GetCell(number).UpdateState(State.Good);
                        MainNumber = number;
                    }
                    else if (number % MainNumber != 0)
                    {
                        _field.GetCell(number).UpdateState(State.Good);
                        NextFilter.NumbersQueue.Enqueue(number);
                    }
                    else
                    {
                        _field.GetCell(number).UpdateState(State.Bad);
                    }
                }
            }
        }

        /// <summary>
        /// Передача следующему фильтру одно значение
        /// </summary>
        /// <param name="num">Число на передачу</param>
        public void GiveNextOneNum(int num)
        {
            NextFilter.NumbersQueue.Enqueue(num);
        }

        /// <summary>
        /// Возвращает основное число фильтра и числа из очереди на проверку
        /// </summary>
        /// <returns>
        /// Список чисел
        /// </returns>
        public List<int> GetNumbers()
        {
            var result = NumbersQueue.ToList();
            result.Add(MainNumber);

            return result;
        }

        /// <summary>
        /// Добавляет в очередь несколько чисел
        /// </summary>
        /// <param name="numbers">
        /// Числа на добавление
        /// </param>
        public void AddNumbers(IEnumerable<int> numbers)
        {
            foreach (var number in numbers)
            {
                NumbersQueue.Enqueue(number);
            }
        }
    }
}