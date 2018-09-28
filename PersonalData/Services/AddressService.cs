using PersonalData.Entities;
using PersonalData.IServices;
using PersonalData.Models;
using System.Collections.Generic;

namespace PersonalData.Services
{
    public class AddressService : IAddressService
    {
        public void DeleteAddress(int id)
        {
            AppFacade.DataAccess.InTransaction(s =>
            {
                var AddressCreate = s.CreateCriteria(typeof(Address))
                  .List<Address>();

                foreach (var adr in AddressCreate)
                {
                    if (adr.Id == id)
                        s.Delete(adr);
                }
            });
        }

        public void SaveAddress(string street, string city, int zipCode, string country, int personId, int addressId = 0)
        {
            Address address;

            AppFacade.DataAccess.InTransaction(s =>
            {
                if (addressId != 0)
                {
                    address = new Address
                    {
                        Id = addressId,
                        Street = street,
                        City = city,
                        ZipCode = zipCode,
                        Country = country,

                        Person = new Person { Id = personId }
                    };
                }
                else
                {
                    address = new Address
                    {
                        Street = street,
                        City = city,
                        ZipCode = zipCode,
                        Country = country,

                        Person = new Person { Id = personId }
                    };
                }
                s.SaveOrUpdate(address);
            });
        }
        public List<AddressModel> GetAddress()
        {
            var addressModelList = new List<AddressModel>();
            AppFacade.DataAccess.InTransaction(s =>
            {
                var AddressCreate = s.CreateCriteria(typeof(Address))
                  .List<Address>();

                foreach (var adr in AddressCreate)
                {
                    var temp = new AddressModel()
                    {
                        Id = adr.Id,
                        Street = adr.Street,
                        City = adr.City,
                        ZipCode = adr.ZipCode,
                        Country = adr.Country,
                        PersonModel = new PersonModel
                        {
                            Id = adr.Person.Id,
                        }
                    };
                    addressModelList.Add(temp);
                }
            });
            return addressModelList;
        }
    }
}
