namespace Application.Common.Result;

public interface IResult
{
    bool IsSuccess { get; }
    List<Error> Errors { get; }
}