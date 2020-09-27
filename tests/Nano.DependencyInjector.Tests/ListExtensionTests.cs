using System.Collections.Generic;
using Xunit;

namespace Nano.DependencyInjector.Tests
{
    public class ListExtensionTests
    {
        [Fact]
        public void TrueFunc_Should_Get_Items_When_Statement_Is_True()
        {
            var falseHeroes = new List<string>();
            var heroes = new List<string>() {"Conan", "Spawn", "Batman"};
            heroes.If(IsBestHero)
                .True(t => Assert.Equal("Conan", t))
                .False(f => falseHeroes.Add(f));

            Assert.Equal(2, falseHeroes.Count);
        }

        private bool IsBestHero(string hero)
        {
            return hero == "Conan";
        }
    }
}