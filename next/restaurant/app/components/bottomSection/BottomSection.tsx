import Card from "../card/Card";
import home1 from "../../images/bamya.png";
import home2 from "../../images/fool.png";
import home3 from "../../images/kordonblue.png";
import home4 from "../../images/shish.png";
import "./bottomSection.css";
import "bootstrap/dist/css/bootstrap.min.css";
function BottomSection() {
  return (
    <div className="bottom-section-content">
      <div className="bottom-section container">
        <div>
          <div className="header-note">أشهى الوجبات</div>
          <div className="bottom-note">انت تشتهي ونحن نقدم مايكفي</div>
        </div>

        <div className="slider">
          <div className=" row justify-content-between align-items-center cards-container">
            <div className="d-flex justify-content-center  col-md-6 col-sm-12 col-lg-3 ">
              <Card imageName="بامية ورز" imageUrl={home1} />
            </div>
            <div className="d-flex justify-content-center  col-md-6 col-sm-12 col-lg-3 ">
              <Card imageName="رز بفول" imageUrl={home2} />
            </div>
            <div className="d-flex justify-content-center  col-md-6 col-sm-12 col-lg-3 ">
              <Card imageName="كوردون بلو" imageUrl={home3} />
            </div>
            <div className="d-flex justify-content-center  col-md-6 col-sm-12 col-lg-3 ">
              <Card imageName="شيش طاووك" imageUrl={home4} />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
export default BottomSection;
