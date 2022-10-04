namespace RxUI.MauiToolkit.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class ValueObject
{
	public static bool operator ==(ValueObject one, ValueObject two)
	{
		return EqualOperator(one, two);
	}

	public static bool operator !=(ValueObject one, ValueObject two)
	{
		return NotEqualOperator(one, two);
	}

	public override bool Equals(object? obj)
	{
		if (obj == null || obj.GetType() != GetType())
			return false;

		ValueObject other = (ValueObject)obj;
		IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
		IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
		while (thisValues.MoveNext() && otherValues.MoveNext())
		{
			if (thisValues.Current is null ^ otherValues.Current is null)
			{
				return false;
			}
			if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
			{
				return false;
			}
		}
		return !thisValues.MoveNext() && !otherValues.MoveNext();
	}

	public override int GetHashCode()
	{
		return GetAtomicValues()
		 .Select(x => x != null ? x.GetHashCode() : 0)
		 .Aggregate((x, y) => x ^ y);
	}

	public ValueObject GetCopy()
	{
		return (ValueObject)MemberwiseClone();
	}



	protected abstract IEnumerable<object> GetAtomicValues();



	protected static bool EqualOperator(ValueObject left, ValueObject right)
	{
		if (left is null ^ right is null)
		{
			return false;
		}

		return left is null || left.Equals(right);
	}

	protected static bool NotEqualOperator(ValueObject left, ValueObject right)
	{
		return !EqualOperator(left, right);
	}
}
