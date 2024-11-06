using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IziTravel
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();


        }

        // button_Clicked handler
        void button_Clicked(object sender, EventArgs e)
        {
            //webView.Source = new UrlWebViewSource 
            //{ 
            //    Url = urlEntry.Text 
            //};
            // or
            webView.Source = urlEntry.Text;
           // webView.Reload();
        }
    }
}
