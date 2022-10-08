using Microsoft.AspNetCore.Mvc;
using Record_Store.Data;
using Record_Store.Data.DTOS.Orders;
using Record_Store.Data.Repositories;
using Record_Store.Entity;
using System.Text.Json;

namespace Record_Store.Controllers
{
    [ApiController]
    [Route("recordings")]
    //[Route("api/orders/{orderID}/recordings")]
    public class RecordingsController : ControllerBase
    {
        private readonly IRecordsRepository _recordingsRepository;
        public RecordingsController(IRecordsRepository recordingsRepository)
        {
            _recordingsRepository= recordingsRepository;
        }

        //[HttpGet]
        //public async Task<IEnumerable<RecordDTO>> GetMany()
        //{
        //    var recordings = await _recordingsRepository.GetRecordingsManyAsync();

        //    return recordings.Select(o => new RecordDTO(o.ID, o.Name, o.Description, o.Price, o.CreationDate, o.LastUpdated));
        //}



        [HttpGet(Name = "GetRecording")]
        public async Task<ActionResult<RecordDTO>> Get(uint recordingID)
        {
            var recording = await _recordingsRepository.GetRecording(recordingID);

            if (recording == null)
            {
                return NotFound();
            }

            return new RecordDTO(recording.ID, recording.Name, recording.Description, recording.Price, recording.CreationDate);
        }

        [HttpPost]
        public async Task<ActionResult<RecordDTO>> Create(CreateRecordDTO createRecordDTO)
        {
            var recording = new Recording { Name = createRecordDTO.Name, Description = createRecordDTO.Description, Price=createRecordDTO.Price, CreationDate=DateTime.UtcNow };
            await _recordingsRepository.CreateRecording(recording);

            //return CreatedAtAction("GetOrder", new { orderID = order.ID }, new RecordDTO(order.Name, order.Price,order.CreatedDate));
            //return CreatedAtAction(nameof(Get), new RecordDTO(order.Name, order.Price, order.CreatedDate));
            return Created("", new RecordDTO(recording.ID, recording.Name, recording.Description, recording.Price, recording.CreationDate));
        }

        [HttpPut]
        public async Task<ActionResult<RecordDTO>> Update(uint recordID, UpdateRecordDTO updateRecordDTO)
        {
            var recording = await _recordingsRepository.GetRecording(recordID);

            if (recording == null)
            {
                return NotFound();
            }
            recording.Name = updateRecordDTO.Name;
            recording.Description = updateRecordDTO.Description;
            recording.Price = updateRecordDTO.Price;
            recording.LastUpdated = DateTime.UtcNow;
            await _recordingsRepository.UpdateRecording(recording);
            return Ok(new RecordDTO(recording.ID, recording.Name, recording.Description, recording.Price, recording.CreationDate));
        }


        [HttpDelete("recordings", Name = "RemoveRecording")]
        public async Task<ActionResult> Remove(uint recordingID)
        {
            var recording = await _recordingsRepository.GetRecording(recordingID);

            if (recording == null)
            {
                return NotFound();
            }
            await _recordingsRepository.RemoveRecording(recording);

            return NoContent();
        }
    }
}
