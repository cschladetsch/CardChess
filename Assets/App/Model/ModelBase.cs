using System;

namespace App.Model
{
    public class ModelBase : Logger, IModel
    {
        public Guid Id { get; }

        protected ModelBase()
        {
            Id = Guid.NewGuid();
        }

        protected Main.Arbiter Arbiter => Main.Arbiter.Instance;
    }
}
