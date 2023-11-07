interface Result<T> {
  value: T;
  error: {
    code: string;
    message: string;
  };
  isSuccess: boolean;
  isFailure: boolean;
}

export default Result;
