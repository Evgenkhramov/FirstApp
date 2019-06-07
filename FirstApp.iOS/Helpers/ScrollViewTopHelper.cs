using CoreGraphics;
using FirstApp.Core;
using System;
using UIKit;

namespace FirstApp.iOS.Helpers
{
    public class ScrollViewTopHelper
    {
        private static UIView _activeView;

        public static UIView GetActiveView(UIView view)
        {
            foreach (UIView item in view.Subviews)
            {
                if (item.IsFirstResponder)
                {
                    _activeView = item;

                    break;
                }
                GetActiveView(item);
            }

            return _activeView;
        }

        public static nfloat GetScrollAmount(UIView activeview, CGRect keyBourdSize)
        {
            nfloat offset = Constants.Offset;

            if (activeview == null)
            {
                return default(int);
            }

            nfloat activeViewHeight = activeview.Frame.Height;
            UIView relativePositionView = UIApplication.SharedApplication.KeyWindow;
            CGRect relativeFrame = activeview.Superview.ConvertRectToView(activeview.Frame, relativePositionView);
            nfloat yPosition = relativeFrame.Y;

            CGRect screenSize = UIScreen.MainScreen.Bounds;

            nfloat viewYpositionScreenBottom = screenSize.Height - yPosition;

            nfloat scrollAmount = (keyBourdSize.Height + offset + activeViewHeight - viewYpositionScreenBottom);

            return scrollAmount;
        }
    }
}