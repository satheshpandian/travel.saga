using System;
using System.ComponentModel.DataAnnotations;

namespace Travel.Saga.Models
{
    public class Basket
    {
        [Key]
        public Guid TransactionNumber { get; set; }
        public int NumberOfPassengers { get; set; }
        public int Domain { get; set; }
        public int AgentId { get; set; }
        public string ReferrerUrl { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string UserId { get; set; }
        public string SelectedCurrency { get; set; }
        public string ReservationSystem { get; set; }       

    }
}
