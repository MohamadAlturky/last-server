import { useNavigate } from "react-router-dom";
import Footer from "../footer/Footer";
import NavBar from "../navBar/NavBar";
import "./forgetPassword.css";
import passwordSvg from "../../assets/mail.svg";
import passwordSvg2 from "../../assets/security-lock-svgrepo-com.svg";
import { useState } from "react";
import ClientContext from "../../contexts/api/ClientContext";
import { useCookies } from "react-cookie";
import Result from "../../models/Result";
import Swal from "sweetalert2";
import IResult from "../../models/IResult";
export default function ForgetPassword() {
  const context = new ClientContext(() => {});
  const [cookies] = useCookies(["jwt"]);
  const [serialNumber, setSerialNumber] = useState<number>(-1);
  const [requestId, setRequestId] = useState<number>(-1);
  const [password, setPassword] = useState<string>("");
  const [verificationCode, setVerificationCode] = useState<string>("");
  const navigate = useNavigate();

  const handleSubmitSerialNumber = (
    event: React.FormEvent<HTMLFormElement>
  ) => {
    event.preventDefault();

    if (serialNumber != -1) {
      let token = cookies.jwt;
      context
        .post<Result<number>>(
          "api/Authentication/ForgetPassword",
          token,
          serialNumber
        )
        .then((response) => {
          setRequestId(response.data.value);
          // console.log(response.data);
        })
        .catch(() => {
          // console.log(error);
        });
    }
  };

  const handleFinalSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (password == "" || verificationCode == "") {
      Swal.fire({
        title: "خطأ",
        text: "لم تقم بملئ جميع الحقول أستاذي الكريم حاول ثانية",
        icon: "error",
        confirmButtonText: "فهمت",
      });
    } else {
      let token = cookies.jwt;
      let formData = new FormData();
      formData.append("entryId", requestId.toString());
      formData.append("code", verificationCode);
      formData.append("serialNumber", serialNumber.toString());
      formData.append("password", password);
      context
        .post<IResult>(
          "api/Authentication/VerifyCode",
          token,
          formData,
          "multipart/form-data"
        )
        .then(() => {
          // console.log(response.data);
          navigate("/login");
        })
        .catch(() => {
          // console.log(error);
        });
    }
  };
  console.log("requestId", requestId);

  return (
    <>
      <NavBar
        styles={"fixed-top top-nav-container"}
        backgroundStyle="nav-color"
      ></NavBar>

      {requestId == -1 && (
        <div className="container">
          <div className="d-flex justify-content-center mt-5">
            <form
              className="edit-pricing-form position-relative"
              onSubmit={handleSubmitSerialNumber}
            >
              <img className="password-svg-forget" src={passwordSvg2} alt="" />
              <div className="create-meal-label">الرجاء إدخال الرقم الذاتي</div>

              <div className="input-container-on-meal-form">
                <input
                  className="create-meal-input"
                  type="number"
                  placeholder="الرقم الذاتي"
                  dir="rtl"
                  value={serialNumber != -1 ? serialNumber : ""}
                  onChange={(event) => {
                    setSerialNumber(parseInt(event.target.value));
                  }}
                />
              </div>

              <button type="submit" className="submit-create-meal">
                <p>طلب رمز التحقق</p>
              </button>
            </form>
          </div>
        </div>
      )}
      {requestId != -1 && (
        <div className="container">
          <div className="d-flex justify-content-center mt-5">
            <form
              className="edit-pricing-form position-relative"
              onSubmit={handleFinalSubmit}
            >
              <div className="transform-good">
                <img
                  className="password-svg-forget2"
                  src={passwordSvg}
                  alt=""
                />
              </div>
              <div className="create-meal-label">تفقد بريد المعهد الخاص بك</div>

              <div className="input-container-on-meal-form">
                <input
                  className="create-meal-input"
                  type="password"
                  placeholder="رمز التحقق"
                  dir="rtl"
                  value={verificationCode}
                  onChange={(event) => {
                    setVerificationCode(event.target.value);
                  }}
                />
              </div>

              <div className="input-container-on-meal-form">
                <input
                  className="create-meal-input"
                  type="password"
                  placeholder="كلمة السر الجديدة"
                  dir="rtl"
                  value={password}
                  onChange={(event) => {
                    setPassword(event.target.value);
                  }}
                />
              </div>
              <button type="submit" className="submit-create-meal">
                <p> إرسال</p>
              </button>
            </form>
          </div>
        </div>
      )}

      <Footer></Footer>
    </>
  );
}
