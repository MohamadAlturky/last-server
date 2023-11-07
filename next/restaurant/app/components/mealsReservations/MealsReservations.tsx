import { useContext, useEffect, useState } from "react";
import DoubledMeal from "../doubledMeal/DoubledMeal";
import MealCard from "../mealCard/MealCard";
import { useMediaQuery } from "@mui/material";
import { ResourceContext } from "../../contexts/resource/ResourceContext";
import "./mealsReservation.css";

interface MealsReservationsProps {
  dailyMeals: DailyMeals;
  index: number;
}

function MealsReservations(props: MealsReservationsProps) {
  const [daily, setDaily] = useState<DailyMeals>(props.dailyMeals);

  useEffect(() => {
    setDaily(props.dailyMeals);
  }, [props]);
  const callBack = (
    mealId: number,
    reservationId: number,
    reservationStatus: string,
    add: number
  ) => {
    let meal = daily.meals.find((item) => item.id == mealId);
    if (meal) {
      let newMeal: MealReadModel = {
        description: meal.description,
        id: meal.id,
        atDay: meal.atDay,
        customerCanCancel: meal.customerCanCancel,
        imagePath: meal.imagePath,
        lastNumberInQueue: meal.lastNumberInQueue,
        mealId: meal.mealId,
        name: meal.name,
        numberOfCalories: meal.numberOfCalories,
        preparedCount: meal.preparedCount + add,
        reservationId: reservationId,
        reservationsCount: meal.reservationsCount,
        reservationStatus: reservationStatus,
        type: meal.type,
      };
      let newMeals = daily.meals.filter((item) => item.id != mealId);
      newMeals.push(newMeal);
      console.log("newMeals", newMeals);
      setDaily({ atDay: daily.atDay, meals: newMeals });
    }
  };

  const resources = useContext(ResourceContext);
  const isSmallScreen = useMediaQuery("(max-width: 767px)");
  const isMediumScreen = useMediaQuery("(max-width: 1400px)");
  const isLargeScreen = useMediaQuery("(max-width: 1560px)");

  let [small, setSmall] = useState(false);
  let [medium, setMedium] = useState(false);
  let [large, setLarge] = useState(true);

  let [breakFast, setBreakFast] = useState<MealReadModel | undefined>(
    undefined
  );

  let [meal, setMeal] = useState<MealReadModel | undefined>(undefined);
  let [plate, setPlate] = useState<MealReadModel | undefined>(undefined);
  let [dinner, setDinner] = useState<MealReadModel | undefined>(undefined);

  useEffect(() => {
    setBreakFast(daily.meals.find((e) => e.type == "BreakFast"));
    setMeal(daily.meals.find((e) => e.type == "NormalMeal"));
    setPlate(daily.meals.find((e) => e.type == "Plate"));
    setDinner(daily.meals.find((e) => e.type == "Dinner"));
  }, [daily]);

  useEffect(() => {
    if (isSmallScreen) {
      setSmall(true);
      setMedium(false);
      setLarge(false);
    } else if (isMediumScreen) {
      setMedium(true);
      setSmall(false);
      setLarge(false);
    } else if (isLargeScreen) {
      setLarge(true);
      setMedium(false);
      setSmall(false);
    }
  }, [isSmallScreen, isMediumScreen, isLargeScreen]);

  console.log(daily);
  console.log("breakFast", breakFast);

  return (
    <>
      <div className="d-flex justify-content-end align-items-center refreshable">
        <div className="day-container">
          {resources.DaysOfWeek[props.index]} | {props.dailyMeals.atDay}
        </div>
      </div>
      <>
        {large && (
          <div className="section mt-5 mb-5">
            <div className="row">
              <div className="col-sm-12 col-md-6 col-lg-3">
                <MealCard
                  callBack={callBack}
                  meal={dinner}
                  type={"العشاء"}
                ></MealCard>
              </div>
              <div className="col-sm-12 col-md-6 col-lg-6 d-flex align-items-center justify-content-center">
                <DoubledMeal
                  callBack={callBack}
                  leftMeal={plate}
                  rightMeal={meal}
                  leftType={"غداء صحن"}
                  rightType={"غداء وجبة"}
                ></DoubledMeal>
              </div>
              <div className="col-sm-12 col-md-6 col-lg-3 d-flex flex-column align-items-center justify-content-center">
                <MealCard
                  callBack={callBack}
                  meal={breakFast}
                  type={"الفطور"}
                ></MealCard>
              </div>
            </div>
          </div>
        )}

        {medium && (
          <div className="section mt-3">
            <div className="row">
              <div className="col-12 d-flex align-items-center justify-content-center mt-3 mb-5">
                <DoubledMeal
                  callBack={callBack}
                  leftMeal={breakFast}
                  leftType={"الفطور"}
                  rightType={"العشاء"}
                  rightMeal={dinner}
                ></DoubledMeal>
              </div>
              <div className="col-12 d-flex align-items-center justify-content-center mt-3 mb-5">
                <DoubledMeal
                  callBack={callBack}
                  leftMeal={plate}
                  rightMeal={meal}
                  leftType={"غداء صحن"}
                  rightType={"غداء وجبة"}
                ></DoubledMeal>
              </div>
            </div>
          </div>
        )}

        {small && (
          <div className="section mt-3">
            <div className="row">
              <div className="col-12 d-flex align-items-center justify-content-center ">
                <MealCard
                  type={"الفطور"}
                  callBack={callBack}
                  meal={breakFast}
                ></MealCard>
              </div>

              <div className="col-12 d-flex align-items-center justify-content-center ">
                <MealCard
                  type={"غداء وجبة"}
                  callBack={callBack}
                  meal={meal}
                ></MealCard>
              </div>

              <div className="col-12 d-flex align-items-center justify-content-center">
                <MealCard
                  type={"غداء صحن"}
                  callBack={callBack}
                  meal={plate}
                ></MealCard>
              </div>

              <div className="col-12 d-flex align-items-center justify-content-center ">
                <MealCard
                  type={"العشاء"}
                  callBack={callBack}
                  meal={dinner}
                ></MealCard>
              </div>
            </div>
          </div>
        )}
      </>
    </>
  );
}

export default MealsReservations;
