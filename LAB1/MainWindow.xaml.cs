using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LAB1
{
    public partial class MainWindow : Window
    {

        private enum shifr { Railway, Column, Sieve, Cezar };


        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            SetOutText("");
            MakeShifr(true);
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            SetOutText("");
            MakeShifr(false);
        }

        private void MakeShifr(bool isEncrypt)
        {

            if (!CorrectInput())
            {
                SetErrorText(true);
            }
            else
            {
                SetErrorText(false);
                string someText = GetInText();
                string key = GetKey();
                string encryptText = "";
                switch (GetShifrType())
                {
                    case shifr.Railway:
                        RailwayShifr railway = new RailwayShifr();
                        if (isEncrypt)
                            encryptText = railway.Code(someText, key);
                        else
                            encryptText = railway.Decode(someText, key);
                        break;
                    case shifr.Column:
                        ColumnShifr column = new ColumnShifr();
                        if (isEncrypt)
                            encryptText = column.Code(someText, key);
                        else
                            encryptText = column.Decode(someText, key);
                        break;
                    case shifr.Sieve:
                        SieveShifr sieve = new SieveShifr();
                        string maskKey = GetMask();
                        if (isEncrypt)
                            encryptText = sieve.Code(someText, maskKey);
                        else
                            encryptText = sieve.Decode(someText, maskKey);
                        break;
                    case shifr.Cezar:
                        CezarShifr cezar = new CezarShifr();
                        if (isEncrypt)
                            encryptText = cezar.Code(someText, key);
                        else
                            encryptText = cezar.Decode(someText, key);
                        break;
                }

                SetOutText(encryptText);
            }
        }

        private bool CorrectInput()
        {
            if (GetInText() == "")
                return false;
            string key = GetKey();
            shifr shifrType = GetShifrType();
            if (key == "" && shifrType != shifr.Sieve)
                return false;
            switch (shifrType)
            {
                case shifr.Cezar:
                    try
                    {
                        Int32.Parse(key);
                    }
                    catch
                    {
                        return false;
                    }
                    break;

                case shifr.Column:
                    foreach (char symb in key)
                    {
                        if (!Char.IsLetter(symb))
                            return false;
                    }
                    break;

                case shifr.Railway:
                    try
                    {
                        int numKey = Int32.Parse(key);
                        if (numKey <= 0)
                            return false;
                    }
                    catch
                    {
                        return false;
                    }
                    break;
                case shifr.Sieve:
                    break;
            }
            return true;
        }

        private string GetMask()
        {
            int n = 0, m = 0;
            string mask = "";
            int[,] matrix = new int[5, 5];

            foreach (FrameworkElement bufButton in wrapPanel.Children)
            {
                if (bufButton is RadioButton)
                {
                    RadioButton radioButton = (RadioButton)bufButton;
                    if ((bool)radioButton.IsChecked)
                    {
                        matrix[n, m] = 1;
                    }
                    else
                    {
                        matrix[n, m] = 0;
                    }
                    if (m < 4)
                        m++;
                    else
                    {
                        m = 0;
                        n++;
                    }
                }
            }

            for (n = 0; n < 5; n++)
                for (m = 0; m < 5; m++)
                    if (matrix[n, m] == 1)
                        mask += "1";
                    else
                        mask += "0";


            return mask;
        }

        private void SetErrorText(bool isVisible)
        {
            if (isVisible)
                lblErrorText.Visibility = Visibility.Visible;
            else
                lblErrorText.Visibility = Visibility.Hidden;
        }

        private void SetOutText(string text)
        {
            txtBoxOutText.Text = text;
        }
        private string GetInText()
        {
            return txtBoxInText.Text;
        }
        private string GetKey()
        {
            return txtBoxKey.Text;
        }
        private shifr GetShifrType()
        {
            switch (cmbBoxShifr.SelectedIndex)
            {
                case 0:
                    return shifr.Railway;
                case 1:
                    return shifr.Column;
                case 2:
                    return shifr.Sieve;
                case 3:
                    return shifr.Cezar;
                default:
                    return 0;
            }
        }

        private void cmbBoxShifr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetOutText("");
            if (lblErrorText != null)
                SetErrorText(false);
            txtBoxKey.Text = "";
            if (cmbBoxShifr.SelectedIndex == 2)
            {
                wrapPanel.Visibility = Visibility.Visible;
                lblSieve.Visibility = Visibility.Visible;
                lblKey.IsEnabled = false;
                txtBoxKey.IsEnabled = false;
            }
            else if (wrapPanel != null)
            {
                wrapPanel.Visibility = Visibility.Hidden;
                lblSieve.Visibility = Visibility.Hidden;
                lblKey.IsEnabled = true;
                txtBoxKey.IsEnabled = true;
            }
        }

    }
}
