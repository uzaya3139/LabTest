using MobileAppDevLabTest.Views;

namespace MobileAppDevLabTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Question3();
        }
    }
}