using System;
using System.Collections.Generic;
using System.Linq;

namespace Nano.DependencyInjector
{
    public static class ListExtensions
    {
        public static ITrue<T> If<T>(this IEnumerable<T> list, Predicate<T> ifStatement)
        {
            var trueResult = list.Where(i => ifStatement(i));
            var falseResult = list.Where(i => !ifStatement(i));

            return new IfResult<T>(trueResult, falseResult);
        }
    }

    public class IfResult<T> : ITrue<T>
    {
        private readonly IEnumerable<T> trueResult;
        private readonly IEnumerable<T> falseResult;

        public IfResult(IEnumerable<T> trueResult, IEnumerable<T> falseResult)
        {
            this.trueResult = trueResult;
            this.falseResult = falseResult;
        }

        public IFalse<T> True(Action<T> act)
        {
            foreach (var item in trueResult)
            {
                act(item);
            }

            return new ElseResult<T>(falseResult);
        }
    }

    public class ElseResult<T> : IFalse<T>
    {
        private readonly IEnumerable<T> falseResult;

        public ElseResult(IEnumerable<T> falseResult)
        {
            this.falseResult = falseResult;
        }

        void IFalse<T>.False(Action<T> act)
        {
            foreach (var item in falseResult)
            {
                act(item);
            }
        }
    }

    public interface ITrue<T>
    {
        IFalse<T> True(Action<T> act);
    }

    public interface IFalse<T>
    {
        void False(Action<T> act);
    }
}