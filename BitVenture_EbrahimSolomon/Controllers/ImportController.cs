using BitVenture_EbrahimSolomon.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;

namespace BitVenture_EbrahimSolomon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IDataRepository _repository;
        private readonly DataContext _dataContext;

        public ImportController(IDataRepository repository, DataContext dataContext)
        {
            _repository = repository;
            _dataContext = dataContext;
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile csvFile, CancellationToken cancellationToken)
        {
            if (csvFile == null || csvFile.Length == 0)
                return BadRequest("No file uploaded");

            try
            {
                using (var reader = new StreamReader(csvFile.OpenReadStream()))
                {
                    string line;
                    // Skip header
                    reader.ReadLine();
                    while ((line = reader.ReadLine()) != null && !cancellationToken.IsCancellationRequested)
                    {
                        var columns = line.Split(',');
                        if (columns.Length < 9)
                        {
                            return BadRequest($"Line '{line}' doesn't have the expected number of columns.");
                        }

                        var paymentID = int.Parse(columns[0]);
                        var accountHolder = columns[1];
                        var branchCode = columns[2];
                        var accountNumber = columns[3];
                        var accountType = columns[4];
                        var transactionDate = DateTime.ParseExact(columns[5], "d/M/yyyy", CultureInfo.InvariantCulture);
                        var amount = decimal.Parse(columns[6]);
                        var status = columns[7];
                        var effectiveStatusDate = DateTime.ParseExact(columns[8], "d/M/yyyy", CultureInfo.InvariantCulture);

                        await _repository.AddOrUpdateRecordsAsync(paymentID, accountHolder, branchCode, accountNumber, accountType,
                                                 transactionDate, amount, status, effectiveStatusDate, cancellationToken);
                    }

                    await _repository.SaveChangesAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                // Log or handle exception here
                return BadRequest("An error occurred while processing the file." + ex.StackTrace + ex.InnerException );
            }

            return Ok();
        }

        [HttpGet("GetDetailRecords")]
        public IActionResult GetDetailRecords(int id)
        {
            try
            {
                var detailRecords = _dataContext.DetailRecords
                                                .Where(dr => dr.MasterRecordID == id)
                                                .ToList();
                return new JsonResult(detailRecords);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "An error occurred while fetching detail records.", details = ex.StackTrace });
            }
        }
    }
}
