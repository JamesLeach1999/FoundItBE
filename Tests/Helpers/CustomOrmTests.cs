using FinderBE.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;
using AutoFixture;
using FinderBE.Helpers;
using System.Data.Common;
using FluentAssertions;
using Org.BouncyCastle.Asn1.Cms;
using static Google.Protobuf.Compiler.CodeGeneratorResponse.Types;
using NSubstitute.ExceptionExtensions;
using System.Reflection.PortableExecutable;
using NSubstitute.ReturnsExtensions;

namespace Tests.Helpers;
public class CustomOrmTests
{
    private readonly DbDataReader _reader;
    private readonly Func<DbDataReader, User> _mapper;
    private readonly ICustomOrm<User> _sut;
    public CustomOrmTests()
    {
        _reader = Substitute.For<DbDataReader>();
        _mapper = Substitute.For<Func<DbDataReader, User>>();
        _sut = new CustomOrm<User>();
    }

    [Fact]
    public async Task Given_ValidReadersAndMapper_When_Mapped_Then_ReturnUserList()
    {
        var fixture = new Fixture();
        var userFixtureList = fixture.Create<List<User>>();

        _reader.ReadAsync().Returns(Task.FromResult(true), Task.FromResult(false));

        foreach(var item in userFixtureList)
        {
            _mapper.Invoke(_reader).Returns(item);
        }

        var result = await _sut.MapSqlValues(_reader, _mapper);

        foreach(var user in result)
        {
            user.Username.Should().BeOfType<string>();
            user.Password.Should().BeOfType<string>();
            user.PhoneNumber.Should().BeOfType<string>();
            user.Email.Should().BeOfType<string>();
        }
    }

    [Fact]
    public async Task Given_InvalidReadersAndValidMapper_When_Mapped_Then_ReturnException()
    {
        var fixture = new Fixture();

        var userListFixture = fixture.Create<List<User>>();

        var invalidReader = Substitute.For<DbDataReader>();
        invalidReader.ReadAsync().Throws(new Exception());

        foreach (var user in userListFixture)
        {
            _mapper.Invoke(_reader).Returns(user);
        }

        var result = async () => await _sut.MapSqlValues(invalidReader, _mapper);

        await result
            .Should()
            .ThrowAsync<Exception>()
            .WithMessage("Error mapping returned value User");
    }

    [Fact]
    public async Task Given_CalidReadersAndInalidMapper_When_Mapped_Then_ReturnException()
    {
        var fixture = new Fixture();
        var userFixtureList = fixture.Create<List<User>>();
        var invalidMapper = Substitute.For<Func<DbDataReader, User>>();

        _reader.ReadAsync().Returns(Task.FromResult(true), Task.FromResult(false));
        invalidMapper(_reader).Throws(new Exception());

        var result = async () => await _sut.MapSqlValues(_reader, invalidMapper);

        await result
            .Should()
            .ThrowAsync<Exception>()
            .WithMessage("Error mapping returned value User");
    }
}
