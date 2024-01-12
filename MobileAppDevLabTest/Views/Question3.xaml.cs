using MobileAppDevLabTest.ViewModel;
namespace MobileAppDevLabTest.Views;

public partial class Question3 : ContentPage
{
	public Question3()
	{
		InitializeComponent();
        BindingContext = new PostViewModel();
    }
}