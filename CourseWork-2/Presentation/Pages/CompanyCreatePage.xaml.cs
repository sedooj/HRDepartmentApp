using CourseWork_2.Data.ViewControllers;
using CourseWork_2.Data.ViewModels;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Presentation.Pages;

public partial class CompanyCreatePage
{
    private readonly CompanyCreatePageViewModel _model = new();

    public CompanyCreatePage()
    {
        InitializeComponent();
        NameEntry.TextChanged += OnNameEntryTextChanged;
        AddressEntry.TextChanged += OnAddressEntryTextChanged;
        PhoneEntry.TextChanged += OnPhoneEntryTextChanged;
    }

    private void OnCreateCompanyClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text;
        string address = AddressEntry.Text;
        string phone = PhoneEntry.Text;

        _model.CreateCompany(name, address, phone);
        Navigation.PopAsync();
    }

    private void OnNameEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        EntryUtil.ChangeEntryColor(NameEntry, _model.ValidateCompanyName(e.NewTextValue));
    }

    private void OnAddressEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        EntryUtil.ChangeEntryColor(AddressEntry, _model.ValidateCompanyAddress(e.NewTextValue));
    }

    private void OnPhoneEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        EntryUtil.ChangeEntryColor(PhoneEntry, _model.ValidateCompanyPhone(e.NewTextValue));
    }
}