﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

namespace CalendarPlannerApi.Entities
{
    public partial class RefreshToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TokenHash { get; set; }
        public string TokenSalt { get; set; }
        public DateTime Ts { get; set; }
        public DateTime ExpiryDate { get; set; }
        public virtual User User { get; set; }
    }
}