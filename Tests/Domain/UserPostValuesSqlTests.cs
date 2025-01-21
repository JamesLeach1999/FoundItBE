using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoundItBE.Domain;
using FoundItBE.Models;
using AutoFixture;
using MySqlX.XDevAPI.Common;

namespace Tests.Domain;
public class UserPostValuesSqlTests : CustomWebAppFactory, IClassFixture<DatabaseFixture>
{
    private readonly ICreateValues<UserRequest, object> _sut;
    private readonly DatabaseFixture _fixture;


    public UserPostValuesSqlTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
        var connectionFactory = base.MySqlDatabaseFactory();

        _sut = new UserPostValuesSql(connectionFactory);
    }

    [Fact]
    public async Task Given_ValidUserObject_When_PostedInMethod_Then_200Returned()
    {
        var fixture = new Fixture();

        var stubUser = fixture.Build<UserRequest>().Create();

        var result = _sut.PostUser(stubUser);

        Assert.Equal(stubUser, result);
    }

    [Fact]
    public async Task Given_DuplicateEmail_When_UserCreated_Then_ReturnErrorMessage()
    {
        var fixture = new Fixture();

        var stubUser = fixture.Build<UserRequest>().With(x => x.Email, "Email6f9a215e-d5fc-4d20-9344-a5ba5d5c86e9").Create();

        var result = Assert.Throws<Exception>(() => _sut.PostUser(stubUser));


        Assert.Equal("Error creating user: Duplicate email address", result.Message);
    }
}
