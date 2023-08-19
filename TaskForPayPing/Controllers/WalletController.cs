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
    public async Task<IActionResult> Transaction([FromBody] TransactionModel TransactionModel)
    {
        var Transaction = _mapper.Map<AccountTransaction>(TransactionModel);
        if (TransactionModel.TransactionType == TransactionType.Deposit)
            return Created(string.Empty, _mapper.Map<TransactionResultModel>(await _transactionService.Deposit(Transaction)));
        return Created(string.Empty, _mapper.Map<TransactionResultModel>(await _transactionService.Withdraw(Transaction)));
    }
    [HttpGet("Wallet/transaction")]
    public async Task<IActionResult> TransactionReport(TransactionReportFilterModel transactionReportFilterModel)
    {
        var transactionResult = await _transactionService.TransactionReport(transactionReportFilterModel);
        return Ok(_mapper.Map<TransactionResultModel>(transactionResult));

    }


}




