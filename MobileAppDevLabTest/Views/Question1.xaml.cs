namespace MobileAppDevLabTest.Views;

public partial class Question1 : ContentPage
{
	public Question1()
	{
		InitializeComponent();
	}

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        var sliderValue = (int)e.NewValue; // Cast to int if you want whole numbers
        labelValue.Text = $"{sliderValue}";
        var slider = sender as Slider;
        if (slider != null)
        {
            if (slider.Value >= 0 && slider.Value <= 39)
            {
                labelResult.Text = "Failed";
                labelResult.TextColor = Colors.Red;
            }
            else if (slider.Value >= 40 && slider.Value <= 79)
            {
                labelResult.Text = "Passed";
                labelResult.TextColor = Colors.Black;
            }
            else if (slider.Value >= 80 && slider.Value <= 100)
            {
                labelResult.Text = "Excellent";
                labelResult.TextColor = Colors.Green;
            }
        }
    }

}