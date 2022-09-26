using Android.Content;
using Android.Views;
using ChatProtoType.MAUI.Platforms.Shared;

#nullable enable

namespace ChatProtoType.MAUI.Platforms.Android
{
    public class MAUIPointerInputView : Microsoft.Maui.Platform.ContentViewGroup, IMAUIPointerInputView
    {
        public MAUIPointerInputView(Context context) : base(context)
        {
         
        }

        public event PointerEventHandler? PointerChanged;

        public override bool OnTouchEvent(MotionEvent? e)
        {
            base.OnTouchEvent(e);
            ArgumentNullException.ThrowIfNull(e);

            var touchX = e.GetX();
            var touchY = e.GetY();
            var point = new PointF(touchX / (float)DeviceDisplay.MainDisplayInfo.Density, touchY / (float)DeviceDisplay.MainDisplayInfo.Density);

            switch (e.Action)
            {
                case MotionEventActions.Down:
                    Parent?.RequestDisallowInterceptTouchEvent(true);
                    OnPress(point);
                    break;

                case MotionEventActions.Move:
                    OnMoving(point);
                    break;

                case MotionEventActions.Up:
                    Parent?.RequestDisallowInterceptTouchEvent(false);
                    OnRelease(point);
                    break;
                case MotionEventActions.Cancel:
                    Parent?.RequestDisallowInterceptTouchEvent(false);
                    OnRelease(point);
                    break;

                default:
                    return false;
            }

            return true;
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
    }
}
