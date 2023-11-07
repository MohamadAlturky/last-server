import "./prepareMeal.css";
import Result from "../models/Result";
import { useEffect, useState } from "react";
import PrepareMealDatePicker from "../components/datepicker/PrepareMealDatePicker";
import dayjs, { Dayjs } from "dayjs";
import ClientContext from "../contexts/api/ClientContext";
import { OutletContextType } from "../authentication/models/OutletContextType";
import { useOutletContext } from "react-router-dom";
import { useCookies } from "react-cookie";
import { styled } from "@mui/material/styles";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import url from "../configurations/apiConfiguration.json";
import "./mealsTable.css";
import mealTypes from "../configurations/mealTypes.json";
import { motion } from "framer-motion";
import add from "../assets/plus.svg";
import { PrepareMealForm } from "../components/prepareMealForm/PrepareMealForm";
import MealChartCustomersReservations from "../components/charts/MealChartCustomersReservations";
import Swal from "sweetalert2";
import IResult from "../models/IResult";

const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    // backgroundColor: "#666",
    // backgroundColor: "#E4801E",
    backgroundColor: "#666",
    color: theme.palette.common.white,
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 20,
  },
}));

const StyledTableRow = styled(TableRow)(({ theme }) => ({
  "&:nth-of-type(odd)": {
    backgroundColor: theme.palette.action.hover,
  },
  // hide last border
  "&:last-child td, &:last-child th": {
    border: 0,
  },
}));

