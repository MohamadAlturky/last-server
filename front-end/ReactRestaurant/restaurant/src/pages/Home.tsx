import "./home.css";
import MealsReservations from "../components/mealsReservations/MealsReservations";
import { useContext, useEffect, useState } from "react";
import { useOutletContext } from "react-router-dom";
import { OutletContextType } from "../authentication/models/OutletContextType";
import { useCookies } from "react-cookie";
import ClientContext from "../contexts/api/ClientContext";
import Result from "../models/Result";
import { ResourceContext } from "../contexts/resource/ResourceContext";
import { motion } from "framer-motion";

function Home() {
  const resources = useContext(ResourceContext);
  const [week, setWeek] = useState<number>(0);
  const { outLetProps } = useOutletContext<OutletContextType>();
  const [data, setData] = useState<WeeklyMeals>();
  const context = new ClientContext(outLetProps.error401Handler);
  const [cookies] = useCookies(["jwt"]);

  let token = cookies.jwt;

  const handleWeekChange = (id: number) => {
    setWeek(id);
  };

  useEffect(() => {
    context
      .get<Result<WeeklyMeals>>(
        "api/Meals/GetWeeklyMeals?WeekNumber=" + week.toString(),
        token
      )
      .then((response) => {
        setData(response.data.value);
        // console.log(response.data);
      })
      .catch(() => {
        // console.log(error);
      });
  }, [week]);

  // console.log("Before Render");

  // console.log(data);

  return (
    <>
      <div className="main-page">
        <div className="container mt-4">
          <div className="row flex-wrap-reverse">
            <div className="col-sm-12 col-md-7 d-flex justify-content-evenly">
              {resources.Weeks.map((entry) => (
                <>
                  {entry.id == week && (
                    <div
                      className="weeks green"
                      onClick={() => handleWeekChange(entry.id)}
                    >
                      {entry.name}
                    </div>
                  )}
                  {entry.id != week && (
                    <div
                      className="weeks"
                      onClick={() => handleWeekChange(entry.id)}
                    >
                      {entry.name}
                    </div>
                  )}
                </>
              ))}
            </div>
            <div className="col-sm-12 col-md-4 d-flex justify-content-end">
              <div className="weeks orange">التسجيل الأسبوعي</div>
            </div>
          </div>

          <div className="links-spacer"></div>
          {/* <div className="do-process">تنفيذ العملية</div> */}
          <motion.div
            className="row"
            // initial={{ opacity: 0 }}
            // animate={{ opacity: 100 }}
            // transition={{ duration: 3, ease: "easeInOut" }}
          >
            {data?.dailies.map((day, index) => (
              <MealsReservations index={index} dailyMeals={day} />
            ))}
          </motion.div>
        </div>
      </div>
    </>
  );
}

export default Home;
