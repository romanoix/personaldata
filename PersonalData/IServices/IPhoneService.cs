using PersonalData.Models;
using System.Collections.Generic;

namespace PersonalData.IServices
{
    public interface IPhoneService
    {
        void DeletePhone(int id);
        void SavePhone(int number, int personId, int phoneId = 0);
        List<PhoneModel> GetPhone();
    }
}
