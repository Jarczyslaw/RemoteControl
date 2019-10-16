using System.Collections.Generic;

namespace JToolbox.Core.Extensions
{
    public static class IListExtensions
    {
        public static void ShiftLeft<T>(this IList<T> @this, int index)
        {
            if (@this.Count < 2)
                return;

            if (index < 1)
                return;

            var temp = @this[index - 1];
            @this.RemoveAt(index - 1);
            @this.Insert(index, temp);
        }

        public static void ShiftRight<T>(this IList<T> @this, int index)
        {
            if (@this.Count < 2)
                return;

            if (index > @this.Count - 2)
                return;

            var temp = @this[index + 1];
            @this.RemoveAt(index + 1);
            @this.Insert(index, temp);
        }

        public static void Swap<T>(this IList<T> @this, T oldValue, T newValue)
        {
            var oldIndex = @this.IndexOf(oldValue);
            while (oldIndex > 0)
            {
                @this.RemoveAt(oldIndex);
                @this.Insert(oldIndex, newValue);
                oldIndex = @this.IndexOf(oldValue);
            }
        }
    }
}