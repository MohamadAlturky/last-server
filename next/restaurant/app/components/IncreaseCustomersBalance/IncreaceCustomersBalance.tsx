import { useState } from "react";
import "./increaseCustomersBalance.css";
import { useOutletContext } from "react-router-dom";
import ClientContext from "../../contexts/api/ClientContext";
import { OutletContextType } from "../../authentication/models/OutletContextType";
import { useCookies } from "react-cookie";
import IResult from "../../models/IResult";
import Swal from "sweetalert2";

export function IncreaseCustomersBalance() {
  const [serialNumber, setSerialNumber] = useState<number>(-1);
  const [value, setValue] = useState<number>(-1);
  const [cookies] = useCookies(["jwt"]);
  const { outLetProps } = useOutletContext<OutletContextType>();
  const clientContext = new ClientContext(outLetProps.error401Handler);

  console.log(value);
  console.log(serialNumber);

  const handleSerialNumberChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setSerialNumber(parseInt(event.target.value));
  };
  const handleValueChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setValue(parseInt(event.target.value));
  };

  function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    if (Number.isNaN(value) || Number.isNaN(serialNumber)) {
      Swal.fire({
        title: "خطأ",
        text: "عذراً المعلومات المعطاة خاطئة حاول مرة أخرى",
        icon: "error",
        confirmButtonText: "حسناً",
      });
    } else if (value <= 0) {
      Swal.fire({
        title: "خطأ",
        text: "القيمة المضافة للحساب غير صالحة حاول مرة أخرى",
        icon: "error",
        confirmButtonText: "حسناً",
      });
    } else if (serialNumber < 0) {
      Swal.fire({
        title: "خطأ",
        text: "الرقم الذاتي غير صالح حاول مرة أخرى",
        icon: "error",
        confirmButtonText: "حسناً",
      });
    } else {
      let formData = new FormData();
      formData.append("serialNumber", serialNumber.toString());
      formData.append("valueToAdd", value.toString());
      let token = cookies.jwt;
      clientContext
        .put<IResult>(
          "api/Customers/IncreaseCustomerBalance",
          token,
          formData,
          "multipart/form-data"
        )
        .then(() => {
          const Toast = Swal.mixin({
            toast: true,
            position: "top-end",
            showConfirmButton: false,
            timer: 1000,
            timerProgressBar: true,
            didOpen: (toast) => {
              toast.addEventListener("mouseenter", Swal.stopTimer);
              toast.addEventListener("mouseleave", Swal.resumeTimer);
            },
          });

          Toast.fire({
            icon: "success",
            title: "تم",
          });
        })
        .catch((err) => {
          console.log(err);
          //   Swal.fire({
          //     title: "صح",
          //     text: err.status,
          //     icon: "error",
          //     confirmButtonText: "حسناً",
          //   });
        });
    }
  }

  return (
    <div className="container">
      <div className="d-flex justify-content-center mt-5">
        <div className="scale-sider"></div>

        <form onSubmit={handleSubmit} className="edit-pricing-form">
          <div className="create-meal-label">تغذية رصيد مستخدم</div>

          <div className="input-container-on-meal-form">
            <input
              className="create-meal-input"
              type="number"
              placeholder="الرقم الذاتي"
              dir="rtl"
              onChange={handleSerialNumberChange}
            />
          </div>
          <div className="input-container-on-meal-form">
            <input
              className="create-meal-input"
              type="number"
              placeholder="المبلغ"
              dir="rtl"
              onChange={handleValueChange}
            />
          </div>
          <button type="submit" className="submit-create-meal">
            <p> إضافة</p>
          </button>
        </form>
      </div>
    </div>
  );
}
