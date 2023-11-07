import "./unAuthorized.css";
import detective from "../assets/policeman.svg";
import { useState } from "react";
import { Navigate } from "react-router-dom";
function UnAuthorized() {
  const [returnHome, setReturnHome] = useState<boolean>(false);
  function handleReturn(): void {
    setReturnHome(true);
  }

  return (
    <>
      {returnHome && <Navigate to={"/"}></Navigate>}
      <div className="d-flex flex-column justify-content-center align-items-center un-authrized">
        <h1 className="bolder-text">403</h1>
        <h1 className="mb-1">عذراً عزيزي المستخدم </h1>
        <img className="m-5" src={detective} alt="" />
        <h2 className="mt-3 mb-5">غير مسموح لك الوصول لهذه الصفحة</h2>
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

export default UnAuthorized;
