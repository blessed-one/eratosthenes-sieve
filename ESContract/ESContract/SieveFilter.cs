using ESContract;

namespace ESContract
{
    public class SieveFilter
    {
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
        bool IsDone { get; set; }

        /// <summary>
        /// Очередь чисел на проверку
        /// </summary>
        Queue<int> NumbersQueue { get; set; }

        public SieveFilter()
        {
            IsDone = false;
            NumbersQueue = new Queue<int>();
        }

        /// <summary>
        /// Обработка чисел из NumbersQueue
        /// </summary>
        void Operate()
        {
            int number;

            while (!IsDone)
            {
                if (NumbersQueue.TryDequeue(out number))
                {
                    if (MainNumber == 0)
                    {
                        MainNumber = number;
                        continue;
                    }

                    if (number % MainNumber != 0)
                    {
                        NextFilter.NumbersQueue.Enqueue(number);
                    }

                    if (number == -1)
                    {
                        IsDone = true;
                    }
                }
            }
        }

        /// <summary>
        /// Передача следующему фильтру одно значение
        /// </summary>
        /// <param name="num">Число на передачу</param>
        void GiveNextOneNum(int num)
        {
            NextFilter.NumbersQueue.Enqueue(num);
        }

        public List<int> GetNumbers()
        {
            var result = NumbersQueue.ToList();
            result.Add(MainNumber);

            return result;
        }
    }
}