export default function PrepareMeal() {
  const [date, setDate] = useState<Dayjs | null>(null);
  const [refresh, setRefresh] = useState<number>(0);
  const [mealStatistic, setMealStatistic] = useState<number>(0);
  const getSelectedDate = (day: Dayjs | null) => {
    let stringifiedDate = JSON.stringify(day);
    window.sessionStorage.setItem("date", stringifiedDate);
    setDate(day);
  };

  useEffect(() => {
    let sessionDate = window.sessionStorage.getItem("date");
    let sessionChoise = window.sessionStorage.getItem("choise");
    console.log("sessionDate : " + sessionDate);

    if (sessionDate != null && sessionDate != "") {
      let parsedDate = JSON.parse(sessionDate);
      setDate(dayjs(parsedDate));
    } else {
      setDate(null);
    }
    if (sessionChoise != null && sessionChoise != "") {
      setChoise(sessionChoise);
    }
  }, [refresh]);
  const { outLetProps } = useOutletContext<OutletContextType>();
  const [meals, setMeals] = useState<MealEntry[]>();
  const [acceptedTypes, setAcceptedTypes] = useState<MealType[]>([]);
  const context = new ClientContext(outLetProps.error401Handler);
  const [cookies] = useCookies(["jwt"]);
  const [choise, setChoise] = useState<string | null>(null);
  const JustRefresh = () => {
    setRefresh((pre) => pre + 1);
    setChoise(null);
  };
  const handleChoise = (id: string) => {
    window.sessionStorage.setItem("choise", id.toString());
    setChoise(id);
  };

  const handleDelete = (id: number) => {
    Swal.fire({
      title: "هل متأكد من حذف الوجبة من النظام؟",
      showCancelButton: true,
      cancelButtonText: "إلغاء الطلب",
      confirmButtonText: "المتابعة",
    }).then((result) => {
      if (result.isConfirmed) {
        context
          .delete<IResult>("api/Meals/DeleteMealEntry/" + id, token)
          .then((response) => {
            console.log(response.data);
          });
      }
    });
  };
  const handleConsume = (id: number, name: string) => {
    Swal.fire({
      title: "هل متأكد من استهلاك جميع حجوزات الوجبة " + name + "؟",
      showCancelButton: true,
      cancelButtonText: "إلغاء الطلب",
      confirmButtonText: "المتابعة",
    }).then((result) => {
      if (result.isConfirmed) {
        // context
        //   .put<IResult>("api//", token,)
        //   .then((response) => {
        //     console.log(response.data);
        //   });
      }
    });
  };

  useEffect(() => {
    if (date != null) {
      let month: number = 0;
      if (date != null) {
        month = date?.month() + 1;
      }
      let dateToSend =
        date?.date() + "/" + month.toString() + "/" + date?.year();
      let token = cookies.jwt;
      console.log(dateToSend);

      context
        .get<Result<MealEntry[]>>(
          "api/Meals/GetMealEntriesByDate?date=" + dateToSend,
          token
        )
        .then((response) => {
          console.log("response");
          setMeals(response.data.value);
          let denidedTypes: string[] = [];
          let acceptedTypesVar: MealType[] = [];
          response.data.value.map((value) => {
            denidedTypes.push(value.meal.type);
          });

          mealTypes?.types.map((type) => {
            if (!denidedTypes.includes(type.id)) {
              acceptedTypesVar.push(type);
            }
          });
          setAcceptedTypes(acceptedTypesVar);
          console.log(response.data);
        });
    }
  }, [date]);
  console.log("data", meals);

  if (mealStatistic != 0) {
    return <MealChartCustomersReservations mealId={mealStatistic} />;
  }
  if (choise) {
    return (
      <>
        {choise && date?.month() && (
          <PrepareMealForm
            atDay={
              date?.date() + "/" + (date?.month() + 1) + "/" + date?.year()
            }
            choise={choise}
            refresh={JustRefresh}
          />
        )}
      </>
    );
  }
  let token = cookies.jwt;

  return (
    <>
      {!date && <PrepareMealDatePicker passSelectedDate={getSelectedDate} />}
      <div className="container mt-5">
        {date && (
          <>
            <div className="d-flex justify-content-end">
              <div
                className="cancel"
                onClick={() => {
                  window.sessionStorage.setItem("date", "");
                  JustRefresh();
                }}
              >
                رجوع
              </div>
            </div>
            {meals?.length == 0 && (
              <>
                <div className="d-flex justify-content-center message-for-meal-entries">
                  لا يوجد وجبات محضرة حتى الآن في اليوم
                  {" ( " +
                    date?.date() +
                    "/" +
                    (date?.month() + 1) +
                    "/" +
                    date?.year() +
                    "  ) "}
                </div>
                <div className="mt-5 row d-flex justify-content-evenly">
                  {mealTypes?.types.map((mealType: MealType) => (
                    <div
                      className="add-meal"
                      onClick={() => {
                        handleChoise(mealType.id);
                      }}
                    >
                      إضافة {mealType.value}
                      <img className="add-stuff" src={add} alt="" />
                    </div>
                  ))}
                </div>
              </>
            )}
            {meals && meals?.length != 0 && (
              <>
                <div className="d-flex justify-content-center mb-5 message-for-meal-entries">
                  هذه هي الوجبات التي تم تحضيرها في اليوم
                  {" ( " +
                    date?.date() +
                    "/" +
                    (date?.month() + 1) +
                    "/" +
                    date?.year() +
                    "  ) "}
                </div>
                <div className="mt-5 row d-flex justify-content-evenly">
                  {acceptedTypes.map((mealType: MealType) => (
                    <div
                      className="add-meal"
                      onClick={() => {
                        handleChoise(mealType.id);
                      }}
                    >
                      إضافة {mealType.value}
                      <img className="add-stuff" src={add} alt="" />
                    </div>
                  ))}
                </div>
                <motion.div
                  initial={{ height: 0 }}
                  animate={{ height: "expanded" ? "auto" : 0 }}
                  transition={{ duration: meals.length / 4, ease: "easeInOut" }}
                  style={{
                    overflow: "hidden",
                    border: "1px solid #ccc",
                  }}
                >
                  <TableContainer component={Paper}>
                    <Table sx={{ minWidth: 700 }} aria-label="customized table">
                      <TableHead>
                        <TableRow>
                          <StyledTableCell align="center">
                            <div className="header-table-title">عمليات</div>
                          </StyledTableCell>
                          <StyledTableCell align="center">
                            <div className="header-table-title">
                              عدد الحجوزات حتى الآن
                            </div>
                          </StyledTableCell>
                          <StyledTableCell align="center">
                            <div className="header-table-title">
                              عدد الوجبات المحضرة
                            </div>
                          </StyledTableCell>
                          <StyledTableCell align="center">
                            <div className="header-table-title">نوع الوجبة</div>
                          </StyledTableCell>
                          <StyledTableCell align="center">
                            <div className="header-table-title">اسم الوجبة</div>
                          </StyledTableCell>
                          <StyledTableCell align="center">
                            <div className="header-table-title">الصورة</div>
                          </StyledTableCell>
                        </TableRow>
                      </TableHead>
                      <TableBody>
                        {meals?.map((row) => (
                          <StyledTableRow key={row.id}>
                            <StyledTableCell className="d-flex flex-column">
                              {/* <div
                                className="cancel btn me-2 scale-smaller"
                                onClick={() => {
                                  handleConsume(row.id, row.meal.name);
                                }}
                              >
                                استهلاك كل الوجبات
                              </div> */}
                              <div
                                className="cancel btn me-2 update-color scale-smaller"
                                onClick={() => {
                                  setMealStatistic(row.id);
                                }}
                              >
                                إحصائيات
                              </div>
                              <div
                                className="cancel btn me-2 scale-smaller delete-color"
                                onClick={() => {
                                  handleDelete(row.id);
                                }}
                              >
                                حذف
                              </div>
                            </StyledTableCell>
                            <StyledTableCell align="center">
                              {row.reservationsCount}
                            </StyledTableCell>
                            <StyledTableCell align="center">
                              {row.preparedCount}
                            </StyledTableCell>
                            <StyledTableCell align="center">
                              {
                                mealTypes.types.find(
                                  (type) => type.id == row.meal.type
                                )?.value
                              }
                            </StyledTableCell>
                            <StyledTableCell align="center">
                              {row.meal.name}
                            </StyledTableCell>
                            <StyledTableCell
                              component="th"
                              scope="row"
                              align="center"
                            >
                              <img
                                className="img-in-table"
                                src={url.baseUrl + row.meal.imagePath}
                                alt=""
                              />
                            </StyledTableCell>
                          </StyledTableRow>
                        ))}
                      </TableBody>
                    </Table>
                  </TableContainer>
                </motion.div>
              </>
            )}
          </>
        )}
      </div>
    </>
  );
}
