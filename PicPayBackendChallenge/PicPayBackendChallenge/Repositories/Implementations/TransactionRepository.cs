using Microsoft.EntityFrameworkCore;
using PicPayBackendChallenge.Context;
using PicPayBackendChallenge.Models;
using PicPayBackendChallenge.Repositories.Interfaces;
using PicPayBackendChallenge.Services;

namespace PicPayBackendChallenge.Repositories.Implementations;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Transaction?>> GetAll()
    {
        return await _context.Transaction.ToListAsync();
    }

    public async Task<IEnumerable<Transaction?>> GetByClient(Guid userId)
    {
        IEnumerable<Transaction?> transactionsByUser = await _context.Transaction.Where(t => t.PayerId == userId).ToListAsync();
        return transactionsByUser;
    }


    public async Task<Transaction?> GetById(Guid id)
    {
        Transaction? transaction = await _context.Transaction.FirstOrDefaultAsync(t => t != null && t.TransactionId == id);
        return transaction;
    }

    public async Task<Transaction> Create(Transaction transaction)
    {
        await _context.Transaction.AddAsync(transaction);
        await _context.SaveChangesAsync();

        return transaction;
    }
}