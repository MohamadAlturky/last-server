import "./callUs.css";
import "./welcome.css";
import NavBar from "../components/navBar/NavBar";
import react from "../assets/react-svgrepo-com.svg";
// import ts from "../assets/typescript-logo-svgrepo-com.svg";
import git from "../assets/git-icon-logo-svgrepo-com.svg";
import cSharp from "../assets/csharp-svgrepo-com.svg";
import figma from "../assets/figma-svgrepo-com.svg";
import Footer from "../components/footer/Footer";
export default function CallUs() {
  return (
    <>
      <div className="d-flex justify-content-center ">
        <div className="hero new-background">
          <div className="filter"></div>
          <NavBar styles="top-nav-container" backgroundStyle=""></NavBar>
          <div className="notes">
            <div className="top-note">
              Developer <b className="blue">M.Turky</b>
            </div>
            <div className="down-note">
              Designer <b className="orange"> Rahma</b>
            </div>
          </div>
        </div>
      </div>
      <div className="bottom-section-content">
        <div className="bottom-section container">
          <div>
            <div className="header-note">BEST WEB PAGES</div>
            <div className="bottom-note">Work Is Easy, Don't Be Sad.......</div>
          </div>

          <div className="slider mb-5">
            <div className=" row justify-content-between align-items-center cards-container">
              <div className="d-flex justify-content-center  col-md-6 col-sm-12 col-lg-3 ">
                <img src={react} alt="" />
              </div>
              <div className="d-flex justify-content-center  col-md-6 col-sm-12 col-lg-3 ">
                <img src={git} alt="" />
              </div>
              <div className="d-flex justify-content-center  col-md-6 col-sm-12 col-lg-3 ">
                <img src={cSharp} alt="" />
              </div>
              <div className="d-flex justify-content-center  col-md-6 col-sm-12 col-lg-3 ">
                <img src={figma} alt="" />
              </div>
            </div>
          </div>
        </div>
      </div>
      <Footer></Footer>
    </>
  );
}
