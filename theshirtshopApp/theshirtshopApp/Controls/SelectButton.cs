using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace theshirtshopApp.Controls
{
    public class SelectButton : StackLayout
    {
        public event EventHandler selectionChanged;


        private readonly Label _label;

        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(SelectButton));

        public static readonly BindableProperty selectedProperty = BindableProperty.Create("selected", typeof(Boolean?), typeof(SelectButton), null, BindingMode.TwoWay, propertyChanged: selectedValueChanged);

        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(String), typeof(SelectButton), null, BindingMode.TwoWay, propertyChanged: TextValueChanged);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public Boolean? selected
        {
            get => (bool?)GetValue(selectedProperty);
            set => SetValue(selectedProperty,value);

           
        }

        public String Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public SelectButton()
        {
            Orientation = StackOrientation.Horizontal;
            BackgroundColor = Color.Silver;
            _label = new Label()
            {
                VerticalOptions = LayoutOptions.Center
            };
            var tg = new TapGestureRecognizer();
            tg.Tapped += Tg_Tapped;

            _label.TextColor = Color.Black;
            _label.GestureRecognizers.Add(tg);
            Children.Add(_label);

        }
        private void Tg_Tapped(object sender, EventArgs e)
        {
            selected = !selected;
        }
        private static void selectedValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null && (Boolean)newValue) {
                ((SelectButton)bindable)._label.TextColor = Color.Orange;
                ((SelectButton)bindable).BackgroundColor   = Color.White;
            }
                
            else
            {
                ((SelectButton)bindable)._label.TextColor = Color.Gray;
                ((SelectButton)bindable).BackgroundColor = Color.Silver;
            }
            ((SelectButton)bindable).selectionChanged?.Invoke(bindable, EventArgs.Empty);
            ((SelectButton)bindable).Command?.Execute(null);
        }
        private static void TextValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
                ((SelectButton)bindable)._label.Text = newValue.ToString();
        }

    }
}
