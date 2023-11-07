import "./options.css";
import "./toggler.css";
import userIcon from "./../../assets/person-2.svg";
import { ResourceContext } from "../../contexts/resource/ResourceContext";
import { useContext } from "react";
import { useNavigate } from "react-router-dom";
import NotificationIcon from "../notificationIcon/NotificationIcon";
interface OptionsProps {
  type: "Admin" | "Customer" | "Accountant" | "ReservationConsumer";
  itemNumber: number;
}

function Options(props: OptionsProps) {
  const resources = useContext(ResourceContext);
  const navigate = useNavigate();

  if (props.type == "Accountant") {
    return;
  }
  if (props.type == "ReservationConsumer") {
    return;
  }
  return (
    <>
      <div className="container">
        <div className="row position-relative  options-bar p-0 mt-5">
          <div className="helpers">
            <div>
              <NotificationIcon></NotificationIcon>
            </div>
            <div className="notification">
              {/* <p>اسم المستخدم</p> */}
              <img
                src={userIcon}
                alt=""
                onClick={() => {
                  navigate("/Profile");
                }}
              />
            </div>
          </div>
          <div className="icon-for-menu">
            <input id="checkbox2" type="checkbox" />
            <label className="toggle toggle2" htmlFor="checkbox2">
              <div id="bar4" className="bars"></div>
              <div id="bar5" className="bars"></div>
              <div id="bar6" className="bars"></div>
            </label>
            <div className="menu container">
              {props.type == "Admin" &&
                resources.AdminOptionsBar.map(
                  (link, index) =>
                    (index == props.itemNumber && (
                      <div
                        className="page-item m-2 active-link"
                        onClick={() => {
                          navigate(link.link);
                        }}
                      >
                        {link.name}
                      </div>
                    )) ||
                    (index != props.itemNumber && (
                      <div
                        className="page-item m-2"
                        onClick={() => {
                          navigate(link.link);
                        }}
                      >
                        {link.name}
                      </div>
                    ))
                )}

              {props.type == "Customer" &&
                resources.UserOptionsBar.map(
                  (link, index) =>
                    (index == props.itemNumber && (
                      <div
                        className="page-item m-2 active-link"
                        onClick={() => {
                          navigate(link.link);
                        }}
                      >
                        {link.name}
                      </div>
                    )) ||
                    (index != props.itemNumber && (
                      <div
                        className="page-item m-2"
                        onClick={() => {
                          navigate(link.link);
                        }}
                      >
                        {link.name}
                      </div>
                    ))
                )}
              {/* <div className="page-item m-1">تغير كلمة السر</div>;
              <div className="page-item m-1">كشف حساب</div>
              <div className="page-item m-1">حالة التسجيل</div>
              <div className="page-item m-1">تعديل التسجيلات</div>
              <div className="page-item m-1">التسجيل الاسبوعي</div>
              <div className="page-item m-1">استعراض برنامج الطعام</div> */}
            </div>
          </div>
          <div className="links">
            {props.type == "Admin" &&
              resources.AdminOptionsBar.map(
                (link, index) =>
                  (index == props.itemNumber && (
                    <div
                      className="page-item m-3 active-link"
                      onClick={() => {
                        navigate(link.link);
                      }}
                    >
                      {link.name}
                    </div>
                  )) ||
                  (index != props.itemNumber && (
                    <div
                      className="page-item m-2"
                      onClick={() => {
                        navigate(link.link);
                      }}
                    >
                      {link.name}
                    </div>
                  ))
              )}

            {props.type == "Customer" &&
              resources.UserOptionsBar.map(
                (link, index) =>
                  (index == props.itemNumber && (
                    <div
                      className="page-item m-3 active-link"
                      onClick={() => {
                        navigate(link.link);
                      }}
                    >
                      {link.name}
                    </div>
                  )) ||
                  (index != props.itemNumber && (
                    <div
                      className="page-item m-2"
                      onClick={() => {
                        navigate(link.link);
                      }}
                    >
                      {link.name}
                    </div>
                  ))
              )}
            {/* <div className="page-item">تغير كلمة السر</div>
            <div className="page-item">كشف حساب</div>
            <div className="page-item">حالة التسجيل</div>
            <div className="page-item">تعديل التسجيلات</div>
            <div className="page-item">التسجيل الاسبوعي</div>
            <div className="page-item">استعراض برنامج الطعام</div> */}
          </div>
        </div>
      </div>
    </>
  );
}
export default Options;
