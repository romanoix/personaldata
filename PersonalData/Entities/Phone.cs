namespace PersonalData.Entities
{
    public class Phone
    {
        public virtual int Id { get; set; }
        public virtual int Number { get; set; }
        public virtual Person Person { get; set; }
    }
}
