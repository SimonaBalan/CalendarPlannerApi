﻿namespace CalendarPlannerApi.Requests
{
    public class ActivityRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime PlannedDate { get; set; }
    }
}
