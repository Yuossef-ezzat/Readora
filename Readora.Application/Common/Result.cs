using System;
using System.Text.Json.Serialization;

namespace Readora.Application.Common;

public class Result
{
    public Result()
    {
        
    }
    public bool IsSuccess { get; }

	public bool IsFailure => !IsSuccess;

	public Error Error { get; }

	protected internal Result(bool isSuccess, Error error)
	{
		if (isSuccess && error != Error.None)
		{
			throw new InvalidOperationException();
		}
		if (!isSuccess && error == Error.None)
		{
			throw new InvalidOperationException();
		}
		IsSuccess = isSuccess;
		Error = error;
	}

	public static Result Success()
	{
		return new Result(isSuccess: true, Error.None);
	}

	public static Result<TValue> Success<TValue>(TValue value)
	{
		return new Result<TValue>(value, isSuccess: true, Error.None);
	}

	public static Result Failure(Error error)
	{
		return new Result(isSuccess: false, error);
	}

	public static Result<TValue> Failure<TValue>(Error error)
	{
		return new Result<TValue>(default!, isSuccess: false, error);
	}
}
public class Result<TValue> : Result
{
	private readonly TValue _value;

	public TValue Value
	{
		get
		{
			if (!IsSuccess)
			{
				throw new InvalidOperationException("The value of a failure result can not be accessed.");
			}

			return _value;
		}
	}
	[JsonConstructor]
    public Result(TValue value): base(isSuccess: true, Error.None)
    {
		_value = value;
    }
    protected internal Result(TValue value, bool isSuccess, Error error)
		: base(isSuccess, error)
	{
		_value = value;
	}
}
