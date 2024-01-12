using Microsoft.Maui.Controls;
using System.Text.RegularExpressions;

namespace MobileAppDevLabTest.Views;

public partial class Question2 : ContentPage
{
    public Question2()
    {
        InitializeComponent();

        if (PhoneEntry != null)
            PhoneEntry.TextChanged += OnPhoneEntryTextChanged;
        if (PasswordEntry != null)
            PasswordEntry.TextChanged += OnPasswordEntryTextChanged;
        if (TermsAndConditionsCheckbox != null)
            TermsAndConditionsCheckbox.CheckedChanged += OnTermsAndConditionsCheckboxChanged;
        if (RegisterButton != null)
            RegisterButton.Clicked += OnRegisterButtonClicked;
    }

    private void OnPhoneEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender == null || e == null) return;

        if (InvalidPhoneNumberLabel != null)
        {
            bool isValid = IsPhoneNumberValid(e.NewTextValue);
            InvalidPhoneNumberLabel.IsVisible = !isValid;
            UpdateRegisterButtonState();
        }
    }

    private bool IsPhoneNumberValid(string phoneNumber)
    {
        return !string.IsNullOrEmpty(phoneNumber) && phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit);
    }

    private void OnPasswordEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (InvalidPasswordLabel != null)
        {
            bool isValid = e.NewTextValue.Length >= 6;
            InvalidPasswordLabel.IsVisible = !isValid;
            UpdateRegisterButtonState();
        }
    }

    private void OnTermsAndConditionsCheckboxChanged(object sender, CheckedChangedEventArgs e)
    {
        UpdateRegisterButtonState();
    }

    private void OnTermsAndConditionsLabelTapped(object sender, EventArgs e)
    {
        if (TermsAndConditionsCheckbox != null)
            TermsAndConditionsCheckbox.IsChecked = !TermsAndConditionsCheckbox.IsChecked;
    }

    private void UpdateRegisterButtonState()
    {
        if (PhoneEntry == null || PasswordEntry == null || TermsAndConditionsCheckbox == null || RegisterButton == null)
            return;

        bool isPhoneValid = IsPhoneNumberValid(PhoneEntry.Text);
        RegisterButton.IsEnabled = isPhoneValid
                                   && (PasswordEntry.Text?.Length >= 6)
                                   && TermsAndConditionsCheckbox.IsChecked;
    }

    private void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        DisplayAlert("Registration", "Registration successful!", "OK");
    }
}