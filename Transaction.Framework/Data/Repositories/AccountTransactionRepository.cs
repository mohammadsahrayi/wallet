﻿namespace Transaction.Framework.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Transaction.Framework.Data.Entities;
    using Transaction.Framework.Data.Interface;
    using Transaction.Framework.Exceptions;
    using Transaction.Framework.Types;

    public class AccountTransactionRepository : IAccountTransactionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<AccountSummaryEntity> _accountSummaryEntity;
        private readonly DbSet<AccountTransactionEntity> _accountTransactionEntity;

        public AccountTransactionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _accountSummaryEntity = _dbContext.Set<AccountSummaryEntity>();
            _accountTransactionEntity = _dbContext.Set<AccountTransactionEntity>();
        }

        public async Task Create(AccountTransactionEntity accountTransactionEntity, AccountSummaryEntity accountSummaryEntity)
        {
            _accountTransactionEntity.Add(accountTransactionEntity);

            bool isSaved = false;

            while (!isSaved)
            {
                try
                {
                    await _dbContext.SaveChangesAsync();
                    isSaved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is AccountSummaryEntity)
                        {
                            var databaseValues = entry.GetDatabaseValues();

                            if (databaseValues != null)
                            {
                                entry.OriginalValues.SetValues(databaseValues);
                                CalculateNewBalance();

                                void CalculateNewBalance()
                                {
                                    decimal balance = (decimal)(entry.OriginalValues["Balance"] ?? 0);
                                    decimal amount;
                                    if (accountTransactionEntity?.Amount != null)
                                    {
                                        amount = accountTransactionEntity.Amount;
                                    }
                                    else
                                    {
                                        amount = 0;
                                    }

                                    if (accountTransactionEntity?.TransactionType == TransactionType.Deposit.ToString())
                                    {
                                        accountSummaryEntity.Balance =
                                        balance += amount;
                                    }
                                    else if (accountTransactionEntity?.TransactionType == TransactionType.Withdrawal.ToString())
                                    {
                                        if (amount > balance)
                                            throw new InsufficientBalanceException();

                                        accountSummaryEntity.Balance =
                                        balance -= amount;
                                    }
                                }
                            }
                            else
                            {
                                throw new NotSupportedException();
                            }
                        }
                    }
                }
            }
        }
        public async Task<IEnumerable<AccountTransactionEntity>> Get(TransactionReportFilterModel transactionReportFilterModel)
        {
            try
            {
                return await _accountTransactionEntity.Where(x => x.UserID == transactionReportFilterModel.UserID
                                            && x.Amount >= transactionReportFilterModel.MinAmount
                                            && x.Amount <= transactionReportFilterModel.MaxAmount
                                            && (
                                            x.Date.Date >= transactionReportFilterModel.StartDate.Date
                                            && x.Date.Date <= transactionReportFilterModel.EndDate.Date
                                            )).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Update(AccountTransactionEntity accountTransactionEntity, AccountSummaryEntity accountSummaryEntity)
        {
            _accountTransactionEntity.Add(accountTransactionEntity);
            _accountSummaryEntity.Update(accountSummaryEntity);

            bool isSaved = false;

            while (!isSaved)
            {
                try
                {
                    await _dbContext.SaveChangesAsync();
                    isSaved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is AccountSummaryEntity)
                        {
                            var databaseValues = entry.GetDatabaseValues();

                            if (databaseValues != null)
                            {
                                entry.OriginalValues.SetValues(databaseValues);
                                CalculateNewBalance();

                                void CalculateNewBalance()
                                {
                                    decimal balance = (decimal)(entry.OriginalValues["Balance"] ?? 0);
                                    decimal amount;
                                    if (accountTransactionEntity?.Amount != null)
                                    {
                                        amount = accountTransactionEntity.Amount;
                                    }
                                    else
                                    {
                                        amount = 0;
                                    }

                                    if (accountTransactionEntity?.TransactionType == TransactionType.Deposit.ToString())
                                    {
                                        accountSummaryEntity.Balance =
                                        balance += amount;
                                    }
                                    else if (accountTransactionEntity?.TransactionType == TransactionType.Withdrawal.ToString())
                                    {
                                        if (amount > balance)
                                            throw new InsufficientBalanceException();

                                        accountSummaryEntity.Balance =
                                        balance -= amount;
                                    }
                                }
                            }
                            else
                            {
                                throw new NotSupportedException();
                            }
                        }
                    }
                }
            }
        }
    }
}
