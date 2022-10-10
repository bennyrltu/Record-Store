using Microsoft.AspNetCore.Mvc;
using Record_Store.Data;
using Record_Store.Data.DTOS.Orders;
using Record_Store.Data.Repositories;
using Record_Store.Entity;
using System.Text.Json;


namespace Record_Store.Controllers
{
    [ApiController]
    [Route("api/orders/{orderID}/recordings/{recordingID}/ratings")]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsRepository _ratingsRepository;
        public RatingsController(IRatingsRepository ratingsrepository)
        {
            _ratingsRepository= ratingsrepository;
        }

        [HttpGet]
        public async Task<IEnumerable<RatingDTO>> GetMany(uint recordingID)
        {
            var ratings = await _ratingsRepository.GetRatingsManyAsync(recordingID);

            return ratings.Select(o => new RatingDTO(o.ID, o.Name, o.GivenRating, o.RatingDate));
        }

        [HttpGet("{ratingId}")]
        public async Task<ActionResult<RatingDTO>> GetRating(uint recordingID, uint ratingID)
        {
            var o = await _ratingsRepository.GetRating(recordingID, ratingID);
            if (o == null) return NotFound();

            return new RatingDTO(o.ID, o.Name, o.GivenRating, o.RatingDate);
        }

        [HttpPost]
        public async Task<ActionResult<RatingDTO>> PostAsync(uint recordingID, CreateRatingDTO create)
        {
            var record = await _ratingsRepository.GetRatingsManyAsync(recordingID);
            if (record == null) return NotFound($"Couldn't find a order with id of {recordingID}");

            var rating = new Rating { Name = create.Name, GivenRating = create.Rating, RatingDate=DateTime.UtcNow };
            rating.RecordingID=recordingID;
            await _ratingsRepository.CreateReating(rating);

            return Created("", new RatingDTO(rating.ID, rating.Name, rating.GivenRating, rating.RatingDate));
        }

        [HttpPut("{ratingID}")]
        public async Task<ActionResult<RatingDTO>> Update(uint recordingID, uint ratingID, UpdateRatingDTO update)
        {
            var record = await _ratingsRepository.GetRatingsManyAsync(recordingID);
            if (record == null) return NotFound($"Couldn't find a order with id of {recordingID}");

            var oldRating = await _ratingsRepository.GetRating(recordingID, ratingID);
            if (oldRating == null)
                return NotFound();

            oldRating.GivenRating = update.Rating;
            oldRating.RatingDate = DateTime.UtcNow;
            await _ratingsRepository.UpdateRating(oldRating);
            return Ok(new RatingDTO(oldRating.ID, oldRating.Name, oldRating.GivenRating, oldRating.RatingDate));
        }

        [HttpDelete("{ratingId}")]
        public async Task<ActionResult> DeleteAsync(uint recordingID, uint ratingID)
        {
            var rating = await _ratingsRepository.GetRating(recordingID, ratingID);
            if (rating == null)
                return NotFound();

            await _ratingsRepository.RemoveRating(rating);

            // 204
            return NoContent();
        }
    }
}
