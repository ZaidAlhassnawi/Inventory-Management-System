using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InventoryManagementSystem_PresentaionLayer.Global
{
    public static class InputBehaviors
    {
        public static readonly DependencyProperty IsNumericProperty =
            DependencyProperty.RegisterAttached("IsNumeric", typeof(bool), typeof(InputBehaviors), new PropertyMetadata(false, OnIsNumericChanged));

        public static bool GetIsNumeric(DependencyObject obj) => (bool)obj.GetValue(IsNumericProperty);
        public static void SetIsNumeric(DependencyObject obj, bool value) => obj.SetValue(IsNumericProperty, value);

        private static void OnIsNumericChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                if ((bool)e.NewValue)
                {
                    textBox.PreviewTextInput += BlockNonDigitCharacters;
                    DataObject.AddPastingHandler(textBox, OnPaste);
                }
                else
                {
                    textBox.PreviewTextInput -= BlockNonDigitCharacters;
                    DataObject.RemovePastingHandler(textBox, OnPaste);
                }
            }
        }

        private static readonly Regex _regexOnlyNumbers = new Regex("[^0-9.]");
        private static void BlockNonDigitCharacters(object sender, TextCompositionEventArgs e)
        {

            bool isInvalidChar = _regexOnlyNumbers.IsMatch(e.Text);

            if (!isInvalidChar && e.Text == ".")
            {
                if (((TextBox)sender).Text.Contains("."))
                {
                    isInvalidChar = true;
                }
            }

            e.Handled = isInvalidChar;
        }

        private static void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));

                if (new Regex("[^0-9.]+").IsMatch(text))
                {
                    e.CancelCommand();
                    return;
                }
           
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
