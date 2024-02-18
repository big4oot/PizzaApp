using PizzaApp.Domain.Common;

namespace PizzaApp.Domain.Entities
{

    /// <summary>
    /// Кошелек пользователя
    /// </summary>
    public class Wallet : BaseAuditableEntity
    {

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Баланс кошелька
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// Списать средства
        /// </summary>
        /// <param name="amount">Сумма на списание</param>
        public void Charge(int amount)
        {
            Balance -= amount;
        }

        /// <summary>
        /// Зачислить средства
        /// </summary>
        /// <param name="amount">Сумма на зачисление</param>
        public void Deposit(int amount)
        {
            Balance += amount;
        }
    }
}
