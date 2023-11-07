import { useContext, useEffect, useState } from "react";
import { ResourceContext } from "../../contexts/resource/ResourceContext";
import MealDay from "../mealDay/MealDay";
import "./mealSchedule.css";
import Result from "../../models/Result";
import ClientContext from "../../contexts/api/ClientContext";
import { useOutletContext } from "react-router-dom";
import { OutletContextType } from "../../authentication/models/OutletContextType";
import { useCookies } from "react-cookie";
export default function MealSchedule() {
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
        "api/Meals/GetMealsSchedule?WeekNumber=" + week.toString(),
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
  return (
    <>
      <div className="row flex-wrap-reverse mt-3">
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
          <div className="weeks orange">جدول الطعام الأسبوعي</div>
        </div>
      </div>
      <div className="meal-schedule-container mt-3 p-4">
        <MealDay meals={data} weekNumber={week}></MealDay>
      </div>
    </>
  );
}
