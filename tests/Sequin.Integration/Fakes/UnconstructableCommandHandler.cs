﻿namespace Sequin.Integration.Fakes
{
    using System.Threading.Tasks;
    using Core;

    public class UnconstructableCommandHandler : IHandler<UnconstructableCommandHandlerTest>
    {
        public UnconstructableCommandHandler(object obj)
        {
            // No parameterless constructor to make handler unconstructable with default factory
        }

        public Task Handle(UnconstructableCommandHandlerTest command)
        {
            // Do nothing
            return Task.FromResult(0);
        }
    }

    public class UnconstructableCommandHandlerTest { }
}