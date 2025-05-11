using CalendarPlannerApi.Entities;
using CalendarPlannerApi.Responses;

namespace CalendarPlannerApi.Interfaces
{
    public interface IActivityService
    {
        Task<GetActivitiesResponse> GetActivities(int userId);

        Task<SaveActivityResponse> SaveActivity(Activity task);

        Task<DeleteActivityResponse> DeleteActivity(int activityId, int userId);
    }
}
