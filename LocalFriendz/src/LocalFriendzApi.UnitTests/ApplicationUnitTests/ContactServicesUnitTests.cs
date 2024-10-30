// --------------------------------------------------------------------------------------------------
// <copyright file="ContactServicesUnitTests.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentAssertions;
using LocalFriendzApi.Application.Request;
using LocalFriendzApi.Application.Services;
using LocalFriendzApi.Domain.IRepositories;
using LocalFriendzApi.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace LocalFriendzApi.UnitTests.ApplicationUnitTests
{
    public class ContactServicesUnitTests
    {
        [Fact]
        public async Task CreateContact_Should_Return_Success_Response_When_Contact_Is_Created()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var mockLogger = new Mock<ILogger<ContactServices>>();
            var contactServices = new ContactServices(mockContactRepository.Object, mockLogger.Object);

            var request = new CreateContactRequest
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
            };
            var contact = request.ToEntity(request);

            mockContactRepository.Setup(repo => repo.Add(It.IsAny<Contact>())).Returns(Task.CompletedTask);

            // Act
            var response = await contactServices.CreateContact(request);

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().Be(true);
            response.Message.Should().Be("Contact created with sucess!");
        }

        [Fact]
        public async Task CreateContact_Should_Return_Error_Response_When_Exception_Is_Thrown()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var mockLogger = new Mock<ILogger<ContactServices>>();
            var contactServices = new ContactServices(mockContactRepository.Object, mockLogger.Object);

            var request = new CreateContactRequest
            {
                Name = "Jane Doe",
                Email = "jane.doe@example.com",
            };
            var contact = request.ToEntity(request);

            mockContactRepository.Setup(repo => repo.Add(It.IsAny<Contact>())).Throws(new Exception("Database error"));

            // Act
            var response = await contactServices.CreateContact(request);

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().Be(false);
            response.Message.Should().Be("Bad Request!");
        }

        [Fact]
        public async Task DeleteContact_Should_Return_Success_Response_When_Contact_Is_Deleted()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var mockLogger = new Mock<ILogger<ContactServices>>();
            var contactServices = new ContactServices(mockContactRepository.Object, mockLogger.Object);

            var id = Guid.NewGuid();
            var contact = new Contact { Id = id, Name = "John Doe" };
            var contacts = new List<Contact> { contact }.AsQueryable();

            mockContactRepository.Setup(repo => repo.Search(It.IsAny<Expression<Func<Contact, bool>>>()))
                .ReturnsAsync(contacts);
            mockContactRepository.Setup(repo => repo.Delete(It.IsAny<Guid>())).Returns(Task.CompletedTask);

            // Act
            var response = await contactServices.DeleteContact(id);

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().Be(true);
            response.Message.Should().Be("Removed contact with sucess!");
        }

        [Fact]
        public async Task DeleteContact_Should_Return_NotFound_Response_When_Contact_Is_Not_Found()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var mockLogger = new Mock<ILogger<ContactServices>>();
            var contactServices = new ContactServices(mockContactRepository.Object, mockLogger.Object);

            var id = Guid.NewGuid();
            var contacts = new List<Contact>().AsQueryable();

            mockContactRepository.Setup(repo => repo.Search(It.IsAny<Expression<Func<Contact, bool>>>()))
                .ReturnsAsync(contacts);

            // Act
            var response = await contactServices.DeleteContact(id);

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().Be(false);
        }

        [Fact]
        public async Task DeleteContact_Should_Return_Error_Response_When_Exception_Is_Thrown()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var mockLogger = new Mock<ILogger<ContactServices>>();
            var contactServices = new ContactServices(mockContactRepository.Object, mockLogger.Object);

            var id = Guid.NewGuid();

            mockContactRepository.Setup(repo => repo.Search(It.IsAny<Expression<Func<Contact, bool>>>()))
                .Throws(new Exception("Database error"));

            // Act
            var response = await contactServices.DeleteContact(id);

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().Be(false);
            response.Message.Should().Be("Internal server erro!");
        }

        [Fact]
        public async Task GetAllContactsWithAreaCode_Should_Return_Success_Response_When_Contacts_Are_Found()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var mockLogger = new Mock<ILogger<ContactServices>>();
            var contactServices = new ContactServices(mockContactRepository.Object, mockLogger.Object);

            var request = new GetAllContactRequest
            {
                PageNumber = 1,
                PageSize = 10,
            };
            var contacts = new List<Contact>
        {
            new Contact { Id = Guid.NewGuid(), Name = "John Doe" },
            new Contact { Id = Guid.NewGuid(), Name = "Jane Doe" },
        }.AsQueryable();

            mockContactRepository.Setup(repo => repo.GetAllContactWithAreaCode())
                .Returns(contacts);

            // Act
            var response = await contactServices.GetAllContactsWithAreaCode(request);

            // Assert
            response.Should().NotBeNull();
            response.Code.Should().Be(200);
            response.Message.Should().Be("Contacts found!");
            response.Data.Should().NotBeNull();
            response.Data.Should().HaveCount(contacts.Count());
        }

        [Fact]
        public async Task GetAllContactsWithAreaCode_Should_Return_NotFound_Response_When_No_Contacts_Are_Found()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var mockLogger = new Mock<ILogger<ContactServices>>();
            var contactServices = new ContactServices(mockContactRepository.Object, mockLogger.Object);

            var request = new GetAllContactRequest
            {
                PageNumber = 1,
                PageSize = 10,
            };
            var contacts = new List<Contact>().AsQueryable();

            mockContactRepository.Setup(repo => repo.GetAllContactWithAreaCode())
                .Returns(contacts);

            // Act
            var response = await contactServices.GetAllContactsWithAreaCode(request);

            // Assert
            response.Should().NotBeNull();
            response.Code.Should().Be(404);
            response.Message.Should().Be("Not found contact.");
            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetAllContactsWithAreaCode_Should_Return_Error_Response_When_Exception_Is_Thrown()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var mockLogger = new Mock<ILogger<ContactServices>>();
            var contactServices = new ContactServices(mockContactRepository.Object, mockLogger.Object);

            var request = new GetAllContactRequest
            {
                PageNumber = 1,
                PageSize = 10,
            };

            mockContactRepository.Setup(repo => repo.GetAllContactWithAreaCode())
                .Throws(new Exception("Database error"));

            // Act
            var response = await contactServices.GetAllContactsWithAreaCode(request);

            // Assert
            response.Should().NotBeNull();
            response.Code.Should().Be(500);
            response.Message.Should().Be("Internal Server Erro!");
            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetContactByCodeRegion_Should_Return_Success_Response_When_Contacts_Are_Found()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var mockLogger = new Mock<ILogger<ContactServices>>();
            var contactServices = new ContactServices(mockContactRepository.Object, mockLogger.Object);

            var codeRegion = "123";
            var request = GetByCodeRegionRequest.RequestMapper(codeRegion);
            var contacts = new List<Contact>
                                            {
                                                new Contact { Id = Guid.NewGuid(), Name = "John Doe" },
                                                new Contact { Id = Guid.NewGuid(), Name = "Jane Doe" },
                                            }.AsQueryable();

            mockContactRepository.Setup(repo => repo.GetContactByCodeRegion(codeRegion))
                .Returns(contacts);

            // Act
            var response = await contactServices.GetByFilter(request.CodeRegion);

            // Assert
            response.Should().NotBeNull();
            response.Code.Should().Be(200);
            response.Message.Should().Be("Contacts found!");
            response.Data.Should().NotBeNull();
            response.Data.Should().HaveCount(contacts.Count());
        }

        [Fact]
        public async Task GetContactByCodeRegion_Should_Return_NotFound_Response_When_No_Contacts_Are_Found()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var mockLogger = new Mock<ILogger<ContactServices>>();
            var contactServices = new ContactServices(mockContactRepository.Object, mockLogger.Object);

            var codeRegion = "123";
            var request = GetByCodeRegionRequest.RequestMapper(codeRegion);
            var contacts = new List<Contact>().AsQueryable();

            mockContactRepository.Setup(repo => repo.GetContactByCodeRegion(codeRegion))
                .Returns(contacts);

            // Act
            var response = await contactServices.GetByFilter(request.CodeRegion);

            // Assert
            response.Should().NotBeNull();
            response.Code.Should().Be(404);
            response.Message.Should().Be("Not found contact.");
            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetContactByCodeRegion_Should_Return_Error_Response_When_Exception_Is_Thrown()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var mockLogger = new Mock<ILogger<ContactServices>>();
            var contactServices = new ContactServices(mockContactRepository.Object, mockLogger.Object);

            var codeRegion = "123";
            var request = GetByCodeRegionRequest.RequestMapper(codeRegion);

            mockContactRepository.Setup(repo => repo.GetContactByCodeRegion(codeRegion))
                .Throws(new Exception("Database error"));

            // Act
            var response = await contactServices.GetByFilter(request.CodeRegion);

            // Assert
            response.Should().NotBeNull();
            response.Code.Should().Be(500);
            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task UpdateContact_Should_Return_Success_Response_When_Contact_Is_Updated()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var mockLogger = new Mock<ILogger<ContactServices>>();
            var contactServices = new ContactServices(mockContactRepository.Object, mockLogger.Object);

            var id = Guid.NewGuid();
            var request = new UpdateContactRequest
            {
                Name = "Updated Name",
                Email = "updated.email@example.com",
                Phone = "123456789",
                AreaCode = "001",
            };

            var existingContact = new Contact
            {
                Id = id,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "987654321",
                AreaCode = new AreaCode { CodeRegion = "002" },
            };

            var contacts = new List<Contact> { existingContact }.AsQueryable();

            mockContactRepository.Setup(repo => repo.GetAllContactWithAreaCode())
                .Returns(contacts);

            mockContactRepository.Setup(repo => repo.Update(It.IsAny<Contact>()))
                .Returns(Task.CompletedTask);

            // Act
            var response = await contactServices.UpdateContact(id, request);

            // Assert
            response.Should().NotBeNull();
            response.Code.Should().Be(200);
            response.Message.Should().Be("Contact update with sucess!");
            response.Data.Should().NotBeNull();
            response.Data.Name.Should().Be(request.Name);
            response.Data.Email.Should().Be(request.Email);
            response.Data.Phone.Should().Be(request.Phone);
        }

        [Fact]
        public async Task UpdateContact_Should_Return_NotFound_Response_When_Contact_Is_Not_Found()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var mockLogger = new Mock<ILogger<ContactServices>>();
            var contactServices = new ContactServices(mockContactRepository.Object, mockLogger.Object);

            var id = Guid.NewGuid();
            var request = new UpdateContactRequest
            {
                Name = "Updated Name",
                Email = "updated.email@example.com",
                Phone = "123456789",
                AreaCode = "001",
            };

            var contacts = new List<Contact>().AsQueryable();

            mockContactRepository.Setup(repo => repo.GetAllContactWithAreaCode())
                .Returns(contacts);

            // Act
            var response = await contactServices.UpdateContact(id, request);

            // Assert
            response.Should().NotBeNull();
            response.Code.Should().Be(404);
            response.Message.Should().Be("Contact not found!");
        }

        [Fact]
        public async Task UpdateContact_Should_Return_Error_Response_When_Exception_Is_Thrown()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var mockLogger = new Mock<ILogger<ContactServices>>();
            var contactServices = new ContactServices(mockContactRepository.Object, mockLogger.Object);

            var id = Guid.NewGuid();
            var request = new UpdateContactRequest
            {
                Name = "Updated Name",
                Email = "updated.email@example.com",
                Phone = "123456789",
                AreaCode = "001",
            };

            mockContactRepository.Setup(repo => repo.GetAllContactWithAreaCode())
                .Throws(new Exception("Database error"));

            // Act
            var response = await contactServices.UpdateContact(id, request);

            // Assert
            response.Should().NotBeNull();
            response.Code.Should().Be(500);
        }
    }
}
