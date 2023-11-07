import RequirementProps from "./RequirementProps";
import { Navigate, Outlet, useLocation } from "react-router-dom";
import UnAuthrized from "../../pages/UnAuthorized";
import jwt_decode from "jwt-decode";
import AccessTokenDTO from "../models/AccessTokenDTO";
import { useCookies } from "react-cookie";
import NavBar from "../../components/navBar/NavBar";
import Footer from "../../components/footer/Footer";
import Options from "../../components/options/Options";
import { useEffect, useState } from "react";
import { OutletContextType } from "../models/OutletContextType";
import data from "../../configurations/permissions.json";

function Requirement(props: RequirementProps) {
  const location = useLocation();
  const [cookies, setCookie] = useCookies(["jwt"]);
  let accessToken = cookies.jwt ? cookies.jwt : "";
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(
    accessToken != ""
  );
  const [complete, setComplete] = useState<boolean>(false);
  const [isAuthorized, setIsAuthorized] = useState<boolean>(false);
  const [userType, setUserType] = useState<
    "Admin" | "Customer" | "ReservationConsumer" | "Accountant"
  >("Customer");
  const styles = "fixed-top top-nav-container";
  const backgroundStyle = "nav-color";

  function error401Handler(): void {
    setCookie("jwt", "");
    accessToken = "";
    setIsAuthenticated(false);
    setComplete(true);
  }

  let outLetProps = {
    error401Handler,
  };
  location.state = location.pathname;

  // useEffect(() => {
  //   if (accessToken == "") {
  //     setIsAuthenticated(false);
  //   }
  // }, []);

  // useEffect(() => {
  //   if (accessToken != "") {
  //     const decodedTokenPayload = jwt_decode<AccessTokenDTO>(accessToken);
  //     console.log(decodedTokenPayload);

  //     let permissions: string[] = decodedTokenPayload.permissions;

  //     for (let ind = 0; ind < permissions.length; ind++) {
  //       if (permissions[ind] == props.permission) {
  //         setIsAuthorized(true);
  //       }
  //     }
  //     for (let ind = 0; ind < permissions.length; ind++) {
  //       if (permissions[ind] == data.permissions.CreateSystemInformation) {
  //         setUserType("Admin");
  //         break;
  //       }
  //       if (permissions[ind] == data.permissions.ConsumeReservations) {
  //         setUserType("ReservationConsumer");
  //         break;
  //       }
  //       if (permissions[ind] == data.permissions.EditBalances) {
  //         setUserType("Accountant");
  //         break;
  //       }
  //       if (permissions[ind] == data.permissions.ReadContent) {
  //         setUserType("Customer");
  //         break;
  //       }
  //     }
  //   }
  // }, [isAuthenticated]);

  useEffect(() => {
    if (accessToken != "") {
      const decodedTokenPayload = jwt_decode<AccessTokenDTO>(accessToken);
      console.log(decodedTokenPayload);

      let permissions: string[] = decodedTokenPayload.permissions;

      if (props.permission == data.permissions.NoPermissionNeeded) {
        setIsAuthorized(true);
        setComplete(true);
      }
      if (permissions.includes(props.permission)) {
        setIsAuthorized(true);
        setComplete(true);
      } else {
        setComplete(true);
      }

      if (permissions.includes(data.permissions.CreateSystemInformation)) {
        setUserType("Admin");
      } else if (permissions.includes(data.permissions.ConsumeReservations)) {
        setUserType("ReservationConsumer");
      } else if (permissions.includes(data.permissions.EditBalances)) {
        setUserType("Accountant");
      } else if (permissions.includes(data.permissions.ReadContent)) {
        setUserType("Customer");
      }
    }
  }, [isAuthenticated]);

  return (
    <>
      {/* {isLoading && <Loader />} */}
      {!isAuthenticated && <Navigate to="/login" state={{ from: location }} />}
      {complete && !isAuthorized && isAuthenticated && <UnAuthrized />}
      {complete && isAuthenticated && isAuthorized && (
        <>
          <NavBar styles={styles} backgroundStyle={backgroundStyle}></NavBar>
          {<Options itemNumber={props.activeLink} type={userType}></Options>}

          <div className="container mt-5 p-0">
            <Outlet context={{ outLetProps } satisfies OutletContextType} />
          </div>

          <Footer></Footer>
        </>
      )}
    </>
  );
}

export default Requirement;
