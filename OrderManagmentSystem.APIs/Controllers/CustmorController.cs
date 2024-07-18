using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagmentSystem.APIs.DTOs;
using OrderManagmentSystem.APIs.Errors;
using System.Domain;
using System.Domain.Entities;
using System.Domain.Specification;

namespace OrderManagmentSystem.APIs.Controllers
{
    public class CustmorController : ApiBaseController
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly IMapper _mapper;

        public CustmorController(IGenericRepository<Customer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET /api/customers/{customerId}/orders - Get all orders for a customer

        [HttpGet]
        public async Task<ActionResult<Customer>> GetCustmorWithAllOrders(int id)
        {
            var spec = new CustmorWithOrderSpec(id);
            var custmor = await _repository.GetByIdAsyncWithSpec(spec);

            if (custmor is null)
                return NotFound(new ApiResponse(404));

            return Ok(custmor);
        }

        // POST /api/customers - Create a new customer

        [HttpPost]
        public async Task<ActionResult<CustmorToReturnDto>> CreateCustmor(CustmorToReturnDto AddedCustmor)
        {
            var MappedCustmor = _mapper.Map<CustmorToReturnDto, Customer>(AddedCustmor);
            var result = await _repository.AddAsync(MappedCustmor);

            if (!(result > 0)) return BadRequest(new ApiResponse(400));

            return Ok(AddedCustmor);
        }


    }
}
