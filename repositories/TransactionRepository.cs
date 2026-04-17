using BuggyBackend.Models;

namespace BuggyBackend.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private List<Transaction> _transactions;
        private int _nextId;

        public TransactionRepository()
        {
            _transactions = new List<Transaction>
            {
                new Transaction
                {
                    Id = 1,
                    MemberId = 1,
                    BookId = 1,
                    BorrowDate = DateTime.Now.AddDays(-15),
                    ReturnDate = null
                }

            };
            _nextId = 2;
        }

        public List<Transaction> GetAll()
        {
            return _transactions;
        }

        public Transaction GetById(int id)
        {
            return _transactions.FirstOrDefault(t => t.Id == id);
        }

        public List<Transaction> GetByMemberId(int memberId)
        {
            return _transactions.Where(t => t.MemberId == memberId).ToList();
        }

        public List<Transaction> GetByBookId(int bookId)
        {
            return _transactions.Where(t => t.BookId == bookId).ToList();
        }

        public List<Transaction> GetActiveTransactions()
        {
            return _transactions.Where(t => t.ReturnDate == null).ToList();
        }

        public Transaction Create(Transaction transaction)
        {
            transaction.Id = _nextId++;
            transaction.BorrowDate = DateTime.UtcNow;
            _transactions.Add(transaction);
            return transaction;
        }

        public Transaction Update(int id, Transaction transaction)
        {
            var existingTransaction = GetById(id);
            if (existingTransaction != null)
            {
                existingTransaction.ReturnDate = transaction.ReturnDate;
                return existingTransaction;
            }
            return null;
        }

        public bool Delete(int id)
        {
            var transaction = GetById(id);
            if (transaction != null)
            {
                _transactions.Remove(transaction);
                return true;
            }
            return false;
        }
    }
}
