import { useContext } from "react";
import Arrow from "../../assets/arrow.svg";
import "./sideImage.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { ResourceContext } from "../../contexts/resource/ResourceContext";

function SideImage() {
  const resources = useContext(ResourceContext);

  return (
    <>
      <div className="image-container">
        <div className="welcome-section">{resources.logIn.welcome}</div>
        <div className="note-section flex justify-content-center">
          <div className="svg-container">
            <img src={Arrow} alt="Logo" />
          </div>
          <p>{resources.logIn.logInFromHere}</p>
        </div>
      </div>
    </>
  );
}

export default SideImage;
