using Xunit;
using Moq;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using PopsicleFactory.Domain.Entities;
using PopsicleFactory.Domain.Interfaces;
using PopsicleFactory.Application.Dtos;
using PopsicleFactory.WebAPI.Controllers;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

public class PopsiclesControllerTests
{
    private readonly Mock<IPopsicleRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly PopsiclesController _controller;

    public PopsiclesControllerTests()
    {
        _mockRepo = new Mock<IPopsicleRepository>();

        // Setup Mapster 
        var config = new TypeAdapterConfig();
        config.NewConfig<Popsicle, PopsicleDto>();
        config.NewConfig<CreatePopsicleDto, Popsicle>();
        config.NewConfig<UpdatePopsicleDto, Popsicle>();
        _mapper = new Mapper(config);

        _controller = new PopsiclesController(_mockRepo.Object, _mapper);
    }

    /**************************** Get Unit Tests *********************************/

    [Fact]
    public async Task GetPopsicle_WhenPopsicleExists_ShouldReturnOk()
    {
        // Arrange for preconditions ( input test)
        var popsicle = new Popsicle { Id = Guid.NewGuid(), Name = "Grape", Price = 2.70m };
        _mockRepo.Setup(repo => repo.GetByIdAsync(popsicle.Id)).ReturnsAsync(popsicle);

        // Act method calling 
        var result = await _controller.GetPopsicle(popsicle.Id);

        // Assert for outcome 
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDto = Assert.IsType<PopsicleDto>(okResult.Value);
        Assert.Equal(popsicle.Id, returnedDto.Id);
    }

    [Fact]
    public async Task GetPopsicle_WhenPopsicleDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Popsicle?)null);

        // Act
        var result = await _controller.GetPopsicle(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task SearchPopsicles_WhenPopsiclesExist_ShouldReturnOkAndListOfPopsicles()
    {
        // Arrange
        var popsicles = new List<Popsicle>
        {
            new() { Id = Guid.NewGuid(), Name = "Grape", Price = 1.00m },
            new() { Id = Guid.NewGuid(), Name = "Orange", Price = 1.00m }
        };
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(popsicles);

        // Act
        var result = await _controller.SearchPopsicles();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDtos = Assert.IsAssignableFrom<IEnumerable<PopsicleDto>>(okResult.Value);
        Assert.Equal(2, returnedDtos.Count());
    }


    /**************************** Post Unit Tests *********************************/

    [Fact]
    public async Task CreatePopsicle_WithValidData_ShouldReturnCreatedAtAction()
    {
        // Arrange
        var createDto = new CreatePopsicleDto("Grape", "Grape Flavor", 1.50m);

        // Act
        var result = await _controller.CreatePopsicle(createDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedDto = Assert.IsType<PopsicleDto>(createdAtActionResult.Value);
        Assert.Equal(createDto.Name, returnedDto.Name);
        _mockRepo.Verify(r => r.AddAsync(It.IsAny<Popsicle>()), Times.Once);
    }

    /**************************** Put Unit Tests *********************************/

    [Fact]
    public async Task ReplacePopsicle_WhenPopsicleExists_ShouldReturnOk()
    {
        // Arrange
        var popsicleId = Guid.NewGuid();
        var existingPopsicle = new Popsicle { Id = popsicleId, Name = "Original", Price = 1.00m };
        var updateDto = new UpdatePopsicleDto("Updated", "Updated Flavor", 2.00m);
        _mockRepo.Setup(repo => repo.GetByIdAsync(popsicleId)).ReturnsAsync(existingPopsicle);

        // Act
        var result = await _controller.ReplacePopsicle(popsicleId, updateDto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        _mockRepo.Verify(r => r.UpdateAsync(It.Is<Popsicle>(p => p.Name == "Updated")), Times.Once);
    }

    [Fact]
    public async Task ReplacePopsicle_WhenPopsicleDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var updateDto = new UpdatePopsicleDto("Updated", "Updated Flavor", 2.00m);
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Popsicle?)null);

        // Act
        var result = await _controller.ReplacePopsicle(Guid.NewGuid(), updateDto);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePopsicle_WhenPopsicleExists_ShouldReturnNoContent()
    {
        // Arrange
        var popsicleId = Guid.NewGuid();
        var existingPopsicle = new Popsicle { Id = popsicleId, Name = "Original", Price = 1.00m };
        _mockRepo.Setup(repo => repo.GetByIdAsync(popsicleId)).ReturnsAsync(existingPopsicle);

        var patchDoc = new JsonPatchDocument<UpdatePopsicleDto>();
        patchDoc.Operations.Add(new Operation<UpdatePopsicleDto>("replace", "/price", null, 3.99m));

        // Act
        var result = await _controller.UpdatePopsicle(popsicleId, patchDoc);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(r => r.UpdateAsync(It.Is<Popsicle>(p => p.Price == 3.99m)), Times.Once);
    }

    [Fact]
    public async Task UpdatePopsicle_WhenPopsicleDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Popsicle?)null);
        var patchDoc = new JsonPatchDocument<UpdatePopsicleDto>();

        // Act
        var result = await _controller.UpdatePopsicle(Guid.NewGuid(), patchDoc);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }


    /**************************** Delete Unit Tests *********************************/

    [Fact]
    public async Task RemovePopsicle_WhenPopsicleExists_ShouldReturnNoContent()
    {
        // Arrange
        var popsicleId = Guid.NewGuid();
        var existingPopsicle = new Popsicle { Id = popsicleId, Name = "ToBeDeleted", Price = 1.00m };
        _mockRepo.Setup(repo => repo.GetByIdAsync(popsicleId)).ReturnsAsync(existingPopsicle);

        // Act
        var result = await _controller.RemovePopsicle(popsicleId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(r => r.DeleteAsync(popsicleId), Times.Once);
    }

    [Fact]
    public async Task RemovePopsicle_WhenPopsicleDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Popsicle?)null);

        // Act
        var result = await _controller.RemovePopsicle(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
