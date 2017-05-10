using System;
using System.Collections.Generic;
using InClassDemo;

namespace InClassDemo
{
	class MainClass
	{
		public static void Main (string [] args)
		{
			IList<int> l = new Node<int> (220, new Node<int> (111, new Empty<int>()));
			IElementVisitor<int,string> ev = new PrintIntElements ();
			while (!l.IsEmpty ()) {
				Console.WriteLine (l.Visit(ev));
				l = l.Tail ();
			}
		}
	}

	interface IElementVisitor<T, U> {
		U OnSome (T val);
		U onNone ();
	}
	class PrintIntElements : IElementVisitor<int, string>
	{
		public string onNone ()
		{
			return "This is an empty onject";
		}

		public string OnSome (int val)
		{
			return (val * 100).ToString ();
		}
	}



	interface IElement<T>
	{
		//int -> string , string -> string , int -> bool
		U Visit<U> (IElementVisitor<T,U> ev);
	}

	interface IList<T> : IElement<T>{
		bool IsEmpty ();
		T Value ();
		IList<T> Tail ();
	}

	class Empty<T> : IList<T>
	{
		public bool IsEmpty ()
		{
			return true;
		}

		public IList<T> Tail ()
		{
			return null;
		}

		public T Value ()
		{
			return default (T); 
		}

		public U Visit<U> (IElementVisitor<T, U> ev)
		{
			return ev.onNone ();
		}
	}
	class Node<T> : IList<T>
	{
		T val;
		IList<T> tail;

		public Node (T val, IList<T> tail)
		{
			this.val = val;
			this.tail = tail;
		}

		public bool IsEmpty ()
		{
			return false;
		}

		public IList<T> Tail ()
		{
			return this.tail;
		}

		public T Value ()
		{
			return this.val;

		}

		public U Visit<U> (IElementVisitor<T, U> ev)
		{
			return ev.OnSome (this.Value ());
		}
	}
}



