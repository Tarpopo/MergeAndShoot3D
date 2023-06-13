using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scriptables.Events
{
    public interface IEvent { }
    
    public abstract class BaseEventSO : ScriptableObject, IEvent
    {
        private event Action Event;

        [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)]
        public void Invoke() => Event?.Invoke();
        public void Subscribe(Action action) => Event += action;
        public void Unsubscribe(Action action) => Event -= action;
    }
    
    public abstract class BaseEventSO<T> : ScriptableObject, IEvent
    {
        private event Action<T> Event;
        
        [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)]
        public void Invoke(T arg) => Event?.Invoke(arg);
        public void Subscribe(Action<T> action) => Event += action;
        public void Unsubscribe(Action<T> action) => Event -= action;
    }
    
    public abstract class BaseEventSO<T, TT> : ScriptableObject, IEvent
    {
        private event Action<T, TT> Event;
        
        [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)]
        public void Invoke(T arg1, TT arg2) => Event?.Invoke(arg1, arg2);
        public void Subscribe(Action<T, TT> action) => Event += action;
        public void Unsubscribe(Action<T, TT> action) => Event -= action;
    }
    
    public abstract class BaseEventSO<T, TT, TTk> : ScriptableObject, IEvent
    {
        private event Action<T, TT, TTk> Event;
        
        [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)]
        public void Invoke(T arg1, TT arg2, TTk arg3) => Event?.Invoke(arg1, arg2, arg3);
        public void Subscribe(Action<T, TT, TTk> action) => Event += action;
        public void Unsubscribe(Action<T, TT, TTk> action) => Event -= action;
    }
}