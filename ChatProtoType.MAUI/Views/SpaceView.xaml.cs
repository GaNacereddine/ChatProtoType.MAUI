using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Controls;
using ChatProtoType.MAUI.Controls;
using System.Data.Common;
using System.Diagnostics;

namespace ChatProtoType.MAUI.Views;

public partial class SpaceView : ContentPage
{
    private PointerInputView _draggedObject;
    private PointerInputView _inputView;
    private AbsoluteLayout _layout;
    private Point _ellipsePosition;


    public SpaceView()
    {
        InitializeComponent();

        Loaded += SpaceView_Loaded;
    }

    private void SpaceView_Loaded(object sender, EventArgs e)
    {
        _layout = (AbsoluteLayout)this.FindByName("TestLayout");
        _inputView = (PointerInputView)this.FindByName("TestInput");
        _inputView.PointerChanged += _inputView_PointerChanged;
        var top = 20;
        var column = 0;
        for (int i = 1; i < 20; i++)
        {
            var avatar = CreateAvatar(i);
            _layout.SetLayoutBounds(avatar, new(20 + column * 100, top, 50, 50));
            _layout.Children.Add(avatar);
            column++;

            if (i % 4 == 0)
            {
                column = 0;
                top += 100;
            }

        }
    }

    private void _inputView_PointerChanged(object sender, Platforms.Shared.PointerEventArgs e)
    {
        Debug.WriteLine($"_inputView => {e}");
    }

    private PointerInputView CreateAvatar(int avatarValue)
    {
        var ellipse = new Ellipse() { WidthRequest = 50, HeightRequest = 50, Fill = Colors.Yellow,  };
        var avatar = new PointerInputView() { HeightRequest = 50, WidthRequest = 50, BackgroundColor = Colors.Yellow, Content = ellipse };
        avatar.PointerChanged += Avatar_PointerChanged;
        return avatar;
    }

    private void Avatar_PointerChanged(object sender, Platforms.Shared.PointerEventArgs e)
    {
        
        if (sender is PointerInputView avatar)
        {
            Debug.WriteLine($"Avatar => (X={avatar.X} Y={avatar.Y}),{e}");

            switch (e.State)
            {
                case Platforms.Shared.PointerState.Up:
                    _draggedObject = null;
                    break;
                case Platforms.Shared.PointerState.Down:
                    _ellipsePosition = new(e.X, e.Y);
                   
                    _draggedObject = avatar;
                    break;
                case Platforms.Shared.PointerState.Move:
                    if (_draggedObject == null)
                        return;
                    Debug.WriteLine($"_ellipsePosition => {_ellipsePosition}");

                    var x = avatar.X - _ellipsePosition.X + e.X;
                    var y = avatar.Y - _ellipsePosition.Y + e.Y;

                    _layout.SetLayoutBounds(_draggedObject, new(x, y, 50, 50));



                    break;
            }
        }

        
    }
  
}