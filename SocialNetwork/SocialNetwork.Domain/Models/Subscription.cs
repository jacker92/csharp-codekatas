﻿namespace SocialNetwork.Domain.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        public User Subscriber { get; set; }
        public User Subscribed { get; set; }
        public int SubscribedId { get; set; }
        public int SubscriberId { get; set; }
    }
}