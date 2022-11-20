///////////////////////////////////////////////////////////////////////////////
//
// Author: https://www.nuget.org/packages/adjunct-System.DataStructures.AvlTree/
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 6
// Description: Part of the NuGet AVL Tree implementation.
//
///////////////////////////////////////////////////////////////////////////////

namespace System.DataStructures
{
	/// <summary>
	/// Node used by <see cref="AvlTree{T}"/>.
	/// </summary>
	/// <typeparam name="T">Type of the data stored in the node.</typeparam>
	internal class AvlTreeNode<T> : IBinaryTreeNode<AvlTreeNode<T>, T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AvlTreeNode{T}"/> class.
		/// </summary>
		/// <param name="value">Value of node</param>
		public AvlTreeNode(T value)
		{
			Value = value;
			Height = 1;
		}

		/// <summary>
		/// Gets the height of the node.
		/// </summary>
		public int Height { get; internal set; }

		/// <summary>
		/// Gets or sets the left node reference.
		/// </summary>
		public AvlTreeNode<T> Left { get; set; }

		/// <summary>
		/// Gets or sets the right node reference.
		/// </summary>
		public AvlTreeNode<T> Right { get; set; }

		/// <summary>
		/// Gets or sets the value of the node.
		/// </summary>
		public T Value { get; set; }
	}
}