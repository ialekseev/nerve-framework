﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NerveFramework.Core.Transactions;
using SimpleInjector;

namespace NerveFramework.Infrastructure.Transactions
{
    internal sealed class CommandLifetimeScopeDecorator<TCommand> : IHandleCommand<TCommand> where TCommand : ICommand
    {
        private readonly Container _container;
        private readonly Func<IHandleCommand<TCommand>> _handlerFactory;

        public CommandLifetimeScopeDecorator(Container container, Func<IHandleCommand<TCommand>> handlerFactory)
        {
            _container = container;
            _handlerFactory = handlerFactory;
        }

        [DebuggerStepThrough]
        public Task Handle(TCommand command)
        {
            if (_container.GetCurrentLifetimeScope() != null)
                return _handlerFactory().Handle(command);
            using (_container.BeginLifetimeScope())
                return _handlerFactory().Handle(command);
        }
    }
}
