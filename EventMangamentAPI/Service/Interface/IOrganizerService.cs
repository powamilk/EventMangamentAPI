using EventMangamentAPI.ViewModel;

namespace EventMangamentAPI.Service.Interface
{
    public interface IOrganizerService
    {
        bool CreateOrganizer(CreateOrganizerVM request, out string errorMessage);
        List<OrganizerVM> GetAllOrganizers(out string errorMessage);
        OrganizerVM GetOrganizerById(int id, out string errorMessage);
        bool UpdateOrganizer(int id, UpdateOrganizerVM request, out string errorMessage);
        bool DeleteOrganizer(int id, out string errorMessage);
    }
}
