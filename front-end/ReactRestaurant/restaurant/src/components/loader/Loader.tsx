import "./loader.css";
import { PuffLoader } from "react-spinners";

function Loader() {
  return (
    <div className="loader-container">
      <PuffLoader color="#36d7b7" />
    </div>
  );
}
export default Loader;
