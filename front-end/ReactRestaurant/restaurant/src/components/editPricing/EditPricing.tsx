import { useOutletContext } from "react-router-dom";
import { useCookies } from "react-cookie";
import { useState } from "react";
// import Sider from "../createMeal/Sider";
import "./editPricing.css";
import { OutletContextType } from "../../authentication/models/OutletContextType";
import ClientContext from "../../contexts/api/ClientContext";
import IResult from "../../models/IResult";
import Swal from "sweetalert2";

function EditPricing() {
  const [mealType, setMealType] = useState<string>("NormalMeal");
  const [customerType, setCustomerType] = useState<string>("Visitor");
  const [price, setPrice] = useState<number>(0);
  const [cookies] = useCookies(["jwt"]);
  const { outLetProps } = useOutletContext<OutletContextType>();
  const clientContext = new ClientContext(outLetProps.error401Handler);

  const handleMealChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setMealType(event.target.value);
  };
  const handleCustomerChange = (
    event: React.ChangeEvent<HTMLSelectElement>
  ) => {
    setCustomerType(event.target.value);
  };

  const handlePriceChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPrice(parseInt(event.target.value));
  };

  function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    if (price != 0) {
      let formData = new FormData();
      formData.append("price", price.toString());
      formData.append("mealTypeValue", mealType);
      formData.append("customerTypeValue", customerType);
      let token = cookies.jwt;
      clientContext
        .put<IResult>(
          "api/Pricing/UpdateRecordValue",
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

          // if (err.response.status != 401) {
          //   Swal.fire({
          //     title: "wronnnng",
          //     text: err.status,
          //     icon: "warning",
          //     confirmButtonText: "حسناً",
          //   });
          // } else {
          //   Swal.fire({
          //     title: "info",
          //     text: "الرجاء تسجيل الدخول انتهت جلستك",
          //     icon: "info",
          //     confirmButtonText: "حسناً",
          //   });
          // }
        });
    } else {
      Swal.fire({
        title: "خطأ",
        text: "لم تقم بملئ جميع الحقول أستاذي الكريم حاول ثانية",
        icon: "error",
        confirmButtonText: "فهمت",
      });
    }
  }
  return (
    <>
      <div className="container">
        <div className="d-flex justify-content-center mt-5">
          <div className="scale-sider">
            {/* <Sider transformer="pricing-transformer"></Sider> */}
          </div>
          <form onSubmit={handleSubmit} className="edit-pricing-form">
            <div className="create-meal-label">تعديل أسعار الوجبات</div>

            <div className="input-container-on-meal-form">
              <select
                value={mealType}
                className="form-select select-meal-type"
                aria-label="Default select example"
                dir="rtl"
                onChange={handleMealChange}
              >
                <option value="NormalMeal">وجبة</option>
                <option value="Plate">صحن</option>
                <option value="Dinner">عشاء</option>
                <option value="BreakFast">فطور</option>
              </select>
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
                <option value="VeryPoorPeaple">طلاب متميزين</option>
                <option value="PoorPeople">طلاب دراسة عامة</option>
                <option value="RichPeople">طلاب دراسة خاصة</option>
                <option value="RestautantManager">مدير المطعم</option>
              </select>
            </div>

            <div className="input-container-on-meal-form">
              <input
                className="create-meal-input"
                type="number"
                placeholder="السعر الجديد"
                dir="rtl"
                onChange={handlePriceChange}
              />
              <div className="input-svg"></div>
            </div>
            <button type="submit" className="submit-create-meal">
              <p> حفظ السعر الجديد</p>
            </button>
          </form>
        </div>
      </div>
    </>
  );
}
export default EditPricing;
