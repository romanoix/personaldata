﻿namespace PersonalData.Entities
{
    public class Address
    {
        public virtual int Id { get; set; }
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual int ZipCode { get; set; }
        public virtual string Country { get; set; }
        public virtual Person Person { get; set; }
    }
}
