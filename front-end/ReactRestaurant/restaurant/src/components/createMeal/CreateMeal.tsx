import "./file.css";
import "./createMeal.css";
import Swal from "sweetalert2";
import { useState } from "react";
import { useCookies } from "react-cookie";
import IResult from "../../models/IResult";
import fileIcon from "../../assets/file.svg";
import { useOutletContext } from "react-router-dom";
import ClientContext from "../../contexts/api/ClientContext";
import { OutletContextType } from "../../authentication/models/OutletContextType";

function CreateMeal() {
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [name, setName] = useState<string | null>(null);
  const [description, setDescription] = useState<string | null>(null);
  const [numberOfCalories, setNumberOfCalories] = useState<number | null>(null);
  const [mealType, setMealType] = useState<string>("NormalMeal");
  const [cookies] = useCookies(["jwt"]);
  const { outLetProps } = useOutletContext<OutletContextType>();
  const clientContext = new ClientContext(outLetProps.error401Handler);

  console.log(name);
  console.log(selectedFile);
  console.log(description);
  console.log(mealType);
  console.log(numberOfCalories);

  function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    if (
      numberOfCalories &&
      description &&
      selectedFile &&
      name &&
      name != "" &&
      numberOfCalories > 0 &&
      description != ""
    ) {
      let formData = new FormData();
      formData.append("name", name);
      formData.append("description", description);
      formData.append("numberOfCalories", numberOfCalories.toString());
      formData.append("imageFile", selectedFile);
      formData.append("type", mealType);
      let token = cookies.jwt;
      clientContext
        .post<IResult>(
          "api/Meals/Create",
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
  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files && event.target.files.length > 0) {
      setSelectedFile(event.target.files[0]);
    }
  };
  const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setName(event.target.value);
  };

  const handleDescriptionChange = (
    event: React.ChangeEvent<HTMLTextAreaElement>
  ) => {
    setDescription(event.target.value);
  };

  const handleNumberOfCaloriesChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setNumberOfCalories(parseInt(event.target.value));
  };
  const handleOptionChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setMealType(event.target.value);
  };
  return (
    <>
      <form
        onSubmit={handleSubmit}
        className="d-flex justify-content-center mt-5"
      >
        <div className="create-meal-form">
          <div className="create-meal-label">إضافة وجبة جديدة</div>
          <div className="input-container-on-meal-form">
            <input
              className="create-meal-input"
              type="text"
              placeholder="اسم الوجبة"
              dir="rtl"
              onChange={handleNameChange}
            />
            <div className="input-svg"></div>
          </div>

          <div className="input-container-on-meal-form">
            <input
              className="create-meal-input"
              type="number"
              placeholder="عدد السعرات الحرارية"
              dir="rtl"
              onChange={handleNumberOfCaloriesChange}
            />
            <div className="input-svg"></div>
          </div>

          <div className="input-container-on-meal-form">
            <textarea
              className="form-control meal-description"
              placeholder="وصف الوجبة"
              rows={3}
              dir="rtl"
              onChange={handleDescriptionChange}
            ></textarea>

            <div className="input-svg"></div>
          </div>

          <div className="input-container-on-meal-form">
            <select
              value={mealType}
              className="form-select select-meal-type"
              aria-label="Default select example"
              dir="rtl"
              onChange={handleOptionChange}
            >
              <option value="NormalMeal">وجبة</option>
              <option value="Plate">صحن</option>
              <option value="Dinner">عشاء</option>
              <option value="BreakFast">فطور</option>
            </select>
          </div>
          <div className="input-container-on-meal-form">
            <label className="custum-file-upload" htmlFor="file">
              <div className="icon">
                <img className="file-image" src={fileIcon} alt="" />
              </div>
              <div className="text">
                <span>انقر لإضافة صورة للوجبة</span>
              </div>
              <input
                className="d-none"
                type="file"
                id="file"
                onChange={handleFileChange}
              />
            </label>
          </div>

          <button type="submit" className="submit-create-meal">
            <p> حفظ الوجبة</p>
          </button>
        </div>
      </form>
    </>
  );
}
export default CreateMeal;
