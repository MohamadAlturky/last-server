import { useState } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "./changePassword.css";
import Swal from "sweetalert2";
import IResult from "../../models/IResult";
import { OutletContextType } from "../../authentication/models/OutletContextType";
import { useOutletContext } from "react-router-dom";
import ClientContext from "../../contexts/api/ClientContext";
import { useCookies } from "react-cookie";
import { AnimatePresence, motion } from "framer-motion";

export default function ChangePassword() {
  const [oldPassword, setOldPassword] = useState<string>("");
  const [newPassword, setNewPassword] = useState<string>("");
  const [confirmPassword, setConfirmPassword] = useState<string>("");
  const [cookies] = useCookies(["jwt"]);
  const { outLetProps } = useOutletContext<OutletContextType>();
  const clientContext = new ClientContext(outLetProps.error401Handler);

  function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    if (newPassword != confirmPassword) {
      Swal.fire({
        title: "خطأ",
        text: "لا يوجد تطابق بين كلمة المرور وتأكيد كلمة المرور حاول ثانيةً",
        icon: "error",
        confirmButtonText: "فهمت",
      });
    } else if (
      oldPassword != "" &&
      newPassword != "" &&
      confirmPassword != ""
    ) {
      let formData = new FormData();
      formData.append("oldPassword", oldPassword);
      formData.append("newPassword", newPassword);
      let token = cookies.jwt;
      clientContext
        .put<IResult>(
          "api/Authentication/ChangePassword",
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
  return (
    <AnimatePresence mode="wait">
      <motion.div
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        exit={{ opacity: 0 }}
        className="container p-0"
        transition={{ duration: 3 }}
      >
        <div className="container d-flex justify-content-center margin-for-password">
          <div className="d-flex justify-content-center content-for-form-password">
            <form onSubmit={handleSubmit} className="change-password-form">
              <div className="change-password-label">تغيير كلمة المرور</div>
              <div className="user-name-container-for-password m-2">
                <input
                  className="password-input"
                  type="password"
                  placeholder="كلمة المرور القديمة"
                  dir="rtl"
                  value={oldPassword}
                  onChange={(text) => setOldPassword(text.target.value)}
                  autoFocus
                />
              </div>
              <div className="user-name-container-for-password m-2">
                <input
                  className="password-input"
                  type="password"
                  placeholder="كلمة المرور الجديدة"
                  dir="rtl"
                  onChange={(text) => setNewPassword(text.target.value)}
                  value={newPassword}
                />
              </div>

              <div className="user-name-container-for-password m-2 mb-4">
                <input
                  className="password-input"
                  type="password"
                  dir="rtl"
                  placeholder="تأكيد كلمة المرور"
                  value={confirmPassword}
                  onChange={(text) => setConfirmPassword(text.target.value)}
                />
              </div>

              <button type="submit" className="submit-change-password">
                <p>تنفيذ العملية</p>
              </button>
            </form>
            <div className="sider">
              <div className="image-container-for-password">
                <div className="welcome-section-for-password">
                  Man الأمان يا{" "}
                </div>
              </div>
            </div>
          </div>
        </div>
      </motion.div>
    </AnimatePresence>
  );
}
