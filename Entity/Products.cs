namespace TaskMediatrFluentValidation.Entity
{
    public class Products
    {
        public int id { get; set; }
        public int merchant_id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }

        public Merchant merchant { get; set; }
    }
}