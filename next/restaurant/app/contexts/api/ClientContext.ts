import Swal from "sweetalert2";
import apiUrl from "../../configurations/apiConfiguration.json";
import axios, { AxiosError, AxiosInstance, AxiosResponse } from "axios";
import IResult from "../../models/IResult";
class ClientContext {
  private apiServerURL: string;
  private axiosInstance: AxiosInstance;
  private error401Handler: () => void;
  constructor(error401Handler: () => void) {
    this.apiServerURL = apiUrl.baseUrl;
    this.axiosInstance = axios.create();
    this.setupInterceptors();
    this.error401Handler = error401Handler;
  }

  private setupInterceptors() {
    // this.axiosInstance.interceptors.request.use(
    //   (config) => {
    //     const token = "";
    //     if (token) {
    //       config.headers.Authorization = `Bearer ${token}`;
    //     }
    //     return config;
    //   },
    //   (error) => Promise.reject(error)
    // );

    this.axiosInstance.interceptors.response.use(
      (response) => {
        Swal.close();
        // Swal.clickConfirm();
        // Swal.hideLoading();
        // Swal.fire({
        //   position: "top-end",
        //   icon: "success",
        //   title: "",
        //   showConfirmButton: false,
        //   timer: 1000,
        // });
        return response;
      },
      (error: AxiosError<IResult>) => {
        Swal.hideLoading();
        Swal.clickConfirm();
        console.log("error in interceptor", error);
        if (error.response && error.response.status == 401) {
          console.log("error.response.status === 401");
          this.error401Handler();
          Swal.fire({
            title: "info",
            text: "الرجاء تسجيل الدخول انتهت جلستك",
            icon: "info",
            confirmButtonText: "حسناً",
          });
        } else if (
          error.response &&
          error.response.data.error.message != undefined &&
          error.response.status == 400
        ) {
          Swal.fire({
            title: "خطأ",
            text: error.response?.data.error.message,
            icon: "error",
            confirmButtonText: "حسناً",
          });
          return Promise.reject(error);
        } else {
          Swal.fire({
            title: "خطأ",
            text: "حدثت مشكلة بالخادم حاول لاحقاً",
            icon: "error",
            confirmButtonText: "حسناً",
          });
          return Promise.reject(error);
        }
      }
    );
  }

  // public async get<T>(
  //   url: string,
  //   config?: AxiosRequestConfig
  // ): Promise<AxiosResponse<T>> {
  //   let requestUrl: string = this.apiServerURL + url;
  //   return this.axiosInstance.get<T>(requestUrl, config);
  // }

  public async post<T>(
    url: string,
    token: string,
    data: unknown,
    contentType: string = "application/json"
  ): Promise<AxiosResponse<T>> {
    Swal.showLoading();
    let requestUrl: string = this.apiServerURL + url;
    return this.axiosInstance.post<T>(requestUrl, data, {
      headers: {
        "Content-Type": contentType,
        Authorization: `Bearer ${token}`,
      },
    });
  }
  public async delete<T>(
    url: string,
    token: string,
    contentType: string = "application/json"
  ): Promise<AxiosResponse<T>> {
    Swal.showLoading();
    let requestUrl: string = this.apiServerURL + url;
    return this.axiosInstance.delete<T>(requestUrl, {
      headers: {
        "Content-Type": contentType,
        Authorization: `Bearer ${token}`,
      },
    });
  }

  public async put<T>(
    url: string,
    token: string,
    data: unknown,
    contentType: string = "application/json"
  ): Promise<AxiosResponse<T>> {
    Swal.showLoading();
    let requestUrl: string = this.apiServerURL + url;
    return this.axiosInstance.put<T>(requestUrl, data, {
      headers: {
        "Content-Type": contentType,
        Authorization: `Bearer ${token}`,
      },
    });
  }

  public async get<T>(
    url: string,
    token: string,
    data?: unknown
  ): Promise<AxiosResponse<T>> {
    Swal.showLoading();
    let requestUrl: string = this.apiServerURL + url;
    return this.axiosInstance.get<T>(requestUrl, {
      params: { data },
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  // public async put<T>(
  //   url: string,
  //   data?: unknown,
  //   config?: AxiosRequestConfig
  // ): Promise<AxiosResponse<T>> {
  //   let requestUrl: string = this.apiServerURL + url;
  //   return this.axiosInstance.put<T>(requestUrl, data, config);
  // }

  // public async delete<T>(
  //   url: string,
  //   config?: AxiosRequestConfig
  // ): Promise<AxiosResponse<T>> {
  //   let requestUrl: string = this.apiServerURL + url;
  //   return this.axiosInstance.delete<T>(requestUrl, config);
  // }
}

export default ClientContext;
