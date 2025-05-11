using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CalendarPlannerApi.Interfaces;
using CalendarPlannerApi.Requests;
using CalendarPlannerApi.Responses;
using CalendarPlannerApi.Entities;

namespace CalendarPlannerApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : BaseApiController
    {
        private readonly IActivityService taskService;

        public ActivityController(IActivityService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var getTasksResponse = await taskService.GetActivities(UserID);

            if (!getTasksResponse.Success)
            {
                return UnprocessableEntity(getTasksResponse);
            }
            
            var tasksResponse = getTasksResponse.Activities.ConvertAll(o => new ActivityResponse { Id = o.Id, IsCompleted = o.IsCompleted, Description = o.Description, Title = o.Title, PlannedDate = o.PlannedDate });

            return Ok(tasksResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ActivityRequest taskRequest)
        {
            var task = new Activity { 
                IsCompleted = taskRequest.IsCompleted, 
                PlannedDate = taskRequest.PlannedDate, 
                Title = taskRequest.Title, 
                Description = taskRequest.Description,
                UserId = UserID 
            };

            var saveTaskResponse = await taskService.SaveActivity(task);

            if (!saveTaskResponse.Success)
            {
                return UnprocessableEntity(saveTaskResponse);
            }

            var taskResponse = new ActivityResponse { 
                        Id = saveTaskResponse.Activity.Id, 
                        IsCompleted = saveTaskResponse.Activity.IsCompleted, 
                        Title = saveTaskResponse.Activity.Title, 
                        Description = saveTaskResponse.Activity.Description,
                        PlannedDate = saveTaskResponse.Activity.PlannedDate };
            
            return Ok(taskResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest(new DeleteActivityResponse { Success = false, ErrorCode = "D01", Error = "Invalid Activity id" });
            }
            var deleteTaskResponse = await taskService.DeleteActivity(id, UserID);
            if (!deleteTaskResponse.Success)
            {
                return UnprocessableEntity(deleteTaskResponse);
            }

            return Ok(deleteTaskResponse.ActivityId);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ActivityRequest taskRequest)
        {
            var task = new Activity { 
                Id = taskRequest.Id, 
                IsCompleted = taskRequest.IsCompleted, 
                PlannedDate = taskRequest.PlannedDate, 
                Title = taskRequest.Title, 
                Description = taskRequest.Description,
                UserId = UserID 
            };

            var saveTaskResponse = await taskService.SaveActivity(task);

            if (!saveTaskResponse.Success)
            {
                return UnprocessableEntity(saveTaskResponse);
            }

            var taskResponse = new ActivityResponse { 
                        Id = saveTaskResponse.Activity.Id, 
                        IsCompleted = saveTaskResponse.Activity.IsCompleted, 
                        Title = saveTaskResponse.Activity.Title,
                        Description = saveTaskResponse.Activity.Description,
                        PlannedDate = saveTaskResponse.Activity.PlannedDate 
                    };

            return Ok(taskResponse);
        }
    }
}
