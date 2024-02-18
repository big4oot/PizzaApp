namespace PizzaApp.Application.Common.DTOs
{
    /// <summary>
    /// DTO клиента
    /// </summary>
    public class ClientDto
    {
        /// <summary>
        /// Id клиента
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Количество заказов
        /// </summary>
        public int OrdersCount { get; set; }

        /// <summary>
        /// Остаток средств на счету
        /// </summary>
        public int? Balance { get; set; }

        /// <summary>
        /// Наличие задолжности
        /// </summary>
        public bool HasDebt { get; set; }
    }
}
