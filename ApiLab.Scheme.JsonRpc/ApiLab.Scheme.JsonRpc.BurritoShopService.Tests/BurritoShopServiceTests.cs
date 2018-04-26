using System;
using ApiLab.Burritos;
using ApiLab.Normal.BurritoShop.Tests;
using ApiLab.Scheme.JsonRpc.Infrastructure.DotNetJsonRpcClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiLab.Scheme.JsonRpc.BurritoShopService.Tests
{
    [TestClass]
    public class BurritoShopServiceTests : NormalBurritoShopTests
    {
        
        public override IBurritoShop GetBurritoShop()
        {
            return JsonRpcClient.Build<IBurritoShop>(new Uri("http://localhost:57982/"));
        }

    }
}
