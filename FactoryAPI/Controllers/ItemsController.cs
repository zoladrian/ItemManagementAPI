using FactoryAPI.DbContexts;
using FactoryAPI.Helpers;
using FactoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace FactoryAPI.Controllers
{
    /// <summary>
    /// ItemsController manages CRUD operations for Item objects.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemDbContext _context;
        private readonly ErrorHandler _errorHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsController"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="errorHandler">The error handler.</param>
        public ItemsController(ItemDbContext context, ErrorHandler errorHandler)
        {
            _context = context;
            _errorHandler = errorHandler;
        }

        /// <summary>
        /// Creates a new Item.
        /// </summary>
        /// <param name="item">The item to create.</param>
        /// <returns>The created item along with its URI.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateItem(Item item)
        {
            try
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetItemById), new { id = item.ID }, item);
            }
            catch (Exception ex)
            {
                return _errorHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// Retrieves an Item by its ID.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <returns>The item if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            try
            {
                var item = await _context.Items.FindAsync(id);
                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item);
            }
            catch (Exception ex)
            {
                return _errorHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// Retrieves all Items.
        /// </summary>
        /// <returns>List of all items.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                return Ok(await _context.Items.ToListAsync());
            }
            catch (Exception ex)
            {
                return _errorHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// Updates an existing Item.
        /// </summary>
        /// <param name="id">The ID of the item to update.</param>
        /// <param name="item">The updated item.</param>
        /// <returns>NoContent if successful; otherwise, BadRequest.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, Item item)
        {
            try
            {
                item.ID = id;
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return _errorHandler.HandleException(ex);
            }
        }


        /// <summary>
        /// Deletes an Item by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        /// <returns>NoContent if successful; otherwise, NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var item = await _context.Items.FindAsync(id);
                if (item == null)
                {
                    return NotFound();
                }

                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return _errorHandler.HandleException(ex);
            }
        }
    }
}
