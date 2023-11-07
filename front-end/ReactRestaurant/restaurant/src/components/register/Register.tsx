import { useState } from "react";
import "./register.css";
import Swal from "sweetalert2";
import { OutletContextType } from "../../authentication/models/OutletContextType";
import { useOutletContext } from "react-router-dom";
import ClientContext from "../../contexts/api/ClientContext";
import { useCookies } from "react-cookie";
import IResult from "../../models/IResult";
function Register() {
  const [customerType, setCustomerType] = useState<string>("PoorPeople");
  const [firstName, setFirstName] = useState<string>("");
  const [lastName, setLastName] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [serialNumber, setSerialNumber] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [mail, setMail] = useState<string>("");

  const [cookies] = useCookies(["jwt"]);
  const { outLetProps } = useOutletContext<OutletContextType>();
  const clientContext = new ClientContext(outLetProps.error401Handler);

  const handleMailChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.value.endsWith("@")) {
      setMail(event.target.value + "hiast.edu.sy");
    } else {
      setMail(event.target.value);
    }
  };

  const handleCustomerChange = (
    event: React.ChangeEvent<HTMLSelectElement>
  ) => {
    setCustomerType(event.target.value);
  };
  const handleFirstNameChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setFirstName(event.target.value);
  };
  const handlePasswordChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value);
  };
  const handleSerialNumberChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setSerialNumber(event.target.value);
  };
  const handleLastNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setLastName(event.target.value);
  };
  const handleDescriptionChange = (
    event: React.ChangeEvent<HTMLTextAreaElement>
  ) => {
    setDescription(event.target.value);
  };
  function hasNumber(str: string): boolean {
    return /\d/.test(str);
  }

  function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    if (!mail.endsWith("@hiast.edu.sy")) {
      Swal.fire({
        title: "خطأ",
        text: "(@hiast.edu.sy) بريد المعهد غير صالح يجب أن ينتهي ب",
        icon: "error",
        confirmButtonText: "فهمت",
      });
    } else if (hasNumber(firstName) || hasNumber(lastName)) {
      Swal.fire({
        title: "خطأ",
        text: " لا يمكن للاسم أن يحتوي على أرقام",
        icon: "error",
        confirmButtonText: "فهمت",
      });
    } else if (
      firstName == "" ||
      password == "" ||
      serialNumber == "" ||
      description == "" ||
      lastName == ""
    ) {
      Swal.fire({
        title: "خطأ",
        text: "لم تقم بملئ جميع الحقول أستاذي الكريم حاول ثانية",
        icon: "error",
        confirmButtonText: "فهمت",
      });
    } else {
      let formData = new FormData();
      formData.append("password", password);
      formData.append("hiastMail", mail);
      formData.append("serialNumber", serialNumber);
      formData.append("notes", description);
      formData.append("lastName", lastName);
      formData.append("category", customerType);
      formData.append("firstName", firstName);

      let token = cookies.jwt;

      clientContext
        .post<IResult>(
          "api/Authentication/Register",
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
        });
    }
  }
  return (
    <div className="container">
      <div className="d-flex justify-content-center mt-5">
        {/* <Sider transformer="register-transformer"></Sider> */}

        {/* <form className="form"> */}
        <form className="create-meal-form" onSubmit={handleSubmit}>
          <div className="create-meal-label">تسجيل مستخدم جديد</div>
          <div className="input-container-on-meal-form">
            <div className="input-container-on-meal-form d-flex justify-content-center role-title">
              المعلومات الشخصية
            </div>
            <input
              className="create-meal-input"
              type="text"
              placeholder="اسم المستخدم"
              dir="rtl"
              onChange={handleFirstNameChange}
            />
            <div className="input-svg"></div>
          </div>

          <div className="input-container-on-meal-form">
            <input
              className="create-meal-input"
              type="text"
              placeholder="الكنية"
              dir="rtl"
              onChange={handleLastNameChange}
            />
            <div className="input-svg"></div>
          </div>
          <div className="input-container-on-meal-form">
            <textarea
              className="form-control meal-description"
              placeholder="ملاحظات"
              rows={3}
              dir="rtl"
              onChange={handleDescriptionChange}
            ></textarea>

            <div className="input-svg"></div>
          </div>

          <div className="input-container-on-meal-form">
            <select
              value={customerType}
              className="form-select select-meal-type"
              aria-label="Default select example"
              dir="rtl"
              onChange={handleCustomerChange}
            >
              <option value="Visitor">زائر</option>
              <option value="Employee">موظف</option>
              <option value="VeryPoorPeaple">طالب متميزين</option>
              <option value="PoorPeople">طالب دراسة عامة</option>
              <option value="RichPeople">طالب دراسة خاصة</option>
            </select>
          </div>
          <div className="input-container-on-meal-form d-flex justify-content-center role-title">
            الأمان
          </div>
          <div className="input-container-on-meal-form">
            <input
              className="create-meal-input"
              type="number"
              placeholder="الرقم الذاتي"
              dir="rtl"
              onChange={handleSerialNumberChange}
            />
            <div className="input-svg"></div>
          </div>
          <div className="input-container-on-meal-form">
            <input
              className="create-meal-input"
              type="password"
              placeholder="كلمة المرور"
              dir="rtl"
              onChange={handlePasswordChange}
            />
          </div>

          <div className="input-container-on-meal-form">
            <input
              className="create-meal-input"
              type="text"
              value={mail}
              placeholder="بريد المعهد"
              dir="rtl"
              onChange={handleMailChange}
            />
          </div>
          <button type="submit" className="submit-create-meal">
            <p>حفظ المستخدم الجديد</p>
          </button>
        </form>
      </div>
    </div>
  );
}

export default Register;
