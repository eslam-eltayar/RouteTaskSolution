using AutoMapper;
using OrderManagmentSystem.APIs.DTOs;
using System.Domain.Entities;

namespace OrderManagmentSystem.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReurnDto>();
            CreateMap<CustmorToReturnDto, Customer>();
            CreateMap<CreateOrderItemParams, OrderItem>();
            CreateMap<Invoice, InvoiceDto>();

        }
    }
}
