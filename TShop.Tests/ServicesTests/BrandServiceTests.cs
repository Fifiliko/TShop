using Xunit;
using Moq;
using FluentAssertions;
using TShop.Application.DTOs;
using TShop.Application.Services;
using TShop.Domain.Entities;
using TShop.Domain.Interfaces.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TShop.Application.Interfaces.Repositories;

namespace TShop.Tests.ServicesTests
{
    public class BrandServiceTests
    {
        private readonly Mock<IBrandRepository> _brandRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BrandService _brandService;

        public BrandServiceTests()
        {
            _brandRepoMock = new Mock<IBrandRepository>();
            _mapperMock = new Mock<IMapper>();
            _brandService = new BrandService(_brandRepoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedDtos()
        {
            // Arrange
            var entities = new List<Brand> { new Brand { Id = 1, Name = "Brand1" }, new Brand { Id = 2, Name = "Brand2" } };
            var dtos = new List<BrandDto> { new BrandDto { Id = 1, Name = "Brand1" }, new BrandDto { Id = 2, Name = "Brand2" } };

            _brandRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<IEnumerable<BrandDto>>(entities)).Returns(dtos);

            // Act
            var result = await _brandService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(dtos);
            _brandRepoMock.Verify(r => r.GetAllAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<BrandDto>>(entities), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnMappedDto()
        {
            // Arrange
            var entity = new Brand { Id = 1, Name = "Brand1" };
            var dto = new BrandDto { Id = 1, Name = "Brand1" };

            _brandRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<BrandDto>(entity)).Returns(dto);

            // Act
            var result = await _brandService.GetByIdAsync(1);

            // Assert
            result.Should().BeEquivalentTo(dto);
            _brandRepoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
            _mapperMock.Verify(m => m.Map<BrandDto>(entity), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallRepositoryUpdateAndReturnDto()
        {
            // Arrange
            var dto = new BrandDto { Id = 1, Name = "Updated Brand" };
            var entity = new Brand { Id = 1, Name = "Updated Brand" };

            _mapperMock.Setup(m => m.Map<Brand>(dto)).Returns(entity);
            _brandRepoMock.Setup(r => r.UpdateAsync(entity)).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<BrandDto>(entity)).Returns(dto);

            // Act
            var result = await _brandService.UpdateAsync(dto);

            // Assert
            result.Should().BeEquivalentTo(dto);
            _brandRepoMock.Verify(r => r.UpdateAsync(entity), Times.Once);
            _mapperMock.Verify(m => m.Map<Brand>(dto), Times.Once);
            _mapperMock.Verify(m => m.Map<BrandDto>(entity), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenBrandExists_ShouldDeleteAndReturnTrue()
        {
            // Arrange
            var entity = new Brand { Id = 1, Name = "BrandToDelete" };

            _brandRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
            _brandRepoMock.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _brandService.DeleteAsync(1);

            // Assert
            result.Should().BeTrue();
            _brandRepoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
            _brandRepoMock.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenBrandDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            _brandRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Brand)null);

            // Act
            var result = await _brandService.DeleteAsync(1);

            // Assert
            result.Should().BeFalse();
            _brandRepoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
            _brandRepoMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
        }
    }
}
