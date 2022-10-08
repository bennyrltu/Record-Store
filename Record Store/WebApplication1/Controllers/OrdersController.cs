using Microsoft.AspNetCore.Mvc;
using Record_Store.Data;
using Record_Store.Data.DTOS.Orders;
using Record_Store.Data.Repositories;
using Record_Store.Entity;
using System.Text.Json;

namespace Record_Store.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _ordersRepository;
        public OrdersController(IOrdersRepository ordersRepository)
        {
            _ordersRepository= ordersRepository;
        }
        //[HttpGet]
        public async Task<IEnumerable<OrderDTO>> GetMany()
        {
            var orders = await _ordersRepository.GetOrdersManyAsync();

            return orders.Select(o => new OrderDTO(o.ID, o.Name, o.Price, o.CreatedDate));
        }

        [HttpGet(Name = "GetOrders")]
        public async Task<IEnumerable<OrderDTO>> GetManyPaging([FromQuery] SearchParameters searchParameters)
        {
            var orders = await _ordersRepository.GetOrdersManyPagedAsync(searchParameters);

            var previousPage = orders.hasPrevious ? CreateOrdersResourceUri(searchParameters, ResourceUriType.PreviousPage) : null;
            var nextPage = orders.hasNext ? CreateOrdersResourceUri(searchParameters, ResourceUriType.NextPage) : null;

            var paginationMetaData = new
            {
                totalCount = orders.TotalCount,
                pageSize = orders.PageSize,
                currentPage = orders.CurrentPage,
                totalPages = orders.TotalPages,
                previousPage,
                nextPage
            };

            Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetaData));
            return orders.Select(o => new OrderDTO(o.ID, o.Name, o.Price, o.CreatedDate));
        }

        [HttpGet("{orderID}", Name = "GetOrder")]
        public async Task<ActionResult<OrderDTO>> Get(uint orderID)
        {
            var order = await _ordersRepository.GetOrder(orderID);

            if (order == null)
            {
                return NotFound();
            }

            var links = CreateLinks(orderID);

            var OrderDTO = new OrderDTO(order.ID, order.Name, order.Price, order.CreatedDate);
            return Ok(new { Resource = OrderDTO, Links = links });
        }

        //[HttpGet(Name = "GetOrder")]
        //[Route("{orderID}")]
        //public async Task<ActionResult<OrderDTO>> Get(uint orderID)
        //{
        //    var order = await _ordersRepository.GetOrder(orderID);

        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return new OrderDTO(order.ID, order.Name, order.Price, order.CreatedDate);
        //}

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Create(CreateOrderDTO createOrderDTO)
        {
            var order = new Order { Name = createOrderDTO.Name, Price=createOrderDTO.Price, CreatedDate=DateTime.UtcNow };
            await _ordersRepository.CreateOrder(order);

            //return CreatedAtAction("GetOrder", new { orderID = order.ID }, new OrderDTO(order.Name, order.Price,order.CreatedDate));
            //return CreatedAtAction(nameof(Get), new OrderDTO(order.Name, order.Price, order.CreatedDate));
            return Created("", new OrderDTO(order.ID, order.Name, order.Price, order.CreatedDate));
        }

        [HttpPut]
        [Route("{orderID}")]
        public async Task<ActionResult<OrderDTO>> Update(uint orderID, UpdateOrderDTO updateOrderDTO)
        {
            var order = await _ordersRepository.GetOrder(orderID);

            if (order == null)
            {
                return NotFound();
            }
            order.Name = updateOrderDTO.Name;
            await _ordersRepository.UpdateOrder(order);
            return Ok(new OrderDTO(order.ID, order.Name, order.Price, order.CreatedDate));
        }

        [HttpDelete("{orderID}", Name = "RemoveOrder")]
        [Route("{orderID}")]
        public async Task<ActionResult> Remove(uint orderID)
        {
            var order = await _ordersRepository.GetOrder(orderID);

            if (order == null)
            {
                return NotFound();
            }
            await _ordersRepository.RemoveOrder(order);

            return NoContent();
        }

        private string? CreateOrdersResourceUri(SearchParameters orderSearchParameters, ResourceUriType type)
        {
            return type switch
            {
                ResourceUriType.PreviousPage => Url.Link("GetOrders", new { pageNumber = orderSearchParameters.PageNumber -1, pageSize = orderSearchParameters.PageSize, }),
                ResourceUriType.NextPage => Url.Link("GetOrders", new { pageNumber = orderSearchParameters.PageNumber +1, pageSize = orderSearchParameters.PageSize, }),
                _ => Url.Link("GetOrders", new { pageNumber = orderSearchParameters.PageNumber, pageSize = orderSearchParameters.PageSize, })
            };
        }

        private IEnumerable<LinkDTO> CreateLinks(uint orderID)
        {
            yield return new LinkDTO { Href = Url.Link("GetOrder", new { orderID }), Rel = "self", Method = "GET" };
            yield return new LinkDTO { Href = Url.Link("RemoveOrder", new { orderID }), Rel = "delete_order", Method = "DELETE" };
        }
    }
}