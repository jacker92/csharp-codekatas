﻿//houssem.dellai@ieee.org 
//+216 95 325 964 

using System;
using System.Windows;
using System.Windows.Controls;

namespace MyCalculatorv1
{
    public partial class MainWindow : Window
    {
        private readonly Calculator calculator;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button b = (Button) sender;
            tb.Text += b.Content.ToString();
        }

        private void Result_click(object sender, RoutedEventArgs e)
        {
            try
            {
              tb.Text += GetResult();
            }
            catch (Exception exc)
            {
                tb.Text = "Error!";
            }
        }

        private string GetResult()
        {
            int iOp = GetOperationIndex();

            string op = tb.Text.Substring(iOp, 1);
            double op1 = Convert.ToDouble(tb.Text.Substring(0, iOp));
            double op2 = Convert.ToDouble(tb.Text.Substring(iOp + 1, tb.Text.Length - iOp - 1));

            if (op == "+")
            {
                return "=" + (op1 + op2);
            }
            else if (op == "-")
            {
                return "=" + (op1 - op2);
            }
            else if (op == "*")
            {
                return "=" + (op1 * op2);
            }
            else
            {
                return "=" + (op1 / op2);
            }
        }

        private int GetOperationIndex()
        {
            int iOp = 0;
            if (tb.Text.Contains("+"))
            {
                iOp = tb.Text.IndexOf("+");
            }
            else if (tb.Text.Contains("-"))
            {
                iOp = tb.Text.IndexOf("-");
            }
            else if (tb.Text.Contains("*"))
            {
                iOp = tb.Text.IndexOf("*");
            }
            else if (tb.Text.Contains("/"))
            {
                iOp = tb.Text.IndexOf("/");
            }
            else
            {
                //error
            }

            return iOp;
        }

        private void Off_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            tb.Text = "";
        }

        private void R_Click(object sender, RoutedEventArgs e)
        {
            if (tb.Text.Length > 0)
            {
                tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
            }
        }
    }
}
