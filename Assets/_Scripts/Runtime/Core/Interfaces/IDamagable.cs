using System;

namespace Interfaces
{
    public interface IDamageable
    {
        public bool IsAlive { get; }
        event Action<IDamageable> OnDie;
        void TakeDamage(int damage);
    }
}