using CalendarPlannerApi.Entities;
using CalendarPlannerApi.Requests;
using CalendarPlannerApi.Responses;

namespace CalendarPlannerApi.Interfaces
{
    public interface ITokenService
    {
        Task<Tuple<string, string>> GenerateTokensAsync(int userId);
        Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
        Task<bool> RemoveRefreshTokenAsync(User user);
    }
}
