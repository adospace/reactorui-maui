using IntegrationTest.Components;
using MauiReactor;

namespace IntegrationTest
{
    public partial class MainPage : Microsoft.Maui.Controls.ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync<ChildPage>();
        }
    }
}