namespace BitVenture_EbrahimSolomon.Models
{
    public class MasterRecord
    {
        public int ID { get; set; }
        public string AccountHolder { get; set; }
        public string BranchCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public ICollection<DetailRecord> DetailRecords { get; set; }
        public string AccountTypeDisplayName
        {
            get
            {
                return AccountType switch
                {
                    "1" => "Current / Cheque",
                    "2" => "Savings",
                    _ => "Unknown"
                };
            }
        }
    }
}
