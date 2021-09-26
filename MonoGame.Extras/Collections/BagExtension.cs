namespace MonoGame.Extras.Collections
{
    /// <summary>
    /// A set of extension methods for <see cref="Bag{T}"/> objects.
    /// </summary>
    public static class BagExtension
    {
        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first
        ///  occurrence within the entire <see cref="Bag{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="Bag{T}"/>.</typeparam>
        /// <param name="bag">The <see cref="Bag{T}"/>.</param>
        /// <param name="element">The object to locate in the <see cref="Bag{T}"/>. The value can be null for reference types.</param>
        /// <returns>The zero-based index of the first occurrence of item within the entire <see cref="Bag{T}"/>, if found; otherwise, –1.</returns>
        public static int IndexOf<T>(this Bag<T> bag, T element)
        {
            for (int index = 0; index < bag.Count; index++)
            {
                if (bag[index].Equals(element))
                {
                    return index;
                }
            }

            return -1;
        }
    }
}
