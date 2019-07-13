﻿using Xamarin.Forms;

namespace theshirtshopApp.Controls
{
    public class CurvedCornersLabel:Label
    {
        public static readonly BindableProperty CurvedCornerRadiusProperty =
    BindableProperty.Create(
        nameof(CurvedCornerRadius),
        typeof(double),
        typeof(CurvedCornersLabel),
        12.0);
        public double CurvedCornerRadius
        {
            get { return (double)GetValue(CurvedCornerRadiusProperty); }
            set { SetValue(CurvedCornerRadiusProperty, value); }
        }


        public static readonly BindableProperty CurvedBackgroundColorProperty =
            BindableProperty.Create(
                nameof(CurvedBackgroundColor),
                typeof(Color),
                typeof(CurvedCornersLabel),
                Color.Default);
        public Color CurvedBackgroundColor
        {
            get { return (Color)GetValue(CurvedBackgroundColorProperty); }
            set { SetValue(CurvedBackgroundColorProperty, value); }
        }
    }
}
