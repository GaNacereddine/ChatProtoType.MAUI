#nullable enable

using ChatProtoType.MAUI.Platforms.Shared;
using System.Diagnostics;

namespace ChatProtoType.MAUI.Controls
{

    public interface IPointerInputViewHandler
    {
    }

    public interface IPointerInputView : IView, IPointerInputEvents
    {
        void RaisePointerChanged(PointerEventArgs e);
    }

    public class PointerInputView : Border, IPointerInputView
    {
        public event PointerEventHandler? PointerChanged;

        public virtual void RaisePointerChanged(PointerEventArgs e)
        {
            PointerChanged?.Invoke(this, e);
        }
    }
}
