namespace UserSubscriptionWebApi.Models.DTOs
{
    public class UserSubscription
    {
        public string username { get; set; }
        public string email { get; set; }
        public int subscription_id { get; set; }
        public string product_name { get; set; }
        public string subscription_name { get; set; }
        public string subscription_price { get; set; }
        public int subscription_days { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public bool is_active { get; set; }

    }
}
