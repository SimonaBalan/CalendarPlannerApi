using CalendarPlannerApi.Entities;
using CalendarPlannerApi.Responses;

namespace CalendarPlannerApi.Responses
{
    public class GetActivitiesResponse : BaseResponse
    {
        public List<Activity> Activities { get; set; }
    }
}
