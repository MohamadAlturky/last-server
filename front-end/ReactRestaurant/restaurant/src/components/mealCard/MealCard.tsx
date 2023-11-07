import serverUrl from "../../configurations/apiConfiguration.json";
import "./mealCard.css";
import { useContext, useEffect, useState } from "react";
import noEat from "../../assets/porridge-svgrepo-com.svg";
import { ResourceContext } from "../../contexts/resource/ResourceContext";
import Swal from "sweetalert2";
import { OutletContextType } from "../../authentication/models/OutletContextType";
import { useOutletContext } from "react-router-dom";
import ClientContext from "../../contexts/api/ClientContext";
import { useCookies } from "react-cookie";
import Result from "../../models/Result";
import Loader from "../loader/Loader";
interface MealCardProps {
  meal: MealReadModel | undefined;
  type: string;
  callBack: (
    mealId: number,
    reservationId: number,
    reservationStatus: string,
    add: number
  ) => void;
}

function MealCard(props: MealCardProps) {
  const resources = useContext(ResourceContext);
  const [reservationStatus, setReservationStatus] = useState<string>("Empty");
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const { outLetProps } = useOutletContext<OutletContextType>();
  const [cookies] = useCookies(["jwt"]);
  const clientContext = new ClientContext(outLetProps.error401Handler);
  const handleCreateReservation = () => {
    if (reservationStatus == "Reserved") {
      Swal.fire({
        title: "خطأ",
        text: "انت مسجل بلفعل على هذه الوجبة",
        icon: "error",
        confirmButtonText: "فهمت",
      });
    } else {
      Swal.fire({
        title: "هل أنت متأكد من تثبيت الحجز؟",
        showDenyButton: true,
        // showCancelButton: true,
        confirmButtonText: "المتابعة",
        denyButtonText: `إلغاء العملية`,
      }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
          // Swal.fire("Saved!", "", "success");
          setIsLoading(true);
          let token = cookies.jwt;
          let formData = new FormData();
          if (props.meal)
            formData.append("orderedMealId", props.meal?.id.toString());

          clientContext
            .post<Result<CreateReservationResponse>>(
              "api/Reservations/Create",
              token,
              formData,
              "multipart/form-data"
            )
            .then((response) => {
              setIsLoading(false);
              if (props.meal) {
                props.callBack(
                  props.meal?.id,
                  response.data.value.reservationId,
                  response.data.value.reservationStatus,
                  -1
                );
              }
              const Toast = Swal.mixin({
                toast: true,
                position: "top-end",
                showConfirmButton: false,
                timer: 1000,
                timerProgressBar: true,
                didOpen: (toast) => {
                  toast.addEventListener("mouseenter", Swal.stopTimer);
                  toast.addEventListener("mouseleave", Swal.resumeTimer);
                },
              });

              Toast.fire({
                icon: "success",
                title: "تم",
              });
            })
            .catch((err) => {
              setIsLoading(false);
              console.log(err);
            });
        } else if (result.isDenied) {
          Swal.fire("لم يتم الحجز", "", "info");
        }
      });
    }
  };

  const handleCancel = () => {
    if (reservationStatus == "Empty") {
      Swal.fire({
        title: "خطأ",
        text: "انت لست مسجلاً أصلاً على هذه الوجبة",
        icon: "error",
        confirmButtonText: "فهمت",
      });
    } else if (reservationStatus == "OnTheCanceledListButNotCanceledYet") {
      Swal.fire({
        title: "خطأ",
        text: "الوجبة بالفعل ملغية",
        icon: "warning",
        confirmButtonText: "فهمت",
      });
    } else {
      Swal.fire({
        title: "هل أنت متأكد من أنك تريد إلغاء الحجز؟",
        showDenyButton: true,
        // showCancelButton: true,
        confirmButtonText: "المتابعة",
        denyButtonText: `إلغاء العملية`,
      }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
          // Swal.fire("Saved!", "", "success");

          let token = cookies.jwt;
          let formData = new FormData();
          if (props.meal) {
            formData.append(
              "reservationId",
              props.meal.reservationId.toString()
            );
          }

          clientContext
            .post<Result<string>>(
              "api/Reservations/Cancel",
              token,
              formData,
              "multipart/form-data"
            )
            .then((response) => {
              if (props.meal) {
                props.callBack(props.meal?.id, 0, response.data.value, 1);
              }

              let newValue = response.data.value.toString();
              console.log("newValue", newValue);

              setReservationStatus(newValue);
              console.log(response);
              const Toast = Swal.mixin({
                toast: true,
                position: "top-end",
                showConfirmButton: false,
                timer: 1000,
                timerProgressBar: true,
                didOpen: (toast) => {
                  toast.addEventListener("mouseenter", Swal.stopTimer);
                  toast.addEventListener("mouseleave", Swal.resumeTimer);
                },
              });

              Toast.fire({
                icon: "success",
                title: "تم",
              });
            })
            .catch((err) => {
              console.log(err);
            });
        } else if (result.isDenied) {
          Swal.fire("لم يتم إلغاء الحجز", "", "info");
        }
      });
    }
  };

  useEffect(() => {
    if (props.meal) {
      let newValue = props.meal.reservationStatus;
      setReservationStatus(newValue);
    }
  }, [props]);
  console.log("props.meal?.name  ", props.meal?.name);

  console.log("reservationStatus  ", reservationStatus);

  return (
    <>
      {isLoading && <Loader />}
      {props.meal !== undefined && (
        <div className=" d-flex flex-column align-items-center justify-content-center position-relative">
          <div className="meal-card">
            <div className="header-for-card">{props.type}</div>
            {reservationStatus && (
              //  reservationStatus != "Empty"&&
              <div
                className={
                  "annotation " +
                  resources.styles.find((e) => e.id == reservationStatus)?.style
                }
              >
                {
                  resources.styles.find((e) => e.id == reservationStatus)
                    ?.message
                }
              </div>
            )}
            <div className="meal-image m-1">
              <img src={serverUrl.baseUrl + props.meal.imagePath} alt="" />
            </div>
            <div className="meal-name m-1">{props.meal.name}</div>
            <div className="meal-info m-1 mb-3">
              <div className="meal-price">
                {props.meal.preparedCount - props.meal.reservationsCount}
                {": العدد المتوفر"}
              </div>
              <div className="d-flex justify-content-center align-items-center">
                {props.meal.reservationStatus == "Empty" && (
                  <div
                    className="meal-plus-minus"
                    onClick={handleCreateReservation}
                  >
                    التسجيل
                  </div>
                )}
                {props.meal.reservationStatus == "Reserved" && (
                  <div
                    className="meal-plus-minus-cancel"
                    onClick={handleCancel}
                  >
                    إلغاء الوجبة
                  </div>
                )}
              </div>
            </div>
          </div>
        </div>
      )}
      {props.meal == undefined && (
        <>
          <div className=" d-flex flex-column align-items-center justify-content-center">
            <div className="meal-card">
              <div className="header-for-card">{props.type}</div>
              <div className="meal-image m-1">
                <img src={noEat} alt="" />
              </div>
              <div className="meal-name m-1"> عذرا لا يوجد وجبة متاحة</div>
              <div className="meal-info m-1 mb-3">
                <div className="meal-price font-30">&#8734;</div>
                <div className="d-flex justify-content-center align-items-center">
                  {/* <div className="meal-plus-minus">
                    <img src={plus} alt="" />
                  </div>
                  <div className="meal-plus-minus">
                    <img src={minus} alt="" />
                  </div> */}
                </div>
              </div>
            </div>
          </div>
        </>
      )}
    </>
  );
}
export default MealCard;
