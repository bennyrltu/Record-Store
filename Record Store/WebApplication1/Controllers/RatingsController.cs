using Microsoft.AspNetCore.Mvc;
using Record_Store.Data;
using Record_Store.Data.DTOS.Orders;
using Record_Store.Data.Repositories;
using Record_Store.Entity;
using System.Text.Json;


namespace Record_Store.Controllers
{
    [ApiController]
    [Route("ratings")]
    //[Route("api/orders/{orderID}/recordings/{recordingID}")]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsRepository _ratingsRepository;
        public RatingsController(IRatingsRepository ratingsrepository)
        {
            _ratingsRepository= ratingsrepository;
        }

        [HttpGet]
        public async Task<IEnumerable<RatingDTO>> GetMany()
        {
            var ratings = await _ratingsRepository.GetRatingsManyAsync();

            return ratings.Select(o => new RatingDTO(o.ID, o.Name, o.GivenRating, o.RatingDate));
        }

        //[HttpGet(Name = "GetRatings")]
        //public async Task<IEnumerable<RatingDTO>> GetManyPaging([FromQuery] SearchParameters searchParameters)
        //{
        //    var orders = await _ratingsRepository.GetRatingsManyPagedAsync(searchParameters);

        //    var previousPage = orders.hasPrevious ? CreateOrdersResourceUri(searchParameters, ResourceUriType.PreviousPage) : null;
        //    var nextPage = orders.hasNext ? CreateOrdersResourceUri(searchParameters, ResourceUriType.NextPage) : null;

        //    var paginationMetaData = new
        //    {
        //        totalCount = orders.TotalCount,
        //        pageSize = orders.PageSize,
        //        currentPage = orders.CurrentPage,
        //        totalPages = orders.TotalPages,
        //        previousPage,
        //        nextPage
        //    };

        //    Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetaData));
        //    return orders.Select(o => new RatingDTO(o.ID, o.Name, o.Price, o.CreatedDate));
        //}

        //[HttpGet("{orderID}", Name = "GetOrder")]
        //public async Task<ActionResult<RatingDTO>> Get(uint orderID)
        //{
        //    var order = await _ratingsRepository.GetOrder(orderID);

        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    var links = CreateLinks(orderID);

        //    var RatingDTO = new RatingDTO(order.ID, order.Name, order.Price, order.CreatedDate);
        //    return Ok(new { Resource = RatingDTO, Links = links });
        //}

        [HttpGet(Name = "GetRating")]
        [Route("{orderID}")]
        public async Task<ActionResult<RatingDTO>> Get(uint recordID)
        {
            var order = await _ratingsRepository.GetRating(recordID);

            if (order == null)
            {
                return NotFound();
            }

            return new RatingDTO(order.ID, order.Name, order.GivenRating, order.RatingDate);
        }

        [HttpPost]
        public async Task<ActionResult<RatingDTO>> Create(CreateRatingDTO createRatingDTO)
        {
            var rating = new Rating { Name = createRatingDTO.Name, GivenRating=createRatingDTO.Rating, RatingDate=DateTime.UtcNow };
            await _ratingsRepository.CreateReating(rating);

            //return CreatedAtAction("GetOrder", new { orderID = order.ID }, new RatingDTO(order.Name, order.Price,order.CreatedDate));
            //return CreatedAtAction(nameof(Get), new RatingDTO(order.Name, order.Price, order.CreatedDate));
            return Created("", new RatingDTO(rating.ID, rating.Name, rating.GivenRating, rating.RatingDate));
        }

        [HttpPut]
        [Route("{orderID}")]
        public async Task<ActionResult<RatingDTO>> Update(uint recordID, UpdateRatingDTO updateRatingDTO)
        {
            var rating = await _ratingsRepository.GetRating(recordID);

            if (rating == null)
            {
                return NotFound();
            }
            rating.GivenRating = updateRatingDTO.Rating;
            await _ratingsRepository.UpdateRating(rating);
            return Ok(new RatingDTO(rating.ID, rating.Name, rating.GivenRating, rating.RatingDate));
        }

        [HttpDelete("{orderID}", Name = "RemoveRating")]
        [Route("{orderID}")]
        public async Task<ActionResult> Remove(uint recordID)
        {
            var rating = await _ratingsRepository.GetRating(recordID);

            if (rating == null)
            {
                return NotFound();
            }
            await _ratingsRepository.RemoveRating(rating);

            return NoContent();
        }
    }
}
