﻿using Infrastructure.Enums;

namespace HousingService.Offers.Models
{
    public class OfferDTO
    {
        public int Id { get; set; }
        public int HousingId { get; set; }
        public OfferStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double PricePerDay { get; set; }
    }
}