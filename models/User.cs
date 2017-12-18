using System;
using System.Collections.Generic;
using WeddingPlanner.Models;


namespace WeddingPlanner
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<RSVP> RSVPs { get; set; }

    }
}