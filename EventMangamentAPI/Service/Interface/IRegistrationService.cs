using EventMangamentAPI.ViewModel;

namespace EventMangamentAPI.Service.Interface
{
    public interface IRegistrationService
    {
        bool CreateRegistration(CreateRegistrationVM request, out string errorMessage);
        List<RegistrationVM> GetAllRegistrations(out string errorMessage);
        RegistrationVM GetRegistrationById(int id, out string errorMessage);
        bool UpdateRegistration(int id, UpdateRegistrationVM request, out string errorMessage);
        bool DeleteRegistration(int id, out string errorMessage);
    }
}
