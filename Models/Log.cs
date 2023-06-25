using System.ComponentModel.DataAnnotations;

namespace UserSubscriptionWebApi.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string MachineName { get; set; }
        public string Logged { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Callsite { get; set; }
        public string Exception { get; set; }


    }
}
