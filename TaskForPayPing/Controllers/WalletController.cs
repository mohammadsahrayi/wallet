namespace TaskForPayPing.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Transaction.Framework.Domain;
using Transaction.Framework.Services.Interface;
using Transaction.Framework.Types;
using Transaction.WebApi.Models;
using Transaction.WebApi.Services;

[Route("api/account")]
[ApiController]
public class WalletController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly IMapper _mapper;

    public WalletController(ITransactionService transactionService, IMapper mapper)
    {
        _transactionService = transactionService;
        _mapper = mapper;
    }


    [HttpPost("Wallet/transaction")]
    public async Task<IActionResult> Transaction([FromBody] AccountTransaction TransactionModel)
    {
        return Created(
            string.Empty,
            TransactionModel.TransactionType == TransactionType.Deposit ? 
            await _transactionService.Deposit(TransactionModel) :
            await _transactionService.Withdraw(TransactionModel));
    }
    [HttpGet("Wallet/transaction")]
    public async Task<IActionResult> TransactionReport([FromQuery]TransactionReportFilterModel transactionReportFilterModel)
    {
        var transactionResult = await _transactionService.TransactionReport(transactionReportFilterModel);
        return Ok(_mapper.Map<TransactionResultModel>(transactionResult));

    }


}




