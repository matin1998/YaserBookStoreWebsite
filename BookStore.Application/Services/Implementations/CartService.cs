using BookStore.Application.DTOs.Cart;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.RepositoryInterfaces;
using BookStore.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Implementations;

public class CartService: ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CartService(
        ICartRepository cartRepository,
        ICartItemRepository cartItemRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _cartItemRepository = cartItemRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddToCartAsync(long userId, long productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product == null)
            throw new Exception("Product not found.");

        var cart = await _cartRepository.GetUserCartAsync(userId);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId
            };

            await _cartRepository.AddAsync(cart);

            await _unitOfWork.SaveChangesAsync();
        }

        var item = await _cartItemRepository
            .GetByCartAndProductAsync(cart.Id, productId);

        if (item == null)
        {
            item = new CartItem
            {
                CartId = cart.Id,
                ProductId = productId,
                Count = 1
            };

            await _cartItemRepository.AddAsync(item);
        }
        else
        {
            item.Count++;

            await _cartItemRepository.UpdateAsync(item);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<CartDTO?> GetUserCartAsync(long userId)
    {
        var cart = await _cartRepository.GetUserCartAsync(userId);

        if (cart == null)
            return null;

        var items = await _cartItemRepository.GetCartItemsAsync(cart.Id);

        var model = new CartDTO();

        foreach (var item in items)
        {
            model.Items.Add(new CartItemDTO
            {
                ProductId = item.ProductId,
                ProductTitle = item.Product.Title,
                Price = item.Product.Price,
                Inventory = item.Product.Inventory,
                Count = item.Count,
                ImageName = item.Product.Images
                    .FirstOrDefault(x => x.IsMainImage)?.ImageName
            });
        }

        model.TotalPrice = model.Items.Sum(x => x.TotalPrice);

        return model;
    }

    public async Task<CartOperationResultDTO> IncreaseCountAsync(
    long userId,
    long productId)
    {
        var cart = await _cartRepository.GetUserCartAsync(userId);

        if (cart == null)
            return new CartOperationResultDTO
            {
                Success = false,
                ProductId = productId
            };

        var item = await _cartItemRepository
            .GetByCartAndProductAsync(cart.Id, productId);

        if (item?.Product == null)
            return new CartOperationResultDTO
            {
                Success = false,
                ProductId = productId
            };

        var product = item.Product;

        if (item.Count >= product.Inventory)
        {
            var currentItems = await _cartItemRepository
                .GetCartItemsWithProductAsync(cart.Id);

            return new CartOperationResultDTO
            {
                Success = false,
                ProductId = productId,
                Count = item.Count,
                Inventory = product.Inventory,
                ItemTotalPrice = product.Price * item.Count,
                CartTotalPrice = currentItems.Sum(
        x => x.Product.Price * x.Count)
            };
        }

        item.Count++;

        await _cartItemRepository.UpdateAsync(item);
        await _unitOfWork.SaveChangesAsync();

        var items = await _cartItemRepository
            .GetCartItemsWithProductAsync(cart.Id);

        return new CartOperationResultDTO
        {
            Success = true,
            ProductId = productId,
            Count = item.Count,
            Inventory = product.Inventory,
            ItemTotalPrice = product.Price * item.Count,
            CartTotalPrice = items.Sum(
        x => x.Product.Price * x.Count)
        };
    }
    public async Task<CartOperationResultDTO> DecreaseCountAsync(
    long userId,
    long productId)
    {
        var cart = await _cartRepository.GetUserCartAsync(userId);

        if (cart == null)
            return new CartOperationResultDTO
            {
                Success = false,
                ProductId = productId
            };

        var item = await _cartItemRepository
            .GetByCartAndProductAsync(cart.Id, productId);

        if (item?.Product == null)
            return new CartOperationResultDTO
            {
                Success = false,
                ProductId = productId
            };

        if (item.Count <= 1)
        {
            var currentItems = await _cartItemRepository
                .GetCartItemsWithProductAsync(cart.Id);

            return new CartOperationResultDTO
            {
                Success = false,
                ProductId = productId,
                Count = item.Count,
                Inventory = item.Product.Inventory,
                ItemTotalPrice = item.Product.Price * item.Count,
                CartTotalPrice = currentItems.Sum(
                    x => x.Product.Price * x.Count)
            };
        }

        item.Count--;

        await _cartItemRepository.UpdateAsync(item);
        await _unitOfWork.SaveChangesAsync();

        var items = await _cartItemRepository
            .GetCartItemsWithProductAsync(cart.Id);

        return new CartOperationResultDTO
        {
            Success = true,
            ProductId = productId,
            Count = item.Count,
            Inventory = item.Product.Inventory,
            ItemTotalPrice = item.Product.Price * item.Count,
            CartTotalPrice = items.Sum(
                x => x.Product.Price * x.Count)
        };
    }

    public async Task<UpdateCartResultDTO> UpdateCartAsync(
    long userId,
    UpdateCartDTO model)
    {
        var result = new UpdateCartResultDTO();

        var cart = await _cartRepository.GetUserCartAsync(userId);

        if (cart == null)
            return result;

        var cartItems = await _cartItemRepository
            .GetCartItemsWithProductAsync(cart.Id);

        foreach (var requestItem in model.Items)
        {
            var cartItem = cartItems.FirstOrDefault(
                x => x.ProductId == requestItem.ProductId);

            if (cartItem?.Product == null)
                continue;

            if (requestItem.Count < 1)
                continue;

            cartItem.Count = Math.Min(
                requestItem.Count,
                cartItem.Product.Inventory);

            await _cartItemRepository.UpdateAsync(cartItem);
        }

        await _unitOfWork.SaveChangesAsync();

        cartItems = await _cartItemRepository
            .GetCartItemsWithProductAsync(cart.Id);

        foreach (var item in cartItems)
        {
            result.Items.Add(new CartItemDTO
            {
                ProductId = item.ProductId,
                ProductTitle = item.Product.Title,
                Price = item.Product.Price,
                Count = item.Count,
                ImageName = item.Product.Images
                    .FirstOrDefault()?.ImageName
            });
        }

        result.TotalPrice = result.Items.Sum(
            x => (decimal)x.Price * x.Count);

        result.Success = true;

        return result;
    }

    public async Task<CartOperationResultDTO> RemoveItemAsync(long userId, long productId)
    {
        var cart = await _cartRepository.GetUserCartAsync(userId);

        if (cart == null)
            return new CartOperationResultDTO { Success = false };

        var item = await _cartItemRepository
            .GetByCartAndProductAsync(cart.Id, productId);

        if (item == null)
            return new CartOperationResultDTO { Success = false };

        await _cartItemRepository.DeleteAsync(item);

        await _unitOfWork.SaveChangesAsync();

        var items = await _cartItemRepository.GetCartItemsWithProductAsync(cart.Id);

        return new CartOperationResultDTO
        {
            Success = true,
            ProductId = productId,
            Removed = true,
            CartTotalPrice = items.Sum(x => x.Product.Price * x.Count)
        };
    }


}
