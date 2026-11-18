

using Alba;
using Alba.Extensions;
using Alba.Security;
using Testcontainers.PostgreSql;

namespace Software.Tests.Vendors;


/// <summary>
/// This is a fixture for all system tests. It can be used as an individual class fixture, 
/// or, more likely, using the collection and the base class below
/// </summary>
public class AuthenticatedSystemTestFixture : IAsyncLifetime
{
    private PostgreSqlContainer _pgContainer = new PostgreSqlBuilder("postgres:18.3").Build();

    public IAlbaHost Host { get; set; } = null!;
    public TimeProviderOverride Clock = new(new DateTimeOffset(1969, 4, 20, 23, 59, 59, TimeSpan.FromHours(-4)));
    public async ValueTask InitializeAsync()
    {
        
        
        await _pgContainer.StartAsync(TestContext.Current.CancellationToken);
        Host = await Alba.AlbaHost.For<Program>(builder =>
        {

        }, new AuthenticationStub(), Clock).WithConfiguration("ConnectionStrings:software-db", _pgContainer.GetConnectionString());
    }
    public async ValueTask DisposeAsync()
    {
        await Host.DisposeAsync();
        await _pgContainer.DisposeAsync();
    }

}


public class UnauthenticatedSystemTestFixture : IAsyncLifetime
{
    //private PostgreSqlContainer _pgContainer = new PostgreSqlBuilder("postgres:18.3").Build();

    public IAlbaHost Host { get; set; } = null!;
    public TimeProviderOverride Clock = new(new DateTimeOffset(1969, 4, 20, 23, 59, 59, TimeSpan.FromHours(-4)));
    public async ValueTask InitializeAsync()
    {


        //await _pgContainer.StartAsync(TestContext.Current.CancellationToken);
        Host = await Alba.AlbaHost.For<Program>(builder =>
        {

        }, Clock).WithConfiguration("ConnectionStrings:software-db", "Host=none;database=none;user=should-not-use-database");
    }
    public async ValueTask DisposeAsync()
    {
        await Host.DisposeAsync();

    }

}


/// <summary>
/// This defines a collection of tests called "SystemTest" that will share the same fixture. 
/// Only one instance of the database and host needs to be created and will be shared by all
/// the tests that use this fixture. Of course, if that doesn't work for your situation, you can
/// create another collection and use that. Also consider looking into Alba pooling.
/// </summary>
[CollectionDefinition("SystemTest")]
public class SystemTestCollection : ICollectionFixture<AuthenticatedSystemTestFixture>;


/// <summary>
/// A Helper base class to inherit from - just cleans up each individual test class a bit.
/// </summary>
[Collection("SystemTest")]
public abstract class SystemTestContext
{
    public IAlbaHost Host { get; }
    protected SystemTestContext(AuthenticatedSystemTestFixture fixture)
    {
        Host = fixture.Host;
    }
}