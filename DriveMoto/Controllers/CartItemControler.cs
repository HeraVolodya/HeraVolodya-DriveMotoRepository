﻿using DriveMoto.DataBase;
using DriveMoto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Security;
using AutoMapper;

namespace DriveMoto.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]

    public class CartItemControler : Controller
    {
        private readonly APIDbContext _dbCartItems;
        private readonly IMapper _mapper;

        public CartItemControler(APIDbContext dbCartItems, IMapper mapper)
        {
            _dbCartItems = dbCartItems;
            _mapper = mapper;
        }
        //getting a shopping list
        [HttpGet]
        public async Task<IActionResult> GetCartItem() => Ok(await _dbCartItems.CartItems.ToListAsync());

        //creating a purchase
        [HttpPost]
        public async Task<IActionResult> AddCarItem(
            [FromBody] AddCartItemRequest addCartItemRequest)
        {
            try
            {
                var cartItem = new CartItem()
                {
                    Id = Guid.NewGuid(),
                    CleantId = addCartItemRequest.CleantId,
                    ProductId = addCartItemRequest.ProductId

                };
                await _dbCartItems.CartItems.AddAsync(cartItem);
                await _dbCartItems.SaveChangesAsync();

                var newCartItem = await _dbCartItems.CartItems
                    .Include(t => t.Product)
                    .FirstOrDefaultAsync(t => t.Id == cartItem.Id);

                return Ok(_mapper.Map<CartItemDTO>(newCartItem));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        //delete purchase
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteCartItem = _dbCartItems.CartItems.SingleOrDefault(cl => cl.Id == id);
            if (deleteCartItem == null)
                return BadRequest();
            _dbCartItems.CartItems.Remove(deleteCartItem);
            await _dbCartItems.SaveChangesAsync();
            return Ok();
        }


    }
}
