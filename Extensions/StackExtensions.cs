//-----------------------------------------------------------------------
// <copyright file="StackExtensions.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Stack{T}"/>.
    /// </summary>
    internal static class StackExtensions
    {
        /// <summary>
        /// Reverses a stack.
        /// </summary>
        /// <typeparam name="T">Specifies the type of elements in the stack.</typeparam>
        /// <param name="stack">The stack to reverse.</param>
        /// <returns>A reversed stack.</returns>
        /// <exception cref="ArgumentNullException">Throw when <paramref name="stack"/> is <c>null</c>.</exception>
        public static Stack<T> ReverseStack<T>(this Stack<T> stack)
        {
            if (stack == null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            if (stack.Count <= 1)
            {
                return new Stack<T>(stack);
            }

            Stack<T> reversed = new(stack.Count);
            while (stack.TryPop(out T? popped))
            {
                reversed.Push(popped);
            }

            return reversed;
        }
    }
}
