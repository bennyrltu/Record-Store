using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Record_Store.Auth;
using Record_Store.Data;
using Record_Store.Data.DTOS.Orders;
using Record_Store.Data.Repositories;
using Record_Store.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace Record_Store.Controllers
{
    [ApiController]
    [Route("api/orders/{orderID}/recordings")]
    public class RecordingsController : ControllerBase
    {
        private readonly IRecordsRepository _recordingsRepository;
        private readonly IAuthorizationService _authorizationService;
        public RecordingsController(IRecordsRepository recordingsRepository, IAuthorizationService authorizationService)
        {
            _recordingsRepository= recordingsRepository;
            _authorizationService=authorizationService;     
        }

        [HttpGet]
        public async Task<IEnumerable<RecordDTO>> GetMany(uint orderID)
        {
            var recordings = await _recordingsRepository.GetRecordingsManyAsync(orderID);

            return recordings.Select(o => new RecordDTO(o.ID, o.Name, o.Description, o.Price, o.CreationDate));
        }

        [HttpGet("{recordId}")]
        public async Task<ActionResult<RecordDTO>> GetRecording(uint orderID, uint recordId)
        {
            var o = await _recordingsRepository.GetRecording(orderID, recordId);
            if (o == null) return NotFound();

            return new RecordDTO(o.ID, o.Name, o.Description, o.Price, o.CreationDate);
        }

        [HttpPost]
        public async Task<ActionResult<RecordDTO>> PostAsync(uint orderId, CreateRecordDTO create)
        {
            var order = await _recordingsRepository.GetRecordingsManyAsync(orderId);
            if (order == null) return NotFound($"Couldn't find a order with id of {orderId}");

            var recording = new Recording { Name = create.Name, Description = create.Description, Price=create.Price, CreationDate=DateTime.UtcNow, IsActive = true};
            recording.OrderId=orderId;
            await _recordingsRepository.CreateRecording(recording);

            return Created("", new RecordDTO(recording.ID, recording.Name, recording.Description, recording.Price, recording.CreationDate));
        }

        [HttpPut("{recordID}")]
        public async Task<ActionResult<OrderDTO>> Update(uint orderID, uint recordID, UpdateRecordDTO update)
        {
            var order = await _recordingsRepository.GetRecordingsManyAsync(orderID);
            if (order == null) return NotFound($"Couldn't find a order with id of {orderID}");

            var oldRecording = await _recordingsRepository.GetRecording(orderID, recordID);
            if (oldRecording == null)
                return NotFound();

            oldRecording.Name = update.Name;
            oldRecording.Description = update.Description;
            oldRecording.Price = update.Price;
            oldRecording.LastUpdated=DateTime.UtcNow;

            if (oldRecording.Price == 0)
            {
                oldRecording.IsActive = false;
            }
            else
            {
                oldRecording.IsActive = true;
            }
                await _recordingsRepository.UpdateRecording(oldRecording);
            return Ok(new RecordDTO(oldRecording.ID, oldRecording.Name, oldRecording.Description, oldRecording.Price, oldRecording.CreationDate));
        }

        [HttpDelete("{recordId}")]
        public async Task<ActionResult> DeleteAsync(uint orderID, uint recordId)
        {
            var recording = await _recordingsRepository.GetRecording(orderID, recordId);
            if (recording == null)
                return NotFound();

            await _recordingsRepository.RemoveRecording(recording);

            // 204
            return NoContent();
        }
    }
}
