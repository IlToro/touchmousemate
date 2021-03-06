using System;

namespace Lextm.TouchMouseMate
{
    internal struct TouchZone
    {
        private TouchPoint _current;
        private TouchPoint _previous;
        private TouchPoint _previousPrevious;
        private int _time;
        internal double Movement;

        public void Consume(int x, int y, int pixel)
        {
            _current.Consume(x, y, pixel);
        }

        public void AppendTime(int mDwTimeDelta)
        {
            _time += mDwTimeDelta;
        }

        public void ComputeCenter()
        {
            if (_current.Pixel > 0)
            {
                _current.ComputeCenter();
                if (_previous.Pixel > 0)
                {
                    var dX = _current.CenterX - _previous.CenterX;
                    var dY = _current.CenterY - _previous.CenterY;
                    Movement += Math.Sqrt(dX*dX + dY*dY);
                }
            }
        }

        public bool KeyUpDetected
        {
            get { return _current.Pixel == 0 && _previous.Pixel > 0; }
        }

        public bool KeyDownDetected
        {
            get { return _previous.Pixel > 0 && _previousPrevious.Pixel == 0; }
        }

        public bool MoveDetected
        {
            get { return Movement > NativeMethods.Section.MoveThreshold; }
        }

        public void Prepare()
        {
            _previousPrevious = _previous;
            _previous = _current;
            _current.Reset();
        }

        public void Reset()
        {
            _time = 0;
            Movement = 0F;
        }
    }
}