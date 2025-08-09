using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing transactions data.
    /// </summary>
    [Authorize]
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : GenericViewController<ITransactionService, TransactionDto, TransactionModel>
    {
        /// <summary>
        /// Initializes a new instance of the TransactionController class.
        /// </summary>
        /// <param name="transactionService">The transaction service for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public TransactionsController(ITransactionService transactionService, IMapper mapper)
            : base(transactionService, mapper)
        {
        }

        #region override abstract Methods
        protected override async Task<IEnumerable<TransactionModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<TransactionModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        #endregion
    }
}

