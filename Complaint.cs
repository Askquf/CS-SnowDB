using System;

namespace WebApi3._0
{
    public class Complaint
    {
        public int Complaint_ID { get; set; }

        public string Address { get; set; }

        public string Text { get; set; }

        public int Resource_ID { get; set; }

        public Resource Resource { get; set; }

        public Street Street { get; set; }

        public int Street_ID { get; set; }

        public double lat_cor { get; set; }

        public double lon_cor { get; set; }
    }
}
