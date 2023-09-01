using BitVenture_EbrahimSolomon.Models;

namespace BitVenture_EbrahimSolomon.Data
{
    public interface IDataRepository
    {
        Task AddOrUpdateRecordsAsync(int paymentID, string accountHolder, string branchCode, string accountNumber,
                                 string accountType, DateTime transactionDate, decimal amount,
                                 string status, DateTime effectiveStatusDate, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<List<ReportViewModel>> GetReportDataAsync();
    }
}
