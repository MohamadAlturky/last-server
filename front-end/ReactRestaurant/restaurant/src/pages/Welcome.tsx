import Hero from "../components/hero/Hero";
import "./welcome.css";
import "bootstrap/dist/css/bootstrap.min.css";
import BottomSection from "../components/bottomSection/BottomSection";
import Footer from "../components/footer/Footer";

function Welcome() {
  return (
    <>
      <Hero></Hero>
      <BottomSection></BottomSection>
      <Footer></Footer>
    </>
  );
}
export default Welcome;
