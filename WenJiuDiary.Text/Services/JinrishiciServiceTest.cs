using Moq;
using WenJiuDiary.Library.Services;

namespace WenJiuDiary.Text.Services;

public class JinrishiciServiceTest
{
    [Fact]
    public async Task GetTokenAsync_ReturnIsNotNullOrWhiteSpace()
    {
        var alertServiceMock = new Mock<IAlertService>();
        var mockAlertService = alertServiceMock.Object;

        var jinrishiciService = new JinrishiciService(mockAlertService);

        var token = await jinrishiciService.GetTokenAsync();

        Assert.False(string.IsNullOrWhiteSpace(token));
        alertServiceMock.Verify(
            p =>p.AlertAsync(It.IsAny<string>(),
                It.IsAny<string>(),It.IsAny<string>()),Times.Never);
    }
}