#nullable enable
#if __IOS__ || MACCATALYST
using MauiPlatformView = Microsoft.Maui.Platform.ContentView;
using UIKit;
using Foundation;
using Microsoft.Maui.Graphics.Platform;
#elif __ANDROID__
using MauiPlatformView = Microsoft.Maui.Platform.ContentViewGroup;
using Android.Views;
#elif WINDOWS
using MauiPlatformView = Microsoft.Maui.Platform.ContentPanel;
using Microsoft.UI.Xaml;
#elif (NETSTANDARD || !PLATFORM)
using MauiPlatformView = System.Object;
#endif

using Microsoft.Maui.Handlers;
using ChatProtoType.MAUI.Controls;
using ChatProtoType.MAUI.Platforms.Shared;


#nullable enable

namespace ChatProtoType.MAUI.Handlers
{

#if WINDOWS
    public partial class PointerInputViewHandler 
    {
        protected void HookInputEvents(MauiPlatformView platformView)
        {
            platformView.PointerPressed += PlatformView_PointerPressed;
            platformView.PointerMoved += PlatformView_PointerMoved;
            platformView.PointerReleased += PlatformView_PointerReleased;
        }

        protected void UnHookInputEvents(MauiPlatformView platformView)
        {
            platformView.PointerPressed -= PlatformView_PointerPressed;
            platformView.PointerMoved -= PlatformView_PointerMoved;
            platformView.PointerReleased -= PlatformView_PointerReleased;
        }

        private void PlatformView_PointerMoved(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var wPoint = e.GetCurrentPoint(sender as UIElement).Position;
            PointerInputVirtualView.RaisePointerChanged(new(wPoint.X, wPoint.Y, PointerState.Move));
        }

        private void PlatformView_PointerPressed(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var wPoint = e.GetCurrentPoint(sender as UIElement).Position;
            PointerInputVirtualView.RaisePointerChanged(new(wPoint.X, wPoint.Y, PointerState.Down));
        }

        private void PlatformView_PointerReleased(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var wPoint = e.GetCurrentPoint(sender as UIElement).Position;
            PointerInputVirtualView.RaisePointerChanged(new(wPoint.X, wPoint.Y, PointerState.Up));
        }
    }
#endif

#if ANDROID
    public partial class PointerInputViewHandler 
    {
        protected void HookInputEvents(MauiPlatformView platformView)
        {
            platformView.Touch += PlatformView_Touch;
        }

        protected void UnHookInputEvents(MauiPlatformView platformView)
        {
            platformView.Touch -= PlatformView_Touch;
        }

        private void PlatformView_Touch(object? sender, Android.Views.View.TouchEventArgs e)
        {
            e.Handled = true;

            if (e.Event is null)
            {
                return;
            }

            var touchX = e.Event.GetX();
            var touchY = e.Event.GetY();

            var point = new PointF(touchX / (float)DeviceDisplay.MainDisplayInfo.Density, touchY / (float)DeviceDisplay.MainDisplayInfo.Density);

            switch (e.Event.Action)
            {
                case MotionEventActions.Down:
                    PointerInputVirtualView.RaisePointerChanged(new(point.X, point.Y, PointerState.Down));
                    break;
                case MotionEventActions.Move:
                    PointerInputVirtualView.RaisePointerChanged(new(point.X, point.Y, PointerState.Move));
                    break;
                case MotionEventActions.Up:
                    PointerInputVirtualView.RaisePointerChanged(new(point.X, point.Y, PointerState.Up));
                    break;
                case MotionEventActions.Cancel:
                    PointerInputVirtualView.RaisePointerChanged(new(point.X, point.Y, PointerState.Up));
                    break;
            }
        }

    }
#endif

#if IOS || MACCATALYST

    public class InputContentView : MauiPlatformView, IMAUIPointerInputView
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

    public partial class PointerInputViewHandler 
    {
        protected override MauiPlatformView CreatePlatformView()
        {
            return new InputContentView();
        }

        protected void HookInputEvents(MauiPlatformView platformView)
        {
            ((InputContentView)platformView).PointerChanged += PointerInputViewHandler_PointerChanged;
        }

        private void PointerInputViewHandler_PointerChanged(object sender, PointerEventArgs e)
        {
            PointerInputVirtualView.RaisePointerChanged(e);
        }

        protected void UnHookInputEvents(MauiPlatformView platformView)
        {
            ((InputContentView)platformView).PointerChanged -= PointerInputViewHandler_PointerChanged;
        }

    }
#endif

    public partial class PointerInputViewHandler
    { 
    
    }

#if ANDROID || IOS || MACCATALYST || WINDOWS

    public partial class PointerInputViewHandler : BorderHandler
    {

        IPointerInputView PointerInputVirtualView => (IPointerInputView)VirtualView;

        protected override void ConnectHandler(MauiPlatformView platformView)
        {
            base.ConnectHandler(platformView);

            // Control setup here
            HookInputEvents(platformView);
        }

        protected override void DisconnectHandler(MauiPlatformView platformView)
        {
            UnHookInputEvents(platformView);
            base.DisconnectHandler(platformView);
        }
    }

#endif

}
