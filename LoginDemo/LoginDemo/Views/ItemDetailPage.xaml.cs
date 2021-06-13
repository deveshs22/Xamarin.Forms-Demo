using LoginDemo.ViewModels;

using System.ComponentModel;

using Xamarin.Forms;

namespace LoginDemo.Views
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