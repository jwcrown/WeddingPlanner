using System;
using System.Collections.Generic;
using WeddingPlanner.Models;


namespace WeddingPlanner
{
    public class Wedding : BaseEntity
    {
        public int WeddingId { get; set; }
        public User Creator { get; set; }
        public int UserId { get; set; }
        public string Wedder1 { get; set; }
        public string Wedder2 { get; set; }
        public string WeddingDate { get; set; }
        public string WeddingAddress { get; set; }
        public List<RSVP> Guests { get; set; }

    }
}