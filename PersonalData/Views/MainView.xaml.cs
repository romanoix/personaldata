using PersonalData.IServices;
using PersonalData.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PersonalData
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private readonly IPersonService _personService;
        private readonly IAddressService _addressService;
        private readonly IPhoneService _phoneService;
        private readonly IDeleteService _deleteService;

        string personName;
        string personSurmane;
        string addressStreet;
        string addressCity;
        string addressZipCode;
        string addressCountry;
        string phoneNumber;

        private int Id_Person { get; set; }
        private int Id_Address { get; set; }
        private int Id_Phone { get; set; }
        private int Place_Id_Building { get; set; }
        private int Place_Id { get; set; }

        public MainView(IAddressService addressService, IPersonService personService, IPhoneService phoneService, IDeleteService deleteService)
        {
            _addressService = addressService;
            _personService = personService;
            _phoneService = phoneService;
            _deleteService = deleteService;

            InitializeComponent();
            PersonComboBoxList();
        }

        private void PersonComboBoxList() //Column 1
        {
            IPersonService _personService = new PersonService();
            PersonComboBox.Items.Clear();
            var Per = _personService.GetPerson().Select(x => x.Name);

            foreach (var per in Per)
            {
                PersonComboBox.Items.Add(per);
            }
        }
        private void AddressComboBoxList() //Column 2
        {
            AddressComboBox.Items.Clear();
            var Adr = _addressService.GetAddress().Where(x => x.PersonModel.Id == Id_Person).Select(x => x.Street);
            foreach (var adr in Adr)
            {
                AddressComboBox.Items.Add(adr);
            }
        }
        private void PhoneComboBoxList() //Column 3
        {
            PhoneComboBox.Items.Clear();
            var Pho = _phoneService.GetPhone().Where(x => x.PersonModel.Id == Id_Person).Select(x => x.Number);
            foreach (var pho in Pho)
            {
                PhoneComboBox.Items.Add(pho);
            }
        }

        //Column 1. Select person 
        private void PersonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PersonComboBox.SelectedItem != null)
            {
                Id_Person = _personService.GetPerson()
                    .Where(x => (x.Name == PersonComboBox.SelectedItem.ToString()))
                    .Select(x => x.Id).FirstOrDefault();

                personName = PersonSetName.Text = _personService.GetPerson()
                        .Where(x => (x.Name == PersonComboBox.SelectedItem.ToString()))
                        .Select(x => x.Name).FirstOrDefault().ToString();

                personSurmane = PersonSetSurname.Text = _personService.GetPerson()
                   .Where(x => (x.Name == PersonComboBox.SelectedItem.ToString()))
                   .Select(x => x.Surname).FirstOrDefault().ToString();

                AddressComboBoxList();
                PhoneComboBoxList();

                AddresstSetStreet.Clear();
                AddressSetCity.Clear();
                AddressSetZipCode.Clear();
                AddressSetCountry.Clear();
                PhoneSetNumber.Clear();
            }
        }
        //Column 2. Select address from selected person 
        private void AddressComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AddressComboBox.SelectedItem != null)
            {
                Id_Address = _addressService.GetAddress()
                    .Where(x => (x.Street == AddressComboBox.SelectedItem.ToString()))
                    .Select(x => x.Id).FirstOrDefault();

                addressStreet = AddresstSetStreet.Text = _addressService.GetAddress()
                   .Where(x => (x.Street == AddressComboBox.SelectedItem.ToString()))
                   .Select(x => x.Street).FirstOrDefault().ToString();

                addressCity = AddressSetCity.Text = _addressService.GetAddress()
                   .Where(x => (x.Street == AddressComboBox.SelectedItem.ToString()))
                   .Select(x => x.City).FirstOrDefault().ToString();

                addressZipCode = AddressSetZipCode.Text = _addressService.GetAddress()
                   .Where(x => (x.Street == AddressComboBox.SelectedItem.ToString()))
                   .Select(x => x.ZipCode).FirstOrDefault().ToString();

                addressCountry = AddressSetCountry.Text = _addressService.GetAddress()
                   .Where(x => (x.Street == AddressComboBox.SelectedItem.ToString()))
                   .Select(x => x.Country).FirstOrDefault().ToString();

                PhoneComboBoxList();

                PhoneSetNumber.Clear();
            }
        }
        //Column 3. Select phone from selected person
        private void PhoneComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PhoneComboBox.SelectedItem != null)
            {
                Id_Phone = _phoneService.GetPhone()
                    .Where(x => (x.Number == int.Parse(PhoneComboBox.SelectedItem.ToString())))
                    .Select(x => x.Id).FirstOrDefault();

                phoneNumber = PhoneSetNumber.Text = _phoneService.GetPhone()
                   .Where(x => (x.Number == int.Parse(PhoneComboBox.SelectedItem.ToString())))
                   .Select(x => x.Number).FirstOrDefault().ToString();
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string _personName = PersonSetName.Text;
            string _personSurname = PersonSetSurname.Text;
            string _addressStreet = AddresstSetStreet.Text;
            string _addressCity = AddressSetCity.Text;
            int _addressZipCode;
            bool _addressZipCodeBool = Int32.TryParse(AddressSetZipCode.Text, out _addressZipCode);
            string _addressCountry = AddressSetCountry.Text;
            int _phoneNumber;
            bool _phoneNumberBool = Int32.TryParse(PhoneSetNumber.Text, out _phoneNumber);

            if (_personName == "" || _addressStreet == "")
            {
                MessageBox.Show("Wymagane jest podanie przynajmniej imienia i ulicy wprowadzanej osoby");
                return;
            }
            if (!_addressZipCodeBool || !_phoneNumberBool)
            {
                MessageBox.Show("Kod pocztowy oraz nr telefonu należy wprowadzić przy pomocy cyfr nie używając znaków specjalnychc oraz spacji");
                return;
            }

            if (_personName == personName && _personSurname == personSurmane)
            {
                if (_addressStreet == addressStreet && (_addressCity != addressCity || _addressZipCode != int.Parse(addressZipCode) || _addressCountry != addressCountry))
                {
                    _addressService.SaveAddress(_addressStreet, _addressCity, _addressZipCode, _addressCountry, Id_Person, Id_Address);
                }
                if(_addressStreet != addressStreet)
                {
                    _addressService.SaveAddress(_addressStreet, _addressCity, _addressZipCode, _addressCountry, Id_Person);
                }

                if (_phoneNumber != int.Parse(phoneNumber))
                {
                    _phoneService.SavePhone(_phoneNumber, Id_Person);
                }
            }
            else
            {
                _personService.SavePerson(_personName, _personSurname);
                Id_Person = _personService.GetPerson().Select(x => x.Id).LastOrDefault();
                _addressService.SaveAddress(_addressStreet, _addressCity, _addressZipCode, _addressCountry, Id_Person);
                _phoneService.SavePhone(_phoneNumber, Id_Person);
            }

            PersonComboBoxList();
            AddressComboBoxList();
            PhoneComboBoxList();

            PersonSetName.Clear();
            PersonSetSurname.Clear();
            AddresstSetStreet.Clear();
            AddressSetCity.Clear();
            AddressSetZipCode.Clear();
            AddressSetCountry.Clear();
            PhoneSetNumber.Clear();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Id_Person != 0)
            {
                _deleteService.DeletePersonData(Id_Person);
            }
            PersonComboBoxList();
            AddressComboBoxList();
            PhoneComboBoxList();

            PersonSetName.Clear();
            PersonSetSurname.Clear();
            AddresstSetStreet.Clear();
            AddressSetCity.Clear();
            AddressSetZipCode.Clear();
            AddressSetCountry.Clear();
            PhoneSetNumber.Clear();
        }
        private void AddressSetCity_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
