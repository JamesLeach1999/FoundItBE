using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;
public class DatabaseFixture : IAsyncLifetime
{
    public CustomWebAppFactory WebAppFactory { get; } = new CustomWebAppFactory();

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        if (CustomWebAppFactory.Container != null)
        {
            //await CustomWebAppFactory.Container.DisposeAsync();
        }
    }
}