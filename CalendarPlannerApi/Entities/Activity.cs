namespace CalendarPlannerApi.Entities
{
    public partial class Activity
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;

        public int UserId { get; set; }

        public string Description { get; set; } = default!;

        public DateTime PlannedDate { get; set; }  

        public bool IsCompleted { get; set; }

        // Link to the user who created it
        public virtual User User { get; set; }
    }
}
