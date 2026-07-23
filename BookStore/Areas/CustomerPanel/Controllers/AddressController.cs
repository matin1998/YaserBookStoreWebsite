using BookStore.Application.DTOs;
using BookStore.Application.DTOs.AdminSide.Books;
using BookStore.Application.Services.Implementations;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Presentation.Areas.CustomerPanel.Controllers;


public class AddressController : CustomerBaseController
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _addressService.GetUserAddressesAsync(CurrentUserId);

        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new AddressDTO());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AddressDTO model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _addressService.AddAsync(model, CurrentUserId);

        SuccessMessage("آدرس با موفقیت ثبت شد.");

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        var address = await _addressService.GetByIdAsync(id, CurrentUserId);
        

        if (address == null)
            return NotFound();
        var model = new AddressDTO
        {
            Id = address.Id,
            Title = address.Title,
            FullName = address.FullName,
            Mobile = address.Mobile,
            Province = address.Province,
            City = address.City,
            PostalCode = address.PostalCode,
            AddressText = address.AddressText,
            IsDefault = address.IsDefault
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AddressDTO model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _addressService.UpdateAsync(model, CurrentUserId);

        SuccessMessage("آدرس با موفقیت ویرایش شد.");

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(long id)
    {
        await _addressService.DeleteAsync(id, CurrentUserId);

        SuccessMessage("آدرس با موفقیت حذف شد.");

        return RedirectToAction(nameof(Index));
    }
}
