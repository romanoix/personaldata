using PersonalData.Models;
using System.Collections.Generic;

namespace PersonalData.IServices
{
    public interface IPersonService
    {
        void DeletePerson(int id);
        void SavePerson(string name, string surname, int id = 0);
        List<PersonModel> GetPerson();
    }
}
