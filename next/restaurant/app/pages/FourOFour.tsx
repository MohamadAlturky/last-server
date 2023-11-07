import "./unAuthorized.css";
import person from "../assets/person-shrugging.svg";
import { useState } from "react";
import { Navigate } from "react-router-dom";

export default function FourOFour() {
  const [returnHome, setReturnHome] = useState<boolean>(false);
  function handleReturn(): void {
    setReturnHome(true);
  }

  return (
    <>
      {returnHome && <Navigate to={"/"}></Navigate>}
      <div className="d-flex flex-column justify-content-center align-items-center un-authrized">
        <h1 className="bolder-text">401</h1>
        <h1 className="mb-1">عذراً عزيزي المستخدم </h1>
        <img className="m-5" src={person} alt="" />
        <h2 className="mt-3 mb-5">الصفحة المطلوبة غير موجودة</h2>
        <h3
          className="return-back"
          onClick={() => {
            handleReturn();
          }}
        >
          العودة للصفحة الرئيسية
        </h3>
      </div>
    </>
  );
}
