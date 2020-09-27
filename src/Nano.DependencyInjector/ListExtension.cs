using System;
using System.Collections.Generic;

namespace Nano.DependencyInjector
{
    public static class ListExtensions
    {
        public static ITrue<T> If<T>(this IEnumerable<T> list, Predicate<T> ifStatement)
        {
            return new IfTrueWrapper<T>(list, ifStatement);
        }
    }

    public class IfTrueWrapper<T> : ITrue<T>
    {
        public IEnumerable<T> List { get; }
        public Predicate<T> IfStatement { get; }
        public Action<T> TrueAction { get; private set; }

        public IfTrueWrapper(IEnumerable<T> list, Predicate<T> ifStatement)
        {
            List = list;
            IfStatement = ifStatement;
        }

        public IFalse<T> True(Action<T> trueAction)
        {
            TrueAction = trueAction;

            return new ElseResult<T>(this);
        }
    }

    public class ElseResult<T> : IFalse<T>
    {
        private readonly IfTrueWrapper<T> _ifTrueWrapper;

        public ElseResult(IfTrueWrapper<T> ifTrueWrapper)
        {
            _ifTrueWrapper = ifTrueWrapper;
        }

        void IFalse<T>.False(Action<T> falseAction)
        {
            InternalRun(_ifTrueWrapper.List, _ifTrueWrapper.IfStatement, _ifTrueWrapper.TrueAction, falseAction);
        }

        public void Run()
        {
            InternalRun(_ifTrueWrapper.List, _ifTrueWrapper.IfStatement, _ifTrueWrapper.TrueAction, null);
        }

        private void InternalRun(IEnumerable<T> list,
            Predicate<T> ifStatement,
            Action<T> trueAction,
            Action<T> falseAction)
        {
            foreach (var item in list)
            {
                if (ifStatement(item))
                {
                    trueAction(item);
                }
                else
                {
                    falseAction?.Invoke(item);
                }
            }
        }
    }

    public interface IRunnable
    {
        void Run();
    }

    public interface ITrue<T>
    {
        IFalse<T> True(Action<T> trueAction);
    }

    public interface IFalse<T> : IRunnable
    {
        void False(Action<T> falseAction);
    }
}