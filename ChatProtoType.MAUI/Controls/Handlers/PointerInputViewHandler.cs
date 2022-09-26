#nullable enable
#if __IOS__ || MACCATALYST
using MauiPlatformView = Microsoft.Maui.Platform.ContentView;
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
using System.Diagnostics;
using ChatProtoType.MAUI.Platforms.Shared;


#nullable enable

namespace ChatProtoType.MAUI.Handlers
{

#if WINDOWS
    public partial class PointerInputViewHandler 
    {
        protected void HookInputEvents(PlatformView platformView)
        {
            platformView.PointerPressed += PlatformView_PointerPressed;
            platformView.PointerMoved += PlatformView_PointerMoved;
            platformView.PointerReleased += PlatformView_PointerReleased;
        }

        protected void UnHookInputEvents(PlatformView platformView)
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
            PointerInputVirtualView.RaisePointerChanged(new(wPoint.X, wPoint.Y, PointerState.Up));
        }

        private void PlatformView_PointerReleased(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var wPoint = e.GetCurrentPoint(sender as UIElement).Position;
            PointerInputVirtualView.RaisePointerChanged(new(wPoint.X, wPoint.Y, PointerState.Down));
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

            /*if ((touchX > PlatformView.GetX() + PlatformView.Width) || (touchX < PlatformView.GetX()))
            {
                return;
            }

            if ((touchY > PlatformView.GetY() + PlatformView.Height) || (touchY < PlatformView.GetY()))
            {
                return;
            }*/

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
    public partial class PointerInputViewHandler 
    {
        protected void HookInputEvents(PlatformView platformView)
        {
            //platformView.AddObserver() += PlatformView_Touch;
        }

        protected void UnHookInputEvents(PlatformView platformView)
        {
            //platformView.Touch -= PlatformView_Touch;
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
