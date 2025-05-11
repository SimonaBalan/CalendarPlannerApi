using Microsoft.EntityFrameworkCore;
using CalendarPlannerApi.Interfaces;
using CalendarPlannerApi.Responses;
using CalendarPlannerApi.Entities;

namespace CalendarPlannerApi.Services
{
    public class ActivityService : IActivityService
    {
        private readonly PlannerDbContext tasksDbContext;

        public ActivityService(PlannerDbContext tasksDbContext)
        {
            this.tasksDbContext = tasksDbContext;
        }

        public async Task<DeleteActivityResponse> DeleteActivity(int taskId, int userId)
        {
            var task = await tasksDbContext.Activities.FindAsync(taskId);

            if (task == null)
            {
                return new DeleteActivityResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }

            if (task.UserId != userId)
            {
                return new DeleteActivityResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }

            tasksDbContext.Activities.Remove(task);

            var saveResponse = await tasksDbContext.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new DeleteActivityResponse
                {
                    Success = true,
                    ActivityId = task.Id
                };
            }

            return new DeleteActivityResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<GetActivitiesResponse> GetActivities(int userId)
        {
            var tasks = await tasksDbContext.Activities.Where(o => o.UserId == userId).ToListAsync();

            return new GetActivitiesResponse { Success = true, Activities = tasks };

        }

        public async Task<SaveActivityResponse> SaveActivity(Activity task)
        {
            if (task.Id == 0)
            {
                await tasksDbContext.Activities.AddAsync(task);
            }
            else
            {
                var taskRecord = await tasksDbContext.Activities.FindAsync(task.Id);
                if (taskRecord != null)
                {
                    taskRecord.Description = task.Description;
                    taskRecord.IsCompleted = task.IsCompleted;
                    taskRecord.PlannedDate = task.PlannedDate;
                    taskRecord.Title = task.Title;
                }                
            }
            
            var saveResponse = await tasksDbContext.SaveChangesAsync();
            
            if (saveResponse >= 0)
            {
                return new SaveActivityResponse
                {
                    Success = true,
                    Activity = task
                };
            }
            return new SaveActivityResponse
            {
                Success = false,
                Error = "Unable to save task",
                ErrorCode = "T05"
            };
        }
    }
}
