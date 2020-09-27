using System;
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
            heroes.If(IsThisTheBestHero)
                .True(t => Assert.Equal("Conan", t))
                .False(f => falseHeroes.Add(f));

            Assert.Equal(2, falseHeroes.Count);
        }

        [Fact]
        public void There_Can_Be_Only_True_Action()
        {
            bool called = false;

            void TrueAct(string t)
            {
                called = true;
            }

            var heroes = new List<string>() {"Conan", "Spawn", "Batman"};
            heroes.If(IsThisTheBestHero)
                .True(TrueAct)
                .Run();

            Assert.True(called);
        }

        private bool IsThisTheBestHero(string hero)
        {
            return hero == "Conan";
        }
    }
}