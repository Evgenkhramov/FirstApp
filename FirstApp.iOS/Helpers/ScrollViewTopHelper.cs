using System;
using CoreGraphics;
using UIKit;

namespace FirstApp.iOS.Helpers
{
    public class ScrollViewTopHelper
    {
        public static UIView GetActiveView(UIView view)
        {
            UIView activeview = null;
            GetView(view);

            void GetView(UIView viewItem)
            {
                foreach (UIView item in viewItem.Subviews)
                {
                    if (item.IsFirstResponder)
                    {
                        activeview = item;
                        return;
                    }
                    else
                    {
                        GetView(item);
                    }
                }
            }

            return activeview;
        }

        public static nfloat GetScrollAmount(UIView activeview, CGRect keyBourdSize)
        {
            nfloat offset = 10.0f;

            if (activeview != null)
            {
                nfloat activeViewHeight = activeview.Frame.Height;
                UIView relativePositionView = UIApplication.SharedApplication.KeyWindow;
                CGRect relativeFrame = activeview.Superview.ConvertRectToView(activeview.Frame, relativePositionView);
                nfloat yPosition = relativeFrame.Y;

                CGRect screenSize = UIScreen.MainScreen.Bounds;

                nfloat viewYpositionScreenBottom = screenSize.Height - yPosition;

                nfloat scrollAmount = (keyBourdSize.Height + offset + activeViewHeight - viewYpositionScreenBottom);

                return scrollAmount;
            }

            return 0;
        }
    }
}