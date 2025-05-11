using CalendarPlannerApi.Entities;
using CalendarPlannerApi.Responses;

namespace CalendarPlannerApi.Responses
{
    public class SaveActivityResponse : BaseResponse
    {
        public Activity Activity { get; set; }
    }
}
