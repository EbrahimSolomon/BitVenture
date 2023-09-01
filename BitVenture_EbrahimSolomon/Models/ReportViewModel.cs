namespace BitVenture_EbrahimSolomon.Models
{
    public class ReportViewModel
    {
        public string BranchCode { get; set; }
        public string AccountType { get; set; }
        public string Status { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalAmount { get; set; }
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
