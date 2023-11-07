interface IResult {
  error: {
    code: string;
    message: string;
  };
  isSuccess: boolean;
  isFailure: boolean;
}
export default IResult;
