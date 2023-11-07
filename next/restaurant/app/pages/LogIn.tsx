import NavBar from "../components/navBar/NavBar";
import LogIn from "../components/logIn/LogIn";
import SideImage from "../components/logIn/SideImage";
import "./login.css";
import Footer from "../components/footer/Footer";

function LogInPage() {
  return (
    <>
      {/* {isLoading && <Loader />} */}
      <NavBar
        styles={"fixed-top top-nav-container"}
        backgroundStyle="nav-color"
      ></NavBar>

      <div className="container d-flex justify-content-center login-content">
        <div className="d-flex justify-content-center content">
          <LogIn></LogIn>
          <div className="sider">
            <SideImage></SideImage>
          </div>
        </div>
      </div>
      <Footer></Footer>
    </>
  );
}

export default LogInPage;
