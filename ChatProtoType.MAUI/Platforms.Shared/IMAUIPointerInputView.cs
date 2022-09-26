using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace ChatProtoType.MAUI.Platforms.Shared
{
    public enum PointerState
    { 
        Up,
        Down,
        Move,
    }

    public class PointerEventArgs
    {
        public double X;
        public double Y;
        public PointerState State;

        public PointerEventArgs(double x, double y, PointerState state)
        {
            X = x;
            Y = y;
            State = state;
        }

        public override string ToString()
        {
            return $"PointerEventArgs => State={State} X={X}, Y={Y}";
        }
    }

    public delegate void PointerEventHandler(object sender, PointerEventArgs e);

    public interface IPointerInputEvents
    {
        event PointerEventHandler? PointerChanged;
    }

    public interface IMAUIPointerInputView : IPointerInputEvents, IDisposable
    {

    }
}
