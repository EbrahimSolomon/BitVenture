using BitVenture_EbrahimSolomon.Models;
using Microsoft.EntityFrameworkCore;

namespace BitVenture_EbrahimSolomon.Data
{
    public class DataRepository:IDataRepository
    {
        private readonly DataContext _context;

        public DataRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddOrUpdateRecordsAsync(int paymentID, string accountHolder, string branchCode, string accountNumber,
                                                  string accountType, DateTime transactionDate, decimal amount,
                                                  string status, DateTime effectiveStatusDate, CancellationToken cancellationToken)
        {
            var masterRecord = _context.MasterRecords.FirstOrDefault(m => m.AccountNumber == accountNumber);
            if (masterRecord == null)
            {
                masterRecord = new MasterRecord
                {
                    AccountHolder = accountHolder,
                    BranchCode = branchCode,
                    AccountNumber = accountNumber,
                    AccountType = accountType
                };

                _context.MasterRecords.Add(masterRecord);
            }
            else
            {
                masterRecord.AccountHolder = accountHolder;
                masterRecord.BranchCode = branchCode;
                masterRecord.AccountType = accountType;
            }

            var existingDetailRecord = _context.DetailRecords.FirstOrDefault(d =>
            d.TransactionDate == transactionDate &&
            d.Amount == amount &&
            d.Status == status &&
            d.EffectiveStatusDate == effectiveStatusDate &&
            d.MasterRecord.ID == masterRecord.ID
            );

            if (existingDetailRecord == null)
            {
                var detailRecord = new DetailRecord
                {
                    TransactionDate = transactionDate,
                    Amount = amount,
                    Status = status,
                    EffectiveStatusDate = effectiveStatusDate,
                    MasterRecord = masterRecord
                };

                _context.DetailRecords.Add(detailRecord);
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error saving changes: " + ex.Message + (ex.InnerException != null ? " Inner Exception: " + ex.InnerException.Message : ""), ex);
            }
        }

        public async Task<List<ReportViewModel>> GetReportDataAsync()
        {
            // Step 1: Fetch the data and bring it into memory
            var records = await _context.DetailRecords
                                        .Include(dr => dr.MasterRecord)
                                        .ToListAsync();

            // Step 2: Perform operations in memory
            var grouped = records.GroupBy(d => new
            {
                BranchCode = d.MasterRecord.BranchCode,
                AccountType = d.MasterRecord.AccountType,
                StatusDisplay = d.StatusDisplay  
            }).Select(g => new ReportViewModel
            {
                BranchCode = g.Key.BranchCode,
                AccountType = g.Key.AccountType,
                Status = g.Key.StatusDisplay,
                TotalCount = g.Count(),
                TotalAmount = g.Sum(r => r.Amount) 
            }).ToList();

            return grouped;
        }
    }
}
