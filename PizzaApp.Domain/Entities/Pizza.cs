using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Domain.Entities
{

    /// <summary>
    /// Пицца
    /// </summary>
    public class Pizza
    {

        /// <summary>
        /// Id (PK)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public int Cost { get; set; }

        public override string ToString() => Name;
    }
}


