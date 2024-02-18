using AutoMapper;
using PizzaApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Application.Common.DTOs
{
    public class PizzaDto
    {
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Стоимость")]
        public int Cost { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Pizza, PizzaDto>();
            }
        }
    }
}
