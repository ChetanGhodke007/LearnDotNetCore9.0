using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using LearnDotNetCore9._0.Controllers;
using LearnDotNetCore9._0.Domain;
using LearnDotNetCore9._0.DTO;

public class AuthControllerTests
{
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        // Initialize the controller
        _controller = new AuthController();
    }

    [Fact]
    public void Login_ReturnsOk_WhenCredentialsAreValid()
    {
        // Arrange
        var validUser = new LoginModel { UserName = "a", Password = "b" };

        // Act
        var result = _controller.Login(validUser);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<AuthenticatedResponse>(okResult.Value);
        Assert.NotNull(response.Token);
    }

    [Fact]
    public void Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
    {
        // Arrange
        var invalidUser = new LoginModel { UserName = "wrong", Password = "wrong" };

        // Act
        var result = _controller.Login(invalidUser);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public void Login_ReturnsBadRequest_WhenUserIsNull()
    {
        // Act
        var result = _controller.Login(null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid client request", badRequestResult.Value);
    }
}