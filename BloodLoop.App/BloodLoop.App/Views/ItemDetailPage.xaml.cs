using BloodLoop.App.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace BloodLoop.App.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}