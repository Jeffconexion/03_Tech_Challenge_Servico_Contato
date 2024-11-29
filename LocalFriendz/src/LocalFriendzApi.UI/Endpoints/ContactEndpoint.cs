// --------------------------------------------------------------------------------------------------
// <copyright file="ContactEndpoint.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Net;
using System.Text;
using System.Text.Json;
using FluentValidation;
using LocalFriendzApi.Application.IServices;
using LocalFriendzApi.Application.Request;
using LocalFriendzApi.Application.Response;
using LocalFriendzApi.CrossCutting.Bus.Contracts;
using LocalFriendzApi.Domain.Models;
using LocalFriendzApi.Infrastructure.Dtos;
using LocalFriendzApi.Infrastructure.ExternalServices.Interfaces;
using LocalFriendzApi.UI.Configuration;

namespace LocalFriendzApi.UI.Endpoints
{
    public static class ContactEndpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var contactGroup = app.MapGroup("/contact");

            contactGroup.MapPost("api/crate/process/lote", async (IContactServices contactServices, IAreaCodeExternalService areaCodeExternalService, IMessageBusService _messageBusService, IValidator<CreateContactRequest> validator, List<CreateContactRequest> request) =>
            {
                foreach (var item in request)
                {
                    var validationResult = await validator.ValidateAsync(item);

                    if (validationResult.IsValid is false)
                    {
                        return Results.ValidationProblem(validationResult.ToDictionary());
                    }

                    try
                    {
                        await areaCodeExternalService.GetAreaCode(int.Parse(item.CodeRegion));
                    }
                    catch (Exception)
                    {
                        return TypedResults.NotFound(new Response<Contact?>(null, 404, message: "Code region not found!"));
                    }
                }

                SendMessageNotificationInLote("fila-processamento", _messageBusService, request, null);

                var response = new Response<Contact> { Message = "Processamento em Lote sendo realizado.", Code = 200 };
                return response.ConfigureResponseStatus();
            })
            .WithTags("Contact")
            .WithName("Contact: Create Process in Lote")
            .WithSummary("Create a list of contact record.")
            .WithDescription("Creates and saves a new contact in the system. This endpoint requires valid contact details including name, email, and phone number. Returns the created contact information upon successful save.")
            .Produces((int)HttpStatusCode.Created)
            .Produces((int)HttpStatusCode.BadRequest)
            .Produces((int)HttpStatusCode.NotFound)
            .WithOpenApi();

            contactGroup.MapPost("api/create", async (IContactServices contactServices, IAreaCodeExternalService areaCodeExternalService, IMessageBusService _messageBusService, IValidator<CreateContactRequest> validator, CreateContactRequest request) =>
            {
                var validationResult = await validator.ValidateAsync(request);

                if (validationResult.IsValid is false)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                try
                {
                    await areaCodeExternalService.GetAreaCode(int.Parse(request.CodeRegion));
                }
                catch (Exception)
                {
                    return TypedResults.NotFound(new Response<Contact?>(null, 404, message: "Code region not found!"));
                }

                var response = await contactServices.CreateContact(request);
                SendMessageNotification("fila-notificacoes", _messageBusService, response, null);
                SendMessageNotification("fila-sentimento", _messageBusService, response, request.FeedbackMessage);

                return response.ConfigureResponseStatus();
            })
            .WithTags("Contact")
            .WithName("Contact: Create Contact")
            .WithSummary("Create a new contact record.")
            .WithDescription("Creates and saves a new contact in the system. This endpoint requires valid contact details including name, email, and phone number. Returns the created contact information upon successful save.")
            .Produces((int)HttpStatusCode.Created)
            .Produces((int)HttpStatusCode.BadRequest)
            .Produces((int)HttpStatusCode.NotFound)
            .WithOpenApi();

            contactGroup.MapGet("api/list-all", async (IContactServices contactServices) =>
            {
                var request = new GetAllContactRequest();
                var response = await contactServices.GetAllContactsWithAreaCode(request);
                return response.ConfigureResponseStatus();
            })
            .WithTags("Contact")
            .WithName("Contact: Gets Record")
            .WithSummary("Retrieve all contact records.")
            .WithDescription("Fetches and returns a list of all contact records stored in the system. This endpoint does not require any parameters and provides a comprehensive list of all available contacts.")
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.InternalServerError)
            .WithOpenApi();

            contactGroup.MapGet("api/list-by-filter", async (IContactServices contactServices, string codeRegion) =>
            {
                var response = await contactServices.GetByFilter(codeRegion);
                return response.ConfigureResponseStatus();
            })
            .WithTags("Contact")
            .WithName("Contact: Get Record")
            .WithSummary("Retrieve a contact record by filter.")
            .WithDescription("Fetches a contact record based on the specified filter criteria, such as coderegion. This endpoint requires a valid coderegion parameter to return the corresponding contact details.")
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.InternalServerError)
            .WithOpenApi();

            contactGroup.MapPut("api/update", async (IContactServices contactServices, Guid id, IValidator<UpdateContactRequest> validator, UpdateContactRequest request) =>
            {
                var validationResult = await validator.ValidateAsync(request);

                if (validationResult.IsValid is false)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                var response = await contactServices.UpdateContact(id, request);
                return response.ConfigureResponseStatus();
            })
            .WithTags("Contact")
            .WithName("Contact: Update")
            .WithSummary("Update an existing contact record.")
            .WithDescription("Updates the details of an existing contact in the system. This endpoint requires the contact's unique identifier and the new information to be updated. If the contact exists, it will be updated with the provided details.")
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.InternalServerError)
            .WithOpenApi();

            contactGroup.MapDelete("api/delete", async (IContactServices contactServices, Guid id) =>
            {
                var response = await contactServices.DeleteContact(id);
                return response.ConfigureResponseStatus();
            })
              .WithTags("Contact")
              .WithName("Contact: remove")
              .WithSummary("Remove an existing contact record.")
              .WithDescription("Deletes a specific contact from the system based on the provided identifier. This endpoint requires the unique identifier of the contact to be deleted. If the contact exists, it will be removed from the system.")
              .Produces((int)HttpStatusCode.OK)
              .Produces((int)HttpStatusCode.NotFound)
              .Produces((int)HttpStatusCode.InternalServerError)
              .WithOpenApi();
        }

        private static void SendMessageNotification(string queue, IMessageBusService _messageBusService, Response<Contact?> response, string? feedbackMessage)
        {
            var messanotification = new MessageNotificationDto { Id = response.Data.Id, Name = response.Data.Name, Email = response.Data.Email, Message = "Contato criado com sucesso!" ,FeedbackMessage = feedbackMessage };
            var messanotificationJson = JsonSerializer.Serialize(messanotification);
            var notificationBytes = Encoding.UTF8.GetBytes(messanotificationJson);
            _messageBusService.Publish(queue, notificationBytes);
        }

        private static void SendMessageNotificationInLote(string queue, IMessageBusService _messageBusService, List<CreateContactRequest> requests, string? feedbackMessage)
        {
            foreach (var item in requests)
            {
                var messanotificationJson = JsonSerializer.Serialize<CreateContactRequest>(item);
                var notificationBytes = Encoding.UTF8.GetBytes(messanotificationJson);
                _messageBusService.Publish(queue, notificationBytes);
            }
        }
    }
}
