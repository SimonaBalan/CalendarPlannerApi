using CalendarPlannerApi.Responses;
using System.Text.Json.Serialization;

namespace CalendarPlannerApi.Responses
{
    public class DeleteActivityResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int ActivityId { get; set; }
    }
}
