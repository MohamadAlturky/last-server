import { useState } from "react";
import { useCookies } from "react-cookie";
import ClientContext from "../../contexts/api/ClientContext";
import { OutletContextType } from "../../authentication/models/OutletContextType";
import { useOutletContext } from "react-router-dom";
import Swal from "sweetalert2";
import IResult from "../../models/IResult";

export default function SendMessage() {
  const [title, setTitle] = useState<string>("");
  const [content, setContent] = useState<string>("");
  const [cookies] = useCookies(["jwt"]);
  const { outLetProps } = useOutletContext<OutletContextType>();
  const clientContext = new ClientContext(outLetProps.error401Handler);

  function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    if (content == "" || title == "") {
      Swal.fire({
        title: "خطأ",
        text: "عذرا لم تملأ جميع الحقول",
        icon: "error",
        confirmButtonText: "فهمت",
      });
    } else {
      let formData = new FormData();
      formData.append("title", title);
      formData.append("content", content);
      let token = cookies.jwt;
      clientContext
        .post<IResult>(
          "api/Chat/SendToAll",
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
    }
  }

  return (
    <>
      <div className="container">
        <div className="d-flex justify-content-center mt-5">
          <div className="scale-sider">
            {/* <Sider transformer="pricing-transformer"></Sider> */}
          </div>
          <form className="edit-pricing-form" onSubmit={handleSubmit}>
            <div className="create-meal-label">
              إرسال إشعار لجميع المستخدمين
            </div>

            <div className="input-container-on-meal-form">
              <input
                className="create-meal-input"
                type="text"
                placeholder="موضوع الرسالة"
                dir="rtl"
                onChange={(event) => setTitle(event.target.value)}
              />
            </div>

            <div className="input-container-on-meal-form">
              <textarea
                className="create-meal-input"
                placeholder="محتوى الرسالة"
                dir="rtl"
                onChange={(event) => setContent(event.target.value)}
              />
            </div>

            <button type="submit" className="submit-create-meal">
              <p> إرسال</p>
            </button>
          </form>
        </div>
      </div>
    </>
  );
}
