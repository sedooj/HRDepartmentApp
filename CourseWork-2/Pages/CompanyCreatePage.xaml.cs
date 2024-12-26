using CourseWork_2.Util;
using CourseWork_2.ViewControllers;

namespace CourseWork_2.Pages;

public partial class CompanyCreatePage
{
    private readonly CompanyCreatePageViewController _controller = new();

    public CompanyCreatePage()
    {
        InitializeComponent();
        NameEntry.TextChanged += OnNameEntryTextChanged;
        AddressEntry.TextChanged += OnAddressEntryTextChanged;
        PhoneEntry.TextChanged += OnPhoneEntryTextChanged;
    }

    private async void OnCreateCompanyClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text;
        string address = AddressEntry.Text;
        string phone = PhoneEntry.Text;

        if (await _controller.CreateCompany(name, address, phone))
        {
            await Navigation.PopAsync();
        }
    }

    private void OnNameEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        EntryUtil.ChangeEntryColor(NameEntry, _controller.ValidateCompanyName(e.NewTextValue));
    }

    private void OnAddressEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        EntryUtil.ChangeEntryColor(AddressEntry, _controller.ValidateCompanyAddress(e.NewTextValue));
    }

    private void OnPhoneEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        EntryUtil.ChangeEntryColor(PhoneEntry, _controller.ValidateCompanyPhone(e.NewTextValue));
    }
}