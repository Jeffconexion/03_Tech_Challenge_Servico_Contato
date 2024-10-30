// --------------------------------------------------------------------------------------------------
// <copyright file="ContactServices.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using LocalFriendzApi.Application.IServices;
using LocalFriendzApi.Application.Request;
using LocalFriendzApi.Application.Response;
using LocalFriendzApi.Domain.IRepositories;
using LocalFriendzApi.Domain.Models;
using Microsoft.Extensions.Logging;

namespace LocalFriendzApi.Application.Services
{
    public class ContactServices : IContactServices
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger<ContactServices> _logger;

        public ContactServices(IContactRepository contactRepository, ILogger<ContactServices> logger)
        {
            _contactRepository = contactRepository;
            _logger = logger;
        }

        public async Task<Response<Contact?>> CreateContact(CreateContactRequest request)
        {
            var contact = request.ToEntity(request);

            try
            {
                await _contactRepository.Add(contact);
                _logger.LogInformation("Contact created successfully: {ContactId}", contact.Name);

                return new Response<Contact?>(contact, 201, message: "Contact created with sucess!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a contact.");
                return new Response<Contact?>(contact, 400, message: "Bad Request!");
            }
        }

        public async Task<Response<Contact?>> DeleteContact(Guid id)
        {
            try
            {
                _logger.LogInformation("Delete method called for Contact ID: {ContactId}", id);

                var contact = await _contactRepository.Search(c => c.Id == id);

                if (contact is null)
                {
                    _logger.LogWarning("Contact not found: {ContactId}", id);
                    return new Response<Contact?>(null, 404, message: "Contact not found!");
                }

                await _contactRepository.Delete(id);

                _logger.LogInformation("Contact deleted successfully: {ContactId}", id);

                return new Response<Contact?>(contact.First(), message: "Removed contact with sucess!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the contact with ID: {ContactId}", id);
                return new Response<Contact?>(null, 500, message: "Internal server erro!");
            }
        }

        public async Task<PagedResponse<List<Contact>?>> GetAllContactsWithAreaCode(GetAllContactRequest request)
        {
            try
            {
                var query = _contactRepository.GetAllContactWithAreaCode();

                var contacts = query
                                       .Skip((request.PageNumber - 1) * request.PageSize)
                                       .Take(request.PageSize)
                                       .ToList();

                var count = query.Count();

                if (contacts.Any() is false)
                {
                    _logger.LogInformation("Contacts not found!");
                    return new PagedResponse<List<Contact>?>(null, 404, message: "Not found contact.");
                }

                _logger.LogInformation("GetAll method executed successfully. Total contacts: {Count}", count);

                return new PagedResponse<List<Contact>?>(
                    contacts,
                    count,
                    request.PageNumber,
                    request.PageSize,
                    message: "Contacts found!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all contacts.");
                return new PagedResponse<List<Contact>?>(null, 500, message: "Internal Server Erro!");
            }
        }

        public async Task<PagedResponse<List<Contact>?>> GetByFilter(string codeRegion)
        {
            var request = GetByCodeRegionRequest.RequestMapper(codeRegion);
            try
            {
                var query = _contactRepository.GetContactByCodeRegion(codeRegion);

                var contacts = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

                var count = query.Count();

                if (contacts.Any() is false)
                {
                    _logger.LogInformation("Contacts not found!");
                    return new PagedResponse<List<Contact>?>(null, 404, message: "Not found contact.");
                }

                _logger.LogInformation("GetAll method executed successfully. Total contacts: {Count}", count);

                return new PagedResponse<List<Contact>?>(
                    contacts,
                    count,
                    request.PageNumber,
                    request.PageSize,
                    message: "Contacts found!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all contacts.");
                return new PagedResponse<List<Contact>?>(null, 500, message: "Internal Server Erro!");
            }
        }

        public async Task<Response<Contact?>> UpdateContact(Guid id, UpdateContactRequest request)
        {
            try
            {
                var query = _contactRepository.GetAllContactWithAreaCode();
                var contact = query.Where(c => c.Id == id).FirstOrDefault();

                if (contact is null)
                {
                    _logger.LogWarning("Contact not found: {ContactId}", id);
                    return new Response<Contact?>(null, 404, message: "Contact not found!");
                }

                // Novos valores
                contact.Name = request.Name;
                contact.Email = request.Email;
                contact.Phone = request.Phone;
                contact.AreaCode.CodeRegion = request.AreaCode;

                await _contactRepository.Update(contact);

                _logger.LogInformation("Contact updated successfully: {ContactId}", id);
                return new Response<Contact?>(contact, message: "Contact update with sucess!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the contact with ID: {ContactId}", id);
                return new Response<Contact?>(null, 500, message: "Internal Server Erro!");
            }
        }
    }
}
