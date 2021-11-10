﻿using Messages.Api.Models;
using Messages.Api.Services;
using Messages.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Messages.Api.Controllers
{
    [Route("api/messages"), ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messagesService;

        public MessagesController(IMessageService service) => _messagesService = service;

        /// <summary>
        /// Gets all messages, ordered alphabetically.
        /// </summary>
        /// <param name="limit">Number of items to be returned.</param>
        /// <param name="offset">Number of items to skip. Can be used to paginate results.</param>
        /// <param name="term">Optional filter. If included, only messages that contain it will be returned.</param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ReadMessageResponse>>> Get(int limit = 100, int offset = 0, string term = null)
        {
            if (limit <= 0 || limit > 1000)
            {
                return BadRequest($"{nameof(limit)} must be between 1 and 1000");
            }

            if (offset < 0)
            {
                return BadRequest($"{nameof(offset)} must be at least zero");
            }

            var messages = await _messagesService.GetAll(limit, offset, term);
            return messages.Select(CreateReadModel).ToList();
        }

        /// <summary>
        /// Gets an existing message.
        /// </summary>
        /// <param name="id">Id of the message to be retrieved.</param>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReadMessageResponse>> Get(Guid id)
        {
            var message = await _messagesService.GetById(id);
            if (message == null)
            {
                return NotFound();
            }

            return CreateReadModel(message);
        }

        /// <summary>
        /// Creates a new message.
        /// </summary>
        /// <param name="model">The message to be created.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(WriteMessageRequest model)
        {
            var newMessage = new Message
            {
                Value = model.Message,
                IsPalindrome = model.Message.IsPalindrome()
            };

            newMessage = await _messagesService.Add(newMessage);
            return CreatedAtAction(nameof(Get), new { id = newMessage.Id }, CreateReadModel(newMessage));
        }

        /// <summary>
        /// Updates an existing message.
        /// </summary>
        /// <param name="id">Id of the message to be updated.</param>
        /// <param name="model">New value of the message.</param>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, WriteMessageRequest model)
        {
            var existingMessage = await _messagesService.GetById(id);
            if (existingMessage == null)
            {
                return NotFound();
            }

            existingMessage.Value = model.Message;
            existingMessage.IsPalindrome = model.Message.IsPalindrome();

            await _messagesService.Update(existingMessage);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing message.
        /// </summary>
        /// <param name="id">Id of the message to be deleted.</param>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingMessage = await _messagesService.GetById(id);
            if (existingMessage == null)
            {
                return NotFound();
            }

            await _messagesService.Delete(existingMessage);
            return NoContent();
        }

        /// <summary>
        /// Maps the message entity (which is saved to the database) to a model returned to clients.
        /// </summary>
        private ReadMessageResponse CreateReadModel(Message message) => new()
        {
            Id = message.Id,
            Message = message.Value,
            IsPalindrome = message.IsPalindrome
        };
    }
}
