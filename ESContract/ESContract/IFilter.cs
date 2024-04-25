namespace ESContract
{
    public interface IFilter
    {
        /// <summary>
        /// Закончена ли работа фильтра
        /// </summary>
        bool IsDone { get; }
        /// <summary>
        /// Очередь из клеток на проверку
        /// </summary>
        Queue<int> NumbersQueue { get; set; }

        /// <summary>
        /// Обработка чисел из NumbersQueue
        /// </summary>
        void Operate();
        /// <summary>
        /// Передача следующему фильтру одно значение
        /// </summary>
        /// <param name="num">Число на передачу</param>
        void GiveNextOneNum(int num);
        /// <summary>
        /// Передача всех оставшихся чисел из очереди
        /// </summary>
        void GiveNextRemains();
    }
}

