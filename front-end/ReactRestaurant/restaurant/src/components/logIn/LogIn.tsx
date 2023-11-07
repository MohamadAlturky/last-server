import Remember from "../../assets/remember.svg";
import Person from "../../assets/person.svg";
import Security from "../../assets/security.svg";
import "./logIn.css";
import { useContext, useRef, useState } from "react";
import ClientContext from "../../contexts/api/ClientContext";
import LogInModel from "../../models/LogIn";
import Swal from "sweetalert2";
import Result from "../../models/Result";
import { AxiosError } from "axios";
import jwt_decode from "jwt-decode";
import { Navigate, useLocation, useNavigate } from "react-router-dom";
import { useCookies } from "react-cookie";
import { ResourceContext } from "../../contexts/resource/ResourceContext";
import AccessTokenDTO from "../../authentication/models/AccessTokenDTO";
import data from "../../configurations/permissions.json";

function LogIn() {
  const resources = useContext(ResourceContext);
  const context = new ClientContext(() => {});
  const serialNumber = useRef<HTMLInputElement>(null);
  const password = useRef<HTMLInputElement>(null);
  const rememberMe = useRef<HTMLInputElement>(null);
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const location = useLocation();
  const [cookie, setCookie] = useCookies(["jwt"]);
  const navigate = useNavigate();
  console.log("location in log");
  console.log(location);

  function handleForgetPassword() {
    navigate("/ForgetPassword");
  }

  function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    console.log(
      "serialNumber.current?.value =" + serialNumber.current?.value + ")"
    );
    console.log(password.current?.value);

    if (
      serialNumber.current?.value.length == 0 ||
      password.current?.value.length == 0
    ) {
      Swal.fire({
        title: "خطأ",
        text: "لم تقم بملئ جميع الحقول أستاذي الكريم حاول ثانية",
        icon: "error",
        confirmButtonText: "فهمت",
      });
    } else if (
      serialNumber.current?.value != undefined &&
      password.current?.value != undefined
    ) {
      let passwordValue = password.current.value;
      let serialNumberValue = serialNumber.current.value;

      const logInModel: LogInModel = {
        serialNumber: parseInt(serialNumberValue),
        password: passwordValue,
      };
      context
        .post<Result<string>>("api/Authentication/LogIn", "", logInModel)
        .then((response) => {
          if (rememberMe.current?.checked) {
            const decodedTokenPayload = jwt_decode<AccessTokenDTO>(
              response.data.value
            );

            var expires = new Date(decodedTokenPayload.exp * 1000);

            setCookie("jwt", response.data.value, {
              expires: expires,
              path: "/",
            });
            console.log("checked");
          } else {
            console.log(response.data.value);
            setCookie("jwt", response.data.value, {
              expires: undefined,
              path: "/",
            });
          }
          console.log("setIsAuthenticated(true);");

          setIsAuthenticated(true);
        })
        .catch((error: AxiosError<Result<string>>) => {
          console.log(error);
          // Swal.fire({
          //   title: "خطأ",
          //   text: error.response?.data.error.message,
          //   icon: "question",
          //   confirmButtonText: "فهمت",
          // });
        });
    } else {
      Swal.fire({
        title: "خطأ",
        text: "هنالك مشكلة ما حاول ثانيةً",
        icon: "error",
        confirmButtonText: "فهمت",
      });
    }
  }

  if (isAuthenticated && location.state != null) {
    return <Navigate to={location.state.from.pathname}></Navigate>;
  }
  if (isAuthenticated && location.state == null) {
    console.log("Please");
    let accessToken = cookie.jwt ? cookie.jwt : "";
    const decodedTokenPayload = jwt_decode<AccessTokenDTO>(accessToken);
    console.log(decodedTokenPayload);

    let permissions: string[] = decodedTokenPayload.permissions;

    if (permissions.includes(data.permissions.ConsumeReservations)) {
      return <Navigate to={"/ConsumeReservations"}></Navigate>;
    }
    if (permissions.includes(data.permissions.CreateSystemInformation)) {
      return <Navigate to={"/AdminServices"}></Navigate>;
    }
    if (permissions.includes(data.permissions.EditBalances)) {
      return <Navigate to={"/IncreaseCustomersBalance"}></Navigate>;
    }

    return <Navigate to={"/Home"}></Navigate>;
  }

  return (
    <>
      <form onSubmit={handleSubmit}>
        <div className="log-in-form">
          <div className="log-in-label">{resources.logIn.cardTitle}</div>
          <div className="user-name-container">
            <input
              className="user-name"
              type="number"
              placeholder={resources.logIn.serialNumber}
              dir="rtl"
              ref={serialNumber}
              autoFocus
            />
            <div className="user-name-svg">
              <img src={Person} alt="Logo" />
            </div>
          </div>

          <div className="user-name-container">
            <input
              className="user-name"
              type="password"
              placeholder={resources.logIn.password}
              dir="rtl"
              ref={password}
            />
            <div className="user-name-svg">
              <img src={Security} alt="Logo" />
            </div>
          </div>

          <div className="options">
            <div className="forget-password" onClick={handleForgetPassword}>
              <p>{resources.logIn.didYouForgetPassword}</p>
            </div>

            <div className="remember-me">
              <p>{resources.logIn.rememberMe}</p>
              <input
                className="remember-check-box"
                type="checkbox"
                id="remember"
                ref={rememberMe}
              />
              <label className="remember-label" htmlFor="remember">
                <img src={Remember} alt="Remeber" />
              </label>
            </div>
          </div>

          <button type="submit" className="submit-log-in">
            <p> {resources.logIn.LogInButton}</p>
          </button>
        </div>
      </form>
    </>
  );
}

export default LogIn;
