using System;
using System.ComponentModel.DataAnnotations;

namespace SAW.DTO.Event
{
    public class UpdateEventResponse
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public double? Price { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public int? SeatingCapacity { get; set; }
        public string Description { get; set; }
    }
}