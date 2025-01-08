using CourseWork_2.Data.Controllers;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Presentation.Pages;

public partial class CompanyCreatePage
{
    private readonly CompanyCreatePageController _controller = new();

    public CompanyCreatePage()
    {
        InitializeComponent();
        Init();
    }

    private void Init()
    {
        NameEntry.TextChanged += OnNameEntryTextChanged;
        AddressEntry.TextChanged += OnAddressEntryTextChanged;
        PhoneEntry.TextChanged += OnPhoneEntryTextChanged;
    }

    private void OnCreateCompanyClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text;
        string address = AddressEntry.Text;
        string phone = PhoneEntry.Text;

        _controller.CreateCompany(name, address, phone);
        Navigation.PopAsync();
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