namespace SistemaDeCadastro.Tests;
using Moq;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Models.ModelsDTO;
using SistemaDeCadastro.Repositories.Interfaces;

public class UnitTest1
{
    private readonly Mock<IProduct> _productRepositoryMock;

    public UnitTest1()
    {
        _productRepositoryMock = new Mock<IProduct>();
    }
    [Test]
    public async Task Criar_shouldReturnProduct_WhenValidRequest()
    {
        //arrange
        var requestProductDTO = new RequestProductDTO
        {
            Name = "Coca-Cola Teste - 2L",
            Quantity = 10,
            Price = 9.99M,
            Validation = DateOnly.FromDateTime(DateTime.Now.AddDays(10))
        };

        var expectedProduct = new ProductModel
        {
            Id = 1,
            Name = requestProductDTO.Name,
            Quantity = requestProductDTO.Quantity,
            Price = requestProductDTO.Price,
            Validation = requestProductDTO.Validation
        };

        _productRepositoryMock
            .Setup(repo => repo.Criar(requestProductDTO))
            .ReturnsAsync(expectedProduct);

        var result = await _productRepositoryMock.Object.Criar(requestProductDTO);

        // act

        Assert.NotNull(result);
        Assert.Equal(expectedProduct.Name, result.Name);
        Assert.Equal(expectedProduct.Price, result.Price);
        Assert.Equal(expectedProduct.Quantity, result.Quantity);
        Assert.Equal(expectedProduct.Validation, result.Validation);


    }
}