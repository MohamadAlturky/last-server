import "./doubledMeal.css";
import serverUrl from "../../configurations/apiConfiguration.json";
import noEat from "../../assets/cooking-svgrepo-com.svg";
import { ResourceContext } from "../../contexts/resource/ResourceContext";
import { useContext, useEffect, useState } from "react";
import { OutletContextType } from "../../authentication/models/OutletContextType";
import { useOutletContext } from "react-router-dom";
import { useCookies } from "react-cookie";
import ClientContext from "../../contexts/api/ClientContext";
import Swal from "sweetalert2";
import Result from "../../models/Result";
interface DoubledMealProps {
  leftMeal: MealReadModel | undefined;
  rightMeal: MealReadModel | undefined;
  leftType: string;
  rightType: string;
  callBack: (
    mealId: number,
    reservationId: number,
    reservationStatus: string,
    add: number
  ) => void;
}

function DoubledMeal(props: DoubledMealProps) {
  const resources = useContext(ResourceContext);
  const [leftReservationStatus, setLeftReservationStatus] =
    useState<string>("Empty");
  const [rightReservationStatus, setRightReservationStatus] =
    useState<string>("Empty");

  const { outLetProps } = useOutletContext<OutletContextType>();
  const [cookies] = useCookies(["jwt"]);
  const clientContext = new ClientContext(outLetProps.error401Handler);

  const handleLeftCreateReservation = () => {
    if (leftReservationStatus == "Reserved") {
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

          let token = cookies.jwt;
          let formData = new FormData();
          if (props.leftMeal)
            formData.append("orderedMealId", props.leftMeal?.id.toString());
          Swal.showLoading();

          clientContext
            .post<Result<CreateReservationResponse>>(
              "api/Reservations/Create",
              token,
              formData,
              "multipart/form-data"
            )
            .then((response) => {
              if (props.leftMeal) {
                props.callBack(
                  props.leftMeal?.id,
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
              console.log(err);
            });
        } else if (result.isDenied) {
          Swal.fire("لم يتم الحجز", "", "info");
        }
      });
    }
  };

  const handleLeftCancel = () => {
    if (leftReservationStatus == "Empty") {
      Swal.fire({
        title: "خطأ",
        text: "انت لست مسجلاً أصلاً على هذه الوجبة",
        icon: "error",
        confirmButtonText: "فهمت",
      });
    } else if (leftReservationStatus == "OnTheCanceledListButNotCanceledYet") {
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
          if (props.leftMeal) {
            formData.append(
              "reservationId",
              props.leftMeal.reservationId.toString()
            );
          }
          Swal.showLoading();

          clientContext
            .post<Result<string>>(
              "api/Reservations/Cancel",
              token,
              formData,
              "multipart/form-data"
            )
            .then((response) => {
              if (props.leftMeal) {
                props.callBack(props.leftMeal?.id, 0, response.data.value, 1);
              }

              let newValue = response.data.value.toString();
              console.log("newValue", newValue);

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

  ////////
  //////
  //////
  //////
  //////
  //////
  //
  ///

  const handleRightCreateReservation = () => {
    if (rightReservationStatus == "Reserved") {
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

          let token = cookies.jwt;
          let formData = new FormData();
          if (props.rightMeal)
            formData.append("orderedMealId", props.rightMeal?.id.toString());

          clientContext
            .post<Result<CreateReservationResponse>>(
              "api/Reservations/Create",
              token,
              formData,
              "multipart/form-data"
            )
            .then((response) => {
              if (props.rightMeal) {
                props.callBack(
                  props.rightMeal?.id,
                  response.data.value.reservationId,
                  response.data.value.reservationStatus,
                  -1
                );
              }
              // Swal.fire({
              //   title: "تمام",
              //   icon: "success",
              //   confirmButtonText: "حسناً",
              // });
            })
            .catch((err) => {
              console.log(err);
            });
        } else if (result.isDenied) {
          Swal.fire("لم يتم الحجز", "", "info");
        }
      });
    }
  };

  const handleRightCancel = () => {
    if (rightReservationStatus == "Empty") {
      Swal.fire({
        title: "خطأ",
        text: "انت لست مسجلاً أصلاً على هذه الوجبة",
        icon: "error",
        confirmButtonText: "فهمت",
      });
    } else if (rightReservationStatus == "OnTheCanceledListButNotCanceledYet") {
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
          if (props.rightMeal) {
            formData.append(
              "reservationId",
              props.rightMeal.reservationId.toString()
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
              if (props.rightMeal) {
                props.callBack(props.rightMeal?.id, 0, response.data.value, 1);
              }
              // Swal.fire({
              //   title: "تمام",
              //   icon: "success",
              //   confirmButtonText: "حسناً",
              // });
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
  //
  //
  //
  //
  //
  //
  //
  //
  //
  //
  //
  //
  //
  //
  //
  //

  useEffect(() => {
    if (props.leftMeal != undefined) {
      setLeftReservationStatus(props.leftMeal.reservationStatus);
    } else {
      setLeftReservationStatus("Empty");
    }
    if (props.rightMeal != undefined) {
      setRightReservationStatus(props.rightMeal.reservationStatus);
    } else {
      setRightReservationStatus("Empty");
    }
  }, [props]);

  return (
    <>
      <div className="d-flex justify-content-center doubled-meal-container position-relative">
        {(props.rightMeal?.reservationStatus == "Reserved" ||
          props.rightMeal?.reservationStatus == "Consumed") && (
          <div className="left-cover"></div>
        )}
        {(props.leftMeal?.reservationStatus == "Reserved" ||
          props.leftMeal?.reservationStatus == "Consumed") && (
          <div className="right-cover"></div>
        )}
        <div className="d-flex flex-column justify-content-end">
          <div className="d-flex justify-content-center m-1">
            <div className="doubled-meal-card-image ">
              <div className="left-meal-header">{props.leftType}</div>
              <div className="right-meal-header">{props.rightType}</div>
              {props.leftMeal && (
                <div
                  className={
                    "annotation-doubled " +
                    resources.styles.find((e) => e.id == leftReservationStatus)
                      ?.style
                  }
                >
                  {
                    resources.styles.find((e) => e.id == leftReservationStatus)
                      ?.message
                  }
                </div>
              )}
              <img
                src={
                  (props.leftMeal &&
                    serverUrl.baseUrl + props.leftMeal?.imagePath) ||
                  noEat
                }
                alt=""
              />
            </div>
            <div className="doubled-meal-card-image ">
              {props.rightMeal && (
                <div
                  className={
                    "annotation-doubled " +
                    resources.styles.find((e) => e.id == rightReservationStatus)
                      ?.style
                  }
                >
                  {
                    resources.styles.find((e) => e.id == rightReservationStatus)
                      ?.message
                  }
                </div>
              )}
              <img
                src={
                  (props.rightMeal &&
                    serverUrl.baseUrl + props.rightMeal?.imagePath) ||
                  noEat
                }
                alt=""
              />
            </div>
          </div>
          <div className="d-flex justify-content-center m-1">
            <div className="doubled-meal-card-name">
              {props.leftMeal?.name || "عذرا لا يوجد وجبة متاحة"}
            </div>
            <div className="doubled-meal-card-name">
              {props.rightMeal?.name || "عذرا لا يوجد وجبة متاحة"}
            </div>
          </div>
          <div className="d-flex justify-content-center m-1 mb-3">
            <div className="doubled-meal-card-info">
              <div className="meal-price">
                {(props.leftMeal &&
                  props.leftMeal?.preparedCount -
                    props.leftMeal?.reservationsCount +
                    ": العدد المتوفر") || <p className="font-30">&#8734;</p>}
              </div>
              <div className="d-flex justify-content-center align-items-center">
                {props.leftMeal?.reservationStatus == "Empty" && (
                  <div
                    className="meal-plus-minus"
                    onClick={() => {
                      if (props.leftMeal) {
                        handleLeftCreateReservation();
                      }
                    }}
                  >
                    التسجيل
                    {/* <img src={plus} alt="" /> */}
                  </div>
                )}
                {props.leftMeal?.reservationStatus == "Reserved" && (
                  <div
                    className="meal-plus-minus-cancel"
                    onClick={() => {
                      if (props.leftMeal) {
                        handleLeftCancel();
                      }
                    }}
                  >
                    إلغاء الوجبة
                    {/* <img src={minus} alt="" /> */}
                  </div>
                )}
              </div>
            </div>
            <div className="doubled-meal-card-info">
              <div className="meal-price">
                {(props.rightMeal &&
                  props.rightMeal?.preparedCount -
                    props.rightMeal?.reservationsCount +
                    ": العدد المتوفر") || <p className="font-30">&#8734;</p>}
              </div>
              <div className="d-flex justify-content-center align-items-center">
                {props.rightMeal?.reservationStatus == "Empty" && (
                  <div
                    className="meal-plus-minus"
                    onClick={() => {
                      if (props.rightMeal) {
                        handleRightCreateReservation();
                      }
                    }}
                  >
                    التسجيل
                    {/* <img src={plus} alt="" /> */}
                  </div>
                )}
                {props.rightMeal?.reservationStatus == "Reserved" && (
                  <div
                    className="meal-plus-minus-cancel"
                    onClick={() => {
                      if (props.rightMeal) {
                        handleRightCancel();
                      }
                    }}
                  >
                    إلغاء الوجبة
                    {/* <img src={minus} alt="" /> */}
                  </div>
                )}
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default DoubledMeal;
