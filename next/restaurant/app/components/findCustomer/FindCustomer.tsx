import { useState } from "react";
import "./findCustomer.css";
import ClientContext from "../../contexts/api/ClientContext";
import { OutletContextType } from "../../authentication/models/OutletContextType";
import { useCookies } from "react-cookie";
import { useOutletContext } from "react-router-dom";
import Swal from "sweetalert2";
import Result from "../../models/Result";
import IResult from "../../models/IResult";

export default function FindCustomer() {
  const [serialNumber, setSerialNumber] = useState<number>();
  const [cookies] = useCookies(["jwt"]);
  const { outLetProps } = useOutletContext<OutletContextType>();
  const clientContext = new ClientContext(outLetProps.error401Handler);
  const [customer, setCustomer] = useState<CustomerInformation>();

  const [customerType, setCustomerType] = useState<string>("PoorPeople");
  const [firstName, setFirstName] = useState<string>("");
  const [lastName, setLastName] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [mail, setMail] = useState<string>("");

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (serialNumber && serialNumber >= 0) {
      let token = cookies.jwt;
      clientContext
        .get<Result<CustomerInformation>>(
          "api/Customers/GetCustomerInformation/" + serialNumber,
          token
        )
        .then((response) => {
          setCustomer(response.data.value);
          setFirstName(response.data.value.firstName);
          setLastName(response.data.value.lastName);
          setDescription(response.data.value.notes);
          setMail(response.data.value.hiastMail);
          setCustomerType(response.data.value.category);
          // setSerialNumber(response.data.value.serialNumber);
        });
    } else {
      Swal.fire({
        title: "خطأ",
        text: "الرجاء ملىء جميع الحقول بشكل صحيح",
        icon: "error",
        confirmButtonText: "حسناً",
      });
    }
  };

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
  // const handleSerialNumberChange = (
  //   event: React.ChangeEvent<HTMLInputElement>
  // ) => {
  //   setSerialNumber(parseInt(event.target.value));
  // };
  const handleLastNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setLastName(event.target.value);
  };
  const handleDescriptionChange = (
    event: React.ChangeEvent<HTMLTextAreaElement>
  ) => {
    setDescription(event.target.value);
  };

  function handleSubmitEdit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    if (!mail.endsWith("@hiast.edu.sy")) {
      Swal.fire({
        title: "خطأ",
        text: "(@hiast.edu.sy) بريد المعهد غير صالح يجب أن ينتهي ب",
        icon: "error",
        confirmButtonText: "فهمت",
      });
    } else if (
      firstName == "" ||
      description == "" ||
      lastName == "" ||
      serialNumber == undefined
    ) {
      Swal.fire({
        title: "خطأ",
        text: "لم تقم بملئ جميع الحقول أستاذي الكريم حاول ثانية",
        icon: "error",
        confirmButtonText: "فهمت",
      });
    } else {
      let formData = new FormData();
      formData.append("hiastMail", mail);
      formData.append("notes", description);
      formData.append("lastName", lastName);
      formData.append("category", customerType);
      formData.append("firstName", firstName);
      formData.append("serialNumber", serialNumber.toString());

      let token = cookies.jwt;

      clientContext
        .put<IResult>(
          "api/Customers/EditCustomerInformation",
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
    <>
      {customer && (
        <div className="container">
          <div className="d-flex justify-content-end">
            <div className="cancel" onClick={() => setCustomer(undefined)}>
              رجوع
            </div>
          </div>
          <div className="d-flex justify-content-center">
            {/* <Sider transformer="register-transformer"></Sider> */}

            {/* <form className="form"> */}
            <form className="create-meal-form" onSubmit={handleSubmitEdit}>
              <div className="create-meal-label">تعديل معلومات حساب </div>
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
                  value={firstName}
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
                  value={lastName}
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
                  value={description}
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
              {/* <div className="input-container-on-meal-form">
                <input
                  className="create-meal-input"
                  type="number"
                  placeholder="الرقم الذاتي"
                  dir="rtl"
                  value={serialNumber}
                  onChange={handleSerialNumberChange}
                />
                <div className="input-svg"></div>
              </div> */}

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

              <div className="input-container-on-meal-form d-flex justify-content-center role-title">
                الرصيد
              </div>
              <div className="input-container-on-meal-form">
                <input
                  className="create-meal-input"
                  type="number"
                  value={customer.balance}
                  placeholder="الرصيد"
                  dir="rtl"
                  disabled
                />
              </div>
              <button type="submit" className="submit-create-meal">
                <p>تعديل </p>
              </button>
            </form>
          </div>
        </div>
      )}
      {!customer && (
        <div className="container">
          <div className="d-flex justify-content-center mt-5">
            <div className="scale-sider"></div>
            <form className="edit-pricing-form" onSubmit={handleSubmit}>
              <div className="create-meal-label">🔎عرض معلومات زبون</div>

              <div className="input-container-on-meal-form">
                <input
                  className="create-meal-input"
                  type="number"
                  placeholder="الرقم الذاتي"
                  dir="rtl"
                  value={serialNumber}
                  onChange={(event) => {
                    setSerialNumber(parseInt(event.target.value));
                  }}
                />
                <div className="input-svg"></div>
              </div>
              <button type="submit" className="submit-create-meal">
                <p>البحث</p>
              </button>
            </form>
          </div>
        </div>
      )}
    </>
  );
}
