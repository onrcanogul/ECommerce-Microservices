﻿namespace Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string name, object key) : base($"Entity {name} ({key}) was not found")
        {
            
        }
    }
}
