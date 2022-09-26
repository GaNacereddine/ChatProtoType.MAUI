using ChatProtoType.MAUI.Platforms.Shared;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace ChatProtoType.MAUI.Platforms.Windows
{
    public class MAUIPointerInputView : PlatformTouchGraphicsView, IMAUIPointerInputView
    {
        public event ChatProtoType.MAUI.Platforms.Shared.PointerEventHandler PointerChanged;

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            base.OnPointerPressed(e);

            var wPoint = e.GetCurrentPoint(this).Position;
            OnPress(new PointF(wPoint._x, wPoint._y));
        }

        /// <inheritdoc />
        protected override void OnPointerMoved(PointerRoutedEventArgs e)
        {
            base.OnPointerMoved(e);
            var wPoint = e.GetCurrentPoint(this).Position;
            OnMoving(new PointF(wPoint._x, wPoint._y));
        }

        /// <inheritdoc />
        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            base.OnPointerReleased(e);
            var wPoint = e.GetCurrentPoint(this).Position;
            OnRelease(new PointF(wPoint._x, wPoint._y));
        }

        /// <inheritdoc />
        protected override void OnPointerCanceled(PointerRoutedEventArgs e)
        {
            base.OnPointerCanceled(e);
            var wPoint = e.GetCurrentPoint(this).Position;
            OnRelease(new PointF(wPoint._x, wPoint._y));
        }

        private void OnRelease(PointF point)
        {
            PointerChanged?.Invoke(this, new(point.X, point.Y, PointerState.Up));
        }

        private void OnMoving(PointF point)
        {
            PointerChanged?.Invoke(this, new(point.X, point.Y, PointerState.Move));
        }

        private void OnPress(PointF point)
        {
            PointerChanged?.Invoke(this, new(point.X, point.Y, PointerState.Down));
        }

        public void Dispose()
        {
            
        }
    }
}
