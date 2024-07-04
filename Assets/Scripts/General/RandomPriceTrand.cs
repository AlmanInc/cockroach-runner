using System;

namespace CockroachRunner
{
    public class RandomPriceTrand
    {
        private double _previos;
        private double _minValue;
        private double _maxValue;
        private bool _lastUp;
        private bool _isInit = true;
        private readonly Random _rand;

        public RandomPriceTrand(double minValue, double maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            _rand = new Random(Environment.TickCount);
        }

        public double Next()
        {
            if (_isInit)
            {                
                _previos = _minValue + _rand.NextDouble() * (_maxValue + _minValue * -1);
                _lastUp = _rand.Next(0, 2) == 0;
                _isInit = false;
                return _previos;
            }
            if (_lastUp)
            {                
                if (_rand.Next(0, 100) <= 60)
                {
                    _previos += Math.Round(_rand.NextDouble() * 3, 3);                    
                }
                else
                {
                    _previos -= Math.Round(_rand.NextDouble() * 3, 3);
                    _lastUp = false;
                }
                return _previos;
            }
            else
            {
                if (_rand.Next(0, 100) <= 60)
                {
                    _previos -= Math.Round(_rand.NextDouble() * 3, 3);
                    _lastUp = true;
                }
                else
                {
                    _previos += Math.Round(_rand.NextDouble() * 3, 3);
                }
                return _previos * 0.001;
            }
        }               
        public void ChangeTrand()
        {
            _lastUp = !_lastUp;
        }
    }
}
