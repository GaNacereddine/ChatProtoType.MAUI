using ChatProtoType.MAUI.Platforms.Shared;
using Foundation;
using Microsoft.Maui.Graphics.Platform;
using UIKit;

#nullable enable

namespace ChatProtoType.MAUI.Platforms.MacCatalyst
{
    public class MAUIPointerInputView : UIView, IMAUIPointerInputView
    {
        public event PointerEventHandler? PointerChanged;

        public override void TouchesBegan(NSSet touches, UIEvent? evt)
        {
            base.TouchesBegan(touches, evt);

            var touch = (UITouch)touches.AnyObject;
            OnPress(touch.PreviousLocationInView(this).AsPointF());
        }

        /// <inheritdoc />
        public override void TouchesMoved(NSSet touches, UIEvent? evt)
        {
            base.TouchesMoved(touches, evt);
            var touch = (UITouch)touches.AnyObject;
            var currentPoint = touch.LocationInView(this);
            OnMoving(currentPoint.AsPointF());
        }

        /// <inheritdoc />
        public override void TouchesEnded(NSSet touches, UIEvent? evt)
        {
            base.TouchesEnded(touches, evt);
            var touch = (UITouch)touches.AnyObject;
            var currentPoint = touch.LocationInView(this);
            OnRelease(currentPoint.AsPointF());
        }

        /// <inheritdoc />
        public override void TouchesCancelled(NSSet touches, UIEvent? evt)
        {
            base.TouchesCancelled(touches, evt);
            var touch = (UITouch)touches.AnyObject;
            var currentPoint = touch.LocationInView(this);
            OnRelease(currentPoint.AsPointF());
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
