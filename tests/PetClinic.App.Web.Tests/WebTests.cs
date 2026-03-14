using PetClinic.Application.Common;
using Xunit;

namespace PetClinic.App.Web.Tests;

public class PagedResultTests
{
    [Fact]
    public void TotalPages_CalculatesCorrectly()
    {
        var result = new PagedResult<string>([], 12, 1, 5);
        Assert.Equal(3, result.TotalPages);
    }

    [Fact]
    public void HasPreviousPage_FirstPage_ReturnsFalse()
    {
        var result = new PagedResult<string>([], 10, 1, 5);
        Assert.False(result.HasPreviousPage);
    }

    [Fact]
    public void HasNextPage_LastPage_ReturnsFalse()
    {
        var result = new PagedResult<string>([], 10, 2, 5);
        Assert.False(result.HasNextPage);
    }

    [Fact]
    public void HasNextPage_NotLastPage_ReturnsTrue()
    {
        var result = new PagedResult<string>([], 12, 1, 5);
        Assert.True(result.HasNextPage);
    }

    [Fact]
    public void HasPreviousPage_NotFirstPage_ReturnsTrue()
    {
        var result = new PagedResult<string>([], 12, 2, 5);
        Assert.True(result.HasPreviousPage);
    }
}
