using System;
using System.Collections.Generic;
using WeddingPlanner.Models;


namespace WeddingPlanner
{
    public class RSVP : BaseEntity
    {
        public int RSVPId { get; set; }
        public User Guest { get; set; }
        public int UserId { get; set; }
        public int WeddingId { get; set; }

    }
}