using EventMangamentAPI.ViewModel;

namespace EventMangamentAPI.Service.Interface
{
    public interface IParticipantService
    {
        bool CreateParticipant(CreateParticipantVM request, out string errorMessage);
        List<ParticipantVM> GetAllParticipants(out string errorMessage);
        ParticipantVM GetParticipantById(int id, out string errorMessage);
        bool UpdateParticipant(int id, UpdateParticipantVM request, out string errorMessage);
        bool DeleteParticipant(int id, out string errorMessage);
    }
}
