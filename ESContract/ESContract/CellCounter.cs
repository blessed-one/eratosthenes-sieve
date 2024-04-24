namespace ESContract
{
    public class CellCounter
    {
        private readonly Field _field;
        private readonly int _startNumber;
        private int _currentNumber;
        public Cell CurrentCell { get; private set; }

        public CellCounter(int startingNum, Field field)
        {
            _field = field;
            _startNumber = startingNum;
            _currentNumber = startingNum;

            CurrentCell = _field.GetCell(startingNum);
            CurrentCell.UpdateState(State.Good);
        }

        /// <summary>
        /// Попытка перехода к следующей клетке и обновление её состояния.
        /// </summary>
        /// <returns>
        /// Возвращает true если шаг был совершен успешно и false в обратном случае
        /// </returns>
        public bool TryStep()
        {
            if (_currentNumber + _startNumber > _field.CellsCount)
            {
                return false;
            }

            _currentNumber += _startNumber;
            CurrentCell = _field.GetCell(_currentNumber);
            State newState = (_currentNumber % _startNumber == 0)
                ? State.Bad
                : State.Good;

            CurrentCell.UpdateState(newState);

            return true;
        }
    }
}

