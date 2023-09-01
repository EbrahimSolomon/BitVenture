namespace BitVenture_EbrahimSolomon.Models
{
    public enum PaymentStatus
    {
        Successful = 00,
        Disputed = 30,
        Failed = -1
    }

    public class DetailRecord
    {
        public int ID { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime EffectiveStatusDate { get; set; }
        public int MasterRecordID { get; set; }
        public MasterRecord MasterRecord { get; set; }
        private bool _timeBreached;

        public bool TimeBreached
        {
            get => _timeBreached;
            private set => _timeBreached = value;
        }

        public string StatusDisplay
        {
            get
            {
                switch (Status)
                {
                    case "00": return PaymentStatus.Successful.ToString();
                    case "30": return PaymentStatus.Disputed.ToString();
                    default: return PaymentStatus.Failed.ToString();
                }
            }
        }

    }
}
