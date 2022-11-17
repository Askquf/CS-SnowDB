using System;

namespace WebApi3._0
{
    public class Street
    {
        public int Street_ID { get; set; }

        public int Number_Of_Complaints { get; set; }

        public string Name { get; set; }

        public District District { get; set; }

        public int District_ID { get; set; }
    }
}