using System;

namespace Utility
{
    /// <summary>
    /// Контейнер записи регистра програмных компонентов
    /// </summary>
    public class ComponentRegisterEntry
    {
        public readonly Type Type;
        public ComponentRegisterEntry(Type type)
        {
            Type = type;
        }
    }
}
