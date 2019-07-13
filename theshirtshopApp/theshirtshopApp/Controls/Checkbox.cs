using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace theshirtshopApp.Controls
{
    public class Checkbox : StackLayout, INotifyPropertyChanged
    {
        public event EventHandler CheckedChanged;

       
        private readonly Label _label;

        //CHANGE THESE 2 STRINGS ACCORDING TO YOUR NAMESPACE AND IMAGE


        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(Checkbox));

        public static readonly BindableProperty CheckedProperty = BindableProperty.Create("Checked", typeof(Boolean?), typeof(Checkbox), null, BindingMode.TwoWay, propertyChanged: CheckedValueChanged);

        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(String), typeof(Checkbox), null, BindingMode.TwoWay, propertyChanged: TextValueChanged);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public Boolean? Checked
        {
            get => (bool?)GetValue(CheckedProperty);
            set { 

            SetValue(CheckedProperty, value);
              
            } 
        }

        public String Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Checkbox()
        {
            Orientation = StackOrientation.Horizontal;
            BackgroundColor = Color.Transparent;

           
            var tg = new TapGestureRecognizer();
            tg.Tapped += Tg_Tapped;



            _label = new Label()
            {
                VerticalOptions = LayoutOptions.Center
            };
            _label.TextColor = Color.Black;
            //_label.GestureRecognizers.Add(tg);
            Children.Add(_label);
            this.GestureRecognizers.Add(tg);

        }

        private void Tg_Tapped(object sender, EventArgs e)
        {
            Checked = !Checked;
        }

        private static void CheckedValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null && (Boolean)newValue) {
                ((Checkbox)bindable)._label.TextColor = Color.Orange;
                ((Checkbox)bindable).BackgroundColor = Color.White;

            }

            else {
                ((Checkbox)bindable)._label.TextColor = Color.Black;
                ((Checkbox)bindable).BackgroundColor = Color.Silver;
            }
                
            ((Checkbox)bindable).CheckedChanged?.Invoke(bindable, EventArgs.Empty);
            ((Checkbox)bindable).Command?.Execute(null);
        }



        private static void TextValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
                ((Checkbox)bindable)._label.Text = newValue.ToString();
        }
    }
}