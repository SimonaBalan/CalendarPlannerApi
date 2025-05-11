using System.Text.Json.Serialization;

namespace CalendarPlannerApi.Responses
{
    public class TokenResponse: BaseResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int UserId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string FirstName { get; set; }

    }
}
