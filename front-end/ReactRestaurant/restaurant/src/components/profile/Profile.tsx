import { useEffect, useState } from "react";
import "./profile.css";
import { useNavigate, useOutletContext } from "react-router-dom";
import { OutletContextType } from "../../authentication/models/OutletContextType";
import { useCookies } from "react-cookie";
import ClientContext from "../../contexts/api/ClientContext";
import hi2 from "../../assets/hi2.svg";
import logOut from "../../assets/log-out.svg";
import Result from "../../models/Result";
export default function Profile() {
  const [user, setUser] = useState<UserDTO>();
  const { outLetProps } = useOutletContext<OutletContextType>();
  const context = new ClientContext(outLetProps.error401Handler);
  const [cookies, setCookies] = useCookies(["jwt"]);
  const navigate = useNavigate();
  let token = cookies.jwt;
  function handleLogOut() {
    setCookies("jwt", "");
    navigate("/");
  }
  useEffect(() => {
    context
      .get<Result<UserDTO>>("api/Authentication/GetAccountInformation", token)
      .then((response) => {
        setUser(response.data.value);
        console.log(response.data);
      });
  }, []);
  return (
    <>
      {user && (
        <div className="container">
          <div className="d-flex justify-content-center mt-5">
            <div className="scale-sider"></div>
            <div className="edit-pricing-form min-height-good position-relative">
              {/* <img className="hi" src={hi} alt="" /> */}
              <img className="hi2" src={hi2} alt="" />
              <div className="create-meal-label">
                أهلا {user.firstName + " " + user.lastName}
              </div>
              <div className="input-container-on-meal-form m-1">
                <div className="d-flex justify-content-end claim">
                  رصيدك الحالي
                </div>
                <div className="d-flex justify-content-end">{user.balance}</div>
              </div>
              <div className="input-container-on-meal-form m-1">
                <div className="d-flex justify-content-end claim">
                  بريد المعهد العالي
                </div>
                <div className="d-flex justify-content-end">
                  {user.hiastMail}
                </div>
              </div>
              <div className="mb-5"></div>
              {/* <div className="input-container-on-meal-form mb-4 mt-1">
                <div className="d-flex justify-content-end claim">
                  معرف الجهاز
                </div>
                <div className="d-flex justify-content-end">
                  {user.macAddress}
                </div>
              </div> */}

              <div className="log-title">{"⬅️ "}تسجيل الخروج</div>
              <img
                className="log-out"
                src={logOut}
                alt=""
                onClick={handleLogOut}
              />
            </div>
          </div>
        </div>
      )}
    </>
  );
}
