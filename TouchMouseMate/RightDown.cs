using System;

namespace Lextm.TouchMouseMate
{
    public class RightDown : IMouseState
    {
        public void Process(MouseEventFlags flag, StateMachine machine)
        {
            if (flag == MouseEventFlags.RightUp)
            {
                Console.WriteLine("right down->idle");
                machine.Idle();
                if (NativeMethods.Section.TouchOverClick)
                {
                    NativeMethods.MouseEvent(NativeMethods.Section.LeftHandMode ? MouseEventFlags.LeftUp : MouseEventFlags.RightUp);
                }
            }
        }
    }
}