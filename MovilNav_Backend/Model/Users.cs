using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovilNav_Backend.Model
{
    public partial class ApiResponse
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Passaword { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public string ProfileUser { get; set; } = null!;
        public bool? AccountStatus { get; set; }
    }
}
