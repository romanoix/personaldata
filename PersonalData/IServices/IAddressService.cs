using PersonalData.Models;
using System.Collections.Generic;

namespace PersonalData.IServices
{
    public interface IAddressService
    {
        void DeleteAddress(int id);
        void SaveAddress(string street, string city, int zipCode, string country, int personId, int addressId = 0);
        List<AddressModel> GetAddress();
    }
}
