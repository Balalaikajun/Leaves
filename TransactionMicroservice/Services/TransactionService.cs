using Acces.Context;
using Acces.Repositories;
using Data.Models;

using System.Transactions;

namespace TransactionMicroservice.Services
{
    public class TransactionService
    {
        private LeavesDbContext _leavesDbContext;
        private TransactionRepository _transactionRepository;
        private AccountRepository _accountRepository;


        public TransactionService(LeavesDbContext leavesDbContext, TransactionRepository transactionRepository, AccountRepository accountRepository)
        {
            _leavesDbContext = leavesDbContext;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public void CreateTransaction(int accountIDFrom, int accountIDTo, decimal amount)
        {

            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                  new TransactionOptions { IsolationLevel = IsolationLevel.Serializable },
                  TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // Загружаем счета напрямую из базы данных для захвата последнего состояния.
                    Account from = _accountRepository.GetAccountById(accountIDFrom);
                    Account to = _accountRepository.GetAccountById(accountIDTo);

                    if (from == null)
                    {
                        throw new ArgumentException("Счёт списания не найден");
                    }
                    if (to == null)
                    {
                        throw new ArgumentException("Счёт зачисления не найден");
                    }

                    if (from.Balance < amount)
                    {
                        throw new InvalidOperationException("Недостаточно средств для перевода");
                    }

                    // Обновляем балансы
                    from.Balance -= amount;
                    to.Balance += amount;

                    _transactionRepository.CreateTransaction(accountIDFrom, accountIDTo, amount);

                    // Сохраняем изменения
                    _leavesDbContext.SaveChanges();

                    // Подтверждаем транзакцию
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    // Логируем ошибку, если необходимо
                    Console.WriteLine($"Ошибка при выполнении транзакции: {ex.Message}");
                    throw; // Пробрасываем исключение дальше
                }
            }

        }

    }
}
