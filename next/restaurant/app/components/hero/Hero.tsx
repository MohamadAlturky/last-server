import "./hero.css";
import "bootstrap/dist/css/bootstrap.min.css";

import NavBar from "../navBar/NavBar";
import { useNavigate } from "react-router-dom";

function Hero() {
  let navigate = useNavigate();

  function handleLogIn() {
    console.log("Navigate");
    navigate("/login");
  }
  function handleGoToSchedule() {
    console.log("Navigate");
    navigate("/MealSchedule");
  }
  return (
    <>
      <div className="d-flex justify-content-center ">
        <div className="hero">
          <div className="filter"></div>
          <NavBar styles="top-nav-container" backgroundStyle=""></NavBar>
          <div className="notes">
            <div className="top-note">عينك مو إلك</div>
            <div className="down-note">بطنك مو إلك؟</div>
            <div className="down-option">
              <div className="log-option" onClick={handleLogIn}>
                تسجيل الدخول
              </div>
              <div className="food-schedule" onClick={handleGoToSchedule}>
                برنامج الطعام
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default Hero;
