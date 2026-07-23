using BookStore.Application.DTOs;
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

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddressService(
        IAddressRepository addressRepository,
        IUnitOfWork unitOfWork)
    {
        _addressRepository = addressRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<AddressDTO>> GetUserAddressesAsync(long userId)
    {
        var addresses = await _addressRepository.GetUserAddressesAsync(userId);

        return addresses.Select(a => new AddressDTO
        {
            Id = a.Id,
            Title = a.Title,
            FullName = a.FullName,
            Mobile = a.Mobile,
            Province = a.Province,
            City = a.City,
            PostalCode = a.PostalCode,
            AddressText = a.AddressText,
            IsDefault = a.IsDefault
        }).ToList();
    }

    public async Task<AddressDTO?> GetByIdAsync(long id, long userId)
    {
        var address = await _addressRepository.GetByIdAsync(id, userId);

        if (address == null)
            return null;

        return new AddressDTO
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
    }

    public async Task AddAsync(AddressDTO model, long userId)
    {
        var address = new Address
        {
            UserId = userId,
            Title = model.Title,
            FullName = model.FullName,
            Mobile = model.Mobile,
            Province = model.Province,
            City = model.City,
            PostalCode = model.PostalCode,
            AddressText = model.AddressText,
            IsDefault = model.IsDefault
        };
        if (model.IsDefault)
        {
            var addresses = await _addressRepository
                .GetUserAddressesForUpdateAsync(userId);

            foreach (var item in addresses)
            {
                item.IsDefault = false;
            }
        }
            await _addressRepository.AddAsync(address);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(AddressDTO model, long userId)
    {
        var address = await _addressRepository.GetByIdAsync(model.Id, userId);

        if (address == null)
            return;

        address.Title = model.Title;
        address.FullName = model.FullName;
        address.Mobile = model.Mobile;
        address.Province = model.Province;
        address.City = model.City;
        address.PostalCode = model.PostalCode;
        address.AddressText = model.AddressText;
        address.IsDefault = model.IsDefault;
        var addresses = await _addressRepository
                .GetUserAddressesForUpdateAsync(userId);
        if (model.IsDefault)
        {
            foreach (var item in addresses)
            {
                if(item.Id != address.Id)
                item.IsDefault = false;
            }
        }
        else
        {
            var newDefault = addresses.FirstOrDefault(a => a.Id != address.Id);

            if (newDefault != null)
            {
                newDefault.IsDefault = true;
            }
        }
            await _addressRepository.UpdateAsync(address);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id, long userId)
    {
        var address = await _addressRepository.GetByIdAsync(id, userId);
        

        if (address == null)
            return;
        if (address.IsDefault)
        {
            var addresses = await _addressRepository.GetUserAddressesForUpdateAsync(userId);

            var newDefault = addresses.FirstOrDefault(a => a.Id != address.Id);

            if (newDefault != null)
            {
                newDefault.IsDefault = true;
            }
        }
        await _addressRepository.DeleteAsync(address);
        await _unitOfWork.SaveChangesAsync();
    }
}
