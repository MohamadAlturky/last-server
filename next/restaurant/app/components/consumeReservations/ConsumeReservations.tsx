import Person from "../../assets/person.svg";
import Security from "../../assets/security.svg";
import "./consumeReservations.css";
import Result from "../../models/Result";
import React, { useEffect, useState } from "react";
import dayjs, { Dayjs } from "dayjs";
import ClientContext from "../../contexts/api/ClientContext";
import { OutletContextType } from "../../authentication/models/OutletContextType";
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
import url from "../../configurations/apiConfiguration.json";
import mealTypes from "../../configurations/mealTypes.json";
import { motion } from "framer-motion";
import Swal from "sweetalert2";

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

export default function ConsumeReservations() {
  const [date] = useState<Dayjs>(dayjs());
  const [mealId, setMealId] = useState<number>(0);
  const [serialNumber, setSerialNumber] = useState<number>();
  const [password, setPassword] = useState<string>("");
  const { outLetProps } = useOutletContext<OutletContextType>();
  const [meals, setMeals] = useState<MealEntry[]>();
  const [customerName, setCustomerName] = useState<string>("");

  const context = new ClientContext(outLetProps.error401Handler);
  const [cookies] = useCookies(["jwt"]);

  useEffect(() => {
    if (date != null) {
      let month: number = 0;
      if (date != null) {
        month = date?.month() + 1;
      }
      let dateToSend =  date?.date() + "/"+ month + "/" + date?.year();
      let token = cookies.jwt;

      context
        .get<Result<MealEntry[]>>(
          "api/Meals/GetMealEntriesByDateForConsume?date=" + dateToSend,
          token
        )
        .then((response) => {
          setMeals(response.data.value);
        });
    }
  }, [date]);
  console.log("data", meals);
  const clientContext = new ClientContext(outLetProps.error401Handler);

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (serialNumber && password) {
      let formData = new FormData();
      formData.append("serialNumber", serialNumber.toString());
      formData.append("password", password);
      formData.append("mealEntryId", mealId.toString());
      let token = cookies.jwt;
      clientContext
        .put<Result<Reservation>>(
          "api/Reservations/Consume",
          token,
          formData,
          "multipart/form-data"
        )
        .then((response) => {
          console.log(response.data.value.customerName);
          setCustomerName(response.data.value.customerName);
        });
    } else {
      Swal.fire({
        title: "خطأ",
        text: "الرجاء ملء جميع الحقول",
        icon: "error",
        confirmButtonText: "حسناً",
      });
    }
  };
  return (
    <>
      {mealId != 0 && (
        <>
          {customerName && (
            <div className="customer-name mt-5">
              تم استهلاك الحجز المسجل باسم
              {" " + customerName}
            </div>
          )}

          <div className="container d-flex justify-content-center login-content mt-5">
            <div className="d-flex justify-content-center content">
              <form className="log-in-form" onSubmit={handleSubmit}>
                <div className="log-in-label">استهلاك الوجبات</div>
                <div className="user-name-container margin-consume1">
                  <input
                    className="user-name"
                    type="number"
                    placeholder={"الرقم الذاتي"}
                    dir="rtl"
                    autoFocus
                    value={serialNumber}
                    onChange={(element) => {
                      setSerialNumber(parseInt(element.target.value));
                    }}
                  />
                  <div className="user-name-svg">
                    <img src={Person} alt="Logo" />
                  </div>
                </div>

                <div className="user-name-container margin-consume2">
                  <input
                    className="user-name"
                    type="password"
                    placeholder={"كلمة المرور"}
                    dir="rtl"
                    value={password}
                    onChange={(element) => {
                      setPassword(element.target.value);
                    }}
                  />
                  <div className="user-name-svg">
                    <img src={Security} alt="Logo" />
                  </div>
                </div>

                <button type="submit" className="submit-log-in">
                  <p>تم</p>
                </button>
              </form>
              <div className="sider">
                <div className="image-container image-container2">
                  <div className="welcome-section">
                    {/* {resources.logIn.welcome} */}
                  </div>
                  <div className="note-section flex justify-content-center">
                    <div className="svg-container">
                      {/* <img src={Arrow} alt="Logo" /> */}
                    </div>
                    {/* <p>{resources.logIn.logInFromHere}</p> */}
                  </div>
                </div>
              </div>
            </div>
          </div>
        </>
      )}
      {mealId == 0 && (
        <>
          <div className="container mt-5">
            {date && (
              <>
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
                    {/* <div className="mt-5 row d-flex justify-content-evenly">
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
                </div> */}
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
                    {/* <div className="mt-5 row d-flex justify-content-evenly">
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
                </div> */}
                    <motion.div
                      initial={{ height: 0 }}
                      animate={{ height: "expanded" ? "auto" : 0 }}
                      transition={{
                        duration: meals.length / 4,
                        ease: "easeInOut",
                      }}
                      style={{
                        overflow: "hidden",
                        border: "1px solid #ccc",
                      }}
                    >
                      <TableContainer component={Paper}>
                        <Table
                          sx={{ minWidth: 700 }}
                          aria-label="customized table"
                        >
                          <TableHead>
                            <TableRow>
                              <StyledTableCell align="center">
                                <div className="header-table-title">
                                  تقديم الوجبات
                                </div>
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
                                <div className="header-table-title">
                                  نوع الوجبة
                                </div>
                              </StyledTableCell>
                              <StyledTableCell align="center">
                                <div className="header-table-title">
                                  اسم الوجبة
                                </div>
                              </StyledTableCell>
                              <StyledTableCell align="center">
                                <div className="header-table-title">الصورة</div>
                              </StyledTableCell>
                            </TableRow>
                          </TableHead>
                          <TableBody>
                            {meals?.map((row) => (
                              <StyledTableRow key={row.id}>
                                <StyledTableCell align="center">
                                  <div
                                    className="cancel smaller"
                                    onClick={() => {
                                      setMealId(row.id);
                                    }}
                                  >
                                    بدء التقديم
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
      )}
    </>
  );
}